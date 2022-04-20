using Newtonsoft.Json;
using System.Collections.Generic;

namespace GCoreApp.Models
{
    public class FrameAttributes
    {
        public bool key_frame { get; set; }
    }

    public class Annotation
    {
        [JsonProperty("object-score")]
        public double ObjectScore { get; set; }

        [JsonProperty("object-name")]
        public string ObjectName { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class DetectionAnnotation
    {
        [JsonProperty("frame-no")]
        public int FrameNo { get; set; }

        [JsonProperty("frame-attributes")]
        public FrameAttributes FrameAttributes { get; set; }
        public List<Annotation> annotations { get; set; }
    }

    public class _FileInfo
    {
        public long start_of_processing { get; set; }
        public int elapsed_time { get; set; }
        public string url { get; set; }
        public string stop_objects { get; set; }
        public long end_of_processing { get; set; }
    }

    public class CVTaskResut
    {
        [JsonProperty("detection-annotations")]
        public List<DetectionAnnotation> DetectionAnnotations { get; set; }
        public _FileInfo file_info { get; set; }
    }
}
