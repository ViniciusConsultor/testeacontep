using System;
using System.Collections.Generic;
using System.Text;

namespace Secv.UI.Web
{
    /// <summary>
    /// Utilitario para leituras de mensagens pre-construidas para uso na aplicacao
    /// </summary>
    public static class MensagemUtil
    {
        /// <summary>
        /// Obtem mensagem
        /// </summary>
        /// <param name="nomeMensagem">Nome da mensagem desejada</param>
        /// <param name="paramsMensagem">Valores customizados para a mensagem</param>
        /// <returns>Mensagem customizada</returns>
        public static string ObterMensagem(string nomeMensagem, params object[] paramsMensagem)
        {
            // TODO validar se a quantidade de parametros corresponde ao existente na mensagem
            string mensagem = String.Empty;//Mensagens.ResourceManager.GetString(nomeMensagem);
            if (paramsMensagem != null && paramsMensagem.Length > 0)
                return String.Format(mensagem, paramsMensagem);
            else
                return mensagem;
        }

        /// <summary>
        /// 
        /// </summary>
        public static Exception AcessoInvalidoException()
        {
            return new AcessoInvalidoException(ObterMensagem("ErroLoginInvalido"));
        }

        internal static void ThrowNullReferenceException(string nomeMensagem, params object[] paramsMensagem)
        {
            throw new NullReferenceException(ObterMensagem(nomeMensagem, paramsMensagem));
        }

        internal static void ThrowInvalidOperationException(string nomeMensagem, params object[] paramsMensagem)
        {
            throw new InvalidOperationException(ObterMensagem(nomeMensagem, paramsMensagem));
        }
    }
}
