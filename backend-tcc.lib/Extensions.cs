using backend_tcc.lib.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace backend_tcc.lib
{
    public static class Extensions
    {
        public static void AddRange<T>(this ICollection<T> collection, ICollection<T> data)
        {
            foreach (var item in data)
                collection.Add(item);
        }

        public static string GetDescricao(this Enum eventId)
        {
            var result = GetAttributeValueGeneric(eventId, (DescricaoEnumeradorAttribute a) => a.Value);
            return result;
        }

        public static string GetExtencaoArquivo(this eTipoArquivoExportacao tp)
        {
            return "." + tp.ToString().ToLower();
        }

        public static TValue GetAttributeValueGeneric<TAttribute, TValue>(Enum eventId, Func<TAttribute, TValue> selector)
           where TAttribute : Attribute
        {
            var result = eventId.GetType().GetField(eventId.ToString())
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .Select(selector);

            return result.SingleOrDefault();
        }

        /// <summary>
        /// Retorna um valor único ou tenta retornar uma propriedade decorada com o atributo [AttributeNullObject]
        /// que será tratará de não retornar nulo evitando erro de referência de objeto. Pattern NullObject
        /// </summary>
        /// <typeparam name="T">Tipo Base que contém a propriedade 'static' do pattern NullObject</typeparam>
        /// <param name="source">Lista de dados</param>        
        /// <returns>Objeto único ou NullObject ou null</returns>
        public static T SingleOrDefault<T>(this IQueryable<T> source) where T : class
        {
            var result = System.Linq.Enumerable.SingleOrDefault(source);


            return result ?? GetNullObject<T>();
        }

        /// <summary>
        /// Retorna um valor único ou tenta retornar uma propriedade decorada com o atributo [AttributeNullObject]
        /// que será tratará de não retornar nulo evitando erro de referência de objeto. Pattern NullObject
        /// </summary>
        /// <typeparam name="T">Tipo Base que contém a propriedade 'static' do pattern NullObject</typeparam>
        /// <param name="source">Lista de dados</param>        
        /// <returns>Objeto único ou NullObject ou null</returns>
        public static T SingleOrDefault<T>(this ICollection<T> source) where T : class
        {
            var result = System.Linq.Enumerable.SingleOrDefault(source);


            return result ?? GetNullObject<T>();
        }

        private static T GetNullObject<T>() where T : class
        {
            var properties = typeof(T).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)
                            .Where(c => c.GetCustomAttribute(typeof(AttributeNullObject), true) != null);

            try
            {
                foreach (var property in properties)
                {
                    var valueFor = (T)property.GetValue(null);
                    return valueFor;
                }
            }
            catch (InvalidCastException e)
            {
                throw new ApplicationException("Para utilizar o padrão NullObject, não deverá utilizar OfType<T> para forçar a conversão do tipo base para o tipo específico. Faça cast nos predicados.", e);
            }

            return null;
        }

        /// <summary>
        /// Retorna um valor único ou tenta retornar uma propriedade decorada com o atributo [AttributeNullObject]
        /// que será tratará de não retornar nulo evitando erro de referência de objeto. Pattern NullObject
        /// </summary>
        /// <typeparam name="T">Tipo Base que contém a propriedade 'static' do pattern NullObject</typeparam>
        /// <param name="source">Lista de dados</param>
        /// <param name="predicate">Condições</param>
        /// <returns>Objeto único ou NullObject ou null</returns>
        public static T SingleOrDefault<T>(this ICollection<T> source, Func<T, bool> predicate) where T : class
        {
            var result = System.Linq.Enumerable.SingleOrDefault(source, predicate);

            return result ?? GetNullObject<T>();
        }
    }

    /// <summary>
    /// Fonte: http://ivanmeirelles.wordpress.com/2012/10/27/escrever-valores-por-extenso-em-c
    /// </summary>
    public static class MoedaExtenso
    {
        /// <summary>
        /// O método toExtenso recebe um valor do tipo decimal
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static string MoedaPorExtenso(this decimal valor)
        {
            if (valor < 0 | valor >= 1000000000000000)
                return "Valor por extenso não suportado.";
            else
            {
                if (valor.Equals(0))
                    return "Zero";

                string strValor = valor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;

                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += PorExtenso(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " TRILHÃO" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " TRILHÕES" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " BILHÃO" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " BILHÕES" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " MILHÃO" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " MILHÕES" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " MIL" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " E " : string.Empty);

                    if (i == 12)
                    {
                        if (valor_por_extenso.Length > 8)
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "BILHÃO" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "MILHÃO")
                                valor_por_extenso += " DE";
                            else
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "TRILHÕES")
                                valor_por_extenso += " DE";
                            else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "TRILHÕES")
                                valor_por_extenso += " DE";

                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " REAL";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " REAIS";

                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                            valor_por_extenso += " E ";
                    }

                    if (i == 15)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " CENTAVO";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " CENTAVOS";
                }
                return valor_por_extenso;
            }
        }


        public static string PorExtenso(this ValueType valor)
        {
            return PorExtenso(decimal.Parse(valor.ToString()));
        }

        public static string ToTitleCase(this string s)
        {
            string teste = string.IsNullOrEmpty(s) ? s : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
            return teste;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static string PorExtenso(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                if (a == 1) montagem += (b + c == 0) ? "CEM" : "CENTO";
                else if (a == 2) montagem += "DUZENTOS";
                else if (a == 3) montagem += "TREZENTOS";
                else if (a == 4) montagem += "QUATROCENTOS";
                else if (a == 5) montagem += "QUINHENTOS";
                else if (a == 6) montagem += "SEISCENTOS";
                else if (a == 7) montagem += "SETECENTOS";
                else if (a == 8) montagem += "OITOCENTOS";
                else if (a == 9) montagem += "NOVECENTOS";

                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " E " : string.Empty) + "DEZ";
                    else if (c == 1) montagem += ((a > 0) ? " E " : string.Empty) + "ONZE";
                    else if (c == 2) montagem += ((a > 0) ? " E " : string.Empty) + "DOZE";
                    else if (c == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TREZE";
                    else if (c == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUATORZE";
                    else if (c == 5) montagem += ((a > 0) ? " E " : string.Empty) + "QUINZE";
                    else if (c == 6) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSEIS";
                    else if (c == 7) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSETE";
                    else if (c == 8) montagem += ((a > 0) ? " E " : string.Empty) + "DEZOITO";
                    else if (c == 9) montagem += ((a > 0) ? " E " : string.Empty) + "DEZENOVE";
                }
                else if (b == 2) montagem += ((a > 0) ? " E " : string.Empty) + "VINTE";
                else if (b == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TRINTA";
                else if (b == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUARENTA";
                else if (b == 5) montagem += ((a > 0) ? " E " : string.Empty) + "CINQUENTA";
                else if (b == 6) montagem += ((a > 0) ? " E " : string.Empty) + "SESSENTA";
                else if (b == 7) montagem += ((a > 0) ? " E " : string.Empty) + "SETENTA";
                else if (b == 8) montagem += ((a > 0) ? " E " : string.Empty) + "OITENTA";
                else if (b == 9) montagem += ((a > 0) ? " E " : string.Empty) + "NOVENTA";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " E ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "UM";
                    else if (c == 2) montagem += "DOIS";
                    else if (c == 3) montagem += "TRÊS";
                    else if (c == 4) montagem += "QUATRO";
                    else if (c == 5) montagem += "CINCO";
                    else if (c == 6) montagem += "SEIS";
                    else if (c == 7) montagem += "SETE";
                    else if (c == 8) montagem += "OITO";
                    else if (c == 9) montagem += "NOVE";

                return montagem;
            }
        }
    }
}