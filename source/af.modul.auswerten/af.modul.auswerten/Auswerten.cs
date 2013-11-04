using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using af.contracts;

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
            throw new NotImplementedException();
        }

        public event Action<string> Json_output;
    }
}
