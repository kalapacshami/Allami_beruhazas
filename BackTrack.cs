using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allami_beruhazas
{
    abstract class Backtrack
    {
        protected int N;
        protected int[] M;
        protected object[,] R;

        protected abstract bool ft(int szint, object s);

        protected abstract bool fk(int szint1, object s1, object[] E);

        

       

        protected object[] Kereses()
        {
            bool van = false;
            object[] E = new object[N];
            Probal(0, ref van, E);

            //if (!van)
            //{
            //    //throw new NincsMegoldasKivetel("Nincs megoldása a feladatnak!");
            //}
            return E;

        }

        void Probal(int szint, ref bool van, object[] E)
        {
            int i = 0;
            //if (N == 0) van = true; //ha egy teljesen kitöltött táblát kapunk
            while (!van && i < M[szint])
            {
                
                if (ft(szint, R[szint, i]))
                {
                    
                    if (fk(szint, R[szint, i], E))
                    {
                        E[szint] = R[szint, i];
                        

                        if (szint == N - 1)
                        {
                            van = true;
                        }
                        else
                        {
                            Probal(szint + 1, ref van, E);
                        }
                    }
                }
                i++;
            }
        }


    }
}
