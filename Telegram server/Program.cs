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
        private static string? lastmessage;
        private static bool flagmessage = false;
        private static bool symptommenu = false;
        private static bool mainmenu = true;

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
            var client = new TelegramBotClient("6497653264:AAFruV6L5WFBy3DudYPt-WyhQgWsXJQlFqY");
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }





        async static Task Update(ITelegramBotClient botclient, Update update, CancellationToken token)
        {
            //обработка входных данных
            var message = update.Message;
            string TextMessage = message.Text.ToLower();
            if (message == null || message.Type != MessageType.Text) return;

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
                //await botclient.SendTextMessageAsync(message.Chat.Id, " ", replyMarkup: welcomkeyboard);
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

                            break;
                        }
                    default: break;
                }



                if (TextMessage != "")
                {
                    await botclient.SendTextMessageAsync(message.Chat.Id, "Проверка значений....");
                    if (TextMessage == "cock")
                    {
                        await botclient.SendTextMessageAsync(message.Chat.Id, "Успех!");
                    }
                    else
                    {
                        await botclient.SendTextMessageAsync(message.Chat.Id, "Неправильные данные!");
                    }


                }


            }






            if (lastmessage != TextMessage && flagmessage == false)
            {
                flagmessage = true;
                lastmessage = TextMessage;
            }
            else
            {
                flagmessage = false;
            }
            Console.WriteLine(TextMessage + "  " + lastmessage);

        }

        private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}