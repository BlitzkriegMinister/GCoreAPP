using System.Collections.Generic;

namespace GCoreApp.Models
{
    public class GetUrlAndTokenResponse
    {
        public List<Server> servers { get; set; }
        public string token { get; set; }
        public GetVideoResponse video { get; set; }
    }
}
