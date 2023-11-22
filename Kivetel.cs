using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allami_beruhazas
{
    class KeresHiba:Exception
    {
        public KeresHiba(string uzenet):
            base(uzenet)
        {

        }
        
    }
    class KulcsUtkozes : Exception
    {
        public KulcsUtkozes(string key, string Message):
            base(Message)
        {

        }
    }
}
