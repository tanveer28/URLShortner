namespace URLShortner.Interfaces
{
    public interface IWebsiteRepository
    {
        string SaveUrl(string url);
        string GetUrl(int url);
    }
}
