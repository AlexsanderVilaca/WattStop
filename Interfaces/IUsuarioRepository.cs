using APIClient.Models;

namespace APIClient.Interfaces
{
    public interface IUsuarioRepository
    {
        bool CreateUpdate(UsuarioModel usuario);
        bool Delete(String usuario);
        UsuarioModel GetUsuario(String usuario);
        bool ValidateUsuario(String usuario, String secret);
    }
}
