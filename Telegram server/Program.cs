using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.IO;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using System.ComponentModel;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.Internal;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            String line;
            String about = "";
            int i = 0;
            var client = new TelegramBotClient("6497653264:AAFruV6L5WFBy3DudYPt-WyhQgWsXJQlFqY");
            client.StartReceiving(Update, Error);

            StreamReader sr = new StreamReader(@"Textdata4bot.txt");
            line = sr.ReadToEnd();
            sr.Close();

            while (line[i] == '1')
            {
                char[] charStr = about.ToCharArray();
                charStr[i] = line[i];
                about = new string(charStr);
                Console.WriteLine(line[i]);
                i++;
            }
            Console.WriteLine(about);
            Console.ReadLine();

        }




        async static Task Update(ITelegramBotClient botclient, Update update, CancellationToken token)
        {

            var message = update.Message;

            if (message == null || message.Type != MessageType.Text) return;
            string TextMessage = message.Text.ToLower();

            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "😷Определение заболевания😷","💊Поиск лекарств💊"},
                new KeyboardButton[] { "🔖Справка⁉️" },
            })
            {
                ResizeKeyboard = true
            };


            switch (TextMessage)
            {
                case "/start":
                    {
                        await botclient.SendTextMessageAsync(message.Chat.Id, "Привет! Добро пожаловать в MediSick - твоего личного помощника в мире медицины и определения болезней. Я здесь, чтобы помочь тебе разобраться в твоих симптомах и предложить возможные диагнозы. Просто расскажи мне о своих проблемах, и я постараюсь дать тебе наилучшую рекомендацию. Не забывай, что я не заменяю визит к врачу, но могу быть полезным и информативным источником для начала.Начнем?", replyMarkup: replyKeyboardMarkup);
                        break;
                    }
                case "😷определение заболевания😷":
                    {
                        await botclient.SendTextMessageAsync(message.Chat.Id, "Поздравляю,у вас СПИД!", replyMarkup: replyKeyboardMarkup);
                        break;
                    }
                case "💊поиск лекарств💊":
                    {
                        await botclient.SendTextMessageAsync(message.Chat.Id, "Зачем?", replyMarkup: replyKeyboardMarkup);
                        break;
                    }
                case "🔖справка⁉️":
                    {
                        await botclient.SendTextMessageAsync(message.Chat.Id, "MediSick является личным помощником в мире медицины, который доступен для вас в любое время. Он готов ответить на ваши вопросы и предложить рекомендации на основе введенных симптомов.\n   -Определение болезней: Бот обладает базой данных, содержащей информацию о различных заболеваниях и связанных с ними симптомах. Он анализирует введенные вами симптомы и сравнивает их с базой данных, чтобы предложить возможные диагнозы.\n    -Поиск лекарств: Бот позволяет пользователям искать информацию о конкретных лекарствах. Пользователь может ввести название препарата, и бот предоставит подробную информацию о нем, включая инструкцию по применению, побочные эффекты, дозировку и другую важную информацию.\n   -Направление к врачу: Бот всегда подчеркивает, что он не заменяет визит к врачу, но может служить полезным и информативным источником для начальной оценки состояния. Он рекомендует обратиться к специалисту для более точного диагноза и лечения.\n\nС уважением: C.O.C.K. inc.", replyMarkup: replyKeyboardMarkup);
                        break;
                    }
                default: break;
            }












            //Message sentMessage =  await botclient.SendTextMessageAsync(message.Chat.Id, "cock", replyMarkup: replyKeyboardMarkup, cancellationToken: token);


        }

        private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}