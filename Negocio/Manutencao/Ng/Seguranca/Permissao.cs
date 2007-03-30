using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Configuration;
//using Acontep.AD.Manutencao.Seguranca;
using Acontep;
using System.Web.Security;
using System.Web;
using Acontep.AD.Manutencao.Seguranca;

namespace Acontep.Manutencao.Seguranca
{
    public static class Permissao
    {
        #region Propriedades de classe

        /// <summary>
        /// Retorna o CodigoModulo da página        
        /// </summary>
        public static string CodigoSistema
        {
            get
            {
                return ConfigurationManager.AppSettings["CodigoSistema"];
            }
        }
        /// <summary>
        /// Retorna o CodigoModulo da página
        /// </summary>
        public static string CodigoModulo
        {
            get
            {
                return ConfigurationManager.AppSettings["CodigoModulo"];
            }
        }
        /// <summary>
        /// Verifica se o ususario é autenticado
        /// observado se existe um ticket
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                return AuthenticationTicket.Atual != null;
            }
        }

        #endregion Propriedades de classe

        #region Operacoes privadas

        /// <summary>
        /// Verifica se foi realizado o login, redirecionando para pagina de login.
        /// </summary>
        private static void VerificarLogin()
        {
            if (!IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
                //throw MensagemUtil.AcessoInvalidoException();
            }
        }

        #endregion Operacoes privadas

        /// <summary>
        /// Verifica a permissão do usuário na função
        /// </summary>
        /// <param name="CodigoSistema">Código do sistema</param>
        /// <param name="plt_Planta">Código da planta</param>
        /// <returns>True se tiver permissão e false se não tiver</returns>
        public static bool ChecarPermissao(string CodigoSistema)
        {
            VerificarLogin();
            AuthenticationTicket ticket = AuthenticationTicket.Atual;
            PermissaoAcessoDados permissaoAcessoDados = new PermissaoAcessoDados();
         
            if (ticket == null)
                return false;
            else
                return permissaoAcessoDados.PossuiPermissao(CodigoSistema, ticket.IDUsuario);
        }

        /// <summary>
        /// Verifica a permissão do usuário na função e no modulo do sistema logado
        /// </summary>        
        /// <param name="CodigoSistema">Código do sistema</param>
        /// <param name="CodigoModulo">Codigo do Módulo</param>
        /// <param name="CodigoFuncao">Código da Função</param>
        /// <returns>True se tiver permissão e false se não tiver</returns>
        private static bool ChecarPermissao(string CodigoSistema, string CodigoModulo, string CodigoFuncao)
        {
            AuthenticationTicket ticket = AuthenticationTicket.Atual;
            PermissaoAcessoDados permissaoAcessoDados = new PermissaoAcessoDados();
            if (ticket != null)
            {
                return permissaoAcessoDados.PossuiPermissao(CodigoSistema, CodigoModulo, CodigoFuncao, ticket.IDUsuario);
            }
            else
                return false;
        }

        /// <summary>
        /// Verifica a permissão do usuário na função
        /// </summary>        
        /// <param name="CodigoFuncao">Código da Função</param>
        /// <returns>True se tiver permissão e false se não tiver</returns>
        public static bool ChecarPermissaoFuncao(string CodigoFuncao)
        {
            AuthenticationTicket ticket = AuthenticationTicket.Atual;
            
            return ChecarPermissao(ticket.CodigoSistema, ticket.CodigoModulo, CodigoFuncao);
        }

        /// <summary>
        /// Guarda a permissao para o sistema e planta logada.
        /// </summary>
        /// <param name="CodigoModulo">Código do sistema</param>
        /// <returns>True se tiver permissão e false se não tiver</returns>
        public static void ConfigAcessoSistema(string CodigoSistema)
        {
            VerificarLogin();
            AuthenticationTicket ticket = AuthenticationTicket.Atual;
            if (ChecarPermissao(CodigoSistema))
            {
                ticket.AtribuirAcessoSistema(CodigoSistema);
                //if (ticket.Sis_LogAcesso)
                //{
                //    LogUsuarios.Inserir(plt_Planta, CodigoSistema, null, null, ticket.Usu_Login, DateTime.Now, "S");
                //}                
                //DadosWeb.DadoSistema = null;
                //DadosWeb.DadoFuncao = null;
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Guarda a permissão para a função.
        /// </summary>
        /// <param name="CodigoFuncao">Código da Função</param>
        public static void ConfigAcessoFuncao(string CodigoFuncao)
        {
            VerificarLogin();

            //if (HttpContext.Current.Session.IsNewSession)
            //{
            //    HttpContext.Current.Response.Redirect("~/");
            //}
            if (AuthenticationTicket.Atual == null)
            {
                HttpContext.Current.Response.Cookies.Clear();
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                return;
            }

            if (string.IsNullOrEmpty(AuthenticationTicket.Atual.CodigoSistema ))
                HttpContext.Current.Response.Redirect("~/");
            AuthenticationTicket ticket = AuthenticationTicket.Atual;
            if (ChecarPermissao(Permissao.CodigoSistema, Permissao.CodigoModulo, CodigoFuncao))
            {
                ticket.AtribuirAcessoFuncao(Permissao.CodigoSistema, Permissao.CodigoModulo, CodigoFuncao);
                //if (ticket.Fun_LogAcesso)
                //{
                //    LogUsuarios.Inserir(ticket.Plt_Planta, ticket.CodigoSistema, ticket.CodigoModulo, ticket.CodigoFuncao, ticket.Usu_Login, DateTime.Now, "F");
                //}
            }
            else
                throw new AcontepException("O acesso para a página requisitada foi negado.");
        }
        /// <summary>
        /// Verifica se deve existir valor para modulo e funcao
        /// </summary>
        /// <returns></returns>
        private static bool DeveValidarModuloFuncao()
        {
            // Se nao estiver autenticado entrega ao ASP.NET a responsabilidade de garantir
            // de que o usuario nao pode estar na sessao restrita

            // TODO Problema de seguranca se esquecer de colocar CodigoModulo no Web.Config e
            // de esquecer de setar a CodigoFuncao na pagina
            return Permissao.IsAuthenticated && ! String.IsNullOrEmpty(CodigoModulo);
        }

        /// <summary>
        /// Verifica se deve existir valor para sistema e planta
        /// </summary>
        /// <returns></returns>
        private static bool DeveValidarSistemaPlanta()
        {
            // Se nao estiver autenticado entrega ao ASP.NET a responsabilidade de garantir
            // de que o usuario nao pode estar em sessao restrita

            // TODO Problema de seguranca se esquecer de colocar CodigoSistema no Web.Config
            return Permissao.IsAuthenticated && !String.IsNullOrEmpty(CodigoSistema); 
        }

        /// <summary>
        /// Verifica se o usuario possui acesso a possicao em que se ele se encontra.
        /// </summary>
        /// <param name="CodigoFuncao">Código da Função</param>
        public static void ValidarPermissao()
        {
            //Impedi uma requisição a area restrita caso ñ tenha ticket
            if (AuthenticationTicket.Atual == null && ! String.IsNullOrEmpty( CodigoSistema ))
                throw new AcontepException("O acesso para a página requisitada foi negado.");



            if (DeveValidarModuloFuncao())
            {
                VerificarLogin();
                AuthenticationTicket ticket = AuthenticationTicket.Atual;
                if (!ChecarPermissao(Permissao.CodigoSistema, Permissao.CodigoModulo, ticket.CodigoFuncao))
                    throw new AcontepException("O acesso para a página requisitada foi negado.");
            }
            else if (DeveValidarSistemaPlanta())
            {
                VerificarLogin();
                AuthenticationTicket ticket = AuthenticationTicket.Atual;
                if (!ChecarPermissao(Permissao.CodigoSistema))
                    throw new AcontepException("O acesso para a página requisitada foi negado.");
            }
            //else
            //{
            //    if (AuthenticationTicket.Atual == null && ! String.IsNullOrEmpty( CodigoSistema ))
            //    {
            //        throw MensagemUtil.AcessoInvalidoException();
            //        //FormsAuthentication.RedirectToLoginPage();
            //    }
            //}
        }
        /// <summary>
        /// Realiza o logout e finaliza o ticket
        /// do usuario e suas outras sessões
        /// </summary>
        public static void Sair()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();
        }

        /// <summary>
        /// Sai do sistema mas continua logado para outros acessos
        /// </summary>
        public static void SairSistema()
        {
            if (AuthenticationTicket.Atual != null)
            {
                AuthenticationTicket.Atual.AtribuirAcessoSistema(null);
            }
        }

        /// <summary>
        /// Limpar a funcção registrado no ticket
        /// </summary>
        public static void SairFuncao()
        {
            if (AuthenticationTicket.Atual != null)
            {
                AuthenticationTicket.Atual.CodigoFuncao = "";
                AuthenticationTicket.Atual.NomeFuncao = "";
            }
        }
    }
}