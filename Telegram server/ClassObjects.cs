using Telegram.Bot;

namespace Program
{


    public class SymptomsList
    {
        public required Symptoms[] Symptoms { get; set; }
    }
    public class Symptoms
    {
        public required string Сategory { get; set; }
        public required string List { get; set; }
    }

    public class Textbot
    {
        public required Textarray[] Textforbot { get; set; }
    }
    public class Textarray
    {
        public required string TextName { get; set; }
        public required string Text { get; set; }
        public required int Number { get; set; }
    }


    public class Settings
    {
        public int countsymptoms { get; set; } = 0;
        public string token { get; set; } = "";
        public string pathdatabasejson { get; set; } = "";
        public string pathtextforbotjson { get; set; } = "";
        public string pathsymptomslistjson { get; set; } = "";
        public string pathaiexe { get; set; } = "";
        public string pathinputuser { get; set; } = "";
        public string pathoutputuser { get; set; } = "";
        public bool enablelogging { get; set; } = false;
        public Settings()
        {
            pathaiexe = pathaiexe;
            pathinputuser = pathinputuser;
            pathoutputuser = pathoutputuser;
            countsymptoms = countsymptoms;
            pathdatabasejson = pathdatabasejson;
            pathtextforbotjson = pathtextforbotjson;
            pathsymptomslistjson = pathsymptomslistjson;
            countsymptoms = countsymptoms;
            enablelogging = enablelogging;
        }
    }


    public class User
    {

        public bool symptommenu { get; set; } = false;
        public bool mainmenu { get; set; } = true;
        public bool inlinesymptomkey { get; set; } = false;
        public string name { get; set; } = "no name";
        public List<int>? inlinebuttpressed = new List<int>();
        public string lastmessage { get; set; } = "";
        public string gender { get; set; } = "non";
        public string language { get; set; } = "non";


        public User()
        {
            mainmenu = mainmenu;
            symptommenu = symptommenu;
            name = name;
            inlinesymptomkey = inlinesymptomkey;
            lastmessage = lastmessage;
            gender = gender;
            language = language;
        }
    }
}