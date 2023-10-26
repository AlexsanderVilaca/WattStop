﻿using APIClient.Data;
using APIClient.DTO;
using APIClient.Interfaces;
using APIClient.Models;
using AutoMapper;
using DataNoSQL.DAL;
using DataNoSQL.DTC;
using MongoDB.Driver;

namespace APIClient.Repository
{
    public class PontoRecargaRepository : IPontoRecargaRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly PontoRecargaDALNoSQL _DAL;
        public PontoRecargaRepository(DataContext context, IMapper mapper, PontoRecargaDALNoSQL dal)
        {
            _context = context;
            _mapper = mapper;
            _DAL = dal;
        }

        public bool CreatePontoRecarga(PontoRecargaModel pontoRecarga)
        {
            try
            {
                _context.PontoRecarga.Add(pontoRecarga);
                if (Save())
                {
                    var pontoRecargaMap = _mapper.Map<PontoRecargaDTCNoSQL>(pontoRecarga);
                    _DAL.Insert(pontoRecargaMap);
                    return true;
                }
                else
                    return false;

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }

        public bool DeletePontoRecarga(Guid id)
        {
            try
            {
                var pontoRecarga = GetPontoRecarga(id);
                _context.PontoRecarga.Remove(pontoRecarga);
                if (Save())
                {
                    _DAL.Delete(id);
                    return true;
                }
                else
                    return false;

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }

        public PontoRecargaModel GetPontoRecarga(Guid id)
        {
            return _context.PontoRecarga.FirstOrDefault(x => x.Id == id);
        }

        public List<PontoRecargaModel> GetPontosRecarga()
        {
            return _context.PontoRecarga.OrderBy(p => p.Id).ToList();
        }

        public List<PontoRecargaModel> GetPontosRecargaByEmpresa(Guid empresaId)
        {
            return _context.PontoRecarga.Where(p => p.EmpresaId == empresaId).ToList();
        }

        public bool PontoRecargaEmpresaExists(Guid empresaId)
        {
            return _context.PontoRecarga.FirstOrDefault(x => x.EmpresaId == empresaId) != null;
        }

        public bool PontoRecargaExists(Guid id)
        {
            return _context.PontoRecarga.FirstOrDefault(x => x.Id == id) != null;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePontoRecarga(PontoRecargaModel pontoRecargaModel)
        {
            try
            {
                var pontoRecarga = GetPontoRecarga(pontoRecargaModel.Id);

                pontoRecarga.Localizacao = pontoRecargaModel.Localizacao;
                pontoRecarga.EmpresaId = pontoRecargaModel.EmpresaId;
                pontoRecarga.TipoCarregador = pontoRecargaModel.TipoCarregador;

                if (Save())
                {
                    var pontoRecargaMap = _mapper.Map<PontoRecargaDTCNoSQL>(pontoRecargaModel);
                    var filtro = Builders<PontoRecargaDTCNoSQL>.Filter.Eq("_id", pontoRecargaModel.Id);
                    _DAL.Update(filtro, pontoRecargaMap);
                    return true;
                }
                else
                    return false;

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }
    }
}
