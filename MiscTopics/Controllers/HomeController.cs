using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MiscTopics.Models;
using System.Diagnostics;

namespace MiscTopics.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            IMemoryCache memoryCache    )
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            DateTime time;
            bool value = _memoryCache.TryGetValue("cachedTime", out time);
            if (!value)
            {

                time = DateTime.Now;
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.)
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(5));
                _memoryCache.Set("cachedTime", time, cacheOptions);
            }
            return View(time);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}