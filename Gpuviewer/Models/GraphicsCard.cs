namespace Gpuviewer.Models
{
    public class GraphicsCard
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; } = string.Empty; 
        public string Model { get; set; } = string.Empty;        
        public string? Architecture { get; set; }                
        public DateTime? ReleaseDate { get; set; }
        public double VramGb { get; set; }
        public string MemoryType { get; set; } = string.Empty;   
        public int MemoryBusWidth { get; set; }
        public double CoreClockMhz { get; set; }
        public double BoostClockMhz { get; set; }
        public int TdpWatts { get; set; }
        public int? ShaderUnits { get; set; }   
        public int? RtCores { get; set; }
        public int? TensorCores { get; set; }
        public double? BenchmarkScore { get; set; } 
        public decimal? Price { get; set; }          
    }
}
