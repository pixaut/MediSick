using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Newtonsoft.Json;

namespace Program
{

    class TelegramBot
    {
        private static Dictionary<long, List<int>> inlinebuttonstouser = new Dictionary<long, List<int>>();
        public static Dictionary<string, string> botword = new Dictionary<string, string>();
        private static Dictionary<long, User> database = new Dictionary<long, User>();
        private static Settings? settings = JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText(@"Telegramassets/Botsettings.json"));
        private static SymptomsList? SymptomsList = JsonConvert.DeserializeObject<SymptomsList>(System.IO.File.ReadAllText(settings!.pathsymptomslistjson));
        private static long userid = 0;
        static void Main()
        {
            botword = Dictionarypreparer.BotwordDictpreparer(botword, settings!.pathtextforbotjson);
            database = Dictionarypreparer.DatabaseDictFillFromJSON(settings.pathdatabasejson);

            var client = new TelegramBotClient(settings.token);
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }


        async static Task Update(ITelegramBotClient botclient, Update update, CancellationToken token)
        {
            var message = update.Message;


            if (update.Type == UpdateType.CallbackQuery && userid > 0 && database[userid].symptommenu)
            {
                CallbackQuery callbackQuery = update!.CallbackQuery!;
                await botclient.AnswerCallbackQueryAsync(callbackQuery!.Id, $"Выбрано {callbackQuery.Data}");
                if (callbackQuery.Data != "send")
                {
                    inlinebuttonstouser.TryAdd(userid, new List<int>(int.Parse(callbackQuery?.Data ?? "")));
                    inlinebuttonstouser[userid].Add(int.Parse(callbackQuery?.Data ?? ""));
                }
                else if (callbackQuery.Data == "send" && inlinebuttonstouser.ContainsKey(userid))
                {
                    inlinebuttonstouser[userid].Sort();
                    for (int i = 1; i < inlinebuttonstouser[userid].Count; i++)
                    {
                        if (inlinebuttonstouser[userid][i - 1] == inlinebuttonstouser[userid][i])
                        {
                            inlinebuttonstouser[userid].RemoveAt(i - 1);
                        }
                    }
                    Console.WriteLine("Picked symptoms for: " + userid);
                    for (int i = 0; i < inlinebuttonstouser[userid].Count; i++)
                    {
                        Console.WriteLine(inlinebuttonstouser[userid][i]);
                    }
                    await botclient.SendTextMessageAsync(callbackQuery!.Message!.Chat!.Id, Dictionarypreparer.symptomhandler(inlinebuttonstouser[userid], SymptomsList!), parseMode: ParseMode.Html);

                }
                //await botclient.AnswerCallbackQueryAsync(callbackQuery!.Id, $"Received {callbackQuery.Data}");
                //await botclient.SendTextMessageAsync(callbackQuery!.Message.Chat.Id, $"Received {callbackQuery.Data}");
                //Console.WriteLine(callbackQuery.Data);
            }


            if (message == null || message.Type != MessageType.Text) return;
            string TextMessage = message!.Text!.ToLower();
            userid = message.Chat.Id;


            Console.WriteLine(message.Chat.LinkedChatId);
            Console.WriteLine(message.Chat.Location);
            Console.WriteLine(message.Chat.Title);
            Console.WriteLine(message.Chat.InviteLink);
            User user = new User
            {
                name = message!.Chat.FirstName ?? "no name"
            };
            if (database.ContainsKey(userid) == false)
            {
                Console.WriteLine("New user:   " + message.Chat.FirstName);
                database.TryAdd(userid, user);
            }


            Console.WriteLine("maintmenu: " + database[userid].mainmenu + " symptommenu: " + database[userid].symptommenu);
            Console.WriteLine("Username: " + message.Chat.FirstName +/* " " + "Mainmenu: " + !user[userid] +*/ " Message: " + message.Text + " Data: " + message.Date.ToLocalTime());


            if (TextMessage == "/start")
            {
                database[userid].mainmenu = true;
                database[userid].symptommenu = false;
                await botclient.SendStickerAsync(message.Chat.Id, sticker: InputFile.FromUri("https://cdn.tlgrm.app/stickers/348/e30/348e3088-126b-4939-b317-e9036499c515/192/1.webp"));
                await botclient.SendTextMessageAsync(message.Chat.Id, database[userid].name + " " + botword["textwelcome"], parseMode: ParseMode.Html, replyMarkup: Keyboard.welcomkeyboard);
                //await botclient.SendTextMessageAsync(message.Chat.Id, botword["textwelcome2"], parseMode: ParseMode.Html, disableNotification: true, replyMarkup: Keyboard.welcomkeyboard);

            }

            if (database[userid].mainmenu)
            {
                if (TextMessage == botword["textbuttondefinitionofdisease"])
                {
                    database[userid].mainmenu = false;
                    database[userid].symptommenu = true;
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["textinputformat"], replyMarkup: Keyboard.symptomkeyboard, disableNotification: true);
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["textinputformat2"], replyMarkup: Keyboard.inlineKeyboard, parseMode: ParseMode.Html, disableNotification: true);
                    inlinebuttonstouser.Remove(userid);
                    TextMessage = "";
                }
                else if (TextMessage == botword["textbuttonreference"])
                {
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["textreference"], replyMarkup: Keyboard.inlinelinkes, disableNotification: true);
                }
                else return;
            }

            if (database[userid].symptommenu)
            {
                if (TextMessage == botword["textbuttonbacktomainmenu"])
                {
                    await botclient.SendTextMessageAsync(message.Chat.Id, database[userid].name + " " + botword["textwelcome2"], parseMode: ParseMode.Html, replyMarkup: Keyboard.welcomkeyboard, disableNotification: true);
                    database[userid].mainmenu = true;
                    database[userid].symptommenu = false;
                }





                if (TextMessage != "" && database[userid].mainmenu == false) //проверка правильности входных ванных
                {
                    bool wrongmessage = false;//неправильные данных
                    string buf = "";//буфер строк
                    int countinputsymptoms = 1;//для расчетов
                    int[] symptomsarray = new int[settings!.countsymptoms];//введеные симптомы


                    //await botclient.SendTextMessageAsync(message.Chat.Id, "Проверка значений....");
                    //проверка формата строки
                    for (int i = 0; i < TextMessage.Length; i++)
                    {
                        if (!int.TryParse(TextMessage[i].ToString(), out _) && TextMessage[i] != ' ') wrongmessage = true;
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
            Console.WriteLine("Data JSON updated:");
            Dictionarypreparer.DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
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