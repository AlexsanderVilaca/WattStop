namespace APIClient.Helper
{
    public class ValidationHelper
    {
        public bool ValidarCNPJ(string cnpj, out string msg)
        {
            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
            msg = "Este CNPJ é válido";
            if (cnpj.Length != 14)
            {
                msg = "O CNPJ deve possuir 14 dígitos";
                return false;
            }
            int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string parte1 = cnpj.Substring(0, 12);
            string digitoVerificador = cnpj.Substring(12);

            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(parte1[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;

            int digito1 = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            parte1 = parte1 + digito1;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(parte1[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;

            int digito2 = resto < 2 ? 0 : 11 - resto;

            return digitoVerificador == $"{digito1}{digito2}";
        }

    }
}
