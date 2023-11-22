using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allami_beruhazas
{
    class Tartalom_osszeallito:Backtrack
    {
        public BinaryExpressionTree<IBeszállító, string> fa { get; private set;}
        public List<Projekt> projektek { get; private set; }

        List<Projekt> van_palyazo;
        public void Ceg_felvesz(IBeszállító ceg)
        {
            
            fa.Build(ceg.Név, ceg);
          
            Megszunes+=fa.deleteKey;
        }
        public Tartalom_osszeallito(List<Projekt> projektek)
        {
            fa = new BinaryExpressionTree<IBeszállító, string>();
            this.projektek = projektek;

        }
        public delegate void AllapotFigyelo(string key);

        public event AllapotFigyelo Megszunes;
        protected override bool ft(int szint, object s)
        {
            if ((string)s=="")
            {
                return false;
            }
            IBeszállító ceg=fa.Keres(fa.Root,(string)s);
            
 
            int vissza_fizet=(ceg as Ceg_egesz!=null?(ceg as Ceg_egesz).MegvesztegetésiÁr(van_palyazo[szint].projekt_koltseg) :(ceg as Ceg_szazalekos).MegvesztegetésiÁr(van_palyazo[szint].projekt_koltseg));
            if (vissza_fizet>2000000)
            {
                return false;
            }

          
            return true;
        }
        int eddig_fizetett(int szint1, object s1, object[] E)
        {

            
            int k = 0;
            IBeszállító ceg = fa.Keres(fa.Root, (string)s1);
            int vissza_fizet = (ceg as Ceg_egesz != null ? (ceg as Ceg_egesz).MegvesztegetésiÁr(van_palyazo[szint1].projekt_koltseg) : (ceg as Ceg_szazalekos).MegvesztegetésiÁr(van_palyazo[szint1].projekt_koltseg));

            while (k < szint1)
            {
                if ((string)E[k]==(string)s1)
                {
                    vissza_fizet += (ceg as Ceg_egesz != null ? (ceg as Ceg_egesz).MegvesztegetésiÁr(van_palyazo[k].projekt_koltseg) : (ceg as Ceg_szazalekos).MegvesztegetésiÁr(van_palyazo[k].projekt_koltseg));
                }

                k++;
            }
            return vissza_fizet;
        }

        protected override bool fk(int szint1, object s1, object[] E)
        {
           
            if (eddig_fizetett(szint1, s1, E)>2000000)
            {
                return false;
            }
            //return true;

            Dictionary<string, string> Palyazok = palyazok();
           List<string> ures_kulcsok=Palyazok.Where(item=> item.Value=="").Select(item=>item.Key).ToList();
            
            foreach (var item in ures_kulcsok)
            {
               
                    Palyazok.Remove(item);
                
            }

            string[] Palyazok_szint1 = Palyazok[van_palyazo[szint1].project_nev].Split(';');
            
            IBeszállító ceg = fa.Keres(fa.Root, (string)s1);
            int vissza_fizet = (ceg as Ceg_egesz != null ? (ceg as Ceg_egesz).MegvesztegetésiÁr(van_palyazo[szint1].projekt_koltseg) : (ceg as Ceg_szazalekos).MegvesztegetésiÁr(van_palyazo[szint1].projekt_koltseg));

            //IBeszállító max_cg = fa.Keres(fa.Root, Palyazok_szint1[0]);
            int max_fizet = -1;
            for (int i = 0; i < Palyazok_szint1.Length; i++)
            {
                IBeszállító cg = fa.Keres(fa.Root, Palyazok_szint1[i]);

                int fizet = (cg as Ceg_egesz != null ? (cg as Ceg_egesz).MegvesztegetésiÁr(van_palyazo[szint1].projekt_koltseg) : (cg as Ceg_szazalekos).MegvesztegetésiÁr(van_palyazo[szint1].projekt_koltseg));

                if (eddig_fizetett(szint1, Palyazok_szint1[i], E) < 2000000 && max_fizet < fizet)
                {
                    max_fizet = fizet;
                }
            }


            return max_fizet <= vissza_fizet;
        }

        public delegate void feldolgoz(string szoveg);
        public object[] Optimalizalas(feldolgoz metodus)
        {
            Dictionary<string, string> Palyazok = palyazok();
            
            van_palyazo=projektek.FindAll(item=> Palyazok[item.project_nev]!="");

            //van_palyazo.Select(item => item.project_nev).ToList().ForEach(item => Console.WriteLine(item));
            N = van_palyazo.Count();
            //Console.WriteLine(N);
            M = new int[N];
            int i = 0;
            
            foreach (var item in Palyazok)
            {
                if (item.Value!="")
                {
                    M[i] = item.Value.Split(';').Length;
                    i++;
                }
               
            }
            int maxR = Palyazok.Max(item => item.Value.Split(';').Length);
            R = new object[N, maxR];

            i = 0;
            foreach (var item in Palyazok)
            {
                if (item.Value!="")
                {
                    for (int j = 0; j < M[i]; j++)
                    {
                        R[i, j] = item.Value.Split(';')[j];
                        //Console.WriteLine(R[i,j]);

                    }
                    i++;
                    //Console.WriteLine();
                }
                

            }
            object[] seged = Kereses();
            for (i = 0; i < N; i++)
            {
               metodus($"{projektek[i].project_nev}: {(seged[i]==null?"nincs jó pályázó": seged[i].ToString())}");
            }
            return seged;
        }
        string Ceg_keres(Projekt projekt)
        {
            string[] cegek=fa.ToString().TrimEnd(';').Split(';');
            
            string vissza="";
            foreach (var item in cegek)
            {
                IBeszállító ceg = fa.Keres(fa.Root, item);
                vissza +=(ceg.ÉrtHozzá(projekt.szakteruletek)?ceg.Név+';':"");
            }
            return vissza.TrimEnd(';');
        }
        public Dictionary<string, string> palyazok()
        {
            Dictionary<string, string> seged= new Dictionary<string, string>();

            foreach (var item in projektek)
            {
                seged.Add(item.project_nev, Ceg_keres(item));
            }
            return seged;
        }

        public void Eltavolit(string Key)
        {
            if (Megszunes!=null)
            {
                fa.deleteKey(Key);
            }
        }

    }
}
