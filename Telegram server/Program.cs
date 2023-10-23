using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TelegramBotClient("6497653264:AAFruV6L5WFBy3DudYPt-WyhQgWsXJQlFqY");
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

        async static Task Update(ITelegramBotClient botclient, Update update, CancellationToken token)
        {
            var message = update.Message;
            if (message.Text is not null)
            {
                if (message.Text.ToLower().Contains("hallo"))
                {
                    await botclient.SendTextMessageAsync(message.Chat.Id, "hi");
                    return;
                }

            }


        }

        private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}