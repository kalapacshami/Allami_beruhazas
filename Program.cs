using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allami_beruhazas
{
    class Program
    {
        static Tartalom_osszeallito tartalom;
        static void beolvas()
        {
            string[] adatok = System.IO.File.ReadAllLines("palyazatok.txt");
            List<Projekt> projektek = new List<Projekt>();
            foreach (var item in adatok)
            {
                projektek.Add(new Projekt(item.Split(';')[0],Convert.ToInt32(item.Split(';')[1]), item.Split(';')[2].Split().ToList()));
            }
            tartalom = new Tartalom_osszeallito(projektek);
            StreamReader f = new StreamReader("cegek.txt");
            while (!f.EndOfStream)
            {
                string[] sor=f.ReadLine().Split(';');
                IBeszállító ceg;
                if (sor.Last().Last()=='%')
                {
                    ceg= new Ceg_szazalekos(sor[0], sor[1].Split().ToList(),Convert.ToDouble(sor[2].TrimEnd('%'))/100);
                }
                else
                {
                    ceg = new Ceg_egesz(sor[0], sor[1].Split().ToList(), Convert.ToInt32(sor[2]));
                }
                try
                {
                    tartalom.Ceg_felvesz(ceg);
                }
                catch (KulcsUtkozes ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            f.Close();
        }
        static void Main(string[] args)
        {

            Console.WriteLine("Adatok beolvasása...");
            beolvas();

            Console.WriteLine("\nTörlés...");
            try
            {
                tartalom.Eltavolit("Mgyar Kozut");
            }
            catch (KeresHiba ex)
            {
                Console.WriteLine(ex.Message + $" A rendszerben lévő cégek: {tartalom.fa.ToString()}");
            }

            Console.WriteLine("\nA pályázatokra alkalmas pályázók... ");
            foreach (var item in tartalom.palyazok())
            {
                Console.WriteLine($"{item.Key}: {(item.Value == "" ? "Nincs pályázó a projektre" : item.Value)}");
            }


            Console.WriteLine("\nA pályázatokra optimális pályázók...");
            object[] elozo=tartalom.Optimalizalas(Console.WriteLine);

            Console.WriteLine("\nTörlés újra...");
            try
            {
                tartalom.Eltavolit("Magyar Kozut");
                Console.WriteLine("A Magyar Kozut Törölve!");
                tartalom.Eltavolit("Regi Utepito Kft");
                Console.WriteLine("A Regi Utepito Kft!");
            }
            catch (KeresHiba ex )
            {
                Console.WriteLine(ex.Message + $" A rendszerben lévő cégek: {tartalom.fa.ToString()}");
            }

            bool volt=false;
            foreach (var item in elozo)
            {
                if ((string)item=="Magyar Kozut" || (string)item == "Regi Utepito Kft")
                {
                    volt=true;
                }
            }
            if (volt)
            {
                Console.WriteLine("\nÚj keresés a cégek megszűnése miatt...");
                foreach (var item in tartalom.palyazok())
                {
                    Console.WriteLine($"{item.Key}: {(item.Value==""?"Nincs pályázó a projektre":item.Value)}");
                }
                Console.WriteLine("\nA pályázatokra optimális pályázók...");
                elozo=tartalom.Optimalizalas(Console.WriteLine);
            }
            
            Console.ReadKey();
        }
    }
}
