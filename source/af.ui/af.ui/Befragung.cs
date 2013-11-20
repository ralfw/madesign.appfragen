using System.Collections.Generic;

namespace af.ui
{
    public class Befragung
    {
        public string Dateiname { get; set; }
        public List<Befragung.Frage> Fragen { get; set; }

        public void Reset()
        {
            Fragen = new List<Befragung.Frage>();
        }

        public class Frage
        {
            public string Text { get; set; }
            public List<Befragung.Antwortmöglichkeit> Antwortmöglichkeiten { get; set; }
        }

        public class Antwortmöglichkeit
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public bool IstRichtigeAntwort { get; set; }
            public bool IstAlsAntwortSelektiert { get; set; }
        }
    }
}
