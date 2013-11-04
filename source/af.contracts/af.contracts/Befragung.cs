using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace af.contracts
{
    public class Befragung
    {
        public string Dateiname;
        public List<Frage> Fragen;

        public class Frage
        {
            public string Text;
            public List<Antwortmöglichkeit> Antwortmöglichkeiten;
        }

        public class Antwortmöglichkeit
        {
            public string Id;
            public string Text;
            public bool IstRichtigeAntwort;
            public bool IstAlsAntwortSelektiert;
        }


        public void Reset()
        {
            Fragen = new List<Frage>();
        }
    }
}
