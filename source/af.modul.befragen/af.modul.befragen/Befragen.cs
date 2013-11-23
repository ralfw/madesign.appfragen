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
                    // Reset questionaire
                    _befragung.Reset(); // Creates list empty list of answers!
                    var dateiname = (string)input.payload.Dateiname;
                    // Open questionaire catalog file
                    var prefix = @"..\..\..\..\..\bin\Properties\";
                    try
                    {
                        using (StreamReader fileStreamReader = new StreamReader(prefix + dateiname))
                        {
                            while (fileStreamReader.Peek() >= 0)
                            {
                                var line = fileStreamReader.ReadLine();
                                if (line.EndsWith("?"))
                                {
                                    var frage = new Befragung.Frage() { Text = line };
                                    _befragung.Fragen.Add(frage);
                                }
                            }
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
