using System;
using System.Collections.Generic;
using System.Text;

namespace Acontep.Validacoes
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ValorInvalidoException: AcontepException
    {
        /// <summary>
        /// 
        /// </summary>
        public ValorInvalidoException(): 
            base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public ValorInvalidoException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public ValorInvalidoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public ValorInvalidoException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
