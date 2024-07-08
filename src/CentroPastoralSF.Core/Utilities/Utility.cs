namespace CentroPastoralSF.Core.Utilities
{
    public static class Utility
    {
        public static string FormatarTelefone(this string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
            {
                return string.Empty;
            }

            var primeiraParteTelefone = telefone.Substring(0, 5);
            var segundaParteTelefone = telefone.Substring(5, 4);

            telefone = $"{primeiraParteTelefone}-{segundaParteTelefone}";

            return telefone;
        }

        public static string FormatarCep(this string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                return string.Empty;
            }

            var primeiraParteCep = cep.Substring(0, 5);
            var segundaParteCep = cep.Substring(5, 3);

            cep = $"{primeiraParteCep}-{segundaParteCep}";

            return cep;
        }


        public static string RemoverFormatacaoTelefone(this string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
            {
                return string.Empty;
            }

            return telefone.Replace("-", "");
        }

        public static string RemoverFormatacaoCep(this string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                return string.Empty;
            }

            return cep.Replace("-", "");
        }
    }
}