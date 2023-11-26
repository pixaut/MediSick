global using static Program.TelegramBot;
global using static Program.Secondaryfunctions;
global using static Program.Keyboard;
global using static Program.ResponseFromYandexMaps;
global using static Program.DetermineAdressYandexMaps;
global using static Program.DrugsParser;
global using static Program.YandexMapParser;

global using Telegram.Bot;
global using Telegram.Bot.Types.Enums;
global using Telegram.Bot.Exceptions;
global using Telegram.Bot.Types.ReplyMarkups;
global using Telegram.Bot.Types;

global using System.Net;
global using System.Text;
global using System.Diagnostics;

global using HtmlAgilityPack;
global using Newtonsoft.Json;


namespace Program
{
    //Main server MediSick TeleBot:
    class TelegramBot
    {
        //Global variables:
        public static Dictionary<string, string> botwordru = new Dictionary<string, string>();
        public static Dictionary<string, string> botworden = new Dictionary<string, string>();
        public static Dictionary<string, string> botword = new Dictionary<string, string>();
        public static Dictionary<long, User> database = new Dictionary<long, User>();
        public static Settings? settings = JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText(@"Telegramassets/Botsettings.json"));
        public static long userid = 0;
        public static bool serveronline = false;

        //Launching a telegram bot:
        static void Main()
        {
            //Data collection from JSON:
            botwordru = BotwordDictpreparer(botwordru, settings!.pathtextforbotjsonru);
            botworden = BotwordDictpreparer(botworden, settings!.pathtextforbotjsonen);
            database = DatabaseDictFillFromJSON(settings!.pathdatabasejson);

            //Launching a telegram bot:
            var client = new TelegramBotClient(settings!.token!);
            client.StartReceiving(Update, Error);

            //Protection against program closure:
            Console.ReadLine();
        }


