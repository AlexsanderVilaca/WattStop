﻿using APIClient.Models;

namespace APIClient.Interfaces
{
    public interface IAvaliacaoRepository
    {
        List<AvaliacaoModel> GetAvaliacoes();   
        AvaliacaoModel GetAvaliacao(Guid id);
        bool CreateAvaliacao(AvaliacaoModel model);
        bool UpdateAvaliacao(AvaliacaoModel model);
        bool DeleteAvaliacao(Guid id);
        bool AvaliacaoExists(Guid id);
        bool Save();
    }
}
