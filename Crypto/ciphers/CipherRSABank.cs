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
        private int Type { get; set; }

        private int Message { get; set; }
        
        private int EBank { get; set; } 
        private int DBank { get; set; }
        private int NBank { get; set; }

        private int EClient { get; set; }
        private int DClient { get; set; }
        private int NClient { get; set; }

        private  int U { get; set; }

        private  int S { get; set; }
        private int V { get; set; }
        private  int Z { get; set; }



        public CipherRSABank(int eBank,int nBank,int eClient, int nClient, int message, int type = 0) // type 0 - Клиент Банку, type 1 - Банк Клиенту
        {
            this.Message = message;
            this.EBank = eBank;
            this.DBank = new ReversingEl(Equations.Phi(nBank), eBank).CheckRes();
            this.NBank = nBank;
            this.EClient = eClient;
            this.DClient = new ReversingEl(Equations.Phi(nClient), eClient).CheckRes();
            this.NClient = nClient;
            this.Type = type;
            switch (type)
            {
                case 0:
                    this.U = Equations.Power(this.Message, this.DClient, nClient);
                    this.S = Equations.Power(this.U, eBank, nBank);
                    this.V = Equations.Power(this.S, this.DBank, nBank);
                    this.Z = Equations.Power(this.V, eClient, nClient);
                    break;
                case 1:
                    this.U = Equations.Power(this.Message, this.DBank, nBank);
                    this.S = Equations.Power(this.U, eClient, nClient);
                    this.V = Equations.Power(this.S, this.DClient, nClient);
                    this.Z = Equations.Power(this.V + nClient, eBank, nBank);
                    break;
            }
        }


        public int[] CreateMessage()
        {
            return new int[] { this.Message, this.S };
        }

        public string GetRightStrings()
        {
            var str = new StringBuilder();
            if (this.Type == 0)
            {
                str.Append(String.Format("{0}^{1} (mod {2}) = {3} \n", this.Message, this.DClient, this.NClient, this.U));
                str.Append(String.Format("{0}^{1} (mod {2}) = {3} \n", this.U, this.EBank, this.NBank, this.S));
                str.Append(String.Format(" Сообщение ({0},{1})\n",this.Message,this.S));
                str.Append(String.Format("{0}^{1} (mod {2}) = {3} \n", this.S, this.DBank, this.NBank, this.V));
                str.Append(String.Format("{0}^{1} (mod {2}) = {3} \n", this.V, this.EClient, this.NClient, this.Z));
            }
            else
            {
                str.Append(String.Format("{0}^{1} (mod {2}) = {3} \n", this.Message, this.DBank, this.NBank, this.U));
                str.Append(String.Format("{0}^{1} (mod {2}) = {3} \n", this.U, this.EClient, this.NClient, this.S));
                str.Append(String.Format(" Сообщение ({0},{1})\n", this.Message, this.S));
                str.Append(String.Format("{0}^{1} (mod {2}) = {3} \n", this.S, this.DClient, this.NClient, this.V));
                str.Append(String.Format("{0}^{1} (mod {2}) = {3} \n", this.V+this.NClient, this.EClient, this.NBank, this.Z));
            }
            return str.ToString();
        }

        public bool IsCorrect()
        {
            
            return this.Message == this.Z;
        }

    }
}
