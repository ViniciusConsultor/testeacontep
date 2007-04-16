using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net.Mail;
using System.Configuration;
using Acontep.Net.Mail;

namespace Acontep.Diagnostico
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailTraceListener : TraceListener
    {
        //string _EnderecoPara = String.Empty;
        //string _EnderecoDe = String.Empty;
        //string _Assunto = String.Empty;
        //string _ServidorSmtp = String.Empty;
        //int _SmtpPort = 25;

        ///// <summary>
        ///// Initializes a new instance of <see cref="EmailTraceListener"/>.
        ///// </summary>
        //public EmailTraceListener()
        //    : base()
        //{
        //}

        ///// <summary>
        ///// Initializes a new instance of <see cref="EmailTraceListener"/> with a toaddress, fromaddress, 
        ///// _Assunto, smtpserver and the smtpport
        ///// </summary>
        ///// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        ///// <param name="fromAddress">Represents from whom the email is sent.</param>
        ///// <param name="subject">Text for the _Assunto line.</param>
        ///// <param name="smtpServer">The name of the SMTP server.</param>
        ///// <param name="smtpPort">The port on the SMTP server to use for sending the email.</param>
        //public EmailTraceListener(
        //    string toAddress,
        //    string fromAddress,
        //    string subject,
        //    string smtpServer,
        //    int smtpPort
        //):this()
        //{
        //    //this._EnderecoPara = toAddress;
        //    //this._EnderecoDe = fromAddress;
        //    //this._Assunto = subject;
        //    //this._ServidorSmtp = smtpServer;
        //    //this._SmtpPort = smtpPort;
        //}


        ///// <summary>
        ///// Initializes a new instance of <see cref="EmailTraceListener"/> with a toaddress, fromaddress, 
        ///// _Assunto, smtpserver, smtpport, and a formatter
        ///// </summary>
        ///// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        ///// <param name="fromAddress">Represents from whom the email is sent.</param>
        ///// <param name="subject">Text for the _Assunto line.</param>
        ///// <param name="smtpServer">The name of the SMTP server.</param>
        //public EmailTraceListener(string toAddress, string fromAddress, string subject, string smtpServer)
        //    : this(toAddress, fromAddress, subject, smtpServer, 25)
        //{
        //}

        /// <summary>
        /// Sends an email message given a predefined string
        /// </summary>
        /// <param name="message">The string to write as the email message</param>
        public override void Write(string message)
        {
            AcontepSmtpClient.Send(message);
        }

        /// <summary>
        /// Sends an email message given a predefined string
        /// </summary>
        /// <param name="message">The string to write as the email message</param>
        public override void WriteLine(string message)
        {
            Write(message);
        }


        /// <summary>
        /// Delivers the trace data as an email message.
        /// </summary>
        /// <param name="eventCache">The context information provided by <see cref="System.Diagnostics"/>.</param>
        /// <param name="source">The name of the trace source that delivered the trace data.</param>
        /// <param name="eventType">The type of event.</param>
        /// <param name="id">The id of the event.</param>
        /// <param name="data">The data to trace.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            //if (data is LogEntry)
            //{

            //    //EmailMessage message = new EmailMessage(_EnderecoPara, _EnderecoDe, _Assunto, _ServidorSmtp, _SmtpPort, data as LogEntry, this.Formatter);
            //    //message.Send();
            //}
            //else 
            if (data is string)
            {
                Write(data);
            }
            else
            {
                base.TraceData(eventCache, source, eventType, id, data);
            }
        }        
    }
}
