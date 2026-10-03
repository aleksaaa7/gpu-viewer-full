using Gpuviewer.Data;
using Gpuviewer.Models;
using Microsoft.EntityFrameworkCore;

namespace Gpuviewer.Repositories
{
    public class GraphicsCardRepository : IGraphicsCardRepository
    {
        private readonly AppDbContext _context;

        public GraphicsCardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GraphicsCard> AddAsync(GraphicsCard card)
        {
            _context.GraphicsCards.Add(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<List<GraphicsCard>> GetAllAsync()
        {
            var cards = await _context.GraphicsCards.ToListAsync();
            return cards;
        }

        public async Task<GraphicsCard?> GetByIdAsync(int id)
        {
            var card = await _context.GraphicsCards.FindAsync(id);
            return card;
        }
    }
}