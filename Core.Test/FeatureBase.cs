using NUnit.Framework;

namespace Core.Test;

public abstract class FeatureBase
{
    public void Executar()
    {
        bool emExecucao = true;

        Inicializar();

        DadoDeterminadaSituacao();

        try
        {
            QuandoExecutado();
            emExecucao = false;

            EntaoVerificar();
        }
        catch (Exception e) when (false == (e is AssertionException))
        {
            if (emExecucao)
                EntaoVerificar(e);
            else
                throw;
        }
        finally
        {
            Finalizar();
        }
    }

    protected abstract void Inicializar();
    protected abstract void DadoDeterminadaSituacao();
    protected abstract void QuandoExecutado();
    protected abstract void EntaoVerificar(Exception exception = null);

    protected virtual void Finalizar()
    { }
}