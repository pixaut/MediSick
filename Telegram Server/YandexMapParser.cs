using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Program
{
    public class YandexMapParser
    {
        //Searchorganizations YandexMap function:
        public static string searchorganizations(string organization, (double, double) coordinates)
        {

            string buff = "";

            double bias = settings!.kilometerstolerance! / 111.134861111;

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            Uri address = new Uri($"https://search-maps.yandex.ru/v1/?text={organization}&bbox={coordinates.Item2},{coordinates.Item1}~{coordinates.Item2 + bias},{coordinates.Item1 + bias}&type=biz&lang={database[userid].language + "_RU"}&results={settings!.searchresultsarea!}&apikey={settings!.yandexmaptoken!}");
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (WebClient client = new WebClient())
            {
                try
                {
                    database[userid]!.listofrecentsearchedplaces!.Clear();
                    client.Encoding = System.Text.Encoding.UTF8;
                    string request = client.DownloadString(address);
                    Rootobject1 answer = JsonConvert.DeserializeObject<Rootobject1>(request)!;
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
                catch { }
            }
            if (buff == "" || buff == null) buff = botword["sorrynoinfotext"];
            return buff;
        }

        public static void determineaddress((double, double) coordinates)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Uri address = new Uri($"https://geocode-maps.yandex.ru/1.x/?apikey={"df137e56-4bc5-483d-b765-6b7373742442"}&geocode={coordinates.Item2},{coordinates.Item1}&format=json");
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (WebClient client = new WebClient())
            {
                try
                {
                    database[userid]!.listofrecentsearchedplaces!.Clear();
                    client.Encoding = System.Text.Encoding.UTF8;
                    string request = client.DownloadString(address);
                    Rootobject2 answer = JsonConvert.DeserializeObject<Rootobject2>(request)!;
                    database[userid].city = answer.response.GeoObjectCollection.featureMember[0].GeoObject.description.Substring(0, (answer.response.GeoObjectCollection.featureMember[0].GeoObject.description).IndexOf(','));
                }
                catch { }
            }

        }

    }
}