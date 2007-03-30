using System;
using System.Collections.Generic;
using System.Text;

namespace Secv.WebHelper
{
    /// <summary>
    /// Encapsulo a menssagem é o nivel da menssagem
    /// </summary>
    [Serializable]    
    public class MensagemWeb
    {
        private string mensagem = string.Empty;
        private Tipo tipo = Tipo.Sucesso;
        /// <summary>
        /// Tipos de menssagens
        /// </summary>
        public enum Tipo
        {
            /// <summary>
            /// Tipo Erro
            /// </summary>
            Erro,
            /// <summary>
            /// Tipo Avisso
            /// </summary>
            Aviso,
            /// <summary>
            /// Tipo Sucesso
            /// </summary>
            Sucesso
        }

        /// <summary>
        /// Obtem/atribuir a menssagem
        /// </summary>
        public string Mensagem
        {
            get { return mensagem; }
            set { mensagem = value; }
        }        

        /// <summary>
        /// Obtem/atribuir o nivel de erro
        /// </summary>
        public Tipo TipoMsg
        {
            get { return tipo; }
            set { tipo = value; }
        }
        //public MensagemWeb()
        //{
        //}

        /// <summary>
        /// Construtor do objeto 
        /// </summary>
        /// <param name="mensagem"></param>
        /// <param name="nivelErro"></param>
        public MensagemWeb(string mensagem, Tipo nivelErro)
        {
            this.Mensagem = mensagem;
            this.TipoMsg = nivelErro;
        }
    }
}
