using Microsoft.AspNetCore.Mvc;
using System;

namespace CookieExample.Controllers
{
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        [HttpPost("set-cookie")]
        public IActionResult SetCookie([FromForm] string value, [FromForm] DateTime expiry)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = expiry
                };

                Response.Cookies.Append("MyCookie", value, cookieOptions);
                return Ok("Cookie встановлено успішно.");
            }
            return BadRequest("Значення не задане.");
        }

        [HttpGet("check-cookie")]
        public IActionResult CheckCookie()
        {
            if (Request.Cookies.ContainsKey("MyCookie"))
            {
                string cookieValue = Request.Cookies["MyCookie"];
                return Ok($"Значення куки: {cookieValue}");
            }
            return NotFound("Cookie не знайдено.");
        }
    }
}
