using APIClient.DTO;
using APIClient.Models;
using DataNoSQL.DTC;

namespace APIClient.Interfaces
{
    public interface IUsuarioRepository
    {
        bool CreateUsuario(UsuarioModel usuario);
        bool Delete(String usuario);
        bool UpdateUsuario(UsuarioModel model);
        UsuariosDTCNoSQL GetUsuario(String usuario);
        bool ValidateUsuario(String usuario, String secret);
        bool UsuarioExists(String usuario);
        List<UsuarioModel> GetUsuarios();
    }
}
