using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Collections;
using System.Numerics;
using System.Collections.Generic;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Program
{

    class TelegramBot
    {
        //текстовые переменные вывода
        private static string textwronginput = "Неправильный формат данных!";
        private static string textcorrectinput = "Отправка симптомов на обработку....";
        private static string textreference = "no information";
        private static string textwelcome = "no information";
        private static string textsymptoms = "no information";
        private static string textinputformat = "no information";
        //текстовые переменные вывода

        //текстовые названия кнопок(обязательно с маленькой буквы весь текст иначе работать не будет)📑🧾🔖📔
        private const string buttondefinitionofdisease = "🤧определение болезни😷";
        private const string buttonreference = "🤔справка⁉️";
        private const string buttonbacktomainmenu = "главное меню";
        //текстовые названия кнопок


        private static long userid;
        private static bool symptommenu = false;
        private static bool mainmenu = true;
        private static Dictionary<long, bool> user = new Dictionary<long, bool>();
        private static Dictionary<long, List<int>> inlinebuttonstouser = new Dictionary<long, List<int>>();
        private static int countsymptoms = 13; //количество симптомов





        static void Main(string[] args)
        {

            StreamReader sr1 = new StreamReader(@"Telegramassets/reference.txt");
            StreamReader sr2 = new StreamReader(@"Telegramassets/welcome.txt");
            StreamReader sr3 = new StreamReader(@"Telegramassets/symptoms.txt");
            StreamReader sr4 = new StreamReader(@"Telegramassets/inputformat.txt");
            textreference = sr1.ReadToEnd();
            textwelcome = sr2.ReadToEnd();
            textsymptoms = sr3.ReadToEnd();
            textinputformat = sr4.ReadToEnd();
            sr1.Close();
            sr2.Close();
            sr3.Close();
            sr4.Close();
            //Console.WriteLine(symptoms);
            var client = new TelegramBotClient("6525101854:AAFlyWBSUlLEAr_bL0ni4chPMyYwlz4nQF8");
            client.StartReceiving(Update, Error);


            Console.ReadLine();
        }







        async static Task Update(ITelegramBotClient botclient, Update update, CancellationToken token)
        {
            int countinputsymptoms = 1; //количество симптомов
            int air;//заглушка
            int[] symptomsarray = new int[countsymptoms];//введеные симптомы
            string buf = "";//буфер строк
            bool wrongmessage = false;//неправильные данных
            var message = update.Message;





            if (update.Type == UpdateType.CallbackQuery && userid > 0 && symptommenu)
            {
                //Console.WriteLine(userid);
                CallbackQuery callbackQuery = update!.CallbackQuery!;
                await botclient.AnswerCallbackQueryAsync(callbackQuery!.Id, $"Выбрано {callbackQuery.Data}");

                if (callbackQuery.Data != "send")
                {
                    _ = inlinebuttonstouser.TryAdd(userid, new List<int>(int.Parse(callbackQuery?.Data ?? "")));
                    inlinebuttonstouser[userid].Add(int.Parse(callbackQuery?.Data ?? ""));
                }
                else if (callbackQuery.Data != "51" && inlinebuttonstouser.ContainsKey(userid))
                {
                    inlinebuttonstouser[userid].Sort();
                    for (int i = 1; i < inlinebuttonstouser[userid].Count; i++)
                    {
                        if (inlinebuttonstouser[userid][i - 1] == inlinebuttonstouser[userid][i])
                        {
                            inlinebuttonstouser[userid].RemoveAt(i - 1);
                        }
                    }
                    Console.WriteLine("Выбранные симптомы для: " + userid);
                    for (int i = 0; i < inlinebuttonstouser[userid].Count; i++)
                    {
                        Console.WriteLine(inlinebuttonstouser[userid][i]);
                    }
                    await botclient.SendTextMessageAsync(callbackQuery!.Message!.Chat!.Id, symptomhandler(inlinebuttonstouser[userid], textsymptoms), parseMode: ParseMode.Html);

                }
                //await botclient.AnswerCallbackQueryAsync(callbackQuery!.Id, $"Received {callbackQuery.Data}");
                //await botclient.SendTextMessageAsync(callbackQuery!.Message.Chat.Id, $"Received {callbackQuery.Data}");
                //Console.WriteLine(callbackQuery.Data);

            }




            //Console.WriteLine(userid);
            //обработка входных данных

            if (message == null || message.Type != MessageType.Text) return;
            string TextMessage = message!.Text!.ToLower();
            userid = update!.Message!.Chat!.Id;

            //userid = message.Chat.Id;

            //Console.WriteLine(update.Type + "  " + userid);



            //обработка входных данных


            //обработка юзеров



            if (user.ContainsKey(userid) == false)
            {
                Console.WriteLine("новый пользователь:   " + message.Chat.FirstName);
                mainmenu = true;
                symptommenu = false;
                user.Add(userid, true);
            }
            if (user[userid])
            {
                mainmenu = true;
                symptommenu = false;
            }
            if (!user[userid])
            {
                mainmenu = false;
                symptommenu = true;
            }
            //обработка юзеров


            Console.WriteLine("Username: " + message.Chat.FirstName +/* " " + "Mainmenu: " + !user[userid] +*/ " Message: " + message.Text + " Data: " + message.Date.ToLocalTime());

            //отрисовка клавиатур
            ReplyKeyboardMarkup welcomkeyboard = new(new[]
            {
                new KeyboardButton[] { buttondefinitionofdisease,buttonreference},
            })
            {
                ResizeKeyboard = true
            };
            ReplyKeyboardMarkup symptomkeyboard = new(new[]
            {
                new KeyboardButton[] {buttonbacktomainmenu},
            })
            {
                ResizeKeyboard = true
            };
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                // first row
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Симптомы общего состояния", callbackData: "0"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Голова", callbackData: "1"),
                    InlineKeyboardButton.WithCallbackData(text: "Нос", callbackData: "2"),
                    InlineKeyboardButton.WithCallbackData(text: "Уши", callbackData: "3"),
                    InlineKeyboardButton.WithCallbackData(text: "Глаза", callbackData: "4"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Рот", callbackData: "5"),
                    InlineKeyboardButton.WithCallbackData(text: "Грудь", callbackData: "6"),
                    InlineKeyboardButton.WithCallbackData(text: "Спина", callbackData: "7"),
                    InlineKeyboardButton.WithCallbackData(text: "Сердце", callbackData: "8"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Почки", callbackData: "9"),
                    InlineKeyboardButton.WithCallbackData(text: "Печень", callbackData: "10"),
                    InlineKeyboardButton.WithCallbackData(text: "Легкие", callbackData: "11"),
                    InlineKeyboardButton.WithCallbackData(text: "Кожа", callbackData: "12"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Ноги", callbackData: "13"),
                    InlineKeyboardButton.WithCallbackData(text: "Живот", callbackData: "14"),
                    InlineKeyboardButton.WithCallbackData(text: "Руки", callbackData: "15"),
                    InlineKeyboardButton.WithCallbackData(text: "М-П.сист", callbackData: "16"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Получить симптомы для выбора", callbackData: "send"),
                },
            });
            //отрисовка клавиатур
            if (TextMessage == "/restart")
            {
                mainmenu = true;
                symptommenu = false;
                user.Remove(userid);
                await botclient.SendTextMessageAsync(message.Chat.Id, "<b>Выбирайте что вам необходимо:</b>", parseMode: ParseMode.Html, disableNotification: true, replyMarkup: welcomkeyboard);
            }
            //Console.WriteLine(update?.CallbackQuery?.Data ?? "null");





            if (mainmenu)
            {


                switch (TextMessage)
                {
                    case "/start":
                        {
                            await botclient.SendTextMessageAsync(message.Chat.Id, textwelcome, parseMode: ParseMode.Html, replyMarkup: welcomkeyboard);
                            await botclient.SendTextMessageAsync(message.Chat.Id, "<b>Выбирайте что вам необходимо:</b>", parseMode: ParseMode.Html, disableNotification: true, replyMarkup: welcomkeyboard);


                            break;
                        }
                    case buttondefinitionofdisease:
                        {
                            mainmenu = false;
                            symptommenu = true;
                            user[userid] = false;


                            await botclient.SendTextMessageAsync(message.Chat.Id, "Ввод симптомов:", replyMarkup: symptomkeyboard, disableNotification: true);
                            await botclient.SendTextMessageAsync(message.Chat.Id, textinputformat, replyMarkup: inlineKeyboard, parseMode: ParseMode.Html, disableNotification: true);



                            TextMessage = "";
                            break;
                        }
                    case buttonreference:
                        {
                            await botclient.SendTextMessageAsync(message.Chat.Id, textreference, replyMarkup: welcomkeyboard, disableNotification: true);

                            break;
                        }
                    default: break;
                }
            }
            if (symptommenu)
            {
                switch (TextMessage)
                {
                    case buttonbacktomainmenu:
                        {

                            await botclient.SendTextMessageAsync(message.Chat.Id, "<b>Выбирайте что вам необходимо:</b>", parseMode: ParseMode.Html, replyMarkup: welcomkeyboard, disableNotification: true);
                            mainmenu = true;
                            symptommenu = false;
                            user[userid] = true;

                            break;
                        }
                    default: break;
                }




                if (TextMessage != "" && mainmenu == false)
                {
                    //await botclient.SendTextMessageAsync(message.Chat.Id, "Проверка значений....");
                    //проверка формата строки
                    for (int i = 0; i < TextMessage.Length; i++)
                    {
                        if (!int.TryParse(TextMessage[i].ToString(), out air) && TextMessage[i] != ' ') wrongmessage = true;
                        if (i != TextMessage.Length - 1)
                        {
                            if (TextMessage[i] == ' ' && TextMessage[i + 1] == ' ') wrongmessage = true;
                        }
                        else if (TextMessage[i] == ' ' && TextMessage[i - 1] == ' ') wrongmessage = true;
                    }
                    //проверка формата строки

                    //исключение проблем 
                    if (wrongmessage)
                    {
                        await botclient.SendTextMessageAsync(message.Chat.Id, "<b>" + textwronginput + "</b>", parseMode: ParseMode.Html, replyToMessageId: message.MessageId);
                        return;
                    }
                    //исключение проблем 

                    //подготовка массива
                    for (int i = 0, j = 0; i < TextMessage.Length; i++)
                    {
                        if (TextMessage[i] == ' ')
                        {
                            symptomsarray[j] = int.Parse(buf);
                            buf = "";
                            j++;
                            countinputsymptoms++;
                        }
                        else buf += TextMessage[i];
                    }
                    symptomsarray[countinputsymptoms - 1] = int.Parse(buf);
                    Array.Resize(ref symptomsarray, countinputsymptoms);
                    Array.Sort(symptomsarray);
                    //подготовка массива

                    //проверка массива
                    if (symptomsarray[0] > countsymptoms || symptomsarray[0] == 0) wrongmessage = true;
                    for (int i = 1; i < countinputsymptoms; i++)
                    {
                        if (symptomsarray[i - 1] == symptomsarray[i])
                        {
                            wrongmessage = true;
                            break;
                        }

                        if (i == countinputsymptoms - 1)
                        {
                            if (symptomsarray[i] > countsymptoms)
                            {
                                wrongmessage = true;
                                break;
                            }

                        }
                        else
                        {
                            if (symptomsarray[i - 1] > countsymptoms)
                            {
                                wrongmessage = true;
                                break;
                            }
                        }
                    }
                    //проверка массива

                    //отправка на обработку нейросети
                    if (wrongmessage)
                    {
                        await botclient.SendTextMessageAsync(message.Chat.Id, "<b>" + textwronginput + "</b>", parseMode: ParseMode.Html, replyToMessageId: message.MessageId);
                        return;
                    }
                    else
                    {

                        await botclient.SendTextMessageAsync(message.Chat.Id, "<b>" + textcorrectinput + "</b>", parseMode: ParseMode.Html, replyToMessageId: message.MessageId);
                        for (int i = 0; i < countinputsymptoms; i++)
                        {
                            Console.WriteLine(symptomsarray[i]);
                        }
                    }
                    //отправка на обработку нейросети













                }
            }
            /*
            if (lastusername != username && flagmessage == false)
            {
                flagmessage = true;
                lastusername = username;
            }
            else
            {
                flagmessage = false;
            }
            Console.WriteLine(username + "  " + lastusername);
            */
        }


        private static string symptomhandler(List<int> select, string symptoms)
        {
            string symptomsselected = ""; //= symptoms.Substring(symptoms.IndexOf("0-"), symptoms.IndexOf("-0") - symptoms.IndexOf("0-")).Remove(0, 3);

            for (int i = 0; i < select.Count; i++)
            {
                symptomsselected += symptoms.Substring(symptoms.IndexOf(select[i] + "-"), symptoms.IndexOf("-" + select[i]) - symptoms.IndexOf(select[i] + "-")).Remove(0, 3);
            }
            Console.WriteLine(symptomsselected);
            return symptomsselected;


        }


        private static Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}