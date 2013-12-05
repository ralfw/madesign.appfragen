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
                    var prefix = @"..\..\..\..\..\bin\Properties\";
                    try
                    {
                        using (StreamReader fileStreamReader = new StreamReader(prefix + dateiname))
                        {
                            var id = 0;
                            while (fileStreamReader.Peek() >= 0)
                            {
                                var line = fileStreamReader.ReadLine();
                                if (line.EndsWith("?"))
                                {
                                    if (AktuelleFrage != null)
                                    { 
                                        _befragung.Fragen.Add(AktuelleFrage); 
                                    }
                                    AktuelleFrage = new Befragung.Frage() { Text = line };
                                    AktuelleFrage.Antwortmöglichkeiten = new List<Befragung.Antwortmöglichkeit>();
                                }
                                else
                                {
                                    AktuelleFrage.Antwortmöglichkeiten.Add(
                                        new Befragung.Antwortmöglichkeit()
                                        {
                                            Id = (++id).ToString(),
                                            IstAlsAntwortSelektiert = false,
                                            IstRichtigeAntwort = line.EndsWith("*"),
                                            Text = line.Replace("*", string.Empty)
                                        }
                                        );
                                }

                            }
                            _befragung.Fragen.Add(AktuelleFrage);
                        }
                    }
                    catch (FileNotFoundException fileNotFoundException)
                    {
                        Console.WriteLine("{0}", fileNotFoundException);
                    }
                    // Schicke Fragebogen
                    Json_output.Invoke(_befragung.ToJson());
                    break;
            }
        }

        public event Action<string> Json_output;
    }
}
