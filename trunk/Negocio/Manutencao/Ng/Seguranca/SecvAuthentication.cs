using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;

namespace Secv.Manutencao.Seguranca
{
    public class SecvAuthentication
    {
        public bool EstaAutenticado
        {
            get
            {
                return FormsAuthentication.iis
            }
        }
    }
}
