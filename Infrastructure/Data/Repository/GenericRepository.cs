using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly UploadBoletoContext _context;

    public GenericRepository(UploadBoletoContext context)
    {
        _context = context;
    }
    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }
}