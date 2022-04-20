using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace GCoreApp
{
    public class Helpers
    {
        public static void Login(string login, string password)
        {
            Console.WriteLine("Пытаемся войти по логину и паролю...");
            string json = @"{""username"": """+ login + @""", ""password"":"""+password+@"""}";
            var res = HttpPost("https://api.gcdn.co/auth/jwt/login", json);
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.LoginResponse>(res);
            Console.WriteLine("Вход успешен!\n\n" +
                "Токен обноваления: " + model.Refresh + "\n" +
                "Токен доступа: " + model.Access);
            Program.token = model.Access;
        }
        public static Models.GetVideoResponse GetVideo(string id)
        {
            if(id == null)
            {
                return null;
            }
            Console.WriteLine("Пытаемся получить видео...");
            var res = HttpGet($"https://api.gcdn.co/vp/api/videos/{id}");
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.GetVideoResponse>(res);
            Console.WriteLine("Успешно!\n\n" + res);
            return model;
        }
        public static Models.GetVideoResponse CreateVideo(string name)
        {

            Regex reg = new Regex(@"(.+).mp4");
            Match m = reg.Match(name);

            string json = @"{""video"": {""name"": """ + m.Groups[1].Value + @"""}}";

            var res = HttpPost("https://api.gcdn.co/vp/api/videos", json);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Models.GetVideoResponse>(res); ;
        }
        public static Models.GetUrlAndTokenResponse GetUploadTokenAndUrl(string id)
        {
            var res = HttpGet($"https://api.gcdn.co/vp/api/videos/{id}/upload/");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Models.GetUrlAndTokenResponse>(res);
        }
        public static string GetAuthString() => " Bearer " + Program.token;
        public static void GetAllVideos()
        {
            Console.WriteLine("Пытаемся получить все видео на вашем аккаунте...");
            string url = "https://api.gcdn.co/vp/api/videos";
            var res = HttpGet(url);
            Console.WriteLine("Успешно! \n\n" + res);

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.GetVideoResponse>>(res);
            foreach (var item in model)
            {
                item.iframe_url = item.hls_url.Replace("master.m3u8", string.Empty);
            }
            foreach (var item in model)
            {
                if (Program.videos.FirstOrDefault(m => m.iframe_url == item.iframe_url) == null)
                {
                    Program.videos.Add(item);
                }
            }
        }

        public static string SendToAnalyze(string url)
        {
            string json = @"{""task"":{""url"":""" + url + @""",""type"":""cv"",""keyframes_only"":""1"",""stop_objects"":""EXPOSED_GENITALIA_F,EXPOSED_BUTTOCS:0.80,EXPOSED_BREAST_F:0.05""}}";
            var res = HttpPost("https://api.gcdn.co/vp/api/tasks.json", json);
            Regex reg = new Regex(@"(\d+)");
            Match match = reg.Match(res);

            return match.Groups[0].Value;
        }
        public static string CheckStatusAnalyze(string id)
        {
            var res = HttpGet($"https://api.gcdn.co/vp/api/tasks/{id}.json");
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Status>(res);
            if (model != null && model.status == null)
            {
                var _model = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.StatusRes>(res);
                if (_model.error != null)
                {
                    return _model.error;
                }
                else
                {
                    string result = "";
                    foreach (var item in _model.DetectionAnnotations)
                    {
                        var __model = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.FrameResult>(item.ToString());

                        result += "Номер кадра: " + __model.FrameNo + ". Найдено: " +  __model.annotations[0].ObjectName + "\n";  
                    }
                    return result;
                }
            }
            return model.status +" " + model.progress + "%";
        }

        public static string HttpPost(string url, string json)
        {
            var res = "";
            using (HttpClient __client = new HttpClient())
            {

                WebRequest request = WebRequest.Create(new Uri(url));

                request.Headers.Add("Authorization", GetAuthString());
                request.Method = "POST";

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(json);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                try
                {
                    WebResponse response = request.GetResponse();
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            res = reader.ReadToEnd();
                        }
                    }

                    response.Close();
                }
                catch (Exception  ex)
                {
                    return null;
                }
               

            }

            return res;
        }
        public static string HttpGet(string URI)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Authorization", GetAuthString());

                using (var data = client.OpenRead(URI))
                {
                    StreamReader reader = new StreamReader(data);
                    string s = reader.ReadToEnd();
                    data.Close();
                    reader.Close();

                    return s;
                }
            }
        }

    }
}
