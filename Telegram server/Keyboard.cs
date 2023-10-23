using Telegram.Bot.Types.ReplyMarkups;

namespace Program
{
    public class Keyboard
    {
        public static ReplyKeyboardMarkup welcomkeyboard = new(new[]
        {
                new KeyboardButton[] {TelegramBot.botword["textbuttondefinitionofdisease"],TelegramBot.botword["textbuttonreference"]},
        })
        {
            ResizeKeyboard = true
        };
        public static ReplyKeyboardMarkup symptomkeyboard = new(new[]
        {
                new KeyboardButton[] {TelegramBot.botword["textbuttonbacktomainmenu"]},
        })
        {
            ResizeKeyboard = true
        };
        public static InlineKeyboardMarkup inlineKeyboard = new(new[]
        {
                // first row
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textsosinline"], callbackData: "0"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textheadinline"], callbackData: "1"),
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textnoseinline"], callbackData: "2"),
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["texthearinline"], callbackData: "3"),
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["texteyesinline"], callbackData: "4"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textmouthinline"], callbackData: "5"),
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textchestinline"], callbackData: "6"),
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textbackinline"], callbackData: "7"),
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textheartinline"], callbackData: "8"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textkidneysinline"], callbackData: "9"),
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textliverinline"], callbackData: "10"),
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textlungsinline"], callbackData: "11"),
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textskininline"], callbackData: "12"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textlegsinline"], callbackData: "13"),
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textstomachinline"], callbackData: "14"),
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textarmsinline"], callbackData: "15"),
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textgenitourinarysysteminline"], callbackData: "16"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textgetsymptomsinline"], callbackData: "send"),
                    InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textcancelinline"], callbackData: "cancel"),
                },
            });
        public static InlineKeyboardMarkup inlinelinkes = new(new[]
        {
            InlineKeyboardButton.WithUrl(text: "Creator",url: TelegramBot.botword["creatorlinklinline"]),
            InlineKeyboardButton.WithUrl(text: "TeamLid",url: TelegramBot.botword["teamlidlinklinline"]),
            InlineKeyboardButton.WithUrl(text: "Helper",url: TelegramBot.botword["helperlinklinline"]),
            InlineKeyboardButton.WithUrl(text: "Helper2",url: TelegramBot.botword["helper2linklinline"]),
            InlineKeyboardButton.WithUrl(text: "GitHub",url: TelegramBot.botword["githublinklinline"])
        });

    }
}