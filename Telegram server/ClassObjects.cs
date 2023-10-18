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
    }


    public class Settings
    {
        public int countsymptoms { get; set; } = 0;
        public string token { get; set; } = "";
        public string pathdatabasejson { get; set; } = "";
        public string pathtextforbotjson { get; set; } = "";
        public string pathsymptomslistjson { get; set; } = "";
        public Settings()
        {
            countsymptoms = countsymptoms;
            pathdatabasejson = pathdatabasejson;
            pathtextforbotjson = pathtextforbotjson;
            pathsymptomslistjson = pathsymptomslistjson;
            countsymptoms = countsymptoms;
        }
    }


    public class User
    {
        public bool symptommenu { get; set; } = false;
        public bool mainmenu { get; set; } = true;
        public string name { get; set; } = "no name";
        public User()
        {
            mainmenu = mainmenu;
            symptommenu = symptommenu;
            name = name;
        }
    }
}