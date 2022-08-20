using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Test;

public class UploadBaseHostTest : FeatureBase
{
   
    protected override void Inicializar()
    {
        string appSettingsProfile = Environment.GetEnvironmentVariable(ConstantesDoModulo.ENVIRONMENT_CONNECTION_STRING);
        string jsonFile = string.IsNullOrEmpty(appSettingsProfile) ? "appsettings.json"
            : $"appsettings_{appSettingsProfile}.json";
        
        var serviceProvider = new ServiceCollection()
            .UseJsonFile
            .BuildServiceProvider();
        var builder = new DbContextOptionsBuilder<MyContext>();
        var options = builder.UseInMemoryDatabase("test").UseInternalServiceProvider(serviceProvider).Options;
        MyContext dbContext = new MyContext(options);
    }

    protected override void DadoDeterminadaSituacao()
    {
        throw new NotImplementedException();
    }

    protected override void QuandoExecutado()
    {
        throw new NotImplementedException();
    }

    protected override void EntaoVerificar(Exception exception = null)
    {
        throw new NotImplementedException();
    }
}