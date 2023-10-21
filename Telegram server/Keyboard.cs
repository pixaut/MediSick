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
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textskinandhairinline1"], callbackData: "1"),
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textrespiratorysysteminline5"], callbackData: "5"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["texteyesymptomsinline6"], callbackData: "6"),
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textrespiratoryinline7"], callbackData: "7"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textlimbsinline8"], callbackData: "8"),
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textgeneralstateinline9"], callbackData: "9"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textcardiovascularsysteminline0"], callbackData: "0"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textgastrointestinaltractinline2"], callbackData: "2"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textreproductiveandurinarysysteminline3"], callbackData: "3"),
            },

            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botword["textrneurologicalinline4"], callbackData: "4"),
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