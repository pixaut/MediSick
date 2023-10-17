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


    }
}