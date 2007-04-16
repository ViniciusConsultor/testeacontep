using System;
using System.Collections.Generic;
using System.Text;
using Acontep.Validacoes;

namespace Acontep.Validacoes
{
    /// <summary>
    /// Validacoes e testes fiscais ( cnpj, inscrição estadual e etc)
    /// </summary>
    public static class FiscalValidacao
    {
        /// <summary>
        /// Importacao da DLL com validacao da inscricao estadual
        /// </summary>
        [System.Runtime.InteropServices.DllImport("DllInscE32.dll")]
        private extern static int ConsisteInscricaoEstadual(string valor, string UF);

        /// <summary>
        /// Verifica validade da inscricao estadual para a unidade federativa informada
        /// </summary>
        /// <param name="valor">Valor da inscricao estadual</param>
        /// <param name="UF">Unidade federativa a que inscricao estadual pertence</param>
        /// <returns>Verdadeiro se a inscricao e valida ou falso caso contrario</returns>
        public static bool EhInscricaoEstadual(string valor, string UF)
        {
            // nova condicao para suportar GNRE
            if (valor == "INEXISTENTE")
                return true;
            else
                return ConsisteInscricaoEstadual(valor, UF) == 0;
        }

        /// <summary>
        /// Verifica validade do cnpj/cnpj
        /// </summary>
        /// <param name="valor">cnpj/cnpj a ser validado</param>
        /// <returns>Verdadeiro se o cnpj/cnpj e valido ou falso caso contrario</returns>
        public static bool EhCnpj(long valor)
        {
            return EhCnpj(valor.ToString().PadLeft(14, '0'));
        }

        /// <summary>
        /// Verifica validade do cnpj/cnpj
        /// </summary>
        /// <param name="valor">cnpj/cnpj a ser validado</param>
        /// <returns>Verdadeiro se o cnpj/cnpj e valido ou falso caso contrario</returns>
        public static bool EhCnpj(string valor)
        {
            valor = (valor != null) ? valor.Trim() : String.Empty;
            valor = valor.Replace("/", "").Replace("-", "").Replace(".", "");

            // Se a string nao possui 14 digitos o cnpj esta incorreto
            if (valor.Length != 14)
                return false;

            long valorNumTemp;
            if (!Int64.TryParse(valor, out valorNumTemp))
                return false;

            string cnpjParte1, cnpjParte2, controle;
            int i, j, soma, digitoVerificador;
            int[] multiplicadores;
            char[] listaDigitos = valor.ToCharArray();

            cnpjParte1 = valor.Substring(0, 12);
            cnpjParte2 = valor.Substring(12, 2);
            multiplicadores = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            controle = String.Empty;

            digitoVerificador = 0;
            for (j = 1; j <= 2; j++)
            {
                soma = 0;
                for (i = 0; i < 12; i++)
                    soma += Convert.ToByte(cnpjParte1.Substring(i, 1)) * multiplicadores[i];

                if (j == 2)
                    soma += 2 * digitoVerificador;
                digitoVerificador = (soma * 10) % 11;
                if (digitoVerificador == 10)
                    digitoVerificador = 0;
                controle += digitoVerificador.ToString();
                multiplicadores = new int[12] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3 };
            }
            return controle == cnpjParte2;
        }

        /// <summary>
        /// Verifica validade do cpf
        /// </summary>
        /// <param name="valor">ccpf a ser validado</param>
        /// <returns>Verdadeiro se o cpf e valido ou falso caso contrario</returns>
        public static bool EhCpf(long valor)
        {
            return EhCpf(valor.ToString().PadLeft(11, '0'));
        }

        /// <summary>
        /// Verifica validade do cpf
        /// </summary>
        /// <param name="valor">cpf a ser validado</param>
        /// <returns>Verdadeiro se o cpf e valido ou falso caso contrario</returns>
        public static bool EhCpf(string valor)
        {
            string lValor, lcpf2;
            int v1, v2,
                i, resto;
            char[] lcpf1;

            lValor = (valor == null ? "" : valor.Trim());
            if (lValor.Length != 11)
                return false;

            try
            {
                Convert.ToInt64(lValor);
            }
            catch
            {
                return false;
            }

            lcpf1 = lValor.ToCharArray(0, 9);
            lcpf2 = lValor.Substring(9, 2);

            v2 = 0;
            for (i = 10; i >= 2; --i)
                v2 += Convert.ToByte(lcpf1[10 - i].ToString()) * i;
            resto = v2 % 11;
            v2 = resto <= 1 ? 0 : 11 - resto;

            v1 = 0;
            for (i = 11; i >= 3; --i)
                v1 += Convert.ToByte(lcpf1[11 - i].ToString()) * i;
            v1 += 2 * v2;
            resto = v1 % 11;
            v1 = resto <= 1 ? 0 : 11 - resto;

            return lcpf2 == v2.ToString() + v1.ToString();
        }

        /// <summary>
        /// Verifica validade do cpf ou do cnpj/cnpj
        /// </summary>
        /// <param name="valor">cpf ou do cnpj/cnpj a ser validado</param>
        /// <returns>Verdadeiro se o cpf ou do cnpj/cnpj e valido ou falso caso contrario</returns>
        public static bool EhCnpjCpf(string valor)
        {
            valor = (valor == null ? "" : valor.Trim());
            if (valor.Length == 14)
                return EhCnpj(valor);
            else if (valor.Length == 11)
                return EhCpf(valor);
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ValidarCnpjCpf(object sender, ValidacaoEventArgs e)
        {
            string mensagem = String.Format("{0} não é cnpj ou cpf", e.RotuloMensagemErro);
            if (e.Valor == null)
                e.MensagemErro = mensagem;
            //if (e.Valor is System.Int64)
            //    e.MensagemErro = EhCnpjCpf(e.Valor.ToString()) ? mensagem : String.Empty;
            else
                e.MensagemErro = EhCnpjCpf(e.Valor.ToString()) ? mensagem : String.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ValidarCnpj(object sender, ValidacaoEventArgs e)
        {
            string mensagem = String.Format("{0} não é cnpj", e.RotuloMensagemErro);
            if (e.Valor == null)
                e.MensagemErro = mensagem;
            if (e.Valor is System.Int64)
                e.MensagemErro = EhCnpj((Int64)e.Valor) ? mensagem : String.Empty;
            else
                e.MensagemErro = EhCnpj(e.Valor.ToString()) ? mensagem : String.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ValidarCpf(object sender, ValidacaoEventArgs e)
        {
            string mensagem = String.Format("{0} não é cpf", e.RotuloMensagemErro);
            if (e.Valor == null)
                e.MensagemErro = mensagem;
            else
                e.MensagemErro = EhCpf(e.Valor.ToString()) ? mensagem : String.Empty;
        }
    }
}

