using APIClient.DTO;
using APIClient.Models;
using AutoMapper;

namespace APIClient.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmpresaModel, EmpresaDTO>();
            CreateMap<PontoRecargaModel, PontoRecargaDTO>();
            CreateMap<HistoricoPontoRecargaModel, HistoricoPontoRecargaDTO>();
            CreateMap<QrCodeModel, QrCodeDTO>();

            CreateMap<EmpresaDTO, EmpresaModel>();
            CreateMap<PontoRecargaDTO, PontoRecargaModel>();
            CreateMap<HistoricoPontoRecargaDTO, HistoricoPontoRecargaModel>();
            CreateMap<QrCodeDTO, QrCodeModel>();
        }
    }
}
