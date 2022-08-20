namespace Core.Test;

public abstract class BaseTest
{
    public void Executar()
    {
        bool emExecucao = true;
        
        DadoDeterminadasSituacao();
        
        try
        {
            emExecucao = false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            Finalizar();
        }
    }

    protected abstract void DadoDeterminadasSituacao();
    protected virtual void Finalizar(){}
}