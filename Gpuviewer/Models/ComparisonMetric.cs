namespace Gpuviewer.Models
{
    public class ComparisonMetric
    {
        public string FieldName { get; set; } = string.Empty;
        public string BetterCard { get; set; } = string.Empty;
        public double PercentageDifference { get; set; }
    }
}