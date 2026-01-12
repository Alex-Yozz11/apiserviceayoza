using Microsoft.EntityFrameworkCore;

namespace api.service.factura.infrastructure.context;

public class ContextGeneral<T> : IContextGeneral<T> where T : class
{
    public FacturaDbContext _context { get; }

    public ContextGeneral(FacturaDbContext context)
    {
        _context = context;
    }

    public async Task<List<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await _context.Set<T>().FindAsync(id);
        // Alternativa si tu entidad no usa FindAsync:
        // return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<T> Add(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Update(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}