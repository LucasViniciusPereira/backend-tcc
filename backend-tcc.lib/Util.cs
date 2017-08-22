using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace backend_tcc.lib
{
    public static class Util
    {
        public static bool CpfCheck(string psCpf)
        {
            string vsCpf = psCpf.Replace(".", "");
            vsCpf = vsCpf.Replace("-", "");
            vsCpf = vsCpf.Replace(",", "");

            if (vsCpf.Length != 11)
                return false;

            bool vbIgual = true;
            for (int i = 1; i < 11 && vbIgual; i++)
                if (vsCpf[i] != vsCpf[0])
                    vbIgual = false;

            if (vbIgual || vsCpf == "12345678909")
                return false;

            int[] vaNumeros = new int[11];

            for (int i = 0; i < 11; i++)
                vaNumeros[i] = int.Parse(
                vsCpf[i].ToString());

            int vnSoma = 0;
            for (int i = 0; i < 9; i++)
                vnSoma += (10 - i) * vaNumeros[i];

            int vnResultado = vnSoma % 11;

            if (vnResultado == 1 || vnResultado == 0)
            {
                if (vaNumeros[9] != 0)
                    return false;
            }
            else if (vaNumeros[9] != 11 - vnResultado)
                return false;

            vnSoma = 0;
            for (int i = 0; i < 10; i++)
                vnSoma += (11 - i) * vaNumeros[i];

            vnResultado = vnSoma % 11;

            if (vnResultado == 1 || vnResultado == 0)
            {
                if (vaNumeros[10] != 0)
                    return false;
            }
            else
                if (vaNumeros[10] != 11 - vnResultado)
                    return false;

            return true;
        }

        public static bool CnpjCheck(string psCnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;

            int resto;

            string digito;

            string tempCnpj;

            psCnpj = psCnpj.Trim();

            psCnpj = psCnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace(",", "");

            if (psCnpj.Length != 14)
                return false;

            tempCnpj = psCnpj.Substring(0, 12);

            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;

            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return psCnpj.EndsWith(digito);
        }

        public static bool ValidateEmail(string email)
        {
            Regex rex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return rex.IsMatch(email);
        }

        private static byte[] ConverterString(string str)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetBytes(str);
        }

        public static string CriptografarSenha(string password)
        {
            SHA1 hasher = SHA1.Create();
            StringBuilder gerarString = new StringBuilder();
            byte[] array = ConverterString(password);
            array = hasher.ComputeHash(array);
            foreach (byte item in array)
            {
                gerarString.Append(item.ToString("x2"));
            }
            return gerarString.ToString().ToUpper();
        }
    }
}
