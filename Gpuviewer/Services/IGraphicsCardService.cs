using Gpuviewer.Models;

namespace Gpuviewer.Services
{
    public interface IGraphicsCardService
    {
        Task<List<GraphicsCard>> GetAllCardsAsync();
        Task<GraphicsCard?> GetCardByIdAsync(int id);
        Task<CompareResultDto?> CompareCardsAsync(int id1, int id2);
        Task<GraphicsCard> CreateGraphicsCardAsync(CreateGraphicsCard dto);
    }
}