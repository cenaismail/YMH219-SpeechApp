using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace SpeechApp
{
    public partial class Form1 : Form
    {
        private TextToSpeechService? _tts;
        private List<VoiceInfo> _voices = new List<VoiceInfo>();

        // Hız çubuğu sürüklenirken her adımda değil, bırakıldıktan kısa süre sonra uygulamak için
        private readonly System.Windows.Forms.Timer _rateTimer = new System.Windows.Forms.Timer { Interval = 300 };

        private SpeechToTextService? _stt;
        private List<RecognizerInfo> _recognizers = new List<RecognizerInfo>();

        public Form1()
        {
            InitializeComponent();
            InitTextToSpeech();
            InitSpeechToText();

            FormClosing += (s, e) =>
            {
                _tts?.Dispose();
                _stt?.Dispose();
            };
        }

        // =====================================================================
        // TEXT-TO-SPEECH (Metni sesli okuma)
        // =====================================================================

        private void InitTextToSpeech()
        {
            // Buton ve kontrol olayları
            btnSpeak.Click += BtnSpeak_Click;
            btnPause.Click += (s, e) => _tts?.PauseOrResume();
            btnStopSpeak.Click += (s, e) => _tts?.Stop();
            btnClearTts.Click += BtnClearTts_Click;
            trkRate.ValueChanged += TrkRate_ValueChanged;
            _rateTimer.Tick += RateTimer_Tick;
            cmbVoices.SelectedIndexChanged += CmbVoices_SelectedIndexChanged;

            try
            {
                _tts = new TextToSpeechService();
                _tts.StateChanged += state => RunOnUi(() => UpdateTtsUi(state));
                _tts.SpeakError += message => RunOnUi(() =>
                    MessageBox.Show("Okuma sırasında hata oluştu:\n" + message, "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error));

                // Yüklü sesleri listeye doldur
                _voices = _tts.GetVoices();
                if (_voices.Count == 0)
                    throw new InvalidOperationException("Bilgisayarda yüklü bir ses bulunamadı.");

                foreach (VoiceInfo voice in _voices)
                    cmbVoices.Items.Add($"{voice.Name} ({voice.Culture.Name})");

                // Türkçe ses varsa onu seç, yoksa ilk sesi
                int turkish = _voices.FindIndex(v => v.Culture.Name.StartsWith("tr"));
                cmbVoices.SelectedIndex = turkish >= 0 ? turkish : 0;

                UpdateTtsUi(SynthesizerState.Ready);
            }
            catch (Exception ex)
            {
                _tts?.Dispose();
                _tts = null;
                grpTts.Enabled = false;
                lblTtsStatus.Text = "Kullanılamıyor";
                MessageBox.Show("Metin okuma özelliği başlatılamadı. Hoparlör veya ses sürücüsü bulunamamış olabilir.\n\n" + ex.Message,
                    "Text-to-Speech", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSpeak_Click(object? sender, EventArgs e)
        {
            string text = txtSpeak.Text.Trim();
            if (text.Length == 0)
            {
                MessageBox.Show("Lütfen okunacak bir metin yazın.", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSpeak.Focus();
                return;
            }

            _tts?.Speak(text);
        }

        private void BtnClearTts_Click(object? sender, EventArgs e)
        {
            _tts?.Stop();
            txtSpeak.Clear();
            txtSpeak.Focus();
        }

        private void TrkRate_ValueChanged(object? sender, EventArgs e)
        {
            lblRate.Text = trkRate.Value.ToString();

            // Zamanlayıcıyı yeniden başlat: çubuk 300 ms hareketsiz kalınca hız uygulanır
            _rateTimer.Stop();
            _rateTimer.Start();
        }

        private void RateTimer_Tick(object? sender, EventArgs e)
        {
            _rateTimer.Stop();
            _tts?.ChangeRate(trkRate.Value);
        }

        private void CmbVoices_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_tts == null || cmbVoices.SelectedIndex < 0) return;
            _tts.SelectVoice(_voices[cmbVoices.SelectedIndex].Name);
        }

        /// <summary>Durum yazısını ve butonları okuma durumuna göre ayarlar.</summary>
        private void UpdateTtsUi(SynthesizerState state)
        {
            switch (state)
            {
                case SynthesizerState.Speaking:
                    lblTtsStatus.Text = "Okunuyor...";
                    lblTtsStatus.ForeColor = Color.Green;
                    btnPause.Text = "Duraklat";
                    break;
                case SynthesizerState.Paused:
                    lblTtsStatus.Text = "Duraklatıldı";
                    lblTtsStatus.ForeColor = Color.DarkOrange;
                    btnPause.Text = "Devam Et";
                    break;
                default:
                    lblTtsStatus.Text = "Hazır";
                    lblTtsStatus.ForeColor = Color.Gray;
                    btnPause.Text = "Duraklat";
                    break;
            }

            bool busy = state != SynthesizerState.Ready;
            btnPause.Enabled = busy;
            btnStopSpeak.Enabled = busy;
        }

        // =====================================================================
        // SPEECH-TO-TEXT (Konuşmayı yazıya çevirme)
        // =====================================================================

        private void InitSpeechToText()
        {
            btnListen.Click += BtnListen_Click;
            btnStopListen.Click += BtnStopListen_Click;
            btnClearStt.Click += (s, e) => lstRecognized.Items.Clear();

            // Yüklü konuşma tanıma motorlarını (dilleri) listele
            _recognizers = SpeechToTextService.GetRecognizers();
            foreach (RecognizerInfo r in _recognizers)
                cmbRecognizers.Items.Add($"{r.Culture.DisplayName} ({r.Culture.Name})");

            if (_recognizers.Count > 0)
            {
                // Türkçe motor varsa onu seç, yoksa ilkini
                int turkish = _recognizers.FindIndex(r => r.Culture.Name.StartsWith("tr"));
                cmbRecognizers.SelectedIndex = turkish >= 0 ? turkish : 0;
            }

            _stt = new SpeechToTextService();
            _stt.TextRecognized += (text, confidence) => RunOnUi(() => AddRecognizedText(text, confidence));
            _stt.ListeningStopped += error => RunOnUi(() => OnListeningStopped(error));

            UpdateSttUi();
        }

        private void BtnListen_Click(object? sender, EventArgs e)
        {
            if (_stt == null) return;

            if (_recognizers.Count == 0)
            {
                MessageBox.Show("Bu bilgisayarda konuşma tanıma motoru bulunamadı.\n\n" +
                                "Windows'a İngilizce (ABD) dil ve konuşma paketi eklemek gerekebilir.",
                    "Speech-to-Text", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _stt.Start(_recognizers[cmbRecognizers.SelectedIndex]);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Mikrofona erişilemedi. Mikrofonun bağlı olduğundan ve Windows gizlilik " +
                                "ayarlarında uygulamaların mikrofonu kullanmasına izin verildiğinden emin olun.\n\n" + ex.Message,
                    "Mikrofon hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dinleme başlatılamadı:\n" + ex.Message, "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateSttUi();
        }

        private void BtnStopListen_Click(object? sender, EventArgs e)
        {
            _stt?.Stop();
            lblSttStatus.Text = "Durduruluyor...";
            lblSttStatus.ForeColor = Color.DarkOrange;
            btnStopListen.Enabled = false;
        }

        /// <summary>Tanınan cümleyi saat ve güven oranıyla listeye ekler.</summary>
        private void AddRecognizedText(string text, float confidence)
        {
            lstRecognized.Items.Add($"[{DateTime.Now:HH:mm:ss}] {text}   (güven: %{confidence * 100:0})");
            lstRecognized.TopIndex = lstRecognized.Items.Count - 1;   // en yeni satıra kaydır
        }

        private void OnListeningStopped(string? error)
        {
            if (error != null)
            {
                MessageBox.Show("Dinleme bir hata nedeniyle durdu:\n" + error, "Speech-to-Text",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            UpdateSttUi();
        }

        /// <summary>Durum yazısını ve butonları dinleme durumuna göre ayarlar.</summary>
        private void UpdateSttUi()
        {
            bool listening = _stt?.IsListening == true;

            btnListen.Enabled = !listening;
            btnStopListen.Enabled = listening;
            cmbRecognizers.Enabled = !listening && _recognizers.Count > 0;

            if (listening)
            {
                lblSttStatus.Text = "● Dinleniyor... konuşabilirsiniz";
                lblSttStatus.ForeColor = Color.Red;
            }
            else if (_recognizers.Count == 0)
            {
                lblSttStatus.Text = "Konuşma tanıma motoru bulunamadı";
                lblSttStatus.ForeColor = Color.Gray;
            }
            else
            {
                lblSttStatus.Text = "Dinleme kapalı";
                lblSttStatus.ForeColor = Color.Gray;
            }
        }

        // =====================================================================
        // Yardımcı
        // =====================================================================

        /// <summary>Kodu arayüz iş parçacığında çalıştırır (olaylar başka iş parçacığından gelebilir).</summary>
        private void RunOnUi(Action action)
        {
            if (IsDisposed) return;
            if (InvokeRequired)
                BeginInvoke(action);
            else
                action();
        }
    }
}