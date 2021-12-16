using System.Collections.Generic;

namespace Crypto
{
    public class СipherRSA 
    {

        private static int N {get;set;}
        private static int X { get; set; }
        private static int Y { get; set; }
        private static int E { get; set; }
        private static int D { get; set; }

        public СipherRSA(int x, int n, int e)
        {
            N = n;
            X = x;
            E = e;
            D = new ReversingEl(Equations.Phi(N),E).CheckRes();
        }

        public  int Encryption()
        {

            Y = Equations.Power(X, E, N);
            return Y;
        }

        public int Decryption()
        {
            return Equations.Power(Y, D, N);
        }
        public int GetD()
        {
            return D;
        }

        
    }
}
