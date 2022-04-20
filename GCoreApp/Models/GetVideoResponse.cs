using System;
using System.Collections.Generic;

namespace GCoreApp.Models
{
    public class ConvertedVideo
    {
        public int? id { get; set; }
        public string name { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public int? size { get; set; }
        public int? progress { get; set; }
        public string status { get; set; }
        public string error { get; set; }
    }
    public class GetVideoResponse
    {
        public int id { get; set; }
        public string cv_id { get; set; }
        public string cv_status { get; set; }
        public string cv_result { get; set; }
        public string name { get; set; }
        public object description { get; set; }
        public int client_id { get; set; }
        public int? duration { get; set; }
        public string slug { get; set; }
        public string status { get; set; }
        public object share_url { get; set; }
        public object custom_iframe_url { get; set; }
        public string origin_filename { get; set; }
        public int? origin_size { get; set; }
        public object origin_storage { get; set; }
        public string origin_host { get; set; }
        public string origin_resource { get; set; }
        public int? origin_audio_channels { get; set; }
        public int? origin_height { get; set; }
        public int? origin_width { get; set; }
        public int? screenshot_id { get; set; }
        public object ad_id { get; set; }
        public object stream_id { get; set; }
        public object client_user_id { get; set; }
        public object recording_started_at { get; set; }
        public string projection { get; set; }
        public object player_id { get; set; }
        public object error { get; set; }
        public object demo { get; set; }
        public string encryption { get; set; }
        public DateTime? created_at { get; set; }
        public string hls_url { get; set; }
        public object poster_thumb { get; set; }
        public object poster { get; set; }
        public string screenshot { get; set; }
        public List<string> screenshots { get; set; }
        public int? views { get; set; }
        public List<object> folders { get; set; }
        public string iframe_url { get; set; }
        public string iframe_embed_code { get; set; }
        public string sprite { get; set; }
        public string sprite_vtt { get; set; }
        public object origin_url { get; set; }
        public List<ConvertedVideo> converted_videos { get; set; }

    }
}
