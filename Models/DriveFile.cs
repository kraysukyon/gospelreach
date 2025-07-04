namespace GospelReachCapstone.Models
{
    public class DriveFile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string WebViewLink { get; set; }
        public string ThumbnailLink { get; set; }

        // Size is returned as a string in the API, so make it string to be safe
        public string Size { get; set; }
    }
}
