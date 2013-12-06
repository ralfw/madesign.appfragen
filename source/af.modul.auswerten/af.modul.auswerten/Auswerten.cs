using System;
using System.Dynamic;
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
            dynamic jsonAuswertung = new ExpandoObject();
            jsonAuswertung.cmd = "Auswertung anzeigen";
            jsonAuswertung.payload = new ExpandoObject();

            jsonAuswertung.payload.AnzahlFragen = 10;
            jsonAuswertung.payload.AnzahlRichtig = 3;
            jsonAuswertung.payload.ProzentRichtig = 0.3;
            jsonAuswertung.payload.AnzahlFalsch = 1;
            jsonAuswertung.payload.ProzentFalsch = 0.1;
            jsonAuswertung.payload.AnzahlWeissNicht = 6;
            jsonAuswertung.payload.ProzentWeissNicht = 0.6;

            jsonAuswertung = JsonExtensions.ToJson(jsonAuswertung);

            // Kommando "Auswertung anzeigen" senden
            Json_output(jsonAuswertung);
        }

        private void AuswertenBeendenMethode(object jsonObject)
        {
            // Auswertung speichern (*.csv)
            // Kommando "Auswertung schliessen" senden
        }

        public event Action<string> Json_output;
    }
}
