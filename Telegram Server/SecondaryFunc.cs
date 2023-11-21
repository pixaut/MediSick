using Newtonsoft.Json;
using static Program.TelegramBot;
using static Program.Keyboard;
using Telegram.Bot.Types.ReplyMarkups;
using System.Net;
using static Program.ResponseFromYandexMaps;

namespace Program
{
    class Secondaryfunctions
    {
        //Searchorganizations YandexMap function:
        public static string searchorganizations(string organization, (double, double) coordinates)
        {
            string buff = "";
            
            double bias = settings!.kilometerstolerance! / 111.134861111;
            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            Uri address = new Uri($"https://search-maps.yandex.ru/v1/?text={organization}&bbox={coordinates.Item2},{coordinates.Item1}~{coordinates.Item2 + bias},{coordinates.Item1 + bias}&type=biz&lang={database[userid].language+"_RU"}&results={settings!.searchresultsarea!}&apikey={settings!.yandexmaptoken!}");
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (WebClient client = new WebClient())
            {
                try
                {
                    database[userid]!.listofrecentsearchedplaces!.Clear();
                    client.Encoding = System.Text.Encoding.UTF8;
                    string request = client.DownloadString(address);
                    Rootobject answer = JsonConvert.DeserializeObject<Rootobject>(request)!;
                    buff += botword["longlinetext"];
                    foreach (var feature in answer!.features)
                    {
                        database[userid]!.listofrecentsearchedplaces!.Add((feature.geometry.coordinates[1], feature.geometry.coordinates[0], feature.properties.CompanyMetaData.name, feature.properties.CompanyMetaData.address)!);
                        if (feature.properties.CompanyMetaData.name != null) buff += $"➡️{organization}: <b>\"{feature.properties.CompanyMetaData.name}\"</b>\n";
                        if (feature.properties.CompanyMetaData.address != null) buff += $"🗺️<b>{botword["addresstext"]}</b> <i>{feature.properties.CompanyMetaData.address}</i> \n📞<b>{botword["phonenumberstext"]}</b>\n";
                        if (feature.properties.CompanyMetaData.Phones != null) foreach (var formatted in feature.properties.CompanyMetaData.Phones) buff += $"          <i>{formatted.formatted}</i>\n";
                        if (feature.properties.CompanyMetaData.Hours.text != null) buff += $"📅<b>{botword["operatingscheduletext"]}</b> <i>{feature.properties.CompanyMetaData.Hours.text}</i>\n";
                        if (feature.properties.CompanyMetaData.url != null) buff += $"🌐<b>{botword["websitetext"]}</b> {feature.properties.CompanyMetaData.url}\n";
                        buff += botword["longlinetext"];
                    }
                }
                catch{}
            }
            if(buff == "")buff = "Sorry, we haven't information :(";
            return buff;
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
                InlineKeyboardButton button = new InlineKeyboardButton(botword["descriptiondisease"]+botword["d"+database[userid].listofrecentdiseases![i-1]].Substring(3, botword["d"+database[userid].listofrecentdiseases![i-1]].Length - 7)) { CallbackData = "description" + i };
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
            Console.WriteLine(symptomsselected);
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

            }
            else
            {
                botword = botworden;
                welcomkeyboard = welcomkeyboarden;
                symptomkeyboard = symptomkeyboarden;
                inlineKeyboard = inlineKeyboarden;
                inlinegenderkeyboard = inlinegenderkeyboarden;
                geolocationkeyboard = geolocationkeyboarden;

            }

        }


    }
}