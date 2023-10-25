﻿using APIClient.DTO;
using APIClient.Models;

namespace APIClient.Interfaces
{
    public interface IPontoRecargaRepository
    {
        List<PontoRecargaModel> GetPontosRecarga();
        PontoRecargaModel GetPontoRecarga(Guid id);
        bool CreatePontoRecarga(PontoRecargaModel pontoRecarga);
        bool UpdatePontoRecarga(PontoRecargaModel pontoRecarga);
        bool Save();
    }
}
