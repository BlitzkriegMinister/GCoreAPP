namespace GCoreApp.Models
{
    public class Server
    {
        public int id { get; set; }
        public string role { get; set; }
        public object ip { get; set; }
        public string hostname { get; set; }
        public bool active { get; set; }
        public bool ssl { get; set; }
    }
}
