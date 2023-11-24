namespace Program
{
    //Parser drugs and pharms part:
    public class DrugsParser
    {
        public static HtmlDocument doc = new HtmlDocument();

        //Search drugs in current city function:
        public static async Task parsedrugslist(string drugsearchname, int index)
        {
            database[userid].lastdrugslist.Clear();
            DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
            List<DrugSpecs> drugslist = new List<DrugSpecs>();
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://tabletka.by/search?request={drugsearchname}&region={index}");
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    doc.LoadHtml(await response.Content.ReadAsStringAsync());
                    HtmlNodeCollection drugname = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='name tooltip-info']//div[@class='tooltip-info-header']/a");
                    HtmlNodeCollection drugform = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='form tooltip-info']//div[@class='tooltip-info-header']/a");
                    HtmlNodeCollection drugproducer = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='produce tooltip-info']//div[@class='tooltip-info-header']/a");
                    HtmlNodeCollection drugprice = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='price']//span[@class='price-value']");
                    HtmlNodeCollection numberofpharmacies = doc.DocumentNode.SelectNodes("//div[@class='table-wrap']//td[@class='price']//span[@class='capture']/a");
                    for (int i = 0; i < Math.Min(5, drugname.Count); i++)
                    {
                        int.TryParse(string.Join("", numberofpharmacies[i].InnerText.Where(c => char.IsDigit(c))), out int pharmaciescount);
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
                    }
                    database[userid].lastdrugslist = drugslist;
                }
                catch { }
            }
        }

        //Search pharms for drugs in current city function:
        public static async Task parsedrugsincity(string link)
        {
            List<DrugInSityInfo> pharmlist = new List<DrugInSityInfo>();
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://tabletka.by{link}");
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    doc.LoadHtml(await response.Content.ReadAsStringAsync());
                    HtmlNodeCollection pharmname = doc.DocumentNode.SelectNodes("//div[@class='table-wrap reload result-table']//td[@class='pharm-name']//div[@class='tooltip-info-header']//a");
                    HtmlNodeCollection address = doc.DocumentNode.SelectNodes("//div[@class='table-wrap reload result-table']//td[@class='address tooltip-info']//div[@class='tooltip-info-header']/div[@class='text-wrap']/span");
                    HtmlNodeCollection phonenumber = doc.DocumentNode.SelectNodes("//div[@class='table-wrap reload result-table']//td[@class='phone tooltip-info']//div[@class='tooltip-info-header']/div[@class='text-wrap']/a");
                    HtmlNodeCollection cost = doc.DocumentNode.SelectNodes("//div[@class='table-wrap reload result-table']//td[@class='price tooltip-info']//div[@class='tooltip-info-header']/div[@class='text-wrap']/span");
                    for (int i = 0; i < Math.Min(5, pharmname.Count); ++i)
                    {
                        DrugInSityInfo pharminfo = new DrugInSityInfo()
                        {
                            Pharmname = pharmname[i].InnerText,
                            Address = address[i].InnerText.Trim(),
                            PhoneNumber = phonenumber[i].InnerText,
                            Cost = cost[i].InnerText.Trim(),
                        };
                        pharmlist.Add(pharminfo);
                    }
                }
                catch { }
            }
            database[userid].lastpharmlist = pharmlist;
            DatabaseDictSaverToJSON(database, settings!.pathdatabasejson);
        }
    }
}