using System.Collections.Generic;

namespace af.ui
{
    public class UiBefragung
    {
        public string Dateiname { get; set; }
        public List<Frage> Fragen { get; set; }

        public void Reset()
        {
            Fragen = new List<Frage>();
        }

        public class Frage
        {
            public string Text { get; set; }
            public List<Antwortmöglichkeit> Antwortmöglichkeiten { get; set; }
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
