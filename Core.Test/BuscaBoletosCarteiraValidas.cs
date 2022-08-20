using NUnit.Framework;

namespace Core.Test;

[TestFixture]
public class BuscaBoletosCarteira
{
    [Test] public void Testar_BuscaBoletosCarteira_SomenteClienteA()
    {
        new Testar_SomenteClienteA().Executar();
    }
}

public class Testar_SomenteClienteA
{
}