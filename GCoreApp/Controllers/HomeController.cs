using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TusDotNetClient;

namespace GCoreApp.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class HomeController : Controller
    {


        [HttpPost]
        public async Task<string> Upload()
        {
            if (Program.Authorize == false)
            {
                return "Unauthorize\nGo to (api/login) to authorize";
            }
            var file = Request.Form.Files[0];

            var res = Helpers.CreateVideo(file.FileName);
            var res2 = Helpers.GetUploadTokenAndUrl(res.id.ToString());
            string upload_token = res2.token;
            string upload_url = "https://" + res2.servers[0].hostname + "/upload";

            var client = new TusClient();

            var fileUrl = await client.CreateAsync(upload_url, file.Length, ("filename", file.FileName), ("client_id", res2.video.client_id.ToString()), ("video_id", res2.video.id.ToString()), ("token", upload_token));
            using (Stream str = file.OpenReadStream())
            {
                var d = await client.UploadAsync(fileUrl, str, chunkSize: 5D);
                int x = 2;
            }

            return "Видео успешно загружено!";
        }
        [HttpGet]
        // GET: HomeController
        public IActionResult Index()
        {
            if(Program.Authorize == false)
            {
                return new UnauthorizedObjectResult("Unauthorize\nGo to (api/login) to authorize");
            }
            Helpers.GetAllVideos();

            ViewData["Access"] = Program.token;
            ViewData["MyVideos"] = Program.videos;

            return View();
        }
    }
}
