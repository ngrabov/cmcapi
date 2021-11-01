namespace crpt.Models
{
    public class Datum
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public double? max_supply { get; set; }
        public double circulating_supply { get; set; }
        public int cmc_rank { get; set; }
        public string[] tags { get; set; }
        public Quote Quote { get; set; }
    }
}