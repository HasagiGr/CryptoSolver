using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSolver
{
    class Second_Level
    {
        public Dictionary<string, string> dict = new Dictionary<string, string>();
        private string Value { get; set; }
        public string Key { get; }

        public Second_Level(string value)
        {
            Value = value;
            FillingDict();
            var key = Searching(value);
            Key = key;
        }




        private void FillingDict()
        {
            dict.Add("RSA", "Введите данные в фомате x,n,e");
            dict.Add("El Gamal", "Введите данные в фомате x,n,q,r");
            dict.Add("Разложение на множители", "Введите x");
            dict.Add("Быстрое экспоненцирование", "Введите x, mod, количество шагов");
            dict.Add("y = a^b mod p", "Введите a,b,p");
            dict.Add("y = a*b mod p", "Введите a,b,p");
            dict.Add("Минимальная циклическая группа", "Введите поле field, если необходима циклическая группа больше чем p, введите число p");
            dict.Add("Метод тривиальных(простых) делений", "Введите число для проверки");
            dict.Add("Метод Ферма", "Введите число");
            dict.Add("Тест Ферма", "Введите число x,свидетеля( по умолчанию стоит 2)");
            dict.Add("Обратный элемент", "Введите данные в формате x,mod");
            dict.Add("НОД", "Введите числа в формате a,b");
            dict.Add("Размер хеша и вероятность повторения", "Введите вероятность в формате x,y (х-основание, у - степень)");
            dict.Add("Свойства RSA", "https://sun9-52.userapi.com/impg/77trFRESV5SsqXglyWd19QbNtCU1og0x7-TD_A/wczzEj7N5mE.jpg?size=755x1075&quality=96&sign=3cfb442d84f79146d3f29104cb77a6cc&type=album");
            dict.Add("Свойства El Gamal", "https://sun9-8.userapi.com/impg/ie5AxO8oi-ZygXgj2mZw-qQPjj6bJCV7kyhrYw/H5RgztICWNY.jpg?size=743x626&quality=96&sign=1204a3f13c8bb2485f99143c3cd8c411&type=album");
            dict.Add("Свойства сравнений", "https://sun9-67.userapi.com/impg/0090ne0xZ709xwNjtCaGnAg4hu3PTXhXe9hQig/x69TBhFDzB0.jpg?size=738x517&quality=96&sign=9c7acc8e9b6316c6fac0f7177e67f023&type=album");
            dict.Add("Малая теорема Ферма", "https://sun9-62.userapi.com/impg/qfc3-r8Xea1cZfK06gmTEAVnMG6LxsWO1c3x8w/v5LAUDHfwE8.jpg?size=715x703&quality=96&sign=53a02996b0f3bd32ada18872d7e5b5d5&type=album");
            dict.Add("Теорема Эйлера", "https://sun9-1.userapi.com/impg/dY7ojGRmYDWh3eCccY8Zg95jyM0hW3s6F-_kdA/g809y17OXhU.jpg?size=744x488&quality=96&sign=e35e8f1d806b24c619c992f80b8ea7b7&type=album");
        }

        private string Searching(string value)
        {
            return dict[value];

        }
    }
}