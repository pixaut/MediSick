namespace Program
{
    public class Keyboard
    {
        public static ReplyKeyboardMarkup? geolocationkeyboard;
        public static ReplyKeyboardMarkup? organizationkeyboard;
        public static ReplyKeyboardMarkup? drugkeyboard;
        public static ReplyKeyboardMarkup? welcomkeyboard;
        public static ReplyKeyboardMarkup? symptomkeyboard;
        public static InlineKeyboardMarkup? inlineKeyboard;
        public static InlineKeyboardMarkup? inlinegenderkeyboard;
        public static ReplyKeyboardMarkup welcomkeyboarden = new(new[]
        {
            new[]{botworden["textbuttondefinitionofdisease"],KeyboardButton.WithRequestLocation(botworden["searchbyareatext"])},
            new KeyboardButton[] {TelegramBot.botworden["textbuttonreference"]},
        })
        {
            ResizeKeyboard = true
        };
        public static ReplyKeyboardMarkup welcomkeyboardru = new(new[]
        {
            new[]{botwordru["textbuttondefinitionofdisease"],KeyboardButton.WithRequestLocation(botwordru["searchbyareatext"])},
            new KeyboardButton[] {TelegramBot.botwordru["textbuttonreference"]},
        })
        {
            ResizeKeyboard = true,

        };
        public static ReplyKeyboardMarkup symptomkeyboarden = new(new[]
        {
            new KeyboardButton[] {botworden["textbuttonrepeatforecast"]},
            new KeyboardButton[] {botworden["textbuttonbacktomainmenu"]},
        })
        {
            ResizeKeyboard = true
        };
        public static ReplyKeyboardMarkup symptomkeyboardru = new(new[]
        {
            new KeyboardButton[] {botwordru["textbuttonrepeatforecast"]},
            new KeyboardButton[] {botwordru["textbuttonbacktomainmenu"]},
        })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup geolocationkeyboarden = new(new[]
        {
            new KeyboardButton[]
            {
                botworden["organizationsearchtext"],
                botworden["drugssearchtext"],
            },
            new KeyboardButton[]
            {
                botworden["textbuttonbacktomainmenu"],

            }
        })
        {
            ResizeKeyboard = true
        };
        public static ReplyKeyboardMarkup geolocationkeyboardru = new(new[]
        {
            new KeyboardButton[]
            {
                botwordru["organizationsearchtext"],
                botwordru["drugssearchtext"],
            },
            new KeyboardButton[]
            {
                botwordru["textbuttonbacktomainmenu"],
            }
        })
        {
            ResizeKeyboard = true
        };


        public static ReplyKeyboardMarkup organizationkeyboarden = new(new[]
        {
            new KeyboardButton[] {botworden["pharmaciesnearbytext"]},
            new KeyboardButton[] {botworden["clinicsnearbytext"]},
            new KeyboardButton[] {botworden["hospitalsnearbytext"]},
            new KeyboardButton[] {botworden["textbuttonback"]},
            new KeyboardButton[] {botworden["textbuttonbacktomainmenu"]},
        })
        {
            ResizeKeyboard = true
        };
        public static ReplyKeyboardMarkup organizationkeyboardru = new(new[]
        {
            new KeyboardButton[] {botwordru["pharmaciesnearbytext"]},
            new KeyboardButton[] {botwordru["clinicsnearbytext"]},
            new KeyboardButton[] {botwordru["hospitalsnearbytext"]},
            new KeyboardButton[] {botwordru["textbuttonback"]},
            new KeyboardButton[] {botwordru["textbuttonbacktomainmenu"]},
        })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup drugkeyboarden = new(new[]
        {
            new KeyboardButton[] {botworden["textbuttonback"]},
            new KeyboardButton[] {botworden["textbuttonbacktomainmenu"]},
        })
        {
            ResizeKeyboard = true
        };
        public static ReplyKeyboardMarkup drugkeyboardru = new(new[]
        {
            new KeyboardButton[] {botwordru["textbuttonback"]},
            new KeyboardButton[] {botwordru["textbuttonbacktomainmenu"]},
        })
        {
            ResizeKeyboard = true
        };


        public static InlineKeyboardMarkup inlineKeyboardru = new(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botwordru["textskinandhairinline1"], callbackData: "1"),
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botwordru["textrespiratorysysteminline5"], callbackData: "5"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botwordru["texteyesymptomsinline6"], callbackData: "6"),
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botwordru["textrespiratoryinline7"], callbackData: "7"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botwordru["textlimbsinline8"], callbackData: "8"),
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botwordru["textgeneralstateinline9"], callbackData: "9"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botwordru["textcardiovascularsysteminline0"], callbackData: "0"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botwordru["textgastrointestinaltractinline2"], callbackData: "2"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botwordru["textreproductiveandurinarysysteminline3"], callbackData: "3"),
            },

            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botwordru["textrneurologicalinline4"], callbackData: "4"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botwordru["textgetsymptomsinline"], callbackData: "send"),
                InlineKeyboardButton.WithCallbackData(text:TelegramBot.botwordru["textcancelinline"] , callbackData: $"cock{TelegramBot.userid}"),
            },
        });
        public static InlineKeyboardMarkup inlineKeyboarden = new(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botworden["textskinandhairinline1"], callbackData: "1"),
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botworden["textrespiratorysysteminline5"], callbackData: "5"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botworden["texteyesymptomsinline6"], callbackData: "6"),
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botworden["textrespiratoryinline7"], callbackData: "7"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botworden["textlimbsinline8"], callbackData: "8"),
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botworden["textgeneralstateinline9"], callbackData: "9"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botworden["textcardiovascularsysteminline0"], callbackData: "0"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botworden["textgastrointestinaltractinline2"], callbackData: "2"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botworden["textreproductiveandurinarysysteminline3"], callbackData: "3"),
            },

            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botworden["textrneurologicalinline4"], callbackData: "4"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botworden["textgetsymptomsinline"], callbackData: "send"),
                InlineKeyboardButton.WithCallbackData(text:TelegramBot.botworden["textcancelinline"] , callbackData: $"cock{TelegramBot.userid}"),
            },
        });
        public static InlineKeyboardMarkup inlinelinkes = new(
            new[]
        {
            InlineKeyboardButton.WithUrl(text: "Creator",url: TelegramBot.botword["creatorlinklinline"]),
            InlineKeyboardButton.WithUrl(text: "TeamLid",url: TelegramBot.botword["teamlidlinklinline"]),
            InlineKeyboardButton.WithUrl(text: "Helper",url: TelegramBot.botword["helperlinklinline"]),
            InlineKeyboardButton.WithUrl(text: "Helper2",url: TelegramBot.botword["helper2linklinline"]),
            InlineKeyboardButton.WithUrl(text: "GitHub",url: TelegramBot.botword["githublinklinline"])
        });

        public static InlineKeyboardMarkup inlinegenderkeyboardru = new(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botwordru["textmaninline"], callbackData: "man"),
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botwordru["textwomaninline"], callbackData: "woman"),
            }
        });
        public static InlineKeyboardMarkup inlinegenderkeyboarden = new(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botworden["textmaninline"], callbackData: "man"),
                InlineKeyboardButton.WithCallbackData(text: TelegramBot.botworden["textwomaninline"], callbackData: "woman"),
            }
        });
        public static InlineKeyboardMarkup inlinelanguagekeyboard = new(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "🇬🇧English🇺🇲", callbackData: "en"),
                InlineKeyboardButton.WithCallbackData(text: "🇷🇺Русский🇧🇾", callbackData: "ru"),
            }
        });
    }
}