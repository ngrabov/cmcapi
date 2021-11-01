namespace crpt.Models
{
    public class Status
    {
        public string timestamp { get; set; }
        public int num_cryptocurrencies { get; set; }
        public object error { get; set; }
    }
}