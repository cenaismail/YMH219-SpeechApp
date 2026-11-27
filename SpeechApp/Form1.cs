using System.Speech.Synthesis;

namespace SpeechApp
{
    public partial class Form1 : Form
    {
        private TextToSpeechService? _tts;
        private List<VoiceInfo> _voices = new List<VoiceInfo>();
        // Hız çubuğu sürüklenirken her adımda değil, bırakıldıktan kısa süre sonra uygulamak için
        private readonly System.Windows.Forms.Timer _rateTimer = new System.Windows.Forms.Timer { Interval = 300 };
        public Form1()
        {
            InitializeComponent();
            InitTextToSpeech();

            FormClosing += (s, e) => _tts?.Dispose();
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