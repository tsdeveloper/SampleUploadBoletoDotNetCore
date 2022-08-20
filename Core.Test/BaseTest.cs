using FluentValidation;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Core.Test;

public class BaseTest
{
    protected UploadBoletoContext ctx;
    public BaseTest(UploadBoletoContext ctx = null)
    {
        this.ctx = ctx ?? GetInMemoryDBContext();
    }
    protected UploadBoletoContext GetInMemoryDBContext()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlite()
            .BuildServiceProvider();
        var builder = new DbContextOptionsBuilder<UploadBoletoContext>();
        var options = builder.UseSqlite("test").UseInternalServiceProvider(serviceProvider).Options;
        UploadBoletoContext dbContext = new UploadBoletoContext(options);
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        return dbContext;
    }

    protected void CheckError<T>(AbstractValidator<T> validator, int ErrorCode, T vm)
    {
        var val = validator.Validate(vm);
        Assert.False(val.IsValid);

        if (!val.IsValid)
        {
            bool hasError = val.Errors.Any(a => a.ErrorCode.Equals(ErrorCode.ToString()));
            Assert.True(hasError);
        }
    }
}