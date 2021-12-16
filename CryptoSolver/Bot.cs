using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using Crypto;
using System.Linq;

namespace CryptoSolver
{
    class Bot
    {
        private static string Token { get; set; } = "5085494689:AAFBmzjJiSRspQk_XM9XGUh3iYjrE7F9H_w";
        private static TelegramBotClient client;
        private static bool isAnyOtherMessage = true;
        private static List<string> Path { get; set; } = new List<string>();

        public Bot()
        {
            client = new TelegramBotClient(Token);
            client.StartReceiving();
            client.OnMessage += Client_OnMessage;
            Console.ReadLine();
            client.StopReceiving();
        }

        private static async void Client_OnMessage(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg.Text != null)
            {
                // Уровень решений
                if (Path.Count == 2)
                {
                    //Шифры
                    if (Path.Contains("RSA")) // 2 уровень расчёт для RSA
                    {
                        var data = msg.Text
                            .Split(',', ' ')
                            .Select(x => int.Parse(x))
                            .ToArray();
                        var rsa = new СipherRSA(data[0], data[1], data[2]);
                        await client.SendTextMessageAsync(
                             chatId: msg.Chat.Id,
                             String.Format("y = {0}", rsa.Encryption()),
                             replyMarkup: GeneralMenu());
                    }
                    if (Path.Contains("El Gamal"))
                    {
                        var data = msg.Text
                           .Split(',', ' ')
                           .Select(x => int.Parse(x))
                           .ToArray();
                        var elGamal = new CipherElGamal(data[0], data[1], data[2], data[3]);
                        var elGamalY = elGamal.Encrytion();
                        await client.SendTextMessageAsync(
                             chatId: msg.Chat.Id,
                             String.Format("y1 = {0}, y2 = {1}", elGamalY[0], elGamalY[1]),
                             replyMarkup: GeneralMenu());
                    }
                    //Калькулятор
                    if (Path.Contains("Разложение на множители"))
                    {
                        var data = int.Parse(msg.Text);
                        var answer = Equations.GetPrimeFactors(data);
                        var str = new StringBuilder();
                        foreach (var prime in answer)
                        {
                            str.Append(prime);
                            str.Append(' ');
                        }
                        await client.SendTextMessageAsync(
                             chatId: msg.Chat.Id,
                             str.ToString(),
                             replyMarkup: GeneralMenu());
                    }
                    if (Path.Contains("Быстрое экспоненцирование"))
                    {
                        var data = msg.Text
                           .Split(',', ' ')
                           .Select(x => int.Parse(x))
                           .ToArray();
                        var answer = Equations.FastExp(data[0], data[1], data[2]);
                        var str = new StringBuilder();
                        var i = 1;
                        foreach (var prime in answer)
                        {

                            str.Append(String.Format(" {0}^{1} = {2} (mod {3})\n", data[0], Math.Pow(2, i), prime, data[1]));
                            i++;
                        }
                        await client.SendTextMessageAsync(
                             chatId: msg.Chat.Id,
                             str.ToString(),
                             replyMarkup: GeneralMenu());
                    }
                    if (Path.Contains("y = a^b mod p"))
                    {
                        var data = msg.Text
                           .Split(',', ' ')
                           .Select(x => int.Parse(x))
                           .ToArray();
                        var answer = Equations.Power(data[0], data[1], data[2]);
                        await client.SendTextMessageAsync(
                             chatId: msg.Chat.Id,
                             String.Format("{0}^{1} (mod {2}) = {3}", data[0], data[1], data[2], answer),
                             replyMarkup: GeneralMenu());
                    }
                    if (Path.Contains("y = a*b mod p"))
                    {
                        var data = msg.Text
                           .Split(',', ' ')
                           .Select(x => int.Parse(x))
                           .ToArray();
                        var answer = Equations.Multiply(data[0], data[1], data[2]);
                        await client.SendTextMessageAsync(
                             chatId: msg.Chat.Id,
                             String.Format("{0}*{1} (mod {2}) = {3}", data[0], data[1], data[2], answer),
                             replyMarkup: GeneralMenu());
                    }
                    if (Path.Contains("Минимальная циклическая группа"))
                    {
                        var data = msg.Text
                           .Split(',', ' ')
                           .Select(x => int.Parse(x))
                           .ToArray();
                        int answer = 1;
                        string str;
                        if (data.Length == 1)
                        {
                            answer = Equations.MinimalCycleGroup(data[0]);
                            str = String.Format("{0} = минимальная циклическая группа поля {1}", answer, data[0]);
                        }
                        else if (data[0] > data[1])
                        {
                            answer = Equations.MinimalCycleGroup(data[0], data[1]);
                            str = String.Format("{0} = циклическая группа поля {1}", answer, data[0]);
                        }
                        await client.SendTextMessageAsync(
                             chatId: msg.Chat.Id,
                             String.Format("{0} - циклическая группа поля {1}", answer, data[0]),
                             replyMarkup: GeneralMenu());
                    }
                    if (Path.Contains("Метод тривиальных(простых) делений"))
                    {

                        var data = Equations.MethodTrialDiv(int.Parse(msg.Text));
                        var str = "";
                        var cost = (int)Math.Ceiling(Math.Sqrt(int.Parse(msg.Text)));
                        if (data != -1)
                            str = String.Format("Примерно количество делений - {0}, минимальный простой множитель равен {1}", cost, data);
                        else
                            str = String.Format("Примерно количество делений - {0}, не нашлось ни одного множителя", cost);
                        await client.SendTextMessageAsync(
                             chatId: msg.Chat.Id,
                             str,
                             replyMarkup: GeneralMenu());
                    }
                    if (Path.Contains("Метод Ферма"))
                    {

                        var data = int.Parse(msg.Text);
                        var answer = Equations.MethodDeFermat(data);
                        string str = "";
                        if (answer[0] != 0 && answer[1] != 0)
                            str = String.Format("{0} = {1}*{2}", data, answer[0], answer[1]);
                        else
                            str = String.Format("Для числа {0} метод Ферма не работает");
                        await client.SendTextMessageAsync(
                             chatId: msg.Chat.Id,
                             str,
                             replyMarkup: GeneralMenu());
                    }
                    if (Path.Contains("Тест Ферма"))
                    {

                        var data = msg.Text
                          .Split(',', ' ')
                          .Select(x => int.Parse(x))
                          .ToArray();
                        int[] answer = new int[2];
                        if (data.Length == 1)
                            answer = Equations.TestDeFermat(data[0]);
                        else
                            answer = Equations.TestDeFermat(data[0], data[1]);
                        string str = "";
                        if (answer[0] == 0)
                            str = String.Format("Не является свидетелем числа {1}, остаток = {0} ", answer[1], data[0]);
                        else
                            str = String.Format("Является свидетелем числа {0}", data[0]);
                        await client.SendTextMessageAsync(
                             chatId: msg.Chat.Id,
                             str,
                             replyMarkup: GeneralMenu());
                    }
                    if (Path.Contains("Обратный элемент"))
                    {
                        var data = msg.Text
                           .Split(',', ' ')
                           .Select(x => int.Parse(x))
                           .ToArray();
                        var answer = new ReversingEl(data[1], data[0]).CheckRes();
                        await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            String.Format("{0}*x=1 (mod {1}), x = {2}", data[0], data[1], answer),
                            replyMarkup: GeneralMenu());
                    }
                }
                // Первый уровень
                if (Path.Count == 1)
                {
                    isAnyOtherMessage = false;
                    switch (Path[0])
                    {
                        case "Шифры":
                            Path.Add(msg.Text);
                            switch (msg.Text)
                            {
                                case "RSA":
                                    await client.SendTextMessageAsync(
                                        chatId: msg.Chat.Id,
                                        "Введите данные в фомате x,n,e");
                                    break;
                                case "El Gamal":
                                    await client.SendTextMessageAsync(
                                        chatId: msg.Chat.Id,
                                        "Введите данные в фомате x,n,q,r");
                                    break;
                            }
                            break;
                        case "Калькулятор":
                            Path.Add(msg.Text);
                            switch (msg.Text)
                            {
                                case "Разложение на множители":
                                    await client.SendTextMessageAsync(
                                        chatId: msg.Chat.Id,
                                        "Введите x");
                                    break;
                                case "Быстрое экспоненцирование":
                                    await client.SendTextMessageAsync(
                                        chatId: msg.Chat.Id,
                                        "Введите x,mod,power");
                                    break;
                                case "y = a^b mod p":
                                    await client.SendTextMessageAsync(
                                       chatId: msg.Chat.Id,
                                       "Введите a,b,p");
                                    break;
                                case "y = a*b mod p":
                                    await client.SendTextMessageAsync(
                                       chatId: msg.Chat.Id,
                                       "Введите a,b,p");
                                    break;
                                case "Минимальная циклическая группа":
                                    await client.SendTextMessageAsync(
                                       chatId: msg.Chat.Id,
                                       "Введите поле field, если необходима циклическая группа больше чем p, введите число p");
                                    break;
                                case "Метод тривиальных(простых) делений":
                                    await client.SendTextMessageAsync(
                                       chatId: msg.Chat.Id,
                                       "Введите число для проверки");
                                    break;
                                case "Метод Ферма":
                                    await client.SendTextMessageAsync(
                                       chatId: msg.Chat.Id,
                                       "Введите число");
                                    break;
                                case "Тест Ферма":
                                    await client.SendTextMessageAsync(
                                       chatId: msg.Chat.Id,
                                       "Введите число x,свидетеля( по умолчанию стоит 2)");
                                    break;
                                case "Обратный элемент":
                                    await client.SendTextMessageAsync(
                                       chatId: msg.Chat.Id,
                                       "Введите данные в формате x,mod");
                                    break;
                            }
                            break;
                        case "Свойства":
                            Path.Add(msg.Text);
                            switch (msg.Text)
                            {
                                case "Свойства RSA":
                                    await client.SendPhotoAsync(
                                        chatId: msg.Chat.Id,
                                        photo: "https://sun9-52.userapi.com/impg/77trFRESV5SsqXglyWd19QbNtCU1og0x7-TD_A/wczzEj7N5mE.jpg?size=755x1075&quality=96&sign=3cfb442d84f79146d3f29104cb77a6cc&type=album",
                                         replyMarkup: GeneralMenu());
                                    break;
                                case "Свойства El Gamal":
                                    await client.SendPhotoAsync(
                                        chatId: msg.Chat.Id,
                                        photo: "https://sun9-8.userapi.com/impg/ie5AxO8oi-ZygXgj2mZw-qQPjj6bJCV7kyhrYw/H5RgztICWNY.jpg?size=743x626&quality=96&sign=1204a3f13c8bb2485f99143c3cd8c411&type=album",
                                         replyMarkup: GeneralMenu());
                                    break;
                                case "Свойства сравнений":
                                    await client.SendPhotoAsync(
                                        chatId: msg.Chat.Id,
                                        photo: "https://sun9-67.userapi.com/impg/0090ne0xZ709xwNjtCaGnAg4hu3PTXhXe9hQig/x69TBhFDzB0.jpg?size=738x517&quality=96&sign=9c7acc8e9b6316c6fac0f7177e67f023&type=album",
                                         replyMarkup: GeneralMenu());
                                    break;
                                case "Малая теорема Ферма":
                                    await client.SendPhotoAsync(
                                        chatId: msg.Chat.Id,
                                        photo: "https://sun9-62.userapi.com/impg/qfc3-r8Xea1cZfK06gmTEAVnMG6LxsWO1c3x8w/v5LAUDHfwE8.jpg?size=715x703&quality=96&sign=53a02996b0f3bd32ada18872d7e5b5d5&type=album",
                                         replyMarkup: GeneralMenu());
                                    break;
                                case "Теорема Эйлера":
                                    await client.SendPhotoAsync(
                                        chatId: msg.Chat.Id,
                                        photo: "https://sun9-1.userapi.com/impg/dY7ojGRmYDWh3eCccY8Zg95jyM0hW3s6F-_kdA/g809y17OXhU.jpg?size=744x488&quality=96&sign=e35e8f1d806b24c619c992f80b8ea7b7&type=album",
                                         replyMarkup: GeneralMenu());
                                    break;


                            }
                            break;
                    }
                }

