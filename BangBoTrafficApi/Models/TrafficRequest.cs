namespace BangBoTrafficApi.Models
{
    public class TrafficRequest
    {
        public string Lane { get; set; }   // A, B, C
        public string Status { get; set; } // Green, Yellow, Red
    }
}