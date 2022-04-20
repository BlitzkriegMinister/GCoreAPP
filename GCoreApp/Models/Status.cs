using Newtonsoft.Json;
using System.Collections.Generic;

namespace GCoreApp.Models
{
    public class Status
    {
        public string status { get; set; }
        public int progress { get; set; }
    }
    public class FileInfo
    {
        public long start_of_processing { get; set; }
        public int elapsed_time { get; set; }
        public string url { get; set; }
        public string stop_objects { get; set; }
        public long end_of_processing { get; set; }
    }

    public class StatusRes
    {
        [JsonProperty("detection-annotations")]
        public List<object> DetectionAnnotations { get; set; }
        public FileInfo file_info { get; set; }
        public string error { get; set; }
    }
}
