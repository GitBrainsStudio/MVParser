using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVParser.BLL.Contracts
{
    public interface ILogger
    {
        string LogDirectory { get; }
        void Event(string _message);
        void Error(string _message);
    }
}
