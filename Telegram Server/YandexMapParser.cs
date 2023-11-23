namespace Program
{
    //Parser YandexMap part:
    public class YandexMapParser
    {
        //Searchorganizations YandexMap function:
        public static string searchorganizations(string organization, (double, double) coordinates)
        {
            string outsting = "";
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
                    client.Encoding = Encoding.UTF8;
                    string request = client.DownloadString(address);
                    Rootobject1 answer = JsonConvert.DeserializeObject<Rootobject1>(request)!;
                    outsting += botword["longlinetext"];
                    foreach (var feature in answer!.features)
                    {
                        database[userid]!.listofrecentsearchedplaces!.Add((feature.geometry.coordinates[1], feature.geometry.coordinates[0], feature.properties.CompanyMetaData.name, feature.properties.CompanyMetaData.address)!);
                        if (feature.properties.CompanyMetaData.name != null) outsting += $"➡️{organization}: <b>\"{feature.properties.CompanyMetaData.name}\"</b>\n";
                        if (feature.properties.CompanyMetaData.address != null) outsting += $"🗺️<b>{botword["addresstext"]}</b> <i>{feature.properties.CompanyMetaData.address}</i> \n📞<b>{botword["phonenumberstext"]}</b>\n";
                        if (feature.properties.CompanyMetaData.Phones != null) foreach (var formatted in feature.properties.CompanyMetaData.Phones) outsting += $"          <i>{formatted.formatted}</i>\n";
                        if (feature.properties.CompanyMetaData.Hours.text != null) outsting += $"📅<b>{botword["operatingscheduletext"]}</b> <i>{feature.properties.CompanyMetaData.Hours.text}</i>\n";
                        if (feature.properties.CompanyMetaData.url != null) outsting += $"🌐<b>{botword["websitetext"]}</b> {feature.properties.CompanyMetaData.url}\n";
                        outsting += botword["longlinetext"];
                    }
                }
                catch { }
            }
            if (outsting == "" || outsting == null) outsting = botword["sorrynoinfotext"];
            return outsting;
        }

        //Search current city from YandexMap function:
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
                    client.Encoding = Encoding.UTF8;
                    string request = client.DownloadString(address);
                    Rootobject2 answer = JsonConvert.DeserializeObject<Rootobject2>(request)!;
                    database[userid].city = answer.response.GeoObjectCollection.featureMember[0].GeoObject.description.Substring(0, answer.response.GeoObjectCollection.featureMember[0].GeoObject.description.IndexOf(','));
                }
                catch { }
            }
        }
    }
}