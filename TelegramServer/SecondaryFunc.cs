using Newtonsoft.Json;
using static Program.TelegramBot;
using static Program.Keyboard;
using Telegram.Bot.Types.ReplyMarkups;

namespace Program
{
    class Secondaryfunctions
    {
        public static InlineKeyboardMarkup inlinepreparationdescriptiondiseases()
        {

            InlineKeyboardMarkup inlinedescriptiondiseaseen = new(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: botword["descriptiondisease"]+botword["d"+database[userid].listofrecentdiseases[0]].Substring(3, botword["d"+database[userid].listofrecentdiseases[0]].Length - 7), callbackData: "description1"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: botword["descriptiondisease"]+botword["d"+database[userid].listofrecentdiseases[1]].Substring(3, botword["d"+database[userid].listofrecentdiseases[1]].Length - 7), callbackData: "description2"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: botword["descriptiondisease"]+botword["d"+database[userid].listofrecentdiseases[2]].Substring(3, botword["d"+database[userid].listofrecentdiseases[2]].Length - 7), callbackData: "description3"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: botword["descriptiondisease"]+botword["d"+database[userid].listofrecentdiseases[3]].Substring(3, botword["d"+database[userid].listofrecentdiseases[3]].Length - 7), callbackData: "description4"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: botword["descriptiondisease"]+botword["d"+database[userid].listofrecentdiseases[4]].Substring(3, botword["d"+database[userid].listofrecentdiseases[4]].Length - 7), callbackData: "description5"),
                }
            });
            return inlinedescriptiondiseaseen;
        }

        public static string cantileverstrip(int percent)
        {
            char[] stripfull = new char[] { '░', '░', '░', '░', '░', '░', '░', '░', '░' };
            for (int i = 0; i < (int)percent / 10; ++i)
            {
                stripfull[i] = '█';

            }
            string strip = new string(stripfull);


            return strip;
        }

        public static Dictionary<string, string> BotwordDictpreparer(Dictionary<string, string> botword, string path)
        {
            Textbot? textbot = JsonConvert.DeserializeObject<Textbot>(File.ReadAllText(@path));
            for (int i = 0; i < textbot!.Textforbot!.Length; i++)
            {
                botword.TryAdd(textbot.Textforbot[i].TextName, textbot.Textforbot[i].Text);
            }
            return botword;
        }

        public static Dictionary<long, User> DatabaseDictFillFromJSON(string path)
        {
            Dictionary<long, User>? data = JsonConvert.DeserializeObject<Dictionary<long, User>>(File.ReadAllText(@path));

            return data!;
        }



        public static void DatabaseDictSaverToJSON(Dictionary<long, User> database, string path)
        {

            File.WriteAllText(@path, JsonConvert.SerializeObject(database, Formatting.Indented));
        }

        public static string symptomhandler(List<int> select)
        {
            string symptomsselected = "";

            for (int i = 0; i < select.Count; i++)
            {
                symptomsselected += botword[select[i].ToString()];
                symptomsselected += botword[select[i] + "categoryofdiseases"];
            }
            Console.WriteLine(symptomsselected);
            return symptomsselected;


        }
        public static void interfacelocalization(string language)
        {
            if (language == "ru")
            {
                botword = botwordru;
                welcomkeyboard = welcomkeyboardru;
                symptomkeyboard = symptomkeyboardru;
                inlineKeyboard = inlineKeyboardru;
                inlinegenderkeyboard = inlinegenderkeyboardru;

            }
            else
            {
                botword = botworden;
                welcomkeyboard = welcomkeyboarden;
                symptomkeyboard = symptomkeyboarden;
                inlineKeyboard = inlineKeyboarden;
                inlinegenderkeyboard = inlinegenderkeyboarden;

            }

        }


    }
}