using APIClient.Data;
using APIClient.Interfaces;
using APIClient.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace APIClient.Repository
{
    public class QRCodeRepository : IQRCodeRepository
    {
        private readonly DataContext _context;

        public QRCodeRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateQRCode(QrCodeModel model)
        {
            _context.QrCode.Add(model);
            return Save();
        }

        public QrCodeModel GetQRCode(Guid id)
        {
            return _context.QrCode.FirstOrDefault(x=>x.Id==id);
        }

        public List<QrCodeModel> GetQrCodes()
        {
            return _context.QrCode.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
