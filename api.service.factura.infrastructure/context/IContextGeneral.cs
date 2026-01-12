using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.service.factura.infrastructure.context;

public interface IContextGeneral<T> where T : class
{
    Task<List<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T> Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}