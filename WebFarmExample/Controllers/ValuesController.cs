using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace WebFarmExample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IDistributedCache _memoryCache;

        public ValuesController(IDistributedCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("SetCacheData")]
        public IActionResult SetCacheData()
        {
            try
            {
                var time = DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture);
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddYears(1)
                };
                _memoryCache.Set("serverTime", Encoding.UTF8.GetBytes(time), cacheOptions);

                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                return Json(new { ex = ex });
            }
        }

        [HttpGet("GetCacheData")]
        public string GetCacheData()
        {
            try
            {
                var time = Encoding.UTF8.GetString(_memoryCache.Get("serverTime"));
                ViewBag.data = time;

                return time;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }

        [HttpGet("RemoveCacheData")]
        public bool RemoveCacheData()
        {
            _memoryCache.Remove("serverTime");

            return true;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
