// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace Telegram Server
// {
//     public class cock
// {

//     public class Rootobject
//     {
//         public string type { get; set; }
//         public Properties properties { get; set; }
//         public Feature[] features { get; set; }
//     }

//     public class Properties
//     {
//         public Responsemetadata ResponseMetaData { get; set; }
//     }

//     public class Responsemetadata
//     {
//         public Searchresponse SearchResponse { get; set; }
//         public Searchrequest SearchRequest { get; set; }
//     }

//     public class Searchresponse
//     {
//         public int found { get; set; }
//         public string display { get; set; }
//         public float[][] boundedBy { get; set; }
//     }

//     public class Searchrequest
//     {
//         public string request { get; set; }
//         public int skip { get; set; }
//         public int results { get; set; }
//         public float[][] boundedBy { get; set; }
//     }

//     public class Feature
//     {
//         public string type { get; set; }
//         public Geometry geometry { get; set; }
//         public Properties1 properties { get; set; }
//     }

//     public class Geometry
//     {
//         public string type { get; set; }
//         public float[] coordinates { get; set; }
//     }

//     public class Properties1
//     {
//         public string name { get; set; }
//         public string description { get; set; }
//         public float[][] boundedBy { get; set; }
//         public string uri { get; set; }
//         public Companymetadata CompanyMetaData { get; set; }
//     }

//     public class Companymetadata
//     {
//         public string id { get; set; }
//         public string name { get; set; }
//         public string address { get; set; }
//         public string url { get; set; }
//         public Phone[] Phones { get; set; }
//         public Category[] Categories { get; set; }
//         public Hours Hours { get; set; }
//     }

//     public class Hours
//     {
//         public string text { get; set; }
//         public Availability[] Availabilities { get; set; }
//     }

//     public class Availability
//     {
//         public Interval[] Intervals { get; set; }
//         public bool Monday { get; set; }
//         public bool Tuesday { get; set; }
//         public bool Wednesday { get; set; }
//         public bool Thursday { get; set; }
//         public bool Friday { get; set; }
//         public bool Saturday { get; set; }
//         public bool Sunday { get; set; }
//         public bool Everyday { get; set; }
//         public bool TwentyFourHours { get; set; }
//     }

//     public class Interval
//     {
//         public string from { get; set; }
//         public string to { get; set; }
//     }

//     public class Phone
//     {
//         public string type { get; set; }
//         public string formatted { get; set; }
//     }

//     public class Category
//     {
//         public string _class { get; set; }
//         public string name { get; set; }
//     }



// }
// }