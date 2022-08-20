using Core.Entities;

namespace Core.Interfaces.UploadBoletos;

public interface IUploadBoletoService
{
    Task<UploadBoleto> CreateUploadBoletoAsync(UploadBoleto uploadBoleto);
    Task<List<UploadBoleto>> CreateUploadBoletoListAsync(List<UploadBoleto> listUploadBoletosDto);
}