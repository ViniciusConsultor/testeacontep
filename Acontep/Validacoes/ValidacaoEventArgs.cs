using System;
using System.Collections.Generic;
using System.Text;

namespace Acontep.Validacoes
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidacaoEventArgs
    {
        private object _Valor;
        private string _RotuloMensagemErro;
        private string _MensagemErro;

        /// <summary>
        /// 
        /// </summary>
        public object Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RotuloMensagemErro
        {
            get { return _RotuloMensagemErro; }
            set { _RotuloMensagemErro = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string MensagemErro
        {
            get { return _MensagemErro; }
            set { _MensagemErro = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ValidacaoEventArgs(string rotuloMensagemErro, object valor)
        {
            if (String.IsNullOrEmpty(rotuloMensagemErro))
                throw new NullReferenceException("rotulo mensagem erro");

            _RotuloMensagemErro = rotuloMensagemErro;
            _Valor = valor;
        }
    }
}
