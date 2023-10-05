using APIClient.Models;

namespace APIClient.Interfaces
{
    public interface IQRCodeRepository
    {
        QrCodeModel Conteudo();
        QrCodeModel GetQRCodes();

        bool CreateQRCodes(QrCodeModel model);
    }
}
