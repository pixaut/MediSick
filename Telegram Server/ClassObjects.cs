using Microsoft.VisualBasic;
using Telegram.Bot;
using Telegram.Bot.Requests;

namespace Program
{
    public class SearchResult
    {
        public string name { get; set; }

    }


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
        public bool searchbyareamenu { get; set; } = false;
        public string name { get; set; } = "no name";
        public List<int>? inlinebuttpressed = new List<int>();
        public List<int>? listofrecentdiseases = new List<int>();
        public List<(float,float,string,string)>? listofrecentsearchedplaces = new List<(float,float,string,string)>();
        public string lastmessage { get; set; } = "";
        public string gender { get; set; } = "non";
        public string language { get; set; } = "non";
        public (double, double) geolocation;
        


        public User()
        {
            mainmenu = mainmenu;
            symptommenu = symptommenu;
            name = name;
            inlinesymptomkey = inlinesymptomkey;
            lastmessage = lastmessage;
            gender = gender;
            language = language;
            searchbyareamenu = searchbyareamenu;

        }
    }
    public class ResponseFromYandexMaps
    {

        public class Rootobject
        {
            public string type { get; set; }
            public Properties properties { get; set; }
            public Feature[] features { get; set; }
        }

        public class Properties
        {
            public Responsemetadata ResponseMetaData { get; set; }
        }

        public class Responsemetadata
        {
            public Searchresponse SearchResponse { get; set; }
            public Searchrequest SearchRequest { get; set; }
        }

        public class Searchresponse
        {
            public int found { get; set; }
            public string display { get; set; }
            public float[][] boundedBy { get; set; }
        }

        public class Searchrequest
        {
            public string request { get; set; }
            public int skip { get; set; }
            public int results { get; set; }
            public float[][] boundedBy { get; set; }
        }

        public class Feature
        {
            public string type { get; set; }
            public Geometry geometry { get; set; }
            public Properties1 properties { get; set; }
        }

        public class Geometry
        {
            public string type { get; set; }
            public float[] coordinates { get; set; }
        }

        public class Properties1
        {
            public string name { get; set; }
            public string description { get; set; }
            public float[][] boundedBy { get; set; }
            public string uri { get; set; }
            public Companymetadata CompanyMetaData { get; set; }
        }

        public class Companymetadata
        {
            public string id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public string url { get; set; }
            public Phone[] Phones { get; set; }
            public Category[] Categories { get; set; }
            public Hours Hours { get; set; }
        }

        public class Hours
        {
            public string text { get; set; }
            public Availability[] Availabilities { get; set; }
        }

        public class Availability
        {
            public Interval[] Intervals { get; set; }
            public bool Monday { get; set; }
            public bool Tuesday { get; set; }
            public bool Wednesday { get; set; }
            public bool Thursday { get; set; }
            public bool Friday { get; set; }
            public bool Saturday { get; set; }
            public bool Sunday { get; set; }
        }

        public class Interval
        {
            public string from { get; set; }
            public string to { get; set; }
        }

        public class Phone
        {
            public string type { get; set; }
            public string formatted { get; set; }
        }

        public class Category
        {
            public string _class { get; set; }
            public string name { get; set; }
        }
    }
}