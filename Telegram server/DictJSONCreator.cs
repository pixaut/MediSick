using Newtonsoft.Json;

namespace Program
{
    class Dictionarypreparer
    {

        public static Dictionary<string, string> BotwordDictpreparer(Dictionary<string, string> botword, Textbot textbot)
        {
            for (int i = 0; i < textbot.Textforbot.Length; i++)
            {
                botword.TryAdd(textbot.Textforbot[i].TextName, textbot.Textforbot[i].Text);
            }
            return botword;
        }

        public static Dictionary<long, User> DatabaseDictFillFromJSON(Dictionary<long, User> database, string path)
        {
            Dictionary<long, User> data = JsonConvert.DeserializeObject<Dictionary<long, User>>(File.ReadAllText(@path));

            return data;
        }



        public static void DatabaseDictSaverToJSON(Dictionary<long, User> database, string path)
        {

            File.WriteAllText(@path, JsonConvert.SerializeObject(database, Formatting.Indented));
        }

        public static string symptomhandler(List<int> select, SymptomsList symptoms)
        {
            string symptomsselected = ""; //= symptoms.Substring(symptoms.IndexOf("0-"), symptoms.IndexOf("-0") - symptoms.IndexOf("0-")).Remove(0, 3);

            for (int i = 0; i < select.Count; i++)
            {
                symptomsselected += symptoms.Symptoms[i].List;
            }
            Console.WriteLine(symptomsselected);
            return symptomsselected;


        }


    }
}