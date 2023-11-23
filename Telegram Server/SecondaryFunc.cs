namespace Program
{
    class Secondaryfunctions
    {
        public static async Task<int> returnregionindex(string city)
        {
            int region = 0;//all regions
            string path = "Telegramassets/regionindex.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.Substring(0, line.IndexOf('-')) == city) region = int.Parse(line.Substring(line.IndexOf('-') + 1));
                }
            }
            return region;
        }
        //Dynamic keyboard for search organizations:
        public static InlineKeyboardMarkup inlinepreparationroutebuttons(List<(float, float, string, string)>? listofrecentsearchedplaces)
        {

            List<InlineKeyboardButton[]> list = new List<InlineKeyboardButton[]>();

            for (int i = 0; i < listofrecentsearchedplaces!.Count()!; ++i)
            {
                InlineKeyboardButton button = new InlineKeyboardButton(listofrecentsearchedplaces![i].Item3) { CallbackData = "geolocation" + i };
                InlineKeyboardButton[] row = new InlineKeyboardButton[1] { button };
                list.Add(row);
            }
            var recentsearchedplaceskeyboard = new InlineKeyboardMarkup(list);
            return recentsearchedplaceskeyboard;
        }

        //Dynamic keyboard for description diseases:
        public static InlineKeyboardMarkup inlinepreparationdescriptiondiseases()
        {
            List<InlineKeyboardButton[]> list = new List<InlineKeyboardButton[]>();
            for (int i = 1; i <= 5; ++i)
            {
                InlineKeyboardButton button = new InlineKeyboardButton(botword["descriptiondisease"] + botword["d" + database[userid].listofrecentdiseases![i - 1]].Substring(3, botword["d" + database[userid].listofrecentdiseases![i - 1]].Length - 7)) { CallbackData = "description" + i };
                InlineKeyboardButton[] row = new InlineKeyboardButton[1] { button };
                list.Add(row);
            }
            var inlinedescriptiondiseaseen = new InlineKeyboardMarkup(list);
            return inlinedescriptiondiseaseen;
        }

        //Strip string generator:
        public static string cantileverstrip(int percent)
        {
            char[] stripfull = new char[] { '░', '░', '░', '░', '░', '░', '░', '░', '░' };
            for (int i = 0; i < percent / 10; ++i)
            {
                stripfull[i] = '█';
            }
            return new string(stripfull);
        }

        //Loading words from JSON:
        public static Dictionary<string, string> BotwordDictpreparer(Dictionary<string, string> botword, string path)
        {
            Textbot? textbot = JsonConvert.DeserializeObject<Textbot>(File.ReadAllText(@path));
            for (int i = 0; i < textbot!.Textforbot!.Length; ++i)
            {
                botword.TryAdd(textbot.Textforbot[i].TextName, textbot.Textforbot[i].Text);
            }
            return botword;
        }

        //Returning data from JSON:
        public static Dictionary<long, User> DatabaseDictFillFromJSON(string path)
        {
            Dictionary<long, User>? data = JsonConvert.DeserializeObject<Dictionary<long, User>>(File.ReadAllText(@path));

            return data!;
        }

        //Saving data to JSON:
        public static void DatabaseDictSaverToJSON(Dictionary<long, User> database, string path)
        {
            File.WriteAllText(@path, JsonConvert.SerializeObject(database, Formatting.Indented));
        }

        //Return a list with selected symptoms:
        public static string symptomhandler(List<int> select)
        {
            string symptomsselected = "";
            for (int i = 0; i < select.Count; ++i)
            {
                symptomsselected += botword[select[i].ToString()];
                symptomsselected += botword[select[i] + "categoryofdiseases"];
            }
            return symptomsselected;
        }

        //Interface localization for selected language:
        public static void interfacelocalization(string language)
        {
            if (language == "ru")
            {
                botword = botwordru;
                welcomkeyboard = welcomkeyboardru;
                symptomkeyboard = symptomkeyboardru;
                inlineKeyboard = inlineKeyboardru;
                inlinegenderkeyboard = inlinegenderkeyboardru;
                geolocationkeyboard = geolocationkeyboardru;
                organizationkeyboard = organizationkeyboardru;
                drugkeyboard = drugkeyboardru;
            }
            else
            {
                botword = botworden;
                welcomkeyboard = welcomkeyboarden;
                symptomkeyboard = symptomkeyboarden;
                inlineKeyboard = inlineKeyboarden;
                inlinegenderkeyboard = inlinegenderkeyboarden;
                geolocationkeyboard = geolocationkeyboarden;
                organizationkeyboard = organizationkeyboarden;
                drugkeyboard = drugkeyboarden;
            }

        }


    }
}