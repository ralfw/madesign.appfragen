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
                AuswertenMethode();
            }
            if (jsonObject.cmd == "Auswertung beenden")
            {
                AuswertenBeendenMethode(jsonObject);
            }
        }

        // Auswertung erstellen
        private void AuswertenMethode()
        {
            // AnzahlFragen zählen
            var anzahlFragen = _befragung.Fragen.Count;
            // Variablen
            var richtigAnzahl = 0.0;
            var falschAnzahl = 0.0;
            var weissnichtAnzahl = 0.0;
            var richtigProzent = 0.0;
            var falschProzent = 0.0;
            var weissnichtProzent = 0.0;

            foreach (var frage in _befragung.Fragen)
            {
                var frageIstRichtigBeantwortet = true;

                // frageIstRichtigBeantwortet = false; sobald etwas falsch gemacht wird.
                foreach (var antwortmöglichkeit in frage.Antwortmöglichkeiten)
                {
                    // selektiert und NICHT richtig
                    if (antwortmöglichkeit.IstAlsAntwortSelektiert && !antwortmöglichkeit.IstRichtigeAntwort)
                    {
                        frageIstRichtigBeantwortet = false;
                        break;
                    }
                    // NICHT selektiert und richtig
                    if (!antwortmöglichkeit.IstAlsAntwortSelektiert && antwortmöglichkeit.IstRichtigeAntwort)
                    {
                        frageIstRichtigBeantwortet = false;
                        break;
                    }
                    // selektiert und richtig = true
                    // NICHT selektiert und NICHT richtig = true
                }

                if (frageIstRichtigBeantwortet)
                {
                    // AnzahlRichtig zählen
                    richtigAnzahl++;
                }
                else
                {
                    // AnzahlFalsch zählen
                    falschAnzahl++;
                }
                if (frage.Antwortmöglichkeiten[frage.Antwortmöglichkeiten.Count-1].IstAlsAntwortSelektiert)
                {
                    // AnzahlWeissnicht zählen
                    weissnichtAnzahl++;
                    falschAnzahl--; // Falls "weissnicht" nicht zu "falsch" gezählt werden soll.
                }

            }

            // ProzentRichtig berechnen
            richtigProzent = richtigAnzahl / anzahlFragen;
            // ProzentFalsch berechnen
            falschProzent = falschAnzahl / anzahlFragen;
            // ProzentWeissNicht berechnen
            weissnichtProzent = weissnichtAnzahl / anzahlFragen;

            // Kommando "Auswertung anzeigen" zusammenbauen
            dynamic jsonAuswertungObj = new ExpandoObject();
            jsonAuswertungObj.cmd = "Auswertung anzeigen";
            jsonAuswertungObj.payload = new ExpandoObject();

            jsonAuswertungObj.payload.AnzahlFragen          = anzahlFragen;
            jsonAuswertungObj.payload.AnzahlRichtig         = richtigAnzahl;
            jsonAuswertungObj.payload.ProzentRichtig        = richtigProzent;
            jsonAuswertungObj.payload.AnzahlFalsch          = falschAnzahl;
            jsonAuswertungObj.payload.ProzentFalsch         = falschProzent;
            jsonAuswertungObj.payload.AnzahlWeissNicht      = weissnichtAnzahl;
            jsonAuswertungObj.payload.ProzentWeissNicht     = weissnichtProzent;

            var jsonAuswertung = JsonExtensions.ToJson(jsonAuswertungObj);

            // Kommando "Auswertung anzeigen" senden
            Json_output(jsonAuswertung);
        }

        private void AuswertenBeendenMethode(object jsonObject)
        {
            // Auswertung speichern (*.csv)

            // Kommando "Auswertung schliessen" senden
            dynamic jsonAuswertungSchliessenObj = new ExpandoObject();
            jsonAuswertungSchliessenObj.cmd = "Auswertung schliessen";

            var jsonAuswertungSchliessen = JsonExtensions.ToJson(jsonAuswertungSchliessenObj);
            Json_output(jsonAuswertungSchliessen);
        }

        public event Action<string> Json_output;
    }
}
