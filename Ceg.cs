using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allami_beruhazas
{
    abstract class Ceg_Alap: IBeszállító
    {
        string nev;
        public List<string> szakteruletek { get; private set; }
        
        public Ceg_Alap(string nev, List<string> szakteruletek)
        {
            this.nev = nev;
            this.szakteruletek = szakteruletek;    
        }
        public string Név
        {
            get
            {
                return nev;
            }
            set
            {
                nev = value;
            }
        }

        
        public bool ÉrtHozzá(List<string> Szakterület)
        {
            foreach (var item in Szakterület)
            {
                if (!szakteruletek.Any(x => x == item))
                {
                    return false;
                }
            }
            return true;
        }

        abstract public int MegvesztegetésiÁr(int projectár);
        
    }
    class Ceg_szazalekos:Ceg_Alap
    {
        public double megvesztegetesi_szazalek { get; private set; }
        public Ceg_szazalekos(string nev, List<string> szakteruletek, double megvesztegetesi_szazalek):
            base(nev, szakteruletek)
        {
            this.megvesztegetesi_szazalek = megvesztegetesi_szazalek;
        }
        public override int MegvesztegetésiÁr(int projectár)
        {
            return (int)(projectár*megvesztegetesi_szazalek);
        }
    }
    class Ceg_egesz:Ceg_Alap
    {
        public int megvesztegetesi_ar { get; private set; }
        public Ceg_egesz(string nev, List<string> szakteruletek, int megvesztegetesi_ar):
            base(nev, szakteruletek)
        {
            this.megvesztegetesi_ar = megvesztegetesi_ar;
        }
        public override int MegvesztegetésiÁr(int projectár)
        {
            return megvesztegetesi_ar;
        }
    }
    
}
