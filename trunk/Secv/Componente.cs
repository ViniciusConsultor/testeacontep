using System;
using System.Collections.Generic;
using System.Text;

namespace Acontep
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class Componente: IDisposable
    {
        private bool _EstaFinalizado;

        /// <summary>
        /// 
        /// </summary>
        public bool EstaFinalizado
        {
            get { return _EstaFinalizado; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Componente()
        {
            _EstaFinalizado = false;
        }

        #region IDisposable Members

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            //Impedi que o finalizador seja chamado se Dispose foi chamado
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Libera os recursos associados a classe
        /// </summary>
        /// <remarks>
        /// So libera recursos se estiver sendo chamado por Dispose. 
        /// Se estiver sendo chamado pelo finalizador eu nao posso referenciar outros objetos
        /// uma vez que os finalizadores nao ocorrem em nenhuma ordem definida. Se eu tentar
        /// referenciar um objeto que ja foi finalizado no finalizador eu vou provocar um erro
        /// </remarks>
        /// <param name="disposing">Indica se o metodo esta sendo chamado por dispose ou pelo finalizador</param>
        protected virtual void Dispose(bool disposing)
        {
            _EstaFinalizado = true;
        }

        /// <summary>
        /// A mensagem de erro que sera passada como parametro em <see cref="ObjectDisposedException"/> na
        /// chamada de <see cref="ChecarDisposed"/>
        /// </summary>
        protected abstract string MensagemErroObjectDisposedException
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void ChecarDisposed()
        {
            if (EstaFinalizado)
            {
                throw new ObjectDisposedException(MensagemErroObjectDisposedException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        ~Componente()
        {
            Dispose(false);
        }

        #endregion

    }
}
