using APIClient.Data;
using APIClient.Interfaces;
using APIClient.Models;

namespace APIClient.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext _context;
        public UsuarioRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateUpdate(UsuarioModel usuario)
        {
            _context.Usuario.Add(usuario);
            return Save();
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
