using System.Speech.Recognition;

namespace SpeechApp
{
    /// <summary>
    /// Mikrofondan gelen konuşmayı yazıya çevirir (System.Speech.Recognition).
    /// Form bu sınıfı kullanır; SpeechRecognitionEngine ile doğrudan uğraşmaz.
    /// </summary>
    public class SpeechToTextService : IDisposable
    {
        private SpeechRecognitionEngine? _engine;

        public bool IsListening { get; private set; }

        /// <summary>Bir cümle tanındığında tetiklenir: (metin, güven 0-1).</summary>
        public event Action<string, float>? TextRecognized;

        /// <summary>Dinleme bittiğinde tetiklenir. Hata olduysa mesajı, yoksa null gelir.</summary>
        public event Action<string?>? ListeningStopped;

        /// <summary>Bilgisayarda yüklü konuşma tanıma motorları (genelde dil başına bir tane).</summary>
        public static List<RecognizerInfo> GetRecognizers()
        {
            return SpeechRecognitionEngine.InstalledRecognizers().ToList();
        }

        /// <summary>
        /// Seçilen dilde dinlemeye başlar.
        /// Mikrofon yoksa InvalidOperationException fırlatır.
        /// </summary>
        public void Start(RecognizerInfo recognizer)
        {
            if (IsListening) return;
            DisposeEngine();

            var engine = new SpeechRecognitionEngine(recognizer);
            try
            {
                engine.LoadGrammar(new DictationGrammar());   // serbest konuşma (dikte)
                engine.SetInputToDefaultAudioDevice();         // varsayılan mikrofon
            }
            catch
            {
                engine.Dispose();
                throw;   // hatayı forma ilet
            }

            engine.SpeechRecognized += (s, e) =>
                TextRecognized?.Invoke(e.Result.Text, e.Result.Confidence);

            engine.RecognizeCompleted += (s, e) =>
            {
                IsListening = false;
                ListeningStopped?.Invoke(e.Error?.Message);
            };

            _engine = engine;
            engine.RecognizeAsync(RecognizeMode.Multiple);   // durdurulana kadar sürekli dinle
            IsListening = true;
        }

        /// <summary>Dinlemeyi durdurur (o an söylenen cümle bitince durur).</summary>
        public void Stop()
        {
            if (IsListening)
                _engine?.RecognizeAsyncStop();
        }

        private void DisposeEngine()
        {
            if (_engine == null) return;
            if (IsListening)
                _engine.RecognizeAsyncCancel();
            _engine.Dispose();
            _engine = null;
            IsListening = false;
        }

        public void Dispose()
        {
            DisposeEngine();
        }
    }
}