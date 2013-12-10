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
                var id = 0;
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
                        id = AddUndesided(id, aktuelleFrage);
                        aktuelleFrage = new Befragung.Frage
                                            {
                                                Text = line,
                                                Antwortmöglichkeiten = new List<Befragung.Antwortmöglichkeit>()
                                            };
                    }
                    else
                    {
                        if (aktuelleFrage != null)
                            aktuelleFrage.Antwortmöglichkeiten.Add(
                                new Befragung.Antwortmöglichkeit
                                    {
                                        Id = (++id).ToString(CultureInfo.InvariantCulture),
                                        IstAlsAntwortSelektiert = false,
                                        IstRichtigeAntwort = line.EndsWith("*"),
                                        Text = line.Replace("*", string.Empty)
                                    });
                    }

                }
                AddUndesided(id, aktuelleFrage);
            }
        }

        private int AddUndesided(int id, Befragung.Frage aktuelleFrage)
        {
            if (aktuelleFrage != null)
            {
                aktuelleFrage.Antwortmöglichkeiten.Add(new Befragung.Antwortmöglichkeit
                                                           {
                                                               Id = (++id).ToString(CultureInfo.InvariantCulture),
                                                               IstAlsAntwortSelektiert = false,
                                                               IstRichtigeAntwort = false,
                                                               Text = "Weiß nicht"
                                                           });
                _befragung.Fragen.Add(aktuelleFrage);
            }
            return id;
        }

        private void AnswerQuestion(string id)
        {
            if (_befragung == null)
            {
                return;
            }
            foreach (var antwortmöglichkeit in _befragung.Fragen.SelectMany(
                frage => frage.Antwortmöglichkeiten
                    .Where(antwortmöglichkeit => antwortmöglichkeit.Id == id)))
            {
                antwortmöglichkeit.IstAlsAntwortSelektiert = !antwortmöglichkeit.IstAlsAntwortSelektiert;
            }
        }

        private void SendQuestionaire()
        {
            dynamic obj = new ExpandoObject();
            obj.cmd = "Fragebogen anzeigen";
            obj.payload = new ExpandoObject();
            obj.payload.Fragen = _befragung.Fragen;
            var json = JsonExtensions.ToJson(obj);
            Json_output(json);
        }

        #endregion

    }
}
