using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using Crypto;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptoSolver
{
    public class Bot
    {
        private static string Token { get; set; } = "5085494689:AAFBmzjJiSRspQk_XM9XGUh3iYjrE7F9H_w";
        private static TelegramBotClient client;
        private static bool isAnyOtherMessage = true;
        public static List<string> Path { get; set; } = new List<string>();

        private static Dictionary<string, Func<string, string>> dictMethods = new DictionaryMethods().dict;

        public Bot()
        {
            client = new TelegramBotClient(Token);
            client.StartReceiving();
            client.OnMessage += Client_OnMessage;
            Console.ReadLine();
            client.StopReceiving();
        }

        internal Task SendTextMessageAsync(object chatId, string v, object replyMarkup)
        {
            throw new NotImplementedException();
        }

        private static async void Client_OnMessage(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg.Text != null)
            {
                // Уровень решений
                if (Path.Count == 2)
                {
                    try
                    {
                        var data = msg.Text;
                        data = data.Replace(" ", "");
                        data = Regex.Replace(data, @"[A-Za-z]+", "");
                        Path.Add(data);
                        var answer = dictMethods[Path[1]](Path[2]);
                        await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            answer,
                            replyMarkup: GeneralMenu());
                        Path = new List<string>();
                    }
                    catch (FormatException)
                    {
                        await client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                "Вы ввели неверные значений!",
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
                                        "Введите x, mod, количество шагов");
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
                            Path = new List<string>();
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
