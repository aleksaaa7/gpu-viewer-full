namespace Gpuviewer.Models
{
    public class CompareResultDto
    {
        public GraphicsCard? Card1 { get; set; }
        public GraphicsCard? Card2 { get; set; }
        public string? NewerCard { get; set; }
        public List<ComparisonMetric> Metrics { get; set; } = new();
        public string? OverallBetterCard { get; set; }
        public double OverallPercentage { get; set; }
    }
}