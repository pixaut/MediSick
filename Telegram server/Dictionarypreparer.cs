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
    }
}