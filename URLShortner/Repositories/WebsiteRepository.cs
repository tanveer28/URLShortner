using URLShortner.Interfaces;
using URLShortner.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace URLShortner.Repositories
{
    public class WebsiteRepository : IWebsiteRepository
    {
        private static string fileName = "./sites/sites.json";

        public string GetUrl(int id)
        {
            var websites = GetWebsiteList();
            string newURL;
            if (WebsiteExists(websites, id))
            {
                newURL = GetWebsite(websites, id);
            }
            else
            {
                newURL = "";
            }

            return newURL;
        }

        public string SaveUrl(string url)
        {
            var websites = GetWebsiteList();
            string newURL;

            if (!WebsiteExists(websites, url))
            {
                int id = CreateId();
                newURL = GetWebsiteURL(id);

                SaveWebsites(websites, new()
                {
                    Site = url,
                    Id = id
                }) ;
            }
            else
            {
                newURL = GetWebsiteURL(GetId(websites, url));
            }

            return newURL;
        }

        private static int CreateId()
        {
            Random r = new Random();
            return r.Next(0, 1000000000);
        }

        private List<Website> GetWebsiteList()
        {
            string json = File.ReadAllText(fileName);

            IEnumerable<Website> websites =
                JsonSerializer.Deserialize<IEnumerable<Website>>(json);

            return websites.ToList();
        }

        private static async void SaveWebsites(List<Website> websites, Website website)
        {
            var newSites = GetSiteList(websites, website);
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, newSites);
            await createStream.DisposeAsync();
        }

        private static List<Website> GetSiteList(List<Website> websites, Website website)
        {
            websites.Add(website);
            return websites;
        }

        private static string GetWebsiteURL(int id) => $"https://localhost:7146/Short?id={id}";

        private static bool WebsiteExists (IEnumerable<Website> websites, string website) => websites.Where(x => x.Site == website).Any();
        private static bool WebsiteExists(IEnumerable<Website> websites, int id) => websites.Where(x => x.Id == id).Any();

        private static int GetId(IEnumerable<Website> websites, string website) => websites.First(x => x.Site == website).Id;
        private static string GetWebsite(IEnumerable<Website> websites, int id) => websites.First(x => x.Id == id).Site;
    }
}
