using Gpuviewer.Models;
using Gpuviewer.Repositories;

namespace Gpuviewer.Services
{
    public class GraphicsCardService : IGraphicsCardService
    {
        private readonly IGraphicsCardRepository _repository;

        public GraphicsCardService(IGraphicsCardRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GraphicsCard>> GetAllCardsAsync()
        {
            var cards = await _repository.GetAllAsync();
            return cards;
        }

        public async Task<GraphicsCard?> GetCardByIdAsync(int id)
        {
            var card = await _repository.GetByIdAsync(id);
            return card;
        }
       

        public async Task<CompareResultDto?> CompareCardsAsync(int id1, int id2)
        {
            var card1 = await _repository.GetByIdAsync(id1);
            var card2 = await _repository.GetByIdAsync(id2);

            if (card1 == null || card2 == null)
                return null;

            var result = new CompareResultDto
            {
                Card1 = card1,
                Card2 = card2
            };

            
            if (card1.ReleaseDate.HasValue && card2.ReleaseDate.HasValue)
            {
                result.NewerCard = card1.ReleaseDate.Value > card2.ReleaseDate.Value ? card1.Model : card2.Model;
            }

            
            var metrics = new List<ComparisonMetric>
            {
                CompareValues("VRAM (GB)", card1.VramGb, card2.VramGb, card1.Model, card2.Model),
                CompareValues("Boost Clock (MHz)", card1.BoostClockMhz, card2.BoostClockMhz, card1.Model, card2.Model),
                CompareValues("Core Clock (MHz)", card1.CoreClockMhz, card2.CoreClockMhz, card1.Model, card2.Model),
                CompareValues("Memory Bus Width", card1.MemoryBusWidth, card2.MemoryBusWidth, card1.Model, card2.Model),
                CompareValues("TDP (W)", card1.TdpWatts, card2.TdpWatts, card1.Model, card2.Model, lowerIsBetter: true)
            };
            //here comparing and adding in new list 
            if (card1.ShaderUnits.HasValue && card2.ShaderUnits.HasValue)
            {
                metrics.Add(CompareValues("Shader Units", card1.ShaderUnits.Value, card2.ShaderUnits.Value, card1.Model, card2.Model));
            }

            if (card1.RtCores.HasValue && card2.RtCores.HasValue)
            {
                metrics.Add(CompareValues("RT Cores", card1.RtCores.Value, card2.RtCores.Value, card1.Model, card2.Model));
            }

            if (card1.TensorCores.HasValue && card2.TensorCores.HasValue)
            {
                metrics.Add(CompareValues("Tensor Cores", card1.TensorCores.Value, card2.TensorCores.Value, card1.Model, card2.Model));
            }

            result.Metrics = metrics;

            double signedSum = 0;
            int comparableMetrics = 0;

            foreach (var metric in metrics)
            {
                if (metric.BetterCard == card1.Model)
                {
                    signedSum += metric.PercentageDifference;
                    comparableMetrics++;
                }
                else if (metric.BetterCard == card2.Model)
                {
                    signedSum -= metric.PercentageDifference;
                    comparableMetrics++;
                }
              
            }

            double signedAverage = comparableMetrics > 0
                ? Math.Round(signedSum / comparableMetrics, 2)
                : 0;

            if (signedAverage > 0)
            {
                result.OverallBetterCard = card1.Model;
                result.OverallPercentage = signedAverage;
            }
            else if (signedAverage < 0)
            {
                result.OverallBetterCard = card2.Model;
                result.OverallPercentage = Math.Abs(signedAverage);
            }
            else
            {
                result.OverallBetterCard = "Equal";
                result.OverallPercentage = 0;
            }

            return result;
        }

        private ComparisonMetric CompareValues(string fieldName, double value1, double value2,
            string card1Name, string card2Name, bool lowerIsBetter = false)
        {
            if (value1 == value2)
            {
                return new ComparisonMetric
                {
                    FieldName = fieldName,
                    BetterCard = "euqal",
                    PercentageDifference = 0
                };
            }

            bool card1IsBetter = lowerIsBetter ? value1 < value2 : value1 > value2;

            double betterValue = card1IsBetter ? value1 : value2;
            double worseValue = card1IsBetter ? value2 : value1;
            string betterCardName = card1IsBetter ? card1Name : card2Name;

            double percentage;
            if (worseValue == 0)
            {
                percentage = 100;
            }
            else
            {
                percentage = Math.Round((betterValue - worseValue) / worseValue * 100, 2);
            }

            return new ComparisonMetric
            {
                FieldName = fieldName,
                BetterCard = betterCardName,
                PercentageDifference = percentage
            };
        }

        public async Task<GraphicsCard> CreateGraphicsCardAsync(CreateGraphicsCard dto){
            var card = new GraphicsCard
            {
                Manufacturer = dto.Manufacturer,
                Model = dto.Model,
                Architecture = dto.Architecture,
                ReleaseDate = dto.ReleaseDate,
                VramGb = dto.VramGb,
                MemoryType = dto.MemoryType,
                MemoryBusWidth = dto.MemoryBusWidth,
                CoreClockMhz = dto.CoreClockMhz,
                BoostClockMhz = dto.BoostClockMhz,
                TdpWatts = dto.TdpWatts,
                ShaderUnits = dto.ShaderUnits,
                RtCores = dto.RtCores,
                TensorCores = dto.TensorCores,
                BenchmarkScore = dto.BenchmarkScore,
                Price = dto.Price
            };
            var saved=await _repository.AddAsync(card);
            return saved;
        }
       
    }
}