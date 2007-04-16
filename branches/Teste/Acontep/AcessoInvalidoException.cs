using System;
using System.Collections.Generic;
using System.Text;

namespace Acontep
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class AcessoInvalidoException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        public AcessoInvalidoException(): 
            base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public AcessoInvalidoException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AcessoInvalidoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public AcessoInvalidoException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
