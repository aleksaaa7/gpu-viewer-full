using System.ComponentModel.DataAnnotations;

namespace Gpuviewer.Models
{
    public class CreateGraphicsCard
    {
        [Required(ErrorMessage = "Manufacturer is required.")]
        public string Manufacturer { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; } = string.Empty;

        public string? Architecture { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Range(0.1, 128, ErrorMessage = "VRAM must be between 0.1 and 128 GB.")]
        public double VramGb { get; set; }
        [Required(ErrorMessage = "Memory type is required.")]
        public string MemoryType { get; set; } = string.Empty;
        [Required]
        [Range(1, 1024)]
        public int MemoryBusWidth { get; set; }
        [Required]
        [Range(1, 10000)]
        public double CoreClockMhz { get; set; }
        [Required]
        [Range(1, 10000)]
        public double BoostClockMhz { get; set; }
        [Required]
        [Range(1, 2000)]
        public int TdpWatts { get; set; }
        public int? ShaderUnits { get; set; }
        public int? RtCores { get; set; }
        public int? TensorCores { get; set; }
        public double? BenchmarkScore { get; set; }
        public decimal? Price { get; set; }
    }
}