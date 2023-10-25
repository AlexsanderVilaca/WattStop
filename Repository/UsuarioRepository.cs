using APIClient.Data;
using APIClient.Interfaces;
using APIClient.Models;
using AutoMapper;
using DataNoSQL.DAL;
using DataNoSQL.DTC;
using MongoDB.Driver;

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
        public bool CreateUsuario(UsuarioModel usuario)
        {
            try
            {
                _context.Usuario.Add(usuario);
                if (Save())
                {
                    var usuarioMap = _mapper.Map<UsuariosDTCNoSQL>(usuario);
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
            try
            {
                var user = GetUsuario(usuario);
                _context.Usuario.Remove(user);
                if (Save())
                {
                    _DAL.Delete(user.Id);
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
        public List<UsuarioModel> GetUsuarios()
        {
            return _context.Usuario.OrderBy(x => x.User).ToList();
        }
        public bool UpdateUsuario(UsuarioModel model)
        {
            try
            {
                var usuario = GetUsuario(model.User);
                usuario.Secret= model.Secret;
                usuario.Ativo = model.Ativo;
                usuario.DT_Alteracao = DateTime.Now;
                usuario.TP_Acesso = model.TP_Acesso;

                if (Save())
                {
                    var usuarioMap = _mapper.Map<UsuariosDTCNoSQL>(model);
                    var filtro = Builders<UsuariosDTCNoSQL>.Filter.Eq("_id",model.Id);
                    _DAL.Update(filtro, usuarioMap);
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
        public bool UsuarioExists(string usuario)
        {
            return _context.Usuario.FirstOrDefault(x => x.User.ToUpper() == usuario.ToUpper()) != null;
        }
    }
}
