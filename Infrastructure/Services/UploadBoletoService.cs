using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.UploadBoletos;

namespace Infrastructure.Services;

public class UploadBoletoService : IUploadBoletoService
{
    private readonly IUnitOfWork _unitOfWork;

    public UploadBoletoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<UploadBoleto> CreateUploadBoletoAsync(UploadBoleto uploadBoleto)
    {
        _unitOfWork.Repository<UploadBoleto>().Add(uploadBoleto);

        var result = await _unitOfWork.Complete();

        if (result <= 0) return null;

        return uploadBoleto;
    }

    public async Task<List<UploadBoleto>> CreateUploadBoletoListAsync(List<UploadBoleto> listUploadBoletosDto)
    {
        foreach (var uploadBoleto in listUploadBoletosDto)
        {
           await CreateUploadBoletoAsync(uploadBoleto);
        }

        return listUploadBoletosDto;
    }
}