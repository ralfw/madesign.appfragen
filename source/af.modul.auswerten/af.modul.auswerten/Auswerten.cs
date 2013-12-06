using System;
using af.contracts;
using jsonserialization;

namespace af.modul.auswerten
{
    public class Auswerten : IComponent
    {
        private readonly Befragung _befragung;

        public Auswerten(Befragung befragung)
        {
            _befragung = befragung;
        }

        public void Process(string json)
        {
            dynamic jsonObject = json.FromJson();

            if (jsonObject.cmd == "Auswerten")
            {
                AuswertenMethode(jsonObject);
            }
            if (jsonObject.cmd == "Auswerten beenden")
            {
                AuswertenBeendenMethode(jsonObject);
            }
        }

        private void AuswertenMethode(object jsonObject)
        {
            // Auswertung erstellen (evtl. per foreach durch jede Frage in der Liste und inkrementieren; danach dann Prozente berechnen.)
                // AnzahlFragen zählen
                // AnzahlRichtig(Antworten) zählen
                // AnzahlFalsch zählen
                // AnzahlWeissNicht zählen

                // ProzentRichtig berechnen
                // ProzentFalsch berechnen
                // ProzentWeissNicht berechnen

            // Kommando "Auswertung anzeigen" zusammenbauen
            // Kommando "Auswertung anzeigen" senden
        }

        private void AuswertenBeendenMethode(object jsonObject)
        {
            // Auswertung speichern (*.csv)
            // Kommando "Auswertung schliessen" senden
        }

        public event Action<string> Json_output;
    }
}
