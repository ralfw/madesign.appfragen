using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using af.contracts;

namespace af.ui
{
    public class Class1 : IComponent
    {
        public void Process(string json)
        {
            throw new NotImplementedException();
        }

        public event Action<string> Json_output;
    }
}
