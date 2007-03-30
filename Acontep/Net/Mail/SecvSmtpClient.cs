using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Data;

namespace Acontep.Net.Mail
{
    /// <summary>
    /// Cliente de SMTP extendido para as necessidades do Acontep
    /// </summary>
    public static class AcontepSmtpClient
    {
        /// <summary>
        /// Envia e-mial usando os paramteros passoa por mailMessage
        /// </summary>
        /// <param name="mailMesasge">e-mail a ser enviado</param>
        public static void Send(MailMessage mailMesasge)
        {
            Send(mailMesasge, null);
        }
        /// <summary>
        /// Envia e-mial usando os paramteros passoa por mailMessage 
        /// com a possibilidade de chamada a evento apos o envio
        /// </summary>
        /// <param name="mailMesasge">e-mail a ser enviado</param>
        /// <param name="sendCompleted">evento vinculado a ser disparado ao terminio do envio</param>
        public static void Send(MailMessage mailMesasge, SendCompletedEventHandler sendCompleted)
        {
            bool ambienteOficial = Convert.ToBoolean( ConfigurationManager.AppSettings["AmbienteOficial"] );
            if (!ambienteOficial)
            {
                mailMesasge.To.Clear();//Limpa os receptores da mensagen
                //obtem os usuarios receptores em anbiente de teste
                string mails = ConfigurationManager.AppSettings["MailErro"];
                if(!string.IsNullOrEmpty(mails))
                {
                    //Configurar o envio para os receptores de anbiente de teste
                    foreach (string mail in mails.Split(';'))
                        mailMesasge.To.Add(mail);
                }
                //DataTable dttUsuarios = UsuariosNg.ObterUsuario_PorGrupo(37);
                //StringBuilder sbUsuarios = new StringBuilder();
                //foreach (DataRow drusuario in dttUsuarios.Rows)
                //{                    
                //    mailMesasge.To.Add(drusuario["Usu_Email"].ToString());
                //}
            }
            SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
            if (sendCompleted != null)
                smtpClient.SendCompleted += sendCompleted;            
            smtpClient.Send(mailMesasge);
        }
        /// <summary>
        /// Overload para uso facilitado da classe de Log
        /// </summary>
        /// <param name="mensagem">mensagem formatada para envio</param>
        public static void Send(string mensagem)
        {
            //Cria uma menssagem de e define o usuario de envio o mesmo de recebimento(sistemas@Acontep.com.br)
            MailMessage mailMesasge = new MailMessage();
            mailMesasge.Body = mensagem;
            mailMesasge.To.Add(mailMesasge.From);
            Send(mailMesasge, null);
        }        
    }
}
