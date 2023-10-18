using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Newtonsoft.Json;

namespace Program
{

    class TelegramBot
    {
        private static Dictionary<long, List<int>> inlinebuttonstouser = new Dictionary<long, List<int>>();
        private static Dictionary<string, string> botword = new Dictionary<string, string>();
        private static Dictionary<long, User> database = new Dictionary<long, User>();
        private static Settings settings = JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText(@"Telegramassets/Botsettings.json"));
        private static SymptomsList? SymptomsList = JsonConvert.DeserializeObject<SymptomsList>(System.IO.File.ReadAllText(@settings.pathsymptomslistjson));
        private static long userid;



        static void Main()
        {
            botword = Dictionarypreparer.BotwordDictpreparer(botword, settings.pathtextforbotjson);
            database = Dictionarypreparer.DatabaseDictFillFromJSON(database, settings.pathdatabasejson);

            var client = new TelegramBotClient(settings.token);
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }


        async static Task Update(ITelegramBotClient botclient, Update update, CancellationToken token)
        {
            int countinputsymptoms = 1; //количество симптомов
            int air;//заглушка
            int[] symptomsarray = new int[settings.countsymptoms];//введеные симптомы
            string buf = "";//буфер строк
            bool wrongmessage = false;//неправильные данных
            var message = update.Message;















            if (update.Type == UpdateType.CallbackQuery && userid > 0 && database[userid].symptommenu)
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
                    await botclient.SendTextMessageAsync(callbackQuery!.Message!.Chat!.Id, Dictionarypreparer.symptomhandler(inlinebuttonstouser[userid], SymptomsList), parseMode: ParseMode.Html);

                }
                //await botclient.AnswerCallbackQueryAsync(callbackQuery!.Id, $"Received {callbackQuery.Data}");
                //await botclient.SendTextMessageAsync(callbackQuery!.Message.Chat.Id, $"Received {callbackQuery.Data}");
                //Console.WriteLine(callbackQuery.Data);
            }


            if (message == null || message.Type != MessageType.Text) return;
            string TextMessage = message!.Text!.ToLower();
            userid = update!.Message!.Chat!.Id;



            User user = new User();
            if (database.ContainsKey(userid) == false)
            {
                Console.WriteLine("новый пользователь:   " + message.Chat.FirstName);
                database.TryAdd(userid, user);
            }


            Console.WriteLine("maintmenu: " + database[userid].mainmenu + " symptommenu: " + database[userid].symptommenu);
            Console.WriteLine("Username: " + message.Chat.FirstName +/* " " + "Mainmenu: " + !user[userid] +*/ " Message: " + message.Text + " Data: " + message.Date.ToLocalTime());



            //отрисовка клавиатур
            ReplyKeyboardMarkup welcomkeyboard = new(new[]
            {
                new KeyboardButton[] { botword["textbuttondefinitionofdisease"],botword["textbuttonreference"]},
            })
            {
                ResizeKeyboard = true
            };
            ReplyKeyboardMarkup symptomkeyboard = new(new[]
            {
                new KeyboardButton[] {botword["textbuttonbacktomainmenu"]},
            })
            {
                ResizeKeyboard = true
            };
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                // first row
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: botword["textsosinline"], callbackData: "0"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: botword["textheadinline"], callbackData: "1"),
                    InlineKeyboardButton.WithCallbackData(text: botword["textnoseinline"], callbackData: "2"),
                    InlineKeyboardButton.WithCallbackData(text: botword["texthearinline"], callbackData: "3"),
                    InlineKeyboardButton.WithCallbackData(text: botword["texteyesinline"], callbackData: "4"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: botword["textmouthinline"], callbackData: "5"),
                    InlineKeyboardButton.WithCallbackData(text: botword["textchestinline"], callbackData: "6"),
                    InlineKeyboardButton.WithCallbackData(text: botword["textbackinline"], callbackData: "7"),
                    InlineKeyboardButton.WithCallbackData(text: botword["textheartinline"], callbackData: "8"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: botword["textkidneysinline"], callbackData: "9"),
                    InlineKeyboardButton.WithCallbackData(text: botword["textliverinline"], callbackData: "10"),
                    InlineKeyboardButton.WithCallbackData(text: botword["textlungsinline"], callbackData: "11"),
                    InlineKeyboardButton.WithCallbackData(text: botword["textskininline"], callbackData: "12"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: botword["textlegsinline"], callbackData: "13"),
                    InlineKeyboardButton.WithCallbackData(text: botword["textstomachinline"], callbackData: "14"),
                    InlineKeyboardButton.WithCallbackData(text: botword["textarmsinline"], callbackData: "15"),
                    InlineKeyboardButton.WithCallbackData(text: botword["textgenitourinarysysteminline"], callbackData: "16"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: botword["textgetsymptomsinline"], callbackData: "send"),
                },
            });
            InlineKeyboardMarkup inlinelinkes = new(new[]
            {
                InlineKeyboardButton.WithUrl(text: "Creator",url: botword["creatorlinklinline"]),
                InlineKeyboardButton.WithUrl(text: "TeamLid",url: botword["teamlidlinklinline"]),
                InlineKeyboardButton.WithUrl(text: "Helper",url: botword["helperlinklinline"]),
                InlineKeyboardButton.WithUrl(text: "Helper2",url: botword["helper2linklinline"]),
                InlineKeyboardButton.WithUrl(text: "GitHub",url: botword["githublinklinline"])
            });
            //обработка клавиатур




            if (TextMessage == "/start")
            {
                database[userid].mainmenu = true;
                database[userid].symptommenu = false;
                await botclient.SendTextMessageAsync(message.Chat.Id, "<b>Выбирайте что вам необходимо:</b>", parseMode: ParseMode.Html, disableNotification: true, replyMarkup: welcomkeyboard);
            }






            if (database[userid].mainmenu)
            {
                if (TextMessage == "/start")
                {
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["textwelcome"], parseMode: ParseMode.Html, replyMarkup: welcomkeyboard);
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["textwelcome2"], parseMode: ParseMode.Html, disableNotification: true, replyMarkup: welcomkeyboard);
                }
                else if (TextMessage == botword["textbuttondefinitionofdisease"])
                {
                    database[userid].mainmenu = false;
                    database[userid].symptommenu = true;
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["textinputformat"], replyMarkup: symptomkeyboard, disableNotification: true);
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["textinputformat2"], replyMarkup: inlineKeyboard, parseMode: ParseMode.Html, disableNotification: true);
                    TextMessage = "";
                }
                else if (TextMessage == botword["textbuttonreference"])
                {
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["textreference"], replyMarkup: inlinelinkes, disableNotification: true);
                }
                else return;
            }

            if (database[userid].symptommenu)
            {
                if (TextMessage == botword["textbuttonbacktomainmenu"])
                {
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["textwelcome2"], parseMode: ParseMode.Html, replyMarkup: welcomkeyboard, disableNotification: true);
                    database[userid].mainmenu = true;
                    database[userid].symptommenu = false;
                }





                if (TextMessage != "" && database[userid].mainmenu == false)
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
                        await botclient.SendTextMessageAsync(message.Chat.Id, "<b>" + botword["textwronginput"] + "</b>", parseMode: ParseMode.Html, replyToMessageId: message.MessageId);
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
                    if (symptomsarray[0] > settings.countsymptoms || symptomsarray[0] == 0) wrongmessage = true;
                    for (int i = 1; i < countinputsymptoms; i++)
                    {
                        if (symptomsarray[i - 1] == symptomsarray[i])
                        {
                            wrongmessage = true;
                            break;
                        }

                        if (i == countinputsymptoms - 1)
                        {
                            if (symptomsarray[i] > settings.countsymptoms)
                            {
                                wrongmessage = true;
                                break;
                            }

                        }
                        else
                        {
                            if (symptomsarray[i - 1] > settings.countsymptoms)
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
                        await botclient.SendTextMessageAsync(message.Chat.Id, "<b>" + botword["textwronginput"] + "</b>", parseMode: ParseMode.Html, replyToMessageId: message.MessageId);
                        return;
                    }
                    else
                    {

                        await botclient.SendTextMessageAsync(message.Chat.Id, "<b>" + botword["textcorrectinput"] + "</b>", parseMode: ParseMode.Html, replyToMessageId: message.MessageId);
                        for (int i = 0; i < countinputsymptoms; i++)
                        {
                            Console.WriteLine(symptomsarray[i]);
                        }
                    }
                    //отправка на обработку нейросети
                }
            }
            Console.WriteLine("data updated");
            Dictionarypreparer.DatabaseDictSaverToJSON(database, settings.pathdatabasejson);
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