using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allami_beruhazas
{
    class Projekt
    {
        public string project_nev { get; private set; }
        public int projekt_koltseg { get; private set; }
        public List<string> szakteruletek { get; private set; }

        public Projekt(string project_nev, int projekt_koltseg, List<string> szakteruletek)
        {
            this.project_nev=project_nev;
            this.projekt_koltseg=projekt_koltseg;
            this.szakteruletek=szakteruletek;
        }
    }
}
