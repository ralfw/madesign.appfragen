using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using af.contracts;

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
            throw new NotImplementedException();
        }

        public event Action<string> Json_output;
    }
}
