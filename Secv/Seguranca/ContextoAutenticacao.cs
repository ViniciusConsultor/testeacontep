using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Threading;

namespace Acontep.Seguranca
{
    /// <summary>
    /// 
    /// </summary>
    public class ContextoAutenticacao
    {
        /// <summary>
        /// 
        /// </summary>
        public IPrincipal Principal
        {
            get { return Thread.CurrentPrincipal; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IIdentity Identity
        {
            get { return Principal.Identity; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return Identity.Name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ContextoAutenticacao()
        {
        }
    }
}
