using System;

namespace Crypto
{
    public class CipherElGamal
    {

        private static int X { get; set; }

        private static int Alpha { get; set; }

        // Beta = Alpha^Q

        private static int R { get; set; }

        private static int Q { get; set; }

        private static int P { get; set; }

        private static int Y1 { get; set; }

        private static int Y2 { get; set; }


        public CipherElGamal(int x, int mod,  int q)
        {
            X = x;
            P = mod;
            Alpha = Equations.MinimalCycleGroup(P);
            Q = q;
            var rnd = new Random();
            R = rnd.Next(1, P - 1);
        }

        public CipherElGamal(int x, int mod, int q, int r) : this(x, mod, q)
        {
            R = r;
        }

        public int Decryption()
        {
            var reserved = new ReversingEl(P, Equations.Power(Y1, Q, P)).CheckRes();
            return Equations.Multiply(Equations.Power(Y2,1,P),reserved,P);
        }

        public int[] Encrytion()
        {
            Y1 = Equations.Power(Alpha, R, P);
            Y2 = Equations.Multiply(X,Equations.Power(Alpha,R*Q,P),P);
            return new int[] { Y1, Y2 };
        }
    }
}
