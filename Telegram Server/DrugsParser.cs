using HtmlAgilityPack;

namespace Program
{
    public class DrugsParser
    {
        public static async Task parsedrugslist(string drugsearchname, int index)
        {
            if (drugsearchname == null) drugsearchname = "парацетамол";
            List<DrugSpecs> drugslist = new List<DrugSpecs>();

            HttpClient httpClient = new HttpClient();
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://tabletka.by/search?request={drugsearchname}&region={index}");
            using HttpResponseMessage response = await httpClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            HtmlNodeCollection drugname = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='name tooltip-info']//div[@class='tooltip-info-header']/a");
            HtmlNodeCollection drugform = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='form tooltip-info']//div[@class='tooltip-info-header']/a");
            HtmlNodeCollection drugproducer = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='produce tooltip-info']//div[@class='tooltip-info-header']/a");
            HtmlNodeCollection drugprice = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='price']//span[@class='price-value']");
            HtmlNodeCollection numberofpharmacies = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='price']//span[@class='capture']/a");

            for (int i = 0; i < drugname.Count; i++)
            {
                //Console.WriteLine($"Наименование: {drugname[i].InnerText} Форма: {drugform[i].InnerText} Производитель: {drugproducer[i].InnerText.Trim()} Цена: {drugprice[i].InnerText} В {numberofpharmacies[i].InnerText.Replace("аптеках", "").Trim()} Аптеках");
                int pharmaciescount = 0;
                int.TryParse(string.Join("", numberofpharmacies[i].InnerText.Where(c => char.IsDigit(c))), out pharmaciescount);
                DrugSpecs drugspec = new DrugSpecs()
                {
                    Drugname = drugname[i].InnerText,
                    Drugform = drugform[i].InnerText,
                    Drugproducer = drugproducer[i].InnerText.Trim(),
                    Drugprice = drugprice[i].InnerText,
                    Link = drugname[i].GetAttributeValue("href", ""),
                    Numberofpharmacies = pharmaciescount
                };

                drugslist.Add(drugspec);
                if (i == 4) break;
            }
            database[userid].lastdrugslist.Clear();
            database[userid].lastdrugslist = drugslist;
        }

        public static async Task parsedrugsincity(string link)
        {
            HttpClient httpClient = new HttpClient();
            List<DrugInSityInfo> pharmlist = new List<DrugInSityInfo>();
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://tabletka.by{link}");
            using HttpResponseMessage response = await httpClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            HtmlNodeCollection pharmname = doc.DocumentNode.SelectNodes("//div[@class='table-wrap reload result-table']//td[@class='pharm-name']//div[@class='tooltip-info-header']//a");
            HtmlNodeCollection address = doc.DocumentNode.SelectNodes("//div[@class='table-wrap reload result-table']//td[@class='address tooltip-info']//div[@class='tooltip-info-header']/div[@class='text-wrap']/span");
            HtmlNodeCollection phonenumber = doc.DocumentNode.SelectNodes("//div[@class='table-wrap reload result-table']//td[@class='phone tooltip-info']//div[@class='tooltip-info-header']/div[@class='text-wrap']/a");
            HtmlNodeCollection cost = doc.DocumentNode.SelectNodes("//div[@class='table-wrap reload result-table']//td[@class='price tooltip-info']//div[@class='tooltip-info-header']/div[@class='text-wrap']/span");

            for (int i = 0; i < pharmname.Count; ++i)
            {
                DrugInSityInfo pharminfo = new DrugInSityInfo()
                {
                    Pharmname = pharmname[i].InnerText,
                    Address = address[i].InnerText.Trim(),
                    PhoneNumber = phonenumber[i].InnerText,
                    Cost = cost[i].InnerText.Trim(),
                };
                pharmlist.Add(pharminfo);
                if (i == 4) break;
            }
            database[userid].lastpharmlist.Clear();
            database[userid].lastpharmlist = pharmlist;
        }
    }
}