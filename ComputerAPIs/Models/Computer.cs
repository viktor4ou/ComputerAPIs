namespace ComputerAPIs.Models
{
    public class Computer
    {
        public int ComputerId { get; set; }
        public string Model { get; set; }
        public string Processor { get; set; }
        public int Memory { get; set; }
        public string Graphics { get; set; }
        public decimal Price { get; set; }
    }
}
