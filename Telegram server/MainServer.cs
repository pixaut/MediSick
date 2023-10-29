using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Newtonsoft.Json;
using System.Text;
using static Program.Secondaryfunctions;
using static Program.Keyboard;
using Telegram.Bot.Types.ReplyMarkups;
using System.Diagnostics;
using System.Formats.Tar;
namespace Program
{
    class TelegramBot
    {
        //Global variables:
        public static Dictionary<string, string> botwordru = new Dictionary<string, string>();
        public static Dictionary<string, string> botworden = new Dictionary<string, string>();
        public static Dictionary<string, string> botword = new Dictionary<string, string>();
        public static Dictionary<long, User> database = new Dictionary<long, User>();
        private static Settings? settings = JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText(@"Telegramassets/Botsettings.json"));
        public static long userid = 0;

        static void Main()
        {
            //Data collection from JSON:
            botwordru = BotwordDictpreparer(botwordru, "Telegramassets/Textforbotru.json");
            botworden = BotwordDictpreparer(botworden, "Telegramassets/Textforboten.json");
            database = DatabaseDictFillFromJSON(settings!.pathdatabasejson);

            //Launching a telegram bot:
            var client = new TelegramBotClient(settings!.token!);
            client.StartReceiving(Update, Error);

            //Protection against program closure:
            Console.ReadLine();
        }



        async static Task Update(ITelegramBotClient botclient, Update update, CancellationToken token)
        {
            //Declaring important variables:
            var message = update.Message;
            var callback = update.CallbackQuery;
            if (update.Type != UpdateType.Message && update.Type != UpdateType.CallbackQuery) return;
            if (update.Type == UpdateType.CallbackQuery) userid = callback!.Message!.Chat!.Id;
            if (update.Type == UpdateType.Message) userid = message.Chat.Id;




            //Creating a user object and processing new users:
            User user = new User();
            Console.WriteLine(userid);

            if (database.ContainsKey(userid) == false && update.Type == UpdateType.Message)
            {
                Console.WriteLine("New user:   " + message!.Chat.FirstName);
                database.TryAdd(userid, user);
                database[userid].name = message!.Chat!.FirstName!;
                DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
            }

            interfacelocalization(database[userid].language);

            //Inline buttons processing:
            if (update.Type == UpdateType.CallbackQuery && userid > 0 && database[userid].language == "non" && database[userid].lastmessage == "/start")
            {
                await botclient.DeleteMessageAsync(userid, callback!.Message!.MessageId, cancellationToken: token);
                await botclient.AnswerCallbackQueryAsync(callback!.Id, callback.Data, cancellationToken: token);
                if (callback.Data == "en" && database[userid].language == "non")
                {
                    await botclient.SendTextMessageAsync(userid, "You pick english language.🇬🇧", parseMode: ParseMode.Html, cancellationToken: token);
                    database[userid].language = "en";
                }
                else if (callback.Data == "ru" && database[userid].language == "non")
                {
                    await botclient.SendTextMessageAsync(userid, "Вы выбрали русский язык.🇷🇺", parseMode: ParseMode.Html, cancellationToken: token);
                    database[userid].language = "ru";
                }

                interfacelocalization(database[userid].language);

                database[userid].mainmenu = true;
                database[userid].symptommenu = false;
                database[userid].inlinesymptomkey = false;
                await botclient.SendStickerAsync(userid, sticker: InputFile.FromUri(botword["hallostik"]), cancellationToken: token);
                await botclient.SendTextMessageAsync(userid, database[userid].name + " " + botword["textwelcome"], parseMode: ParseMode.Html, cancellationToken: token);
                await botclient.SendTextMessageAsync(userid, botword["textchoicegender"], parseMode: ParseMode.Html, replyMarkup: inlinegenderkeyboard, cancellationToken: token);
                DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
                return;
            }

            if (update.Type == UpdateType.CallbackQuery && userid > 0 && database[userid].gender == "non" && database[userid].lastmessage == "/start" && database[userid].language != "non")
            {
                await botclient.DeleteMessageAsync(userid, callback!.Message!.MessageId, cancellationToken: token);
                await botclient.AnswerCallbackQueryAsync(callback!.Id, callback.Data, cancellationToken: token);
                if (callback.Data == "man" && database[userid].gender == "non")
                {
                    await botclient.SendTextMessageAsync(userid, botword["textman"], replyMarkup: welcomkeyboard, parseMode: ParseMode.Html, cancellationToken: token);
                    database[userid].gender = "man";
                }
                else if (callback.Data == "woman" && database[userid].gender == "non")
                {
                    await botclient.SendTextMessageAsync(userid, botword["textwoman"], replyMarkup: welcomkeyboard, parseMode: ParseMode.Html, cancellationToken: token);
                    database[userid].gender = "woman";
                }

                DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
                return;
            }

            if (update.Type == UpdateType.CallbackQuery && userid > 0 && database[userid].symptommenu && database[userid].inlinesymptomkey)
            {
                await botclient.AnswerCallbackQueryAsync(callback!.Id, $"picked {database[userid]!.inlinebuttpressed!.Count}", cancellationToken: token);
                if (int.TryParse(callback.Data, out _) && !database[userid]!.inlinebuttpressed!.Contains(int.Parse(callback.Data)))
                {
                    database[userid]!.inlinebuttpressed!.Add(int.Parse(callback?.Data ?? ""));
                }
                else if (callback.Data == "send" && database[userid]!.inlinebuttpressed!.Count != 0)
                {
                    await botclient.DeleteMessageAsync(userid, callback!.Message!.MessageId, cancellationToken: token);
                    database[userid]!.inlinebuttpressed!.Sort();
                    await botclient.SendTextMessageAsync(userid, symptomhandler(database[userid]!.inlinebuttpressed!), parseMode: ParseMode.Html, cancellationToken: token);
                    database[userid]!.inlinebuttpressed!.Clear();
                    database[userid].inlinesymptomkey = false;
                }
                else if (callback.Data == "cancel" && database[userid]!.inlinebuttpressed!.Count != 0)
                {
                    database[userid]!.inlinebuttpressed!.Clear();
                }
                DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
                return;
            }
            if (update.Type == UpdateType.CallbackQuery && userid > 0 && database[userid].mainmenu == false && database[userid].symptommenu == true && database[userid].inlinesymptomkey == false && callback.Data.Substring(0, 11) == "description")
            {
                await botclient.AnswerCallbackQueryAsync(callback!.Id, callback.Data, cancellationToken: token);
                await botclient.SendTextMessageAsync
                (
                    userid,
                    text: botword
                    [
                        "d" + database[userid]!.listofrecentdiseases![int.Parse(callback.Data.Substring(11)) - 1]]!.Substring(3, botword["d" + database[userid]!.listofrecentdiseases![int.Parse(callback.Data.Substring(11)) - 1]]!.Length - 7)! +
                        " - " + botword["textdescriptiondisease" +
                        database![userid]!.listofrecentdiseases![int.Parse(callback.Data.Substring(11)) - 1]]!,
                    parseMode: ParseMode.Html,
                    cancellationToken: token
                );
                interfacelocalization(database[userid].language);
                DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
                return;
            }

            //Some preparation:
            if (message == null || message.Type != MessageType.Text) return;
            string TextMessage = message!.Text!.ToLower();
            database[userid].lastmessage = TextMessage;

            //Logging:
            if (settings!.enablelogging) Console.WriteLine("------------------------------------------------------\nNew Message⬇️\n" + $"Userid: {userid}\n" + $"Username: {message.Chat.FirstName}\n" + $"Message: {message.Text}\n" + $"Data: {message.Date.ToLocalTime()}\n" + "------------------------------------------------------\n");

            //Check for "/start":
            if (TextMessage == "/start")
            {

                database[userid].language = "non";
                database[userid].gender = "non";


                await botclient.SendTextMessageAsync(userid, text: "Let's start....", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: token);
                await botclient.SendTextMessageAsync(userid, botword["textchoicelanguage"], parseMode: ParseMode.Html, replyMarkup: inlinelanguagekeyboard, cancellationToken: token);

            }
            if (database[userid].language == "non" || database[userid].gender == "non")
            {
                database[userid].mainmenu = false;
                database[userid].symptommenu = false;
                return;
            }


            //Checking main menu buttons:
            if (database[userid].mainmenu && !database[userid].symptommenu && !database[userid].inlinesymptomkey)
            {
                if (TextMessage == botword["textbuttondefinitionofdisease"].ToLower())
                {
                    await using Stream stream = System.IO.File.OpenRead($"Telegramassets\\videoinstruction2.mp4");
                    database[userid].mainmenu = false;
                    database[userid].symptommenu = true;


                    await botclient.SendVideoAsync(userid, video: InputFile.FromStream(stream), replyMarkup: symptomkeyboard, thumbnail: InputFile.FromUri("https://raw.githubusercontent.com/TelegramBots/book/master/src/2/docs/thumb-clock.jpg"), supportsStreaming: true, cancellationToken: token);
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["textinputformat2"], replyMarkup: inlineKeyboard, parseMode: ParseMode.Html, disableNotification: true, cancellationToken: token);
                    //await botclient.SendTextMessageAsync(message.Chat.Id, botword["textinputformat"], replyMarkup: inlineKeyboard, disableNotification: true, cancellationToken: token);
                    database[userid].inlinesymptomkey = true;
                    database[userid]!.inlinebuttpressed!.Clear();
                    TextMessage = "";
                }
                else if (TextMessage == botword["textbuttonreference"].ToLower())
                {
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["textreference"], replyMarkup: inlinelinkes, disableNotification: true, cancellationToken: token);
                    await botclient.SendStickerAsync(message.Chat.Id, sticker: InputFile.FromUri(botword["refstik"]), cancellationToken: token);
                }
                else return;
            }

            //Disease detection function:
            if (!database[userid].mainmenu && database[userid].symptommenu)
            {
                //Checking symptom menu buttons:
                if (TextMessage == botword["textbuttonbacktomainmenu"].ToLower())
                {
                    await botclient.SendTextMessageAsync(message.Chat.Id, database[userid].name + " " + botword["textwelcome2"], parseMode: ParseMode.Html, replyMarkup: welcomkeyboard, disableNotification: true, cancellationToken: token);
                    database[userid].mainmenu = true;
                    database[userid].symptommenu = false;
                    database[userid].inlinesymptomkey = false;
                }

                //Processing input symptoms and sending the neural network with output:
                if (!database[userid].mainmenu && !database[userid].inlinesymptomkey && database[userid].symptommenu)
                {
                    string bufs = "";
                    long bufi = 0;
                    int countinputsymptoms = 0;
                    string symptomslist = "";

                    TextMessage += " ";
                    for (int i = 0; i < TextMessage.Length; i++)
                    {
                        if (TextMessage[i] > 47 && TextMessage[i] < 58)
                        {
                            bufs += TextMessage[i];
                            long.TryParse(bufs, out bufi);
                            if (bufi > 110) bufs = "";
                            continue;
                        }
                        if (bufs != "")
                        {
                            symptomslist += bufs + " ";
                            bufs = "";
                            countinputsymptoms++;
                        }
                    }

                    await botclient.SendStickerAsync(message.Chat.Id, sticker: InputFile.FromUri(botword["waitstik"]), cancellationToken: token);
                    await botclient.SendTextMessageAsync(message.Chat.Id, "<b>" + botword["textcorrectinput"] + "</b>", parseMode: ParseMode.Html, replyToMessageId: message.MessageId, cancellationToken: token);

                    using (FileStream fs = new FileStream(settings.pathinputuser, FileMode.Truncate))
                    {
                        byte[] inpiutfile = Encoding.ASCII.GetBytes(database[userid].gender[0] + " " + countinputsymptoms + " " + symptomslist);
                        fs.Write(inpiutfile, 0, inpiutfile.Length);
                        fs.Close();
                    }
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = @"..\..\..\..\WithOutLearning\ProcessTest.exe";
                        process.StartInfo.WorkingDirectory = @"..\..\..\..\WithOutLearning";
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        process.Start();
                        process.WaitForExit(2000);
                    }
                    try
                    {

                        string diagnosis = botword["textdiseaseprognosis"];
                        List<int> disandperc = new List<int>();
                        bufs = "";
                        FileStream fs = new FileStream(settings.pathoutputuser, FileMode.Open);
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        string textFromFile = Encoding.Default.GetString(buffer);
                        fs.Close();

                        for (int i = 0; i < textFromFile.Length; ++i)
                        {
                            if (textFromFile[i] == ' ' || textFromFile[i] == '\n')
                            {
                                disandperc.Add(int.Parse(bufs));
                                bufs = "";
                            }
                            bufs += textFromFile[i];
                        }
                        database[userid]!.listofrecentdiseases!.Clear();
                        for (int i = 0, j = 1; i < disandperc.Count; i += 2, j++)
                        {
                            diagnosis += j + "." + botword["d" + disandperc[i]] + " ▼\n С вероятностью: ";
                            diagnosis += "│" + cantileverstrip(disandperc[i + 1]) + "│ " + disandperc[i + 1] + " %\n\n";
                            database[userid]!.listofrecentdiseases!.Add(disandperc[i]);





                        }
                        diagnosis += botword["textdiseasewarning"];
                        Console.WriteLine(diagnosis);
                        await botclient.SendTextMessageAsync(userid, diagnosis, replyMarkup: inlinepreparationdescriptiondiseases(), parseMode: ParseMode.Html, cancellationToken: token);



                    }
                    catch (Exception) { }
                }
            }

            //Logging and save JSON database:
            Console.WriteLine("Data JSON updated:");
            DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
        }




        //Errors checking:
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