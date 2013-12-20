using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using af.contracts;

using jsonserialization;

namespace af.modul.befragen
{
    public class Befragen : IComponent
    {
        private readonly Befragung _befragung;
        private int _id;

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
                    // Reset questionaire
                    _befragung.Reset(); // Creates list empty list of answers!
                    var dateiname = (string)input.payload.Dateiname;

                    // Open questionaire catalog file
                    try
                    {
                        ParseQuestionaire(dateiname);
                    }
                    catch (FileNotFoundException fileNotFoundException)
                    {
                        Console.WriteLine("{0}", fileNotFoundException);
                    }
                    SendQuestionaire();
                    break;
                case "Beantworten":
                    // toggle answered item (update)
                    AnswerQuestion(input.payload.AntwortmoeglichkeitId);
                    SendQuestionaire();
                    break;
            }
        }

        public event Action<string> Json_output;

        #region private methods
        private void ParseQuestionaire(string fileName)
        {
            TextReader textReader;
            using (textReader = new StreamReader(fileName))
            {
                _id = 0;
                Befragung.Frage aktuelleFrage = null;
                while (textReader.Peek() >= 0)
                {
                    var line = textReader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    if (line.EndsWith("?"))
                    {
                        // Add undecided to former question
                        if (aktuelleFrage != null)
                        {
                            _id = AddUndecided(aktuelleFrage);
                        }
                        // create next question
                        aktuelleFrage = new Befragung.Frage
                                            {
                                                Text = line,
                                                Antwortmöglichkeiten = new List<Befragung.Antwortmöglichkeit>()
                                            };
                    }
                    else
                    {
                        if (aktuelleFrage == null)
                        {
                            continue;
                        }
                        aktuelleFrage.Antwortmöglichkeiten.Add(AddPossibleAnsweringOption(line)
                           );
                    }
                }
                _id = AddUndecided(aktuelleFrage);
            }
        }

        private int AddUndecided(Befragung.Frage aktuelleFrage)
        {
            aktuelleFrage.Antwortmöglichkeiten.Add(AddPossibleAnsweringOption("Weiß nicht"));
            _befragung.Fragen.Add(aktuelleFrage);
            return _id;
        }

        private Befragung.Antwortmöglichkeit AddPossibleAnsweringOption(string line)
        {
            _id = _id + 1;
            return new Befragung.Antwortmöglichkeit
                       {
                           Id = _id.ToString(CultureInfo.InvariantCulture),
                           IstAlsAntwortSelektiert = false,
                           IstRichtigeAntwort = line.EndsWith("*"),
                           Text = line.Replace("*", string.Empty)
                       };
        }

        private void AnswerQuestion(string id)
        {
            if (_befragung == null)
            {
                return;
            }

            foreach (var frage in _befragung.Fragen)
            {
                if (frage.Antwortmöglichkeiten.Any(item => item.Id == id))
                {
                    foreach (var antwortmöglichkeit in frage.Antwortmöglichkeiten)
                    {
                        antwortmöglichkeit.IstAlsAntwortSelektiert = antwortmöglichkeit.Id == id;
                    }
                }
            }
            CheckQuestionaireCompleted();
        }

        private void CheckQuestionaireCompleted()
        {
            _befragung.IstVollständigAusgefuellt = true;
            foreach (Befragung.Frage frage in _befragung.Fragen)
            {
                var aktuelleFrageBeantwortet = false;
                foreach (Befragung.Antwortmöglichkeit antwortmöglichkeit in frage.Antwortmöglichkeiten)
                {
                    aktuelleFrageBeantwortet |= antwortmöglichkeit.IstAlsAntwortSelektiert;
                }
                _befragung.IstVollständigAusgefuellt &= aktuelleFrageBeantwortet;
            }
        }

        private void SendQuestionaire()
        {
            dynamic obj = new ExpandoObject();
            obj.cmd = "Fragebogen anzeigen";
            obj.payload = new ExpandoObject();
            obj.payload.Fragen = _befragung.Fragen;
            obj.payload.IstVollständigAusgefuellt = _befragung.IstVollständigAusgefuellt;
            var json = JsonExtensions.ToJson(obj);
            Json_output(json);
        }

        #endregion

    }
}
