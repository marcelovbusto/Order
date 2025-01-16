using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Order order) => _context.Orders.Add(order);
    public void Delete(Order order) => _context.Orders.Remove(order);
    public void Update(Order order) => _context.Orders.Update(order);
    public async Task<bool> ExistsAsync(OrderId id) => await _context.Orders.AnyAsync(order => order.Id == id);
    public async Task<Order?> GetByIdAsync(OrderId id) => await _context.Orders.SingleOrDefaultAsync(c => c.Id == id);
    public async Task<List<Order>> GetAll() => await _context.Orders.ToListAsync();
}