using System;
using System.Collections.Generic;
using System.Text;

namespace Acontep.UI.Web
{
    /// <summary>
    /// Encapsulo a menssagem é o nivel da menssagem
    /// </summary>
    [Serializable]    
    public class MensagemWeb
    {
        public static MensagemWeb MensagemSucesso
        {
            get
            {
                return new MensagemWeb("Operação realizada com sucesso.", Tipo.Sucesso);
            }
        }
        public static MensagemWeb MensagemInclusaoSucesso
        {
            get
            {
                return new MensagemWeb("Registro incluído com sucesso.", Tipo.Sucesso);
            }
        }
        public static MensagemWeb MensagemSelecioneItem
        {
            get
            {
                return new MensagemWeb("Por favor, selecione um item.", Tipo.Aviso);
            }
        }
        public static MensagemWeb MensagemAlteradoSucesso
        {
            get
            {
                return new MensagemWeb("Registro modificado com sucesso.", Tipo.Sucesso);
            }
        }
        public static MensagemWeb MensagemExclusaoSucesso
        {
            get
            {
                return new MensagemWeb("Registro apagado com sucesso.", Tipo.Sucesso);
            }
        }
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
