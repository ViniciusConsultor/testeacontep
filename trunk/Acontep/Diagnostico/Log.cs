using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.Threading;

namespace Acontep.Diagnostico
{
    /// <summary>
    /// 
    /// </summary>
    public static class Log
    {
        static TraceSwitch _TraceSwitchDebugBiblioteca;
        static TraceSwitch _TraceSwitchDebugNg;
        static TraceSwitch _TraceSwitchDebugUI;
        static TraceSwitch _TraceSwitchGeral;

        static Log()
        {
            _TraceSwitchDebugUI = new TraceSwitch("TraceSwitchDebugUI", "Debug de operações na interface");
            _TraceSwitchDebugNg = new TraceSwitch("TraceSwitchDebugNg", "Debug de qualquer operação que não se enquadre no padrão interface");
            _TraceSwitchDebugBiblioteca = new TraceSwitch("TraceSwitchDebugBiblioteca", "Debug de operação em biblioteca");
            _TraceSwitchGeral = new TraceSwitch("TraceSwitchGeral", "Trace de negocio e dados");
        }

        #region Trace Geral

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("TRACE")]
        public static void DetalhesGeral(string mensagem)
        {
            if (_TraceSwitchGeral.TraceVerbose)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("TRACE")]
        public static void AvisoGeral(string mensagem)
        {
            if (_TraceSwitchGeral.TraceWarning)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("TRACE")]
        public static void InfoGeral(string mensagem)
        {
            if (_TraceSwitchGeral.TraceInfo)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("TRACE")]
        public static void ErroGeral(string mensagem)
        {
            if (_TraceSwitchGeral.TraceError)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        [Conditional("TRACE")]
        public static void ErroGeral(Exception exception)
        {
            if (_TraceSwitchGeral.TraceError)
            {
                Trace.WriteLine(GerarMensagem(exception));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cabecalho"></param>
        /// <param name="exception"></param>
        [Conditional("TRACE")]
        public static void ErroGeral(string cabecalho, Exception exception)
        {
            if (_TraceSwitchGeral.TraceError)
            {
                Trace.WriteLine(GerarMensagem(cabecalho, exception));
            }
        }

        #endregion Trace Geral

        #region Debug Biblioteca

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("DEBUG")]
        public static void DetalhesDebugBiblioteca(string mensagem)
        {
            if (_TraceSwitchDebugBiblioteca.TraceVerbose)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("DEBUG")]
        public static void AvisoDebugBiblioteca(string mensagem)
        {
            if (_TraceSwitchDebugBiblioteca.TraceWarning)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("DEBUG")]
        public static void InfoDebugBiblioteca(string mensagem)
        {
            if (_TraceSwitchDebugBiblioteca.TraceInfo)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("DEBUG")]
        public static void ErroDebugBiblioteca(string mensagem)
        {
            if (_TraceSwitchDebugBiblioteca.TraceError)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        [Conditional("DEBUG")]
        public static void ErroDebugBiblioteca(Exception exception)
        {
            if (_TraceSwitchDebugBiblioteca.TraceError)
            {
                Trace.WriteLine(GerarMensagem(exception));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cabecalho"></param>
        /// <param name="exception"></param>
        [Conditional("DEBUG")]
        public static void ErroDebugBiblioteca(string cabecalho, Exception exception)
        {
            if (_TraceSwitchDebugBiblioteca.TraceError)
            {
                Trace.WriteLine(GerarMensagem(cabecalho, exception));
            }
        }

        #endregion Debug Biblioteca

        #region Debug Ng

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("DEBUG")]
        public static void DetalhesDebugNg(string mensagem)
        {
            if (_TraceSwitchDebugNg.TraceVerbose)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("DEBUG")]
        public static void AvisoDebugNg(string mensagem)
        {
            if (_TraceSwitchDebugNg.TraceWarning)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("DEBUG")]
        public static void InfoDebugNg(string mensagem)
        {
            if (_TraceSwitchDebugNg.TraceInfo)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("DEBUG")]
        public static void ErroDebugNg(string mensagem)
        {
            if (_TraceSwitchDebugNg.TraceError)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        [Conditional("DEBUG")]
        public static void ErroDebugNg(Exception exception)
        {
            if (_TraceSwitchDebugNg.TraceError)
            {
                Trace.WriteLine(GerarMensagem(exception));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cabecalho"></param>
        /// <param name="exception"></param>
        [Conditional("DEBUG")]
        public static void ErroDebugNg(string cabecalho, Exception exception)
        {
            if (_TraceSwitchDebugNg.TraceError)
            {
                Trace.WriteLine(GerarMensagem(cabecalho, exception));
            }
        }

        #endregion Debug Ng
        
        #region Debug UI

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("DEBUG")]
        public static void DetalhesDebugUI(string mensagem)
        {
            if (_TraceSwitchDebugUI.TraceVerbose)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("DEBUG")]
        public static void AvisoDebugUI(string mensagem)
        {
            if (_TraceSwitchDebugUI.TraceWarning)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("DEBUG")]
        public static void InfoDebugUI(string mensagem)
        {
            if (_TraceSwitchDebugUI.TraceInfo)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensagem"></param>
        [Conditional("DEBUG")]
        public static void ErroDebugUI(string mensagem)
        {
            if (_TraceSwitchDebugUI.TraceError)
            {
                Trace.WriteLine(FormatarMensagem(mensagem));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        [Conditional("DEBUG")]
        public static void ErroDebugUI(Exception exception)
        {
            if (_TraceSwitchDebugUI.TraceError)
            {
                Trace.WriteLine(GerarMensagem(exception));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cabecalho"></param>
        /// <param name="exception"></param>
        [Conditional("DEBUG")]
        public static void ErroDebugUI(string cabecalho, Exception exception)
        {
            if (_TraceSwitchDebugUI.TraceError)
            {
                Trace.WriteLine(GerarMensagem(cabecalho, exception));
            }
        }

        #endregion Debug UI

        #region Operacoes privadas
        static void GerarTrace(string trace)
        {
            Trace.WriteLine(trace);
        }

        static string GerarMensagem(Exception exception)
        {
            return GerarMensagem(String.Empty, String.Empty, exception);
        }

        static string GerarMensagem(string cabecalho, Exception exception)
        {
            return GerarMensagem(string.Empty, cabecalho, exception);
        }

        static string GerarMensagem(string cabecalho, string msgLog, Exception exception)
        {
            StringBuilder msgFormatada = new StringBuilder();
            if (!String.IsNullOrEmpty(cabecalho))
            {
                msgFormatada.AppendFormat("Cabecalho.: {0}", cabecalho);
                msgFormatada.Append(Environment.NewLine);
            }
            if (!String.IsNullOrEmpty(msgLog))
            {
                msgFormatada.AppendFormat("Mensagem..: {0}", msgLog);
                msgFormatada.Append(Environment.NewLine);
            }
            msgFormatada.AppendFormat("Data/Hora.: {0:dd/MM/yyyy HH:mm}", DateTime.Now);
            msgFormatada.Append(Environment.NewLine);
            GerarInfoEscopo(msgFormatada);
            GerarInfoException(exception, msgFormatada);
            msgFormatada.Append(Environment.NewLine);
            msgFormatada.Append(Environment.NewLine);
            return msgFormatada.ToString();
        }

        private static void GerarInfoEscopo(StringBuilder msgFormatada)
        {
            string usuario = Thread.CurrentPrincipal.Identity.Name;
            msgFormatada.AppendFormat("Usuario...: {0}", usuario);
            msgFormatada.Append(Environment.NewLine);

            //if (Escopo.Atual != null)
            //{
            //    string sistema = String.Empty;
            //    string planta = String.Empty;
            //    string modulo = String.Empty;
            //    string funcao = String.Empty;
            //    msgFormatada.AppendFormat("Sistema...: {0}\n", sistema);
            //    msgFormatada.AppendFormat("Planta....: {0}\n", planta);
            //    msgFormatada.AppendFormat("Modulo....: {0}\n", modulo);
            //    msgFormatada.AppendFormat("Funcao....: {0}\n", funcao);
            //}
        }

        private static string FormatarMensagem(string mensagem)
        {
            return String.Format("{0:dd/MM/yyyy HH:mm:ss} {1}", DateTime.Now, mensagem);
        }

        private static void GerarInfoException(Exception exception, StringBuilder msgFormatada)
        {
            if (exception != null)
            {
                msgFormatada.AppendFormat("Método....: {0}", exception.TargetSite);
                msgFormatada.Append(Environment.NewLine);
                msgFormatada.AppendFormat("Exception.: {0}", exception.Message);
                msgFormatada.Append(Environment.NewLine);
                msgFormatada.AppendFormat("Trace.....: {0}", exception.StackTrace);
                msgFormatada.Append(Environment.NewLine);
                msgFormatada.AppendFormat("Erro......: {0}", exception.ToString());
                msgFormatada.Append(Environment.NewLine);
            }
        }
        
        #endregion Operacoes privadas
    }
}
