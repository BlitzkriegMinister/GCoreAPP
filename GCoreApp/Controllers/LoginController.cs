using Microsoft.AspNetCore.Mvc;

namespace GCoreApp.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Loging()
        {
            var login = Request.Form["login"];
            var password = Request.Form["password"];
            try
            {
                Helpers.Login(login, password);

            }
            catch (System.Exception)
            {
                return BadRequest();
            }
            Program.Authorize = true;
            return Redirect("upload");
        }
    }
}
