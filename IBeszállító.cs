using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allami_beruhazas
{
    public interface IBeszállító
    {
        string Név { get; set; }
        bool ÉrtHozzá(List<string> Szakterület);
        int MegvesztegetésiÁr(int projectár);
    }
}
