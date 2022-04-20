using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GCoreApp.Controllers
{
    [ApiController]
    [Route("api/status")]
    public class StatusController : Controller
    {
        public IActionResult Index(string id)
        {
            if (Program.Authorize == false)
            {
                return new UnauthorizedObjectResult("Unauthorize\nGo to (api/login) to authorize");
            }
            var vid = Program.videos.FirstOrDefault(m => m.cv_id == id);
            var status = Helpers.CheckStatusAnalyze(id);
            if (!status.Contains("Найдено"))
            {
                vid.cv_status = status;
            }
            else
            {
                vid.cv_status = "end";
                vid.cv_result = status;
            }
            return Redirect("upload");
        }
    }
}
