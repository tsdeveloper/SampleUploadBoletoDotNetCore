using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context;

public class UploadBoletoContext : DbContext
{
    public UploadBoletoContext(DbContextOptions<UploadBoletoContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}