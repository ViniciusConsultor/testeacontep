using System;
using System.Collections.Generic;
using System.Text;

namespace Acontep
{
    /// <summary>
    /// Classe repons�vel pelas formata��es
    /// </summary>
    public static class Formatacao
    {
        /// <summary>
        /// Formata um valor para a mascara de CPF. Caso o tipo n�o seja identificado, ele retorna o pr�prio valor.
        /// </summary>
        /// <param name="cpf">valor que ser� aplicado a m�scara</param>
        public static string FormatarCPF(object cpf)
        {
            return FormatarCnpjCpf(cpf, "P");        
        }
        /// <summary>
        /// Formata um valor para a mascara de Cnpj. Caso o tipo n�o seja identificado, ele retorna o pr�prio valor.
        /// </summary>
        /// <param name="cnpj">valor que ser� aplicado a m�scara</param>
        public static string FormatarCnpj(object cnpj)
        {
            return FormatarCnpjCpf(cnpj, "J");
        }
        /// <summary>
        /// Formata um valor para a mascara de CPF ou Cnpj. Caso o tipo n�o seja identificado, ele retorna o pr�prio valor.
        /// </summary>
        /// <param name="valor">valor que ser� aplicado a m�scara</param>
        /// <param name="tipo">Tipo da m�scara. [P] =  CPF e [J] = CNPJ</param>
        /// <returns></returns>
        public static string FormatarCnpjCpf(object valor, object tipo)
        {
            string Valor = Conversao.Para<string>(valor, "valor para formatar");
            Valor = Valor.Trim();
            switch (tipo.ToString())
            {
                case "P":
                    {
                        Valor = Valor.Replace(".", "");
                        Valor = Valor.Replace("-", "");
                        if (Valor.Length < 9)
                            Valor = Valor.PadLeft(9, '0');
                        return string.Format("{0}.{1}.{2}-{3}",
                            Valor.Substring(0, 3),
                            Valor.Substring(3, 3),
                            Valor.Substring(6, 3),
                            Valor.Substring(9)
                        );
                    }
                case "J":
                    {
                        Valor = Valor.Replace(".", "");
                        Valor = Valor.Replace("-", "");
                        Valor = Valor.Replace("/", "");

                        if (Valor.Length < 14)
                            Valor = Valor.PadLeft(14, '0');
                        return string.Format("{0}.{1}.{2}/{3}-{4}",
                            Valor.Substring(0, 2),
                            Valor.Substring(2, 3),
                            Valor.Substring(5, 3),
                            Valor.Substring(8, 4),
                            Valor.Substring(12)
                        );
                    }

            }
            return Valor;
        }

        /// <summary>
        /// Aplica a m�scara de CEP
        /// </summary>
        /// <param name="valor">Valor a ser aplicada a m�scara.</param>
        /// <returns>Valor na m�scara 99.999-999</returns>
        public static string FormatarCep(object valor)
        {
            if (valor == null)
                return string.Empty;
            string ValorParaFormatar = valor.ToString();
            ValorParaFormatar = ValorParaFormatar.Replace(".", "");
            ValorParaFormatar = ValorParaFormatar.Replace("-", "").Trim();
            if (ValorParaFormatar.Length < 8)
            {
                ValorParaFormatar = ValorParaFormatar.PadLeft(8, '0');
            }
            return string.Format("{0}.{1}-{2}",
                    ValorParaFormatar.Substring(0, 2),
                    ValorParaFormatar.Substring(2, 3),
                    ValorParaFormatar.Substring(5)
                    );

        }
        /// <summary>
        /// Remove a formata��o de CEP.
        /// </summary>
        /// <param name="ValorFormatado"></param>
        /// <returns></returns>
        public static string RemoverFormatacaoCep(string ValorFormatado)
        {
            return ValorFormatado.Replace(".", "").Replace("-", "");
        }
        /// <summary>
        /// Formata para busca que usam like colocando no final "%" caso n�o exista
        /// </summary>
        /// <param name="valor">valor a ser consultado</param>
        /// <returns></returns>
        public static string FormatarLike(string valor)
        {
            if (!valor.Contains("%"))
                valor += '%';
            return valor;
        }
    }
}
