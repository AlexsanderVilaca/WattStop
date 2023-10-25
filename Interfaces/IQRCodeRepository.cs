using APIClient.Models;

namespace APIClient.Interfaces
{
    public interface IQRCodeRepository
    {
        List<QrCodeModel> GetQrCodes();
        QrCodeModel GetQRCode(Guid id);

        bool CreateQRCode(QrCodeModel model);
        bool UpdateQRCode(QrCodeModel model);
        bool DeleteQRCode(Guid id);
        bool QRCodeExists(Guid id);
        bool Save();
    }
}
