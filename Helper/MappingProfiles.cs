using APIClient.DTO;
using APIClient.Models;
using AutoMapper;

namespace APIClient.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AvaliacaoModel,AvaliacaoDTO>();
            CreateMap<EmpresaModel, EmpresaDTO>();
            CreateMap<PontoRecargaModel, PontoRecargaDTO>();
            CreateMap<HistoricoPontoRecargaModel, HistoricoPontoRecargaDTO>();
            CreateMap<QrCodeModel, QrCodeDTO>();
            CreateMap<UsuarioModel, UsuarioDTO>();

            CreateMap<AvaliacaoDTO, AvaliacaoModel>();
            CreateMap<EmpresaDTO, EmpresaModel>();
            CreateMap<PontoRecargaDTO, PontoRecargaModel>();
            CreateMap<HistoricoPontoRecargaDTO, HistoricoPontoRecargaModel>();
            CreateMap<QrCodeDTO, QrCodeModel>();
            CreateMap<UsuarioDTO, UsuarioModel>();
        }
    }
}
