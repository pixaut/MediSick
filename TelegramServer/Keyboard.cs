namespace Program
{
    //Initialization keyboards for TeleBot:
    public class Keyboard
    {
        //Keyboards for localization function:
        public static ReplyKeyboardMarkup? geolocationkeyboard;
        public static ReplyKeyboardMarkup? organizationkeyboard;
        public static ReplyKeyboardMarkup? drugkeyboard;
        public static ReplyKeyboardMarkup? welcomkeyboard;
        public static ReplyKeyboardMarkup? symptomkeyboard;
        public static InlineKeyboardMarkup? inlineKeyboard;
        public static InlineKeyboardMarkup? inlinegenderkeyboard;


        //Main menu replymarkup keyboard on en|ru language:
        public static ReplyKeyboardMarkup welcomkeyboarden = new(new[]
        {
            new[]{botworden["textbuttondefinitionofdisease"],KeyboardButton.WithRequestLocation(botworden["searchbyareatext"])},
            new KeyboardButton[] { botworden["textinstruction"],botworden["textbuttonreference"]},
        })
        {
            ResizeKeyboard = true
        };
        public static ReplyKeyboardMarkup welcomkeyboardru = new(new[]
        {
            new[]{botwordru["textbuttondefinitionofdisease"],KeyboardButton.WithRequestLocation(botwordru["searchbyareatext"])},
            new KeyboardButton[] { botwordru["textinstruction"], botwordru["textbuttonreference"]},
        })
        {
            ResizeKeyboard = true,
        };

        //Diagnosis detect menu replymarkup keyboard on en|ru language:
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

        //Geolocation main menu replymarkup keyboard on en|ru language:
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

        //Geolocation search organizations menu replymarkup keyboard on en|ru language:
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

        //Geolocation drugs in current city menu replymarkup keyboard on en|ru language:
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

        //Selecting subcategories inline keyboard on en|ru language:
        public static InlineKeyboardMarkup inlineKeyboardru = new(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botwordru["textskinandhairinline1"], callbackData: "1"),
                InlineKeyboardButton.WithCallbackData(text: botwordru["textrespiratorysysteminline5"], callbackData: "5"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botwordru["texteyesymptomsinline6"], callbackData: "6"),
                InlineKeyboardButton.WithCallbackData(text: botwordru["textrespiratoryinline7"], callbackData: "7"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botwordru["textlimbsinline8"], callbackData: "8"),
                InlineKeyboardButton.WithCallbackData(text: botwordru["textgeneralstateinline9"], callbackData: "9"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botwordru["textcardiovascularsysteminline0"], callbackData: "0"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botwordru["textgastrointestinaltractinline2"], callbackData: "2"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botwordru["textreproductiveandurinarysysteminline3"], callbackData: "3"),
            },

            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botwordru["textrneurologicalinline4"], callbackData: "4"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botwordru["textgetsymptomsinline"], callbackData: "send"),
                InlineKeyboardButton.WithCallbackData(text: botwordru["textcancelinline"] , callbackData:"cancel"),
            },
        });
        public static InlineKeyboardMarkup inlineKeyboarden = new(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botworden["textskinandhairinline1"], callbackData: "1"),
                InlineKeyboardButton.WithCallbackData(text: botworden["textrespiratorysysteminline5"], callbackData: "5"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botworden["texteyesymptomsinline6"], callbackData: "6"),
                InlineKeyboardButton.WithCallbackData(text: botworden["textrespiratoryinline7"], callbackData: "7"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botworden["textlimbsinline8"], callbackData: "8"),
                InlineKeyboardButton.WithCallbackData(text: botworden["textgeneralstateinline9"], callbackData: "9"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botworden["textcardiovascularsysteminline0"], callbackData: "0"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botworden["textgastrointestinaltractinline2"], callbackData: "2"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botworden["textreproductiveandurinarysysteminline3"], callbackData: "3"),
            },

            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botworden["textrneurologicalinline4"], callbackData: "4"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botworden["textgetsymptomsinline"], callbackData: "send"),
                InlineKeyboardButton.WithCallbackData(text:botworden["textcancelinline"] , callbackData: "cancel"),
            },
        });

        //Help links inline keyboard on en language:
        public static InlineKeyboardMarkup inlinelinkes = new(new[]
        {
            InlineKeyboardButton.WithUrl(text: "Creator",url: botword["creatorlinklinline"]),
            InlineKeyboardButton.WithUrl(text: "TeamLid",url: botword["teamlidlinklinline"]),
            InlineKeyboardButton.WithUrl(text: "Helper",url: botword["helperlinklinline"]),
            InlineKeyboardButton.WithUrl(text: "GitHub",url: botword["githublinklinline"])
        });

        //Gender select inline keyboard on en|ru language:
        public static InlineKeyboardMarkup inlinegenderkeyboardru = new(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: botwordru["textmaninline"], callbackData: "man"),
                InlineKeyboardButton.WithCallbackData(text: botwordru["textwomaninline"], callbackData: "woman"),
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

        //Language select inline keyboard:
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