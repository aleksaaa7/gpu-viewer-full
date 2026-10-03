namespace Gpuviewer.Models
{
    public class GpuJsonDto
    {
        public string? Name { get; set; }
        public string? Manufacturer { get; set; }
        public string? Architecture { get; set; }
        public string? ReleaseDate { get; set; }
        public double? BaseClock { get; set; }
        public double? BoostClock { get; set; }
        public double? MemorySize { get; set; }
        public string? MemoryType { get; set; }
        public int? MemoryBus { get; set; }
        public int? Tdp { get; set; }
        public int? Shaders { get; set; }
        public int? RtCores { get; set; }
        public int? TensorCores { get; set; }
    }
}