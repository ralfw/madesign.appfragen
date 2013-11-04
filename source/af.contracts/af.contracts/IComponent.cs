using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace af.contracts
{
    public interface IComponent
    {
        void Process(string json);
        event Action<string> Json_output;
    }
}
