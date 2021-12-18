using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto;

namespace Crypto
{
    public class CipherRSABank
    {

        private int Message { get; set; }

        private int DBank { get; set; }

        private int DCleint { get; set; }

        private  int U { get; set; }

        private  int S { get; set; }
        private int V { get; set; }
        private  int Z { get; set; }



        public CipherRSABank(int eBank,int nBank,int eClient, int nClient, int message, int type = 0) // type 0 - Клиент Банку, type 1 - Банк Клиенту
        {
            this.Message = message;
            this.DBank = new ReversingEl(Equations.Phi(nBank), eBank).CheckRes();
            this.DCleint = new ReversingEl(Equations.Phi(nClient), eClient).CheckRes();
            switch (type)
            {
                case 0:
                    this.U = Equations.Power(this.Message, this.DCleint, nClient);
                    this.S = Equations.Power(this.U, eBank, nBank);
                    this.V = Equations.Power(this.S, this.DBank, nBank);
                    this.Z = Equations.Power(this.V, eClient, nClient);
                    break;
            }
        }


        public int[] CreateMessage()
        {
            return new int[] { this.Message, this.S };
        }

        public bool IsCorrect()
        {
            if (this.Message == this.Z)
            {
                return true;
            }
            return false;
        }

    }
}
