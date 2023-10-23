using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Telegram.Bot.Exceptions;
using Npgsql.Replication.PgOutput.Messages;

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
            var client = new TelegramBotClient("1193084625:AAHy5_yuKBsqcllgwSn4JCE3x6yS0UoHycA");
            client.StartReceiving(Update, Error);


            Console.ReadLine();
        }





        async static Task Update(ITelegramBotClient botclient, Update update, CancellationToken token)
        {
            int countinputsymptoms = 1; //количество симптомов
            int air;//заглушка
            int[] symptomsarray = new int[countsymptoms];//введеные симптомы
            string buf = "";//буфер строк
            bool wrongmessage = false;//неправильные данные


            //обработка входных данных
            var message = update.Message;
            if (message == null || message.Type != MessageType.Text) return;
            string TextMessage = message!.Text!.ToLower();

            //обработка входных данных

            //обработка юзеров
            userid = message.Chat.Id;
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
            //отрисовка клавиатур





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


                            await botclient.SendTextMessageAsync(message.Chat.Id, textsymptoms, replyMarkup: symptomkeyboard, disableNotification: true);
                            await botclient.SendTextMessageAsync(message.Chat.Id, textinputformat, parseMode: ParseMode.Html, disableNotification: true);

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