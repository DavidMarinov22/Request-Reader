using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestReader
{
    public interface IRequest
    {
        public int[] IP { get; }
        public int Port { get; }
    }
}
