using System.Speech.Synthesis;

namespace SpeechApp
{
    /// <summary>
    /// Metni sesli okuma işlemlerini yönetir (System.Speech.Synthesis).
    /// Form bu sınıfı kullanır; SpeechSynthesizer ile doğrudan uğraşmaz.
    /// </summary>
    public class TextToSpeechService : IDisposable
    {
        private readonly SpeechSynthesizer _synth = new SpeechSynthesizer();

        private string _currentText = "";   // şu an okunan metin
        private int _position;              // okunan kelimenin metindeki konumu

        /// <summary>Okuma durumu değiştiğinde tetiklenir (Hazır / Okunuyor / Duraklatıldı).</summary>
        public event Action<SynthesizerState>? StateChanged;

        /// <summary>Okuma sırasında hata oluşursa tetiklenir.</summary>
        public event Action<string>? SpeakError;

        public TextToSpeechService()
        {
            // Hoparlör yoksa burada hata fırlatılır; form bunu yakalar
            _synth.SetOutputToDefaultAudioDevice();

            _synth.StateChanged += (s, e) => StateChanged?.Invoke(e.State);

            // Her kelime okunmaya başlarken konumunu kaydet
            _synth.SpeakProgress += (s, e) => _position = e.CharacterPosition;

            _synth.SpeakCompleted += (s, e) =>
            {
                // Durdur'a basınca okuma "iptal edildi" olarak biter; bu bir hata değildir
                if (e.Cancelled || e.Error is OperationCanceledException)
                    return;

                if (e.Error != null)
                    SpeakError?.Invoke(e.Error.Message);
            };
        }

        public SynthesizerState State => _synth.State;

        /// <summary>Okuma hızı: -10 (en yavaş) ile 10 (en hızlı) arası.</summary>
        public int Rate => _synth.Rate;

        /// <summary>
        /// Okuma hızını değiştirir. SpeechSynthesizer hızı okuma sırasında değiştiremediği için,
        /// okuma sürüyorsa kalan kısım kaldığı kelimeden itibaren yeni hızla yeniden okunur.
        /// </summary>
        public void ChangeRate(int rate)
        {
            _synth.Rate = rate;

            if (_synth.State != SynthesizerState.Speaking)
                return;   // okuma yoksa yeni hız bir sonraki okumada geçerli olur

            int start = Math.Min(_position, _currentText.Length);
            Speak(_currentText.Substring(start));
        }

        /// <summary>Bilgisayarda yüklü ve kullanılabilir sesler.</summary>
        public List<VoiceInfo> GetVoices()
        {
            return _synth.GetInstalledVoices()
                .Where(v => v.Enabled)
                .Select(v => v.VoiceInfo)
                .ToList();
        }

        public void SelectVoice(string voiceName)
        {
            _synth.SelectVoice(voiceName);
        }

        /// <summary>Metni okumaya başlar; önceki okuma varsa durdurulur.</summary>
        public void Speak(string text)
        {
            Stop();
            _currentText = text;
            _position = 0;
            _synth.SpeakAsync(text);
        }

        /// <summary>Okuma sürüyorsa duraklatır, duraklatılmışsa devam ettirir.</summary>
        public void PauseOrResume()
        {
            if (_synth.State == SynthesizerState.Speaking)
                _synth.Pause();
            else if (_synth.State == SynthesizerState.Paused)
                _synth.Resume();
        }

        /// <summary>Okumayı tamamen durdurur.</summary>
        public void Stop()
        {
            // Duraklatılmış okuma iptal edilemez; önce devam ettirmek gerekir
            if (_synth.State == SynthesizerState.Paused)
                _synth.Resume();

            _synth.SpeakAsyncCancelAll();
        }

        public void Dispose()
        {
            Stop();
            _synth.Dispose();
        }
    }
}