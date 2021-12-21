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

            dict.Add("RSABank", AnswerForRSABank);

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

            dict.Add("НОД", AnswerForGCD);

            dict.Add("Размер хеша и вероятность повторения", AnswerForHash);
            
             dict.Add("Решение сравнения", AnswerForComp);

            dict.Add("Решение системы сравнений", AnswerSystemComp);

            dict.Add("Вероятность некорректной шифровки", AnswerForMistakeRSA);

            dict.Add("Функция Эйлера", AnswerForPhi);
        }

        private string AnswerForPhi(string arg)
        {
            var data = int.Parse(arg);
            var answer = Equations.Phi(data);
            return String.Format("phi({0}) = {1}", data, answer);
        }

        private string AnswerForMistakeRSA(string arg)
        {
            var data = int.Parse(arg);
            var possibleanswer = Equations.MistakeRSA(data);
            if (possibleanswer==-1)
            {
                return "Данное число раскладывается более чем на 2 множителя и не может быть использовано как модуль";
            }
            else
            {
                return String.Format("Для модуля {0} вероятность некорректной шифровки равна {1:0.0000}", arg, possibleanswer);
            }
        }
        private string AnswerForRSABank(string arg)
        {
            var newdata = arg
                                     .Split(',', ' ')
                                     .Select(x => int.Parse(x))
                                     .ToArray();
            if (newdata.Length==5)
                return new CipherRSABank(newdata[0], newdata[1], newdata[2], newdata[3], newdata[4]).GetRightStrings();
            else
                return new CipherRSABank(newdata[0], newdata[1], newdata[2], newdata[3], newdata[4],newdata[5]).GetRightStrings();
        }

        private string AnswerForComp(string arg)
        {
            var newdata = arg
                                  .Split(',', ' ')
                                  .Select(x => int.Parse(x))
                                  .ToArray();
            var answer = Equations.SolveComparison(newdata[0], newdata[1], newdata[2]);
            if (answer != new int[] { -1 })
            {
                var str = new StringBuilder();
                for (int i = 0; i < answer.Length; i++)
                    str.Append(String.Format("{0} ", answer[0]));
                return str.ToString();
            }
            else
                return "Сравнение не имеет решений";
        }

        private string AnswerSystemComp(string arg)
        {
            var newdata = arg
                                  .Split(',', ' ')
                                  .Select(x => int.Parse(x))
                                  .ToArray();
            var answer = Equations.SolveSystemOfComp(newdata);
            if (answer != -1)
                return String.Format("{0} - решение системы сравнений", answer);
            else
                return "Система не имеет решений";
        }
        
        private string AnswerForHash(string arg)
        {
            var newdata = arg
                                 .Split(',', ' ')
                                 .Select(x => double.Parse(x))
                                 .ToArray();
            var answer = Equations.Probability(newdata[0], newdata[1]);
            return String.Format("Для вероятности {0}^{1} размер хеша равен {2}, количество необходимых подборов {4}*10^{3}", newdata[0], newdata[1], answer[0], answer[2],answer[1]);
        }
        
        private string AnswerForGCD(string arg)
        {
            var newdata = arg
                                 .Split(',', ' ')
                                 .Select(x => int.Parse(x))
                                 .ToArray();
            var answer = Equations.Gcd(newdata[0], newdata[1]);
            return String.Format("({0},{1})={2}", newdata[0], newdata[1], answer);
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
            string str = String.Format("поле {0} не имеет циклических групп",newdata[0]);
            if (newdata.Length == 1)
            {
                answer = Equations.MinimalCycleGroup(newdata[0]);
                if (answer!=-1) str = String.Format("{0} - минимальная циклическая группа поля {1}", answer, newdata[0]);
            }
            else 
            {
                answer = Equations.MinimalCycleGroup(newdata[0], newdata[1]);
                if (answer != -1) str = String.Format("{0} - циклическая группа поля {1}", answer, newdata[0]);
            }
            return str;
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
