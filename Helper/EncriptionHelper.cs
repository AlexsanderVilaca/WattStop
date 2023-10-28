using System.Security.Cryptography;
using System.Text;


namespace APIClient.Helper
{
    public static class EncriptionHelper
    {

        public static string? EncriptaString(string? dado)
        {
            if (dado == null)
                return null;

            using (var encriptador = SHA512.Create())
            {
                var dadosBytes = Encoding.UTF8.GetBytes(dado);
                var dadoEncriptado = encriptador.ComputeHash(dadosBytes);
                return Convert.ToBase64String(dadoEncriptado);
            }
        }
    }
}
