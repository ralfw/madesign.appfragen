﻿using System;
using System.Collections.Generic;
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
        public Befragung.Frage AktuelleFrage;
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
                    const string prefix = @"..\..\..\..\..\bin\Properties\";
                    try
                    {
                        ParseQuestionaire(new StreamReader(prefix + dateiname));
                    }
                    catch (FileNotFoundException fileNotFoundException)
                    {
                        Console.WriteLine("{0}", fileNotFoundException);
                    }

                    break;
                case "Beantworten":
                    // toggle answered item (update)
                    AnswerQuestion(input.payload.AntwortmöglichkeitId);
                    break;

            }
            SendQuestionaire();
        }

        public event Action<string> Json_output;

        #region private methods
        private void ParseQuestionaire(TextReader textReader)
        {
            using (textReader)
            {
                var id = 0;
                while (textReader.Peek() >= 0)
                {
                    var line = textReader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    if (line.EndsWith("?"))
                    {
                        if (AktuelleFrage != null)
                        {
                            _befragung.Fragen.Add(AktuelleFrage);
                        }
                        AktuelleFrage = new Befragung.Frage
                                            {
                                                Text = line,
                                                Antwortmöglichkeiten = new List<Befragung.Antwortmöglichkeit>()
                                            };
                    }
                    else
                    {
                        AktuelleFrage.Antwortmöglichkeiten.Add(
                            new Befragung.Antwortmöglichkeit
                            {
                                Id = (++id).ToString(CultureInfo.InvariantCulture),
                                IstAlsAntwortSelektiert = false,
                                IstRichtigeAntwort = line.EndsWith("*"),
                                Text = line.Replace("*", string.Empty)
                            });
                    }

                }
                _befragung.Fragen.Add(AktuelleFrage);
            }
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
            // Schicke Fragebogen
            Json_output.Invoke(_befragung.ToJson());
        }

        #endregion

    }
}
