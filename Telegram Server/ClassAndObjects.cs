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
        public bool searchorganizationmenu { get; set; } = false;
        public bool searchdrugmenu { get; set; } = false;
        public string name { get; set; } = "no name";
        public string? city { get; set; } = null;
        public List<int>? inlinebuttpressed = new List<int>();
        public List<int>? listofrecentdiseases = new List<int>();
        public List<(float, float, string, string)>? listofrecentsearchedplaces = new List<(float, float, string, string)>();
        public List<DrugSpecs> lastdrugslist = new List<DrugSpecs>();
        public List<DrugInSityInfo> lastpharmlist = new List<DrugInSityInfo>();
        public string lastmessage { get; set; } = "";
        public string? gender { get; set; } = null;
        public string? language { get; set; } = null;
        public (double, double) geolocation;
    }
    public class DetermineAdressYandexMaps
    {
        public class Rootobject2
        {
            public Response response { get; set; }
        }

        public class Response
        {
            public Geoobjectcollection GeoObjectCollection { get; set; }
        }

        public class Geoobjectcollection
        {
            public Metadataproperty metaDataProperty { get; set; }
            public Featuremember[] featureMember { get; set; }
        }

        public class Metadataproperty
        {
            public Geocoderresponsemetadata GeocoderResponseMetaData { get; set; }
        }

        public class Geocoderresponsemetadata
        {
            public Point Point { get; set; }
            public string request { get; set; }
            public string results { get; set; }
            public string found { get; set; }
        }

        public class Point
        {
            public string pos { get; set; }
        }

        public class Featuremember
        {
            public Geoobject GeoObject { get; set; }
        }

        public class Geoobject
        {
            public Metadataproperty1 metaDataProperty { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public Boundedby boundedBy { get; set; }
            public string uri { get; set; }
            public Point1 Point { get; set; }
        }

        public class Metadataproperty1
        {
            public Geocodermetadata GeocoderMetaData { get; set; }
        }

        public class Geocodermetadata
        {
            public string precision { get; set; }
            public string text { get; set; }
            public string kind { get; set; }
            public Address Address { get; set; }
            public Addressdetails AddressDetails { get; set; }
        }

        public class Address
        {
            public string country_code { get; set; }
            public string formatted { get; set; }
            public string postal_code { get; set; }
            public Component[] Components { get; set; }
        }

        public class Component
        {
            public string kind { get; set; }
            public string name { get; set; }
        }

        public class Addressdetails
        {
            public Country Country { get; set; }
        }

        public class Country
        {
            public string AddressLine { get; set; }
            public string CountryNameCode { get; set; }
            public string CountryName { get; set; }
            public Administrativearea AdministrativeArea { get; set; }
        }

        public class Administrativearea
        {
            public string AdministrativeAreaName { get; set; }
            public Locality Locality { get; set; }
        }

        public class Locality
        {
            public string LocalityName { get; set; }
            public Thoroughfare Thoroughfare { get; set; }
            public Premise1 Premise { get; set; }
            public Dependentlocality DependentLocality { get; set; }
        }

        public class Thoroughfare
        {
            public string ThoroughfareName { get; set; }
            public Premise Premise { get; set; }
        }

        public class Premise
        {
            public string PremiseNumber { get; set; }
            public Postalcode PostalCode { get; set; }
        }

        public class Postalcode
        {
            public string PostalCodeNumber { get; set; }
        }

        public class Premise1
        {
            public string PremiseName { get; set; }
        }

        public class Dependentlocality
        {
            public string DependentLocalityName { get; set; }
            public Dependentlocality1 DependentLocality { get; set; }
        }

        public class Dependentlocality1
        {
            public string DependentLocalityName { get; set; }
        }

        public class Boundedby
        {
            public Envelope Envelope { get; set; }
        }

        public class Envelope
        {
            public string lowerCorner { get; set; }
            public string upperCorner { get; set; }
        }

        public class Point1
        {
            public string pos { get; set; }
        }


    }
    public class ResponseFromYandexMaps
    {

        public class Rootobject1
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
    public class DrugInSityInfo
    {
        public required string Pharmname { get; set; } = "";
        public required string Address { get; set; } = "";
        public required string PhoneNumber { get; set; } = "";
        public required string Cost { get; set; } = "";
    }
}