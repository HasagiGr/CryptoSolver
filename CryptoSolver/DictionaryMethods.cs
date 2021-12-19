using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crypto;
using CryptoSolver;
namespace CryptoSolver
{
    public class DictionaryMethods
    {
        public Dictionary<string, Func<string,string>> dict = new Dictionary<string, Func<string, string>>();

        public DictionaryMethods()
        {
            FillDict();
        }

        private void FillDict()
        {
            // Шифры
            dict.Add("RSA", AnswerForRSA);

            dict.Add("El Gamal", AnswerForElGamal);

            //калькулятор
            dict.Add("Разложение на множители", AnswerForDiv);

            dict.Add("Быстрое экспоненцирование", AnswerForFExp);

            dict.Add("y = a^b mod p", AnswerForPower);

            dict.Add("y = a*b mod p", AnswerForMultiply);

            dict.Add("Минимальная циклическая группа", AnswerForGroup);

            dict.Add("Метод тривиальных(простых) делений", AnswerForTrialDiv);

            dict.Add("Метод Ферма", AnswerForMFermat);

            dict.Add("Тест Ферма", AnswerForTFermat);

            dict.Add("Обратный элемент", AnswerForRevEl);

        }

        private string AnswerForRevEl(string arg)
        {
            var newdata = arg
                                .Split(',', ' ')
                                .Select(x => int.Parse(x))
                                .ToArray();
            var answer = new ReversingEl(newdata[1], newdata[0]).CheckRes();
            return String.Format("{0}*x=1 (mod {1}), x = {2}", newdata[0], newdata[1], answer);
        }

        private string AnswerForTFermat(string arg)
        {
            var newdata = arg
                              .Split(',', ' ')
                              .Select(x => int.Parse(x))
                              .ToArray();
            int[] answer = new int[2];
            if (newdata.Length == 1)
                answer = Equations.TestDeFermat(newdata[0]);
            else
                answer = Equations.TestDeFermat(newdata[0], newdata[1]);
            string str = "";
            if (answer[0] == 0)
                str = String.Format("Не является свидетелем числа {1}, остаток = {0} ", answer[1], newdata[0]);
            else
                str = String.Format("Является свидетелем числа {0}", newdata[0]);
            return str;
        }

        private string AnswerForMFermat(string arg)
        {
            var newdata = int.Parse(arg);
            var answer = Equations.MethodDeFermat(newdata);
            string str;
            if (answer[0] != 0 && answer[1] != 0)
                str = String.Format("{0} = {1}*{2}", newdata, answer[0], answer[1]);
            else
                str = String.Format("Для числа {0} метод Ферма не работает");
            return str;
        }

        private string AnswerForTrialDiv(string arg)
        {
            var newdata = Equations.MethodTrialDiv(int.Parse(arg));
            string str;
            var cost = (int)Math.Ceiling(Math.Sqrt(int.Parse(arg)));
            if (newdata != -1)
                str = String.Format("Примерно количество делений - {0}, минимальный простой множитель равен {1}", cost, newdata);
            else
                str = String.Format("Примерно количество делений - {0}, не нашлось ни одного множителя", cost);
            return str;
        }

        private string AnswerForGroup(string arg)
        {
            var newdata = arg
                               .Split(',', ' ')
                               .Select(x => int.Parse(x))
                               .ToArray();
            int answer = 1;
            string str;
            if (newdata.Length == 1)
            {
                answer = Equations.MinimalCycleGroup(newdata[0]);
                str = String.Format("{0} = минимальная циклическая группа поля {1}", answer, newdata[0]);
            }
            else if (newdata[0] > newdata[1])
            {
                answer = Equations.MinimalCycleGroup(newdata[0], newdata[1]);
                str = String.Format("{0} = циклическая группа поля {1}", answer, newdata[0]);
            }
            return String.Format("{0} - циклическая группа поля {1}", answer, newdata[0]);
        }

        private string AnswerForMultiply(string arg)
        {

            var newdata = arg
               .Split(',', ' ')
               .Select(x => int.Parse(x))
               .ToArray();
            var answer = Equations.Multiply(newdata[0], newdata[1], newdata[2]);
            return String.Format("{0}*{1} (mod {2}) = {3}", newdata[0], newdata[1], newdata[2], answer);
        }

        private string AnswerForPower(string arg)
        {
            var newdata = arg
                              .Split(',', ' ')
                              .Select(x => int.Parse(x))
                              .ToArray();
            var answer = Equations.Power(newdata[0], newdata[1], newdata[2]);
            return String.Format("{0}^{1} (mod {2}) = {3}", newdata[0], newdata[1], newdata[2], answer);
        }

        private string AnswerForFExp(string arg)
        {
            var newdata = arg
                               .Split(',', ' ')
                               .Select(x => int.Parse(x))
                               .ToArray();
            var answer = Equations.FastExp(newdata[0], newdata[1], newdata[2]);
            var str = new StringBuilder();
            var i = 0;
            foreach (var prime in answer)
            {

                str.Append(String.Format(" {0}^{1} = {2} (mod {3})\n", newdata[0], Math.Pow(2, i), prime, newdata[1]));
                i++;
            }
            return str.ToString();
        }

        private string AnswerForDiv(string arg)
        {
            var newdata = int.Parse(arg);
            var answer = Equations.GetPrimeFactors(newdata);
            var str = new StringBuilder();
            foreach (var prime in answer)
            {
                str.Append(prime);
                str.Append(' ');
            }
            return str.ToString();
        }

        private string AnswerForElGamal(string arg)
        {
            var newdata = arg
                               .Split(',', ' ')
                               .Select(x => int.Parse(x))
                               .ToArray();
            var elGamal = new CipherElGamal(newdata[0], newdata[1], newdata[2], newdata[3]);
            var elGamalY = elGamal.Encrytion();
            return String.Format("y1 = {0}, y2 = {1}", elGamalY[0], elGamalY[1]);

        }

        private string  AnswerForRSA(string arg)
        {
            var newdata = arg
                                .Split(',', ' ')
                                .Select(x => int.Parse(x))
                                .ToArray();
            var rsa = new СipherRSA(newdata[0], newdata[1], newdata[2]);
            return String.Format("d = {1} ,y = {0}", rsa.Encryption(), rsa.GetD());


        }
    }
}
