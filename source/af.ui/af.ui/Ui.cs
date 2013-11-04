using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using af.contracts;

namespace af.ui
{
    public class Ui : IComponent
    {
        public void Process(string json)
        {
            
        }

        public event Action<string> Json_output;
    }
}