                if (Path.Count == 0) //Начало работы
                {
                    isAnyOtherMessage = false;
                    switch (msg.Text)
                    {
                        case "Шифры":
                            Path.Add(msg.Text);
                            var Ciphers = await client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                "Выберите нужный шифр",
                                replyMarkup: GeneralCiphersMenu());
                            break;


                        case "Свойства":
                            Path.Add(msg.Text);
                            var prop = await client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                "Выберите нужную информацию",
                                replyMarkup: GeneralPropertiesMenu());
                            break;

                        case "Калькулятор":
                            Path.Add(msg.Text);
                            var calc = await client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                "Выберите нужный метод",
                                replyMarkup: GeneralMethodsMenu());
                            break;
                        default:
                            Path = new List<string>();
                            var answer = await client.SendTextMessageAsync(
                                 chatId: msg.Chat.Id,
                                 "Выберите нужное решение",
                                 replyMarkup: GeneralMenu());
                            break;
                    }
                }

                if (isAnyOtherMessage) // проверка в конце, если мы так и не нашли нужный ход
                {
                    Path = new List<string>();
                    var answer = await client.SendTextMessageAsync(
                         chatId: msg.Chat.Id,
                         "Выберите нужное решение",
                         replyMarkup: GeneralMenu());
                }
                isAnyOtherMessage = true;

            }
        }
        
        private static IReplyMarkup GeneralPropertiesMenu()
        {
            return new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>{ new KeyboardButton {Text = "Свойства RSA"}, new KeyboardButton {Text = "Свойства El Gamal"} },
                    new List<KeyboardButton>{ new KeyboardButton {Text = "Свойства сравнений"}, new KeyboardButton {Text = "Малая теорема Ферма"} },
                    new List<KeyboardButton>{ new KeyboardButton {Text = "Теорема Эйлера"} }
                }
            };
        }

        private static IReplyMarkup GeneralMethodsMenu()
        {
            return new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>{ new KeyboardButton {Text = "Разложение на множители"}, new KeyboardButton {Text = "Быстрое экспоненцирование" } },
                    new List<KeyboardButton>{ new KeyboardButton {Text = "y = a^b mod p"}, new KeyboardButton {Text = "y = a*b mod p" } },
                    new List<KeyboardButton>{ new KeyboardButton {Text = "Минимальная циклическая группа"}, new KeyboardButton {Text = "Метод тривиальных(простых) делений" } },
                    new List<KeyboardButton>{ new KeyboardButton {Text = "Метод Ферма"}, new KeyboardButton {Text = "Тест Ферма" } },
                    new List<KeyboardButton>{ new KeyboardButton {Text = "Обратный элемент"} }
                }
            };
        }

        private static IReplyMarkup GeneralCiphersMenu()
        {
            return new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>{ new KeyboardButton {Text = "RSA"}, new KeyboardButton {Text = "El Gamal"} }
                }
            };
        }

        private static IReplyMarkup GeneralMenu()
        {
            return new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>{ new KeyboardButton {Text = "Шифры" }, new KeyboardButton {Text ="Свойства" } },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Калькулятор"} }
                }
            };
        }
    }
}
