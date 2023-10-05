using APIClient.Models;

namespace APIClient.Interfaces
{
    public interface IQRCodeRepository
    {
        List<QrCodeModel> GetQrCodes();
        QrCodeModel GetQRCode(Guid id);

        bool CreateQRCode(QrCodeModel model);
        bool Save();
    }
}
