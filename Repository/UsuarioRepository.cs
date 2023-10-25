using APIClient.Data;
using APIClient.Interfaces;
using APIClient.Models;
using AutoMapper;
using DataNoSQL.DAL;
using DataNoSQL.DTC;

namespace APIClient.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UsuariosDALNoSQL _DAL;
        public UsuarioRepository(DataContext context,IMapper mapper, UsuariosDALNoSQL dal)
        {
            _context = context;
            _mapper = mapper;
            _DAL = dal;
        }
        public bool CreateUpdate(UsuarioModel usuario)
        {
            try
            {
                _context.Usuario.Add(usuario);
                if (Save())
                {
                    var usuarioModel = _context.Usuario.FirstOrDefault(x => x.Id == usuario.Id);
                    var usuarioMap = _mapper.Map<UsuariosDTCNoSQL>(usuarioModel);
                    _DAL.Insert(usuarioMap);
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
        public bool Delete(string usuario)
        {
            throw new NotImplementedException();
        }
        public UsuarioModel GetUsuario(string user)
        {
            return _context.Usuario.FirstOrDefault(x => x.User == user);
        }
        public bool ValidateUsuario(string user, string secret)
        {
            if (!string.IsNullOrEmpty(user))
            {
                var usr = GetUsuario(user);
                if (usr != null)
                {
                    if (usr.Secret == secret)
                        return true;
                }
            }
            return false;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return (saved > 0);
        }
    }
}
