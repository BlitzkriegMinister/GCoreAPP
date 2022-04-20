using Newtonsoft.Json;
using System.Collections.Generic;

namespace GCoreApp.Models
{
    public class FrameResult
    {
        [JsonProperty("frame-no")]
        public int FrameNo { get; set; }

        [JsonProperty("frame-attributes")]
        public FrameAttributes FrameAttributes { get; set; }
        public List<Annotation> annotations { get; set; }

    }
}
