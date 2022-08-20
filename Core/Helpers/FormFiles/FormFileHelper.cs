using System.Text;
using Core.Entities;
using Core.Enums.UploadBoletos;
using Core.Extensions;
using Microsoft.AspNetCore.Http;
namespace Core.Helpers.FormFiles;

public static class  FormFileHelper
{
    public static async Task<List<UploadBoleto>> ExtrairDadosBoletoToDto(Stream streamFile)
    {
        var result = new StringBuilder();
        result = await LerLinhasArquivoTxtBoleto(streamFile, result);

        return PopularUploadBoletoFromArquivoTxt(result);
    }

    private static List<UploadBoleto> PopularUploadBoletoFromArquivoTxt(StringBuilder result)
    {
        var listUploadBoletos = new List<UploadBoleto>(); 
        var resultReader = new StringReader(result.ToString());
        while (resultReader.Read() > 0)
        {
            var returnReader =
                resultReader.ReadLine()?.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries) ?? null;
            if (returnReader != null && returnReader.Length > 0)
            {
                var enti = returnReader[0].Split("#", StringSplitOptions.RemoveEmptyEntries);

                var uploadBoletos = new UploadBoleto
                {
                    DataOperacao = StringExtensions.CheckOutOfRange(0, enti)
                        ? Convert.ToDateTime(enti[0]
                            .ConvertStringToDateTime())
                        : default,
                    CodigoCliente = StringExtensions.CheckOutOfRange(1, enti) ? enti[1] : string.Empty,
                    TipoOperacao = StringExtensions.CheckOutOfRange(2, enti) ? enti[2] : string.Empty,
                    IdBolsa = StringExtensions.CheckOutOfRange(3, enti) ? enti[3] : string.Empty,
                    CodigoAtivo = StringExtensions.CheckOutOfRange(4, enti) ? enti[4] : string.Empty,
                    Corretora = StringExtensions.CheckOutOfRange(5, enti) ? enti[5] : string.Empty,
                    Quantidade = StringExtensions.CheckOutOfRange(6, enti) ? enti[6].ParseToInt() : 0,
                    PrecoUnitario = StringExtensions.CheckOutOfRange(7, enti) ? enti[7].ParseToDecimal() : 0M
                };
                var validator = new BoletosValidator();

                var resultValidator = validator.Validate(uploadBoletos);

                if (!resultValidator.IsValid)
                {
                    uploadBoletos.Mensagem.RemoveAt(0);
                    foreach (var error in resultValidator.Errors)
                    {
                        uploadBoletos.Mensagem.Add(
                            new Mensagem { Messagem = error.ErrorMessage});
                        uploadBoletos.StatusBoleto = EnumExtensions.GetDescription(EnumMensagemStatus.Falha);
                    }
                }

                listUploadBoletos.Add(uploadBoletos);
            }
        }

        return listUploadBoletos;
    }

    private static async Task<StringBuilder> LerLinhasArquivoTxtBoleto(Stream streamFile, StringBuilder result)
    {
        using (var stream = streamFile)
        {
            using (var reader = new StreamReader(stream))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadToEndAsync());
            }
        }

        return RemoveUltimoCaracterArquivoTxt(result);
    }

    private static StringBuilder RemoveUltimoCaracterArquivoTxt(StringBuilder result)
    {
        return result.Replace("99#RV", "").Replace("0#RV", "");
    }
}