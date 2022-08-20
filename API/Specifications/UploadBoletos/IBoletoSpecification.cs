using API.Dtos.UploadBoletos;
using Core.Entities;

namespace API.Specifications.UploadBoletos;

public interface IBoletoSpecification
{
    public void ObterBoletosGroupByCodigoCliente(ref List<UploadBoletoDto> listUploadboleto);
}