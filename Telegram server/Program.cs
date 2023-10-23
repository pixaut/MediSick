using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{

    class Program
    {

        private static string? reference;
        private static string? welcome;
        private static string? symptoms;
        private static string? lastuserid;
        private static long userid;
        private static bool flagmessage = false;
        private static bool symptommenu = false;
        private static bool mainmenu = true;
        private static Dictionary<long, bool> user = new Dictionary<long, bool>();


        static void Main(string[] args)
        {
            StreamReader sr1 = new StreamReader(@"Telegramassets/reference.txt");
            StreamReader sr2 = new StreamReader(@"Telegramassets/welcome.txt");
            StreamReader sr3 = new StreamReader(@"Telegramassets/symptoms.txt");
            reference = sr1.ReadToEnd();
            welcome = sr2.ReadToEnd();
            symptoms = sr3.ReadToEnd();
            sr1.Close();
            sr2.Close();
            sr3.Close();
            //Console.WriteLine(symptoms);
            var client = new TelegramBotClient("1193084625:AAHy5_yuKBsqcllgwSn4JCE3x6yS0UoHycA");
            client.StartReceiving(Update, Error);


            Console.ReadLine();
        }





        async static Task Update(ITelegramBotClient botclient, Update update, CancellationToken token)
        {
            int countsymptoms = 13; //количество симптомов
            int[] symptomsarray = new int[countsymptoms];
            string buf = "";//буфер строк

            //обработка входных данных
            var message = update.Message;
            string TextMessage = message.Text.ToLower();
            if (message == null || message.Type != MessageType.Text) return;


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


            Console.WriteLine(message.Chat.FirstName + " " + user[userid] + " " + message.Text);
            //отрисовка клавиатур
            ReplyKeyboardMarkup welcomkeyboard = new(new[]
            {
                new KeyboardButton[] { "Определение заболевания","Справка"},
            })
            {
                ResizeKeyboard = true
            };
            ReplyKeyboardMarkup symptomkeyboard = new(new[]
            {
                new KeyboardButton[] { "Назад в главное меню"},
            })
            {
                ResizeKeyboard = true
            };




            if (mainmenu)
            {

                switch (TextMessage)
                {
                    case "/start":
                        {
                            await botclient.SendTextMessageAsync(message.Chat.Id, welcome, replyMarkup: welcomkeyboard);
                            await botclient.SendTextMessageAsync(message.Chat.Id, "Что вам нужно?Выбирайте:", replyMarkup: welcomkeyboard);
                            break;
                        }
                    case "определение заболевания":
                        {
                            mainmenu = false;
                            symptommenu = true;
                            user[userid] = false;
                            await botclient.SendTextMessageAsync(message.Chat.Id, "Вам предстоит выбрать подходящие симптопы для определения заболевания:", replyMarkup: symptomkeyboard);
                            await botclient.SendTextMessageAsync(message.Chat.Id, symptoms);
                            TextMessage = "";
                            break;
                        }

                    case "справка":
                        {
                            await botclient.SendTextMessageAsync(message.Chat.Id, reference, replyMarkup: welcomkeyboard);
                            break;
                        }
                    default: break;
                }
            }
            if (symptommenu)
            {

                switch (TextMessage)
                {
                    case "назад в главное меню":
                        {
                            await botclient.SendTextMessageAsync(message.Chat.Id, "Что вам нужно?Выбирайте:", replyMarkup: welcomkeyboard);
                            mainmenu = true;
                            symptommenu = false;
                            user[userid] = true;

                            break;
                        }
                    default: break;
                }



                if (TextMessage != "" && mainmenu == false)
                {
                    await botclient.SendTextMessageAsync(message.Chat.Id, "Проверка значений....");
                    for (int i = 0, j = 0; i < TextMessage.Length; i++)
                    {


                        if (TextMessage[i] == ' ')
                        {
                            Console.WriteLine(i + " " + TextMessage.Length);
                            symptomsarray[j] = Int32.Parse(buf);
                            j++;
                            buf = "";
                        }
                        else buf += TextMessage[i];
                    }
                    symptomsarray[^1] += Int32.Parse(buf);
                    Array.Sort(symptomsarray);

                    for (int i = 0; i < countsymptoms; i++)
                    {
                        if (symptomsarray[i] == symptomsarray[i + 1] && symptomsarray[i] != 0)
                        {
                            Console.WriteLine("Неправильные данные!Перепешите пожалуйста!");
                            await botclient.SendTextMessageAsync(message.Chat.Id, "Неправильные данные!Перепешите пожалуйста!");

                            return;
                        }
                        Console.WriteLine("1");
                    }
                    await botclient.SendTextMessageAsync(message.Chat.Id, "Успех!");



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

        private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}