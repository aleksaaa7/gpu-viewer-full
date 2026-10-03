using Gpuviewer.Models;

namespace Gpuviewer.Repositories
{
    public interface IGraphicsCardRepository
    {
        Task<List<GraphicsCard>> GetAllAsync();
        Task<GraphicsCard?> GetByIdAsync(int id);
        Task<GraphicsCard> AddAsync(GraphicsCard card);

    }
}