        //Processing all incoming requests:
        async static Task Update(ITelegramBotClient botclient, Update update, CancellationToken token)
        {
            //Precaution first start:
            if (!serveronline)
            {
                serveronline = true;
                return;
            }

            //Declaring important variables:
            var message = update.Message;
            var callback = update.CallbackQuery;
            var messagetype = update.Type;

            //Validation of incoming values:
            if (update.Type != UpdateType.Message && update.Type != UpdateType.CallbackQuery) return;
            if (update.Type == UpdateType.CallbackQuery) userid = callback!.Message!.Chat!.Id;
            if (update.Type == UpdateType.Message) userid = message!.Chat!.Id!;

            //Creating a user object and processing new users:
            User user = new User();
            if (database.ContainsKey(userid) == false && update.Type == UpdateType.Message)
            {
                Console.WriteLine("New user:   " + message!.Chat.FirstName);
                database.TryAdd(userid, user);
                database[userid].name = message!.Chat!.FirstName!;
                DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
            }

            //Interfacelocalization:
            interfacelocalization(database[userid].language!);

            //Inline buttons processing:
            if (messagetype == UpdateType.CallbackQuery)
            {
                //Language select and hello message processing:
                if (database[userid].language == null && database[userid].lastmessage == "/start")
                {
                    await botclient.DeleteMessageAsync(userid, callback!.Message!.MessageId, cancellationToken: token);
                    await botclient.AnswerCallbackQueryAsync(callback!.Id, callback!.Data, cancellationToken: token);
                    if (callback!.Data == "en")
                    {
                        await botclient.SendTextMessageAsync(userid, "You pick english language. 🇬🇧", parseMode: ParseMode.Html, cancellationToken: token);
                        database[userid].language = "en";
                    }
                    else if (callback!.Data == "ru")
                    {
                        await botclient.SendTextMessageAsync(userid, "Вы выбрали русский язык. 🇷🇺", parseMode: ParseMode.Html, cancellationToken: token);
                        database[userid].language = "ru";
                    }
                    interfacelocalization(database[userid].language!);
                    database[userid].mainmenu = true;
                    await botclient.SendStickerAsync(userid, sticker: InputFile.FromUri(botword["hallostik"]), cancellationToken: token);
                    await botclient.SendTextMessageAsync(userid, database[userid].name + " " + botword["textwelcome"], parseMode: ParseMode.Html, cancellationToken: token);
                    await botclient.SendTextMessageAsync(userid, botword["textchoicegender"], parseMode: ParseMode.Html, replyMarkup: inlinegenderkeyboard, cancellationToken: token);
                    DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
                    return;
                }
                if (database[userid].language != null)
                {
                    //Gender select processing:
                    if (database[userid].gender == null && database[userid].mainmenu)
                    {
                        await botclient.DeleteMessageAsync(userid, callback!.Message!.MessageId, cancellationToken: token);
                        await botclient.AnswerCallbackQueryAsync(callback!.Id, callback!.Data, cancellationToken: token);
                        if (callback!.Data == "man" && database[userid].gender == null)
                        {
                            await botclient.SendTextMessageAsync(userid, botword["textman"], replyMarkup: welcomkeyboard, parseMode: ParseMode.Html, cancellationToken: token);
                            database[userid].gender = "man";
                        }
                        else if (callback!.Data == "woman" && database[userid].gender == null)
                        {
                            await botclient.SendTextMessageAsync(userid, botword["textwoman"], replyMarkup: welcomkeyboard, parseMode: ParseMode.Html, cancellationToken: token);
                            database[userid].gender = "woman";
                        }
                        DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
                        return;
                    }
                    //Subcategory processing:
                    if (database[userid].symptommenu && database[userid].inlinesymptomkey)
                    {
                        await botclient.AnswerCallbackQueryAsync(callback!.Id, "picked", cancellationToken: token);
                        if (int.TryParse(callback!.Data, out _) && !database[userid]!.inlinebuttpressed!.Contains(int.Parse(callback!.Data)))
                        {
                            database[userid]!.inlinebuttpressed!.Add(int.Parse(callback?.Data ?? ""));
                        }
                        else if (callback!.Data == "send" && database[userid]!.inlinebuttpressed!.Count != 0)
                        {
                            await botclient.DeleteMessageAsync(userid, callback!.Message!.MessageId, cancellationToken: token);
                            database[userid]!.inlinebuttpressed!.Sort();
                            await botclient.SendTextMessageAsync(userid, symptomhandler(database[userid]!.inlinebuttpressed!), parseMode: ParseMode.Html, cancellationToken: token);
                            await botclient.SendAnimationAsync(userid, animation: InputFile.FromUri(settings!.linkinstructionsforenteringsymptoms), caption: botword["sampleinputtext"], cancellationToken: token);
                            database[userid]!.inlinebuttpressed!.Clear();
                            database[userid].inlinesymptomkey = false;
                        }
                        else if (callback!.Data == "cancel" && database[userid]!.inlinebuttpressed!.Count != 0)
                        {
                            database[userid]!.inlinebuttpressed!.Clear();
                        }
                        DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
                        return;
                    }
                    //Description processing:
                    if (database[userid].symptommenu && callback!.Data!.Length > 11 && callback!.Data!.Substring(0, 11) == "description")
                    {
                        await botclient.AnswerCallbackQueryAsync(callback!.Id, callback!.Data, cancellationToken: token);
                        await botclient.SendTextMessageAsync
                        (
                            userid,
                            text: botword
                            [
                                "d" + database[userid]!.listofrecentdiseases![int.Parse(callback!.Data.Substring(11)) - 1]]!.Substring(3, botword["d" + database[userid]!.listofrecentdiseases![int.Parse(callback!.Data.Substring(11)) - 1]]!.Length - 7)! +
                                " - " + botword["textdescriptiondisease" +
                                database![userid]!.listofrecentdiseases![int.Parse(callback!.Data.Substring(11)) - 1]]!,
                            parseMode: ParseMode.Markdown,
                            cancellationToken: token
                        );
                        interfacelocalization(database[userid].language!);
                        DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
                        return;
                    }
                    //Geolocation processing:
                    if (database[userid].searchbyareamenu && callback!.Data!.Length > 11 && callback!.Data!.Substring(0, 11) == "geolocation")
                    {
                        await botclient.AnswerCallbackQueryAsync(callback!.Id, callback!.Data, cancellationToken: token);
                        await botclient.SendVenueAsync
                        (
                            chatId: userid,
                            latitude: database[userid].listofrecentsearchedplaces![int.Parse(callback!.Data.Substring(11))].Item1,
                            longitude: database[userid].listofrecentsearchedplaces![int.Parse(callback!.Data.Substring(11))].Item2,
                            title: database[userid].listofrecentsearchedplaces![int.Parse(callback!.Data.Substring(11))].Item3,
                            address: database[userid].listofrecentsearchedplaces![int.Parse(callback!.Data.Substring(11))].Item4,
                            cancellationToken: token
                        );
                        DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
                        return;
                    }
                    //Search drugs in current city inline processing:
                    if (database[userid].searchdrugmenu && callback!.Data!.Substring(0, 4) == "drag")
                    {
                        await botclient.AnswerCallbackQueryAsync(callback!.Id, callback!.Data, cancellationToken: token);
                        string outsting = "";
                        int.TryParse(string.Join("", callback!.Data!.Where(c => char.IsDigit(c))), out int index);
                        --index;
                        await parsedrugsincity(database[userid].lastdrugslist[index].Link);
                        outsting += $"{botword["productnametext"]} {database[userid].lastdrugslist[index].Drugname}\n{botword["longlinetext"]}";
                        for (int i = 0; i < database[userid].lastpharmlist.Count; ++i)
                        {
                            outsting += $"{botword["pharmacynametext"]} {database[userid].lastpharmlist[i].Pharmname}\n{botword["adresstext"]} {database[userid].lastpharmlist[i].Address}\n{botword["numbertext"]} {database[userid].lastpharmlist[i].PhoneNumber}\n{botword["costtext"]} {database[userid].lastpharmlist[i].Cost}\n{botword["longlinetext"]}";
                        }
                        await botclient.SendTextMessageAsync
                        (
                            userid,
                            text: outsting,
                            parseMode: ParseMode.Markdown,
                            cancellationToken: token
                        );
                        return;
                    }
                }
            }

            //Interfacelocalization:
            interfacelocalization(database[userid].language!);

            //Geolocation transition processing:
            if (message != null && message.Type == MessageType.Location)
            {
                database[userid].searchbyareamenu = true;
                database[userid].mainmenu = false;
                database[userid].searchorganizationmenu = false;
                database[userid].searchdrugmenu = false;
                database[userid].geolocation = (update.Message!.Location!.Latitude, update.Message.Location.Longitude);
                determineaddress(database[userid].geolocation);
                await botclient.SendTextMessageAsync(message.Chat.Id, $"{botword["searchbyareastarttext"]}\n{botword["yourcurrentlocationtext"]}\n{botword["citytext"]} 📍{database[userid].city}📍", replyMarkup: geolocationkeyboard, disableNotification: true, cancellationToken: token);
                DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
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
                database.Remove(userid);
                if (database.ContainsKey(userid) == false && update.Type == UpdateType.Message)
                {
                    Console.WriteLine("Clear database for current user:   " + message!.Chat.FirstName);
                    database.TryAdd(userid, user);
                    database[userid].name = message!.Chat!.FirstName!;
                    database[userid].lastmessage = "/start";
                    DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
                }
                await botclient.SendTextMessageAsync(userid, text: "Let's start....", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: token);
                await botclient.SendTextMessageAsync(userid, botword["textchoicelanguage"], parseMode: ParseMode.Html, replyMarkup: inlinelanguagekeyboard, cancellationToken: token);
            }
            if (database[userid].language == null || database[userid].gender == null)
            {
                database[userid].mainmenu = false;
                database[userid].symptommenu = false;
                database[userid].searchbyareamenu = false;
                database[userid].searchorganizationmenu = false;
                database[userid].searchdrugmenu = false;
                return;
            }


            //Processing  main menu buttons:
            if (database[userid].mainmenu && !database[userid].symptommenu && !database[userid].inlinesymptomkey)
            {
                if (TextMessage == botword["textbuttondefinitionofdisease"].ToLower())
                {
                    database[userid].mainmenu = false;
                    database[userid].symptommenu = true;
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["sympmtomstarttext"], replyMarkup: symptomkeyboard, disableNotification: true, cancellationToken: token);
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["textinputformat2"], replyMarkup: inlineKeyboard, parseMode: ParseMode.Html, disableNotification: true, cancellationToken: token);

                    database[userid].inlinesymptomkey = true;
                    database[userid]!.inlinebuttpressed!.Clear();
                    TextMessage = "";
                }
                else if (TextMessage == botword["textinstruction"].ToLower())
                {
                    await botclient.SendVideoAsync
                    (
                        chatId: userid,
                        video: InputFile.FromUri(settings.linkinstructionsforbot),
                        supportsStreaming: true,
                        cancellationToken: token
                    );
                }
                else if (TextMessage == botword["textbuttonreference"].ToLower())
                {
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["textreference"], replyMarkup: inlinelinkes, disableNotification: true, cancellationToken: token);
                    await botclient.SendStickerAsync(message.Chat.Id, sticker: InputFile.FromUri(botword["refstik"]), cancellationToken: token);
                }
                else return;
            }

            //Processing geolocation buttons:
            if (database[userid].searchbyareamenu && !database[userid].mainmenu)
            {
                //Keyboard buttons procesing:
                interfacelocalization(database[userid].language!);
                if (TextMessage == botword["textbuttonbacktomainmenu"].ToLower())
                {
                    await botclient.SendTextMessageAsync(message.Chat.Id, database[userid].name + " " + botword["textwelcome2"], parseMode: ParseMode.Html, replyMarkup: welcomkeyboard, disableNotification: true, cancellationToken: token);
                    database[userid].mainmenu = true;
                    database[userid].searchbyareamenu = false;
                    database[userid].searchorganizationmenu = false;
                    database[userid].searchdrugmenu = false;
                }
                if (TextMessage == botword["organizationsearchtext"].ToLower())
                {
                    database[userid].searchorganizationmenu = true;
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["organizationsearchwelcometext"], parseMode: ParseMode.Html, replyMarkup: organizationkeyboard, disableNotification: true, cancellationToken: token);
                }
                if (TextMessage == botword["drugssearchtext"].ToLower())
                {
                    database[userid].searchdrugmenu = true;
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["searchdrugmenuwelcometext"], parseMode: ParseMode.Html, replyMarkup: drugkeyboard, disableNotification: true, cancellationToken: token);
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["enternameofmedicationtext"], parseMode: ParseMode.Html, disableNotification: true, cancellationToken: token);
                    database[userid].lastmessage = "";
                }

                //Search organizations processing:
                if (database[userid].searchbyareamenu && database[userid].searchorganizationmenu)
                {
                    if (TextMessage == botword["textbuttonback"].ToLower())
                    {
                        await botclient.SendTextMessageAsync(message.Chat.Id, botword["youcomebacktext"], parseMode: ParseMode.Html, replyMarkup: geolocationkeyboard, disableNotification: true, cancellationToken: token);
                        database[userid].searchorganizationmenu = false;
                        return;
                    }
                    if (TextMessage == botword["pharmaciesnearbytext"].ToLower())
                    {
                        await botclient.SendTextMessageAsync(userid, searchorganizations(botword["pharmacytext"], database[userid].geolocation), replyMarkup: inlinepreparationroutebuttons(database[userid].listofrecentsearchedplaces), parseMode: ParseMode.Html, disableWebPagePreview: true, cancellationToken: token);
                    }
                    if (TextMessage == botword["clinicsnearbytext"].ToLower())
                    {
                        await botclient.SendTextMessageAsync(userid, searchorganizations(botword["polyclinictext"], database[userid].geolocation), replyMarkup: inlinepreparationroutebuttons(database[userid].listofrecentsearchedplaces), parseMode: ParseMode.Html, disableWebPagePreview: true, cancellationToken: token);
                    }
                    if (TextMessage == botword["hospitalsnearbytext"].ToLower())
                    {
                        await botclient.SendTextMessageAsync(userid, searchorganizations(botword["hospitaltext"], database[userid].geolocation), replyMarkup: inlinepreparationroutebuttons(database[userid].listofrecentsearchedplaces), parseMode: ParseMode.Html, disableWebPagePreview: true, cancellationToken: token);
                    }
                }

                //Search drugs in city processing:
                if (database[userid].searchbyareamenu && database[userid].searchdrugmenu)
                {
                    if (TextMessage == botword["textbuttonback"].ToLower())
                    {
                        await botclient.SendTextMessageAsync(message.Chat.Id, botword["youcomebacktext"], parseMode: ParseMode.Html, replyMarkup: geolocationkeyboard, disableNotification: true, cancellationToken: token);
                        database[userid].searchdrugmenu = false;
                        return;
                    }
                    string buffstring = "";
                    if (database[userid].lastmessage != "")
                    {
                        await parsedrugslist(TextMessage, await returnregionindex(database[userid]!.city!));
                        if (database[userid].lastdrugslist.Count > 1)
                        {
                            buffstring += botword["longlinetext"];
                            for (int i = 0; i < database[userid].lastdrugslist.Count; ++i)
                            {
                                buffstring += $"{botword["nametext"]} {database[userid].lastdrugslist[i].Drugname}\n\n{botword["formtext"]} {database[userid].lastdrugslist[i].Drugform}\n\n{botword["manufacturertext"]} {database[userid].lastdrugslist[i].Drugproducer}\n\n{botword["costtext"]} {database[userid].lastdrugslist[i].Drugprice}\n\n{botword["intext"]} {database[userid].lastdrugslist[i].Numberofpharmacies} {botword["pharmaciestext"]}\n{botword["longlinetext"]}";
                            }
                            await botclient.SendTextMessageAsync(message.Chat.Id, buffstring, replyMarkup: inlinepreparationdraginsitybuttons(), parseMode: ParseMode.Html, disableNotification: true, cancellationToken: token);
                        }
                        else await botclient.SendTextMessageAsync(message.Chat.Id, botword["notfoundtext"], parseMode: ParseMode.Html, disableNotification: true, cancellationToken: token);
                    }
                }
            }

            //Disease detection part:
            if (!database[userid].mainmenu && database[userid].symptommenu)
            {
                //Checking symptom menu buttons:
                if (TextMessage == botword["textbuttonbacktomainmenu"].ToLower())
                {
                    await botclient.SendTextMessageAsync(message.Chat.Id, database[userid].name + " " + botword["textwelcome2"], parseMode: ParseMode.Html, replyMarkup: welcomkeyboard, disableNotification: true, cancellationToken: token);
                    database[userid].mainmenu = true;
                    database[userid].symptommenu = false;
                    database[userid].inlinesymptomkey = false;
                    DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
                }
                else if (TextMessage == botword["textbuttonrepeatforecast"].ToLower())
                {
                    database[userid].mainmenu = false;
                    database[userid].symptommenu = true;
                    database[userid].inlinesymptomkey = true;
                    await botclient.SendTextMessageAsync(message.Chat.Id, botword["textinputformat2"], replyMarkup: inlineKeyboard, parseMode: ParseMode.Html, disableNotification: true, cancellationToken: token);
                    database[userid]!.inlinebuttpressed!.Clear();
                    return;
                }

                //Processing input symptoms and sending the neural network with output:
                if (!database[userid].mainmenu && !database[userid].inlinesymptomkey && database[userid].symptommenu)
                {
                    string bufs = "";
                    long bufi = 0;
                    int countinputsymptoms = 0;
                    string symptomslist = "";

                    TextMessage += " ";
                    for (int i = 0; i < TextMessage.Length; ++i)
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
                            ++countinputsymptoms;
                        }
                    }

                    await botclient.SendStickerAsync(message.Chat.Id, sticker: InputFile.FromUri(botword["waitstik"]), cancellationToken: token);
                    await botclient.SendTextMessageAsync(message.Chat.Id, "<b>" + botword["textcorrectinput"] + "</b>", parseMode: ParseMode.Html, replyToMessageId: message.MessageId, cancellationToken: token);

                    using (FileStream fs = new FileStream(settings.pathinputuser, FileMode.Truncate))
                    {
                        byte[] inpiutfile = Encoding.ASCII.GetBytes(database[userid].gender![0] + " " + countinputsymptoms + " " + symptomslist);
                        fs.Write(inpiutfile, 0, inpiutfile.Length);
                        fs.Close();
                    }
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = @settings.pathnetworkexe;
                        process.StartInfo.WorkingDirectory = @settings.pathnetworkexeworkingdirectory;
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
                        for (int i = 0, j = 1; i < disandperc.Count; i += 2, ++j)
                        {

                            diagnosis += j + "." + botword["d" + disandperc[i]] + $" ▼\n {botword["withprobability"]}";

                            diagnosis += "│" + cantileverstrip(disandperc[i + 1]) + "│ " + disandperc[i + 1] + " %\n\n";

                            database[userid]!.listofrecentdiseases!.Add(disandperc[i]);

                        }

                        diagnosis += botword["textdiseasewarning"];
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