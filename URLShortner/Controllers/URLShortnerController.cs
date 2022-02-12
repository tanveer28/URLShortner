using Microsoft.AspNetCore.Mvc;
using System.Net;
using URLShortner.Interfaces;
using URLShortner.Repositories;

namespace URLShortner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortController : ControllerBase
    {
        private readonly IWebsiteRepository websiteRepository;

        public ShortController(IWebsiteRepository websiteRepository)
        {
            this.websiteRepository = websiteRepository;
        }

        [HttpPost(Name = "Navigate")]
        public string Post(string url)
        {
            string newURL = websiteRepository.SaveUrl(AddHTTPToURL(url));
            return newURL;
        }

        [HttpGet(Name = "Go/{id}")]
        public RedirectResult Get(int id)
        {
            string newURL = websiteRepository.GetUrl(id);
            return Redirect(AddHTTPToURL(newURL));
        }
        private static string AddHTTPToURL(string url)
        {
            return new UriBuilder(url).Uri.ToString();
        }
    }
}
