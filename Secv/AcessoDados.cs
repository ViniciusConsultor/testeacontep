using System;
using System.Collections.Generic;
using System.Text;

namespace Acontep
{
    /// <summary>
    /// 
    /// </summary>
    public class AcessoDados
    {
        private event EventHandler _Iniciando;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Iniciando
        {
            add 
            {
                _Iniciando += value;
            }
            remove
            {
                _Iniciando -= value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AcessoDados()
        {
            QuandoIniciando();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void QuandoIniciando()
        {
            if (_Iniciando != null)
                _Iniciando(this, new EventArgs());
        }
    }
}
