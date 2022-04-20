using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GCoreApp.Controllers
{
    [ApiController]
    [Route("api/cv")]
    public class CVController : Controller
    {
        public IActionResult Index(string _id)
        {
            if (Program.Authorize == false)
            {
                return new UnauthorizedObjectResult("Unauthorize\nGo to (api/login) to authorize");
            }
            var origin_video = Helpers.GetVideo(_id);
            var url = "https://s-ed1.cloud.gcore.lu/" + origin_video.origin_resource;
            var id = Helpers.SendToAnalyze(url);
            var status = Helpers.CheckStatusAnalyze(id);
            var vid = Program.videos.FirstOrDefault(m => m.id == Convert.ToInt32(_id));
            if (vid == null)
            {
                return Redirect("upload");
            }
            vid.cv_id = id;
            vid.cv_status = status;
            return Redirect("upload");
        }
    }
}
