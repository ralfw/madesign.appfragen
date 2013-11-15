using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;

using af.contracts;

using jsonserialization;

namespace af.modul.befragen
{
    public class Befragen : IComponent
    {
        private readonly Befragung _befragung;

        public Befragen(Befragung befragung)
        {
            _befragung = befragung;
        }

        public void Process(string json)
        {
            dynamic input = json.FromJson();
            switch ((string)input.cmd)
            {
                case "Fragenkatalog laden":
                    var dateiname = (string)input.payload.Dateiname;
                    // Öffne Datei TODO: öffne Datei
                    // Lese zeilenweise
                    // Liste von Fragen und Antwortmöglichkeiten in einen Fragebogen wandeln.
                    // Resette Befragung
                    _befragung.Reset(); // Erzeugt neue Antwortliste!   
                    // Erzeuge Fragebogen aus Fragekatalog, als Text der ersten Frage für den Durchstich nur der Dateiname zurück
                    _befragung.Fragen.Add(new Befragung.Frage { Text = dateiname });
                    // Schicke Fragebogen
                    Json_output(_befragung.ToJson());
                    break;
            }
        }

        public event Action<string> Json_output;
    }
}
