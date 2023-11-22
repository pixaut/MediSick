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
        public double kilometerstolerance { get; set; } = 0.5;//default
        public int searchresultsarea { get; set; } = 5;//default
        public string token { get; set; } = "";
        public string yandexmaptoken { get; set; } = "";
        public string pathdatabasejson { get; set; } = "";
        public string pathtextforbotjsonru { get; set; } = "";
        public string pathtextforbotjsonen { get; set; } = "";
        public string pathsymptomslistjson { get; set; } = "";
        public string pathnetworkexe { get; set; } = "";
        public string pathnetworkexeworkingdirectory { get; set; } = "";
        public string linkinstructionsforenteringsymptoms { get; set; } = "";
        public string pathaiexe { get; set; } = "";
        public string pathinputuser { get; set; } = "";
        public string pathoutputuser { get; set; } = "";
        public bool enablelogging { get; set; } = false;
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
        public List<(float, float, string, string)>? listofrecentsearchedplaces = new List<(float, float, string, string)>();
        public string lastmessage { get; set; } = "";
        public string? gender { get; set; } = null;
        public string? language { get; set; } = null;
        public (double, double) geolocation;
    }
    public class ResponseFromYandexMaps
    {

        public class Rootobject
        {
            public required string type { get; set; }
            public required Properties properties { get; set; }
            public required Feature[] features { get; set; }
        }

        public class Properties
        {
            public required Responsemetadata ResponseMetaData { get; set; }
        }

        public class Responsemetadata
        {
            public required Searchresponse SearchResponse { get; set; }
            public required Searchrequest SearchRequest { get; set; }
        }

        public class Searchresponse
        {
            public int found { get; set; }
            public required string display { get; set; }
            public required float[][] boundedBy { get; set; }
        }

        public class Searchrequest
        {
            public required string request { get; set; }
            public int skip { get; set; }
            public int results { get; set; }
            public required float[][] boundedBy { get; set; }
        }

        public class Feature
        {
            public required string type { get; set; }
            public required Geometry geometry { get; set; }
            public required Properties1 properties { get; set; }
        }

        public class Geometry
        {
            public required string type { get; set; }
            public required float[] coordinates { get; set; }
        }

        public class Properties1
        {
            public required string name { get; set; }
            public required string description { get; set; }
            public required float[][] boundedBy { get; set; }
            public required string uri { get; set; }
            public required Companymetadata CompanyMetaData { get; set; }
        }

        public class Companymetadata
        {
            public required string id { get; set; }
            public required string name { get; set; }
            public required string address { get; set; }
            public required string url { get; set; }
            public required Phone[] Phones { get; set; }
            public required Category[] Categories { get; set; }
            public required Hours Hours { get; set; }
        }

        public class Hours
        {
            public required string text { get; set; }
            public required Availability[] Availabilities { get; set; }
        }

        public class Availability
        {
            public required Interval[] Intervals { get; set; }
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
            public required string from { get; set; }
            public required string to { get; set; }
        }

        public class Phone
        {
            public required string type { get; set; }
            public required string formatted { get; set; }
        }

        public class Category
        {
            public required string _class { get; set; }
            public required string name { get; set; }
        }
    }
    public class DrugSpecs
    {
        public required string Drugname { get; set; } = "";
        public required string Drugform { get; set; } = "";
        public required string Drugproducer { get; set; } = "";
        public required string Drugprice { get; set; } = "";
        public required string Link { get; set; } = "";
        public required int Numberofpharmacies { get; set; } = 0;
    }
}