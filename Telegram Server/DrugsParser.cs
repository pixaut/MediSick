using HtmlAgilityPack;

namespace Program
{
    public class DrugsParser
    {
        public static async Task<List<DrugSpecs>> parsedrugslist()
        {
            List<DrugSpecs> drugslist = new List<DrugSpecs>();

            HttpClient httpClient = new HttpClient();
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://tabletka.by/search?request=ибупрофен&region=91");
            using HttpResponseMessage response = await httpClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            HtmlNodeCollection drugname = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='name tooltip-info']//div[@class='tooltip-info-header']/a");
            HtmlNodeCollection drugform = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='form tooltip-info']//div[@class='tooltip-info-header']/a");
            HtmlNodeCollection drugproducer = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='produce tooltip-info']//div[@class='tooltip-info-header']/a");
            HtmlNodeCollection drugprice = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='price']//span[@class='price-value']");
            HtmlNodeCollection numberofpharmacies = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='price']//span[@class='capture']/a");

            for (int i = 0; i < 5; i++)
            {
                //Console.WriteLine($"Наименование: {drugname[i].InnerText} Форма: {drugform[i].InnerText} Производитель: {drugproducer[i].InnerText.Trim()} Цена: {drugprice[i].InnerText} В {numberofpharmacies[i].InnerText.Replace("аптеках", "").Trim()} Аптеках");
                DrugSpecs drugspec = new DrugSpecs()
                {
                    Drugname = drugname[i].InnerText,
                    Drugform = drugform[i].InnerText,
                    Drugproducer = drugproducer[i].InnerText.Trim(),
                    Drugprice = drugprice[i].InnerText,
                    Link = drugname[i].GetAttributeValue("href", ""),
                    Numberofpharmacies = int.Parse(numberofpharmacies[i].InnerText.Replace("аптеках", "").Replace("в", "").Trim())
                };
                drugslist.Add(drugspec);
            }
            return drugslist;
        }

    }
}