using System;
using System.Collections.Generic;
using System.Linq;

namespace Crypto
{
    public class ReversingEl
    {
        private static int Original { get; set; }
        private static int Mod { get; set; }
        private static int Reversed { get; set; }
        private static List<int> MassiveElements{ get; set; }

        public ReversingEl(int mod,int element)
        {
            Original = element;
            Mod = mod;
            MassiveElements = new List<int>();
            GetKoefs(Mod, Original);
            GetReversed();

        }

        private  void GetReversed()
        {
            var Q = this.GetQ();
            var P = this.GetP(Q.ToList());
            var need = P[P.Length - 2];
            var res = ((int)Math.Pow(-1, P.Length)) * 1 * need;
            Reversed = Equations.GetModded(res, Mod);
        }


        private int[] GetP(List<int> listQ)
        {
            var p = new int[listQ.Count + 1];
            p[0] = 1;
            p[1] = listQ[0];
            for (int i = 0; i <= listQ.Count; i++)
            {
                if (i > 1) p[i] = p[i - 1] * listQ[i - 1] + p[i - 2];   
            }
            return p;
        }

        private static void GetKoefs(int firstN, int secondN)
        {
            MassiveElements.Add(firstN);
            MassiveElements.Add(secondN);
            var listOst = new List<int>();
            var d = 1;
            while (d != 0)
            {
                
                d = firstN - (firstN / secondN) * secondN;
                if (d!=0) MassiveElements.Add(d);
                firstN = secondN;
                secondN = d;
            }
        }

        public  int[] GetQ()
        {
            var list = new List<int>();
            for (var i = 0; i < MassiveElements.Count-1; i++)
            {
                list.Add(MassiveElements[i] / MassiveElements[i + 1]);
                
            }
            return list.ToArray();
        }

        public int CheckRes()
        {
            return Reversed;
        }
    }
}
