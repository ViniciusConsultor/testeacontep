using System;
using System.Collections.Generic;
using System.Text;

namespace Acontep
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class AcontepException: ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        public AcontepException()
            :
            base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public AcontepException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AcontepException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public AcontepException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
