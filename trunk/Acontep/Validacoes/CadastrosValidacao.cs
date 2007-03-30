using System;
using System.Collections.Generic;
using System.Text;

namespace Acontep.Validacoes
{
    /// <summary>
    /// Validacoes e testes de escopo geral
    /// </summary>
    public static class CadastrosValidacao
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool EhCEP(string valor)
        {
            return !(String.IsNullOrEmpty(valor) || valor.Length != 8);
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool EhTelefone(string valor)
        {
            return !(String.IsNullOrEmpty(valor) || valor.Length != 8);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ValidarCEP(object sender, ValidacaoEventArgs e)
        {
            string mensagem = "não é CEP";
            if (e.Valor == null)
                e.MensagemErro = mensagem;
            else if (!EhCEP(e.Valor.ToString()))
                e.MensagemErro = mensagem;
            else
                e.MensagemErro = String.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ValidarTelefone(object sender, ValidacaoEventArgs e)
        {
            string mensagem = String.Format("{0} não é telefone", e.MensagemErro);
            if (e.Valor == null)
                e.MensagemErro = mensagem;
            else if (!EhTelefone(e.Valor.ToString()))
                e.MensagemErro = mensagem;
            else
                e.MensagemErro = String.Empty;
        }
    }
}
