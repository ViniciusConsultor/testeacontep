using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using Secv;
using Secv.Manutencao;
using System.Data;

namespace Secv.WebHelper.NavegacaoUtil
{
    public static class SecvSmtpClient
    {
        public static void Send( MailMessage mailMesasge )
        {
            bool ambienteOficial = Convert.ToBoolean( ConfigurationManager.AppSettings["AmbienteOficial"] );
            if (!ambienteOficial)
            {
                mailMesasge.To.Clear();
                DataTable dttUsuarios = UsuariosNg.ObterUsuario_PorGrupo(37);
                StringBuilder sbUsuarios = new StringBuilder();
                foreach (DataRow drusuario in dttUsuarios.Rows)
                {                    
                    mailMesasge.To.Add(drusuario["Usu_Email"].ToString());
                }
            }
            new System.Net.Mail.SmtpClient().Send(mailMesasge);
        }
    }
}
