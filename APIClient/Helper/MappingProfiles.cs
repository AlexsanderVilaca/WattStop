using APIClient.DTO;
using APIClient.Models;
using AutoMapper;
using DataNoSQL.DTC;

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

            CreateMap<AvaliacaoDTO, AvaliacaoDTCNoSQL>();
            CreateMap<EmpresaDTO, EmpresaDTCNoSQL>();
            CreateMap<PontoRecargaDTO, PontoRecargaDTCNoSQL>();
            CreateMap<HistoricoPontoRecargaDTO, HistoricoPontoRecargaDTCNoSQL>();
            CreateMap<UsuarioDTO, UsuariosDTCNoSQL>();

            CreateMap<AvaliacaoDTCNoSQL, AvaliacaoModel>();
            CreateMap<EmpresaDTCNoSQL, EmpresaModel>();
            CreateMap<PontoRecargaDTCNoSQL, PontoRecargaModel>();
            CreateMap<HistoricoPontoRecargaDTCNoSQL, HistoricoPontoRecargaDTO>();
            CreateMap<UsuariosDTCNoSQL, UsuarioModel>();

            CreateMap<AvaliacaoModel, AvaliacaoDTCNoSQL>();
            CreateMap<EmpresaModel, EmpresaDTCNoSQL>();
            CreateMap<PontoRecargaModel, PontoRecargaDTCNoSQL>();
            CreateMap<HistoricoPontoRecargaModel, HistoricoPontoRecargaDTCNoSQL>();
            CreateMap<UsuarioModel, UsuariosDTCNoSQL>();

            CreateMap<AvaliacaoDTCNoSQL, AvaliacaoDTO>();
            CreateMap<EmpresaDTCNoSQL, EmpresaDTO>();
            CreateMap<PontoRecargaDTCNoSQL, PontoRecargaDTO>();
            CreateMap<HistoricoPontoRecargaDTCNoSQL, HistoricoPontoRecargaModel>();
            CreateMap<UsuariosDTCNoSQL, UsuarioDTO>();

        }
    }
}
