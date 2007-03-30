using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections;

namespace Secv.WebHelper
{
    /// <summary>
    /// Dados armazendos em sessão para uso nos sistemas e funcões web
    /// </summary>
    public static class DadosWeb
    {
        private const string IDENTIFICADOR_DADO_FUNCAO = "0C3AAD9A-D267-4FD5-8617-04821D207178";
        private const string IDENTIFICADOR_DADO_SISTEMA = "EE57E564-970F-4D6D-B34E-4F955D7395E0";
        private const string IDENTIFICADOR_DADO_APLICACAO = "09141004-639B-4CC5-BCD5-FBC0E843A555";
        private const string IDENTIFICADOR_MESSAGEM = "BEBC0F18-53E3-4388-8B94-62ECA53AC070";
        
        private static bool ExisteDado(Type tipoEsperado, object valor)
        {
            if (valor == null)
                return false;
            Type tipoExistente;
            tipoExistente = valor.GetType();
            // Se o tipo esperado for uma interface entao verifica se o tipo do valor armazenado na sessao
            // implementa essa interface
            if (tipoEsperado.IsInterface && tipoEsperado.IsInstanceOfType(valor))
                return true;
            else
                return tipoEsperado.Equals(tipoExistente);
        }

        /// <summary>
        /// Verifica se existe um valor armazenado na sessao para funções com o tipo <typeparamref name="T"/>
        /// informado
        /// </summary>
        /// <typeparam name="T">Tipo de dado esperado na função</typeparam>
        /// <returns>True se existe valor para função e se o valor é do tipo especificado, caso contrario falso. Se <typeparamref name="T"/>
        /// for uma interface entao retorna true se o valor contido em <see cref="DadoFuncao"/> implementa essa interface.</returns>
        public static bool ExisteDadoFuncao<T>()
        {
            return ExisteDado(typeof(T), DadoFuncao);
        }

        /// <summary>
        /// Obtem valor do tipo <typeparamref name="T"/> armazenado para a funcao. Se o valor de <see cref="DadoFuncao"/>
        /// for de um tipo diferente seu valor é limpo e o valor default para <typeparamref name="T"/> é retornado.
        /// </summary>
        /// <remarks>
        /// É recomendado usar <see cref="ExisteDadoFuncao"/> se a função vai dar suporte a mais de um tipo
        /// possivel de <typeparamref name="T"/> para evitar que <see cref="DadoFuncao"/> seja limpo caso a chamada
        /// para <c>ObterDadoFuncao</c> indicar um tipo diferente do armazenado.
        /// </remarks>
        /// <typeparam name="T">Tipo de dado armazenada na funcao</typeparam>
        /// <returns>True se existe valor para função e se o valor é do tipo especificado, caso contrario falso</returns>
        public static T ObterDadoFuncao<T>()
        {
            if (!ExisteDadoFuncao<T>())
            {
                DadoFuncao = null;
                return default(T);
            }
            return (T)DadoFuncao;
        }

        /// <summary>
        /// Obtem ou atribui coleção de pares nome/valor como dados para a função.
        /// </summary>
        /// <remarks>
        /// Essa propriedade sempre retorna uma instancia de um <see cref="IDictionary`2"/>. Se <see cref="DadoFuncao"/> nao possuir um <see cref="IDictionary`2"/> entao um novo é instanciado e atribuído a <see cref="DadoFuncao"/>. 
        /// Se for necessario verificar se ja existe um <see cref="IDictionary`2"/> usar <c>ExisteDadoFuncao<IDictionary<String, String>>()</c>.
        /// Em geral, onde essa solução cabe, outras soluções de navegação que não usam sessões também são recomendadas.
        /// </remarks>
        public static IDictionary<String, String> NomeValorDadoFuncao
        {
            get
            {
                // Se DadoFuncao nao possuir um Dictionary<String, String> 
                // instancia um novo DadoFuncao.
                if (!ExisteDadoFuncao<IDictionary<String, String>>())
                    DadoFuncao = new Dictionary<String, String>();
                return ObterDadoFuncao<IDictionary<String, String>>();
            }
            set
            {
                DadoFuncao = value;
            }
        }

        
        /// <summary>
        /// Obtem ou atribui dados a uma variável de sessão reservado para dados armazenados durante a 
        /// execução de determinada função/caso de uso
        /// </summary>
        /// <remarks>
        /// Uma vez que duas funções não são acessadas simultaneamente então os dados são 
        /// sempre armazenados na mesma variável de sessão, evitando assim dados de função
        /// perdidos durante a vida útil de uma sessão.
        /// </remarks>
        public static object DadoFuncao
        {
            get
            {
                return HttpContext.Current.Session[IDENTIFICADOR_DADO_FUNCAO];
            }
            set
            {
                HttpContext.Current.Session[IDENTIFICADOR_DADO_FUNCAO] = value;
            }
        }

        /// <summary>
        /// Verifica se existe um valor armazenado na sessao para sistemas com o tipo <typeparamref name="T"/>
        /// informado
        /// </summary>
        /// <typeparam name="T">Tipo de dado esperado na sistema</typeparam>
        /// <returns>True se existe valor para sistema e se o valor é do tipo especificado, caso contrario falso. Se <typeparamref name="T"/>
        /// for uma interface entao retorna true se o valor contido em <see cref="DadoSistema"/> implementa essa interface.</returns>
        public static bool ExisteDadoSistema<T>()
        {
            return ExisteDado(typeof(T), DadoSistema);
        }

        /// <summary>
        /// Obtem valor do tipo <typeparamref name="T"/> armazenado para o sistema. Se o valor de <see cref="DadoSistema"/>
        /// for de um tipo diferente seu valor é limpo e o valor default para <typeparamref name="T"/> é retornado.
        /// </summary>
        /// <remarks>
        /// É recomendado usar <see cref="ExisteDadoSistema"/> se o sistema vai dar suporte a mais de um tipo
        /// possivel de <typeparamref name="T"/> para evitar que <see cref="DadoSistema"/> seja limpo caso a chamada
        /// para <c>ObterDadoSistema</c> indicar um tipo diferente do armazenado.
        /// </remarks>
        /// <typeparam name="T">Tipo de dado armazenada para o sistema</typeparam>
        /// <returns>True se existe valor armazenado para o sistema e se o valor é do tipo especificado, caso contrario falso</returns>
        public static T ObterDadoSistema<T>()
        {
            if (!ExisteDadoSistema<T>())
            {
                DadoSistema = null;
                return default(T);
            }
            return (T)DadoSistema;
        }

        /// <summary>
        /// Obtem ou atribui coleção de pares nome/valor como dados para o sistema.
        /// </summary>
        /// <remarks>
        /// Essa propriedade sempre retorna uma instancia de um <see cref="IDictionary`2"/>. Se <see cref="DadoSistema"/> nao possuir um <see cref="IDictionary`2"/> entao um novo é instanciado e atribuído a <see cref="DadoSistema"/>. 
        /// Se for necessario verificar se ja existe um <see cref="IDictionary`2"/> usar <c>ExisteDadoSistema<IDictionary<String, String>>()</c>.
        /// Em geral, onde essa solução cabe, outras soluções de navegação que não usam sessões também são recomendadas.
        /// </remarks>
        public static IDictionary<String, String> NomeValorDadoSistema
        {
            get
            {
                if (!ExisteDadoSistema<IDictionary<String, String>>())
                    DadoSistema = new Dictionary<String, String>();
                return ObterDadoSistema<IDictionary<String, String>>();
            }
            set
            {
                DadoSistema = value;
            }
        }

        /// <summary>
        /// Obtem ou atribui dados a uma variável de sessão reservado para dados armazenados durante todo um acesso a um sistema
        /// específico.
        /// </summary>
        public static object DadoSistema
        {
            get
            {
                return HttpContext.Current.Session[IDENTIFICADOR_DADO_SISTEMA];
            }
            set
            {
                HttpContext.Current.Session[IDENTIFICADOR_DADO_SISTEMA] = value;
            }
        }


        /// <summary>
        /// Obtem ou atribui dados a uma variável de sessão reservado para dados armazenados durante todo a execução da aplicacao
        /// </summary>
        public static object DadoAplicacao
        {
            get
            {
                return HttpContext.Current.Application[IDENTIFICADOR_DADO_APLICACAO];
            }
            set
            {
                HttpContext.Current.Application[IDENTIFICADOR_DADO_APLICACAO] = value;
            }
        }

        /// <summary>
        /// Verifica se existe um valor armazenado no application para Aplicação com o tipo <typeparamref name="T"/>
        /// informado
        /// </summary>
        /// <typeparam name="T">Tipo de dado esperado na aplicação</typeparam>
        /// <returns>True se existe valor para aplicacao e se o valor é do tipo especificado, caso contrario falso. Se <typeparamref name="T"/>
        /// for uma interface entao retorna true se o valor contido em <see cref="DadoAplicacao"/> implementa essa interface.</returns>
        public static bool ExisteDadoAplicacao<T>()
        {
            return ExisteDado(typeof(T), DadoAplicacao);
        }

        /// <summary>
        /// Obtem valor do tipo <typeparamref name="T"/> armazenado para a aplicacao. Se o valor de <see cref="DadoAplicacao"/>
        /// for de um tipo diferente seu valor é limpo e o valor default para <typeparamref name="T"/> é retornado.
        /// </summary>
        /// <remarks>
        /// É recomendado usar <see cref="ExisteDadoAplicacao"/> se a aplicacao vai dar suporte a mais de um tipo
        /// possivel de <typeparamref name="T"/> para evitar que <see cref="DadoAplicacao"/> seja limpo caso a chamada
        /// para <c>ObterDadoAplicacao</c> indicar um tipo diferente do armazenado.
        /// </remarks>
        /// <typeparam name="T">Tipo de dado armazenada para a aplicacao</typeparam>
        /// <returns>True se existe valor armazenado para a aplicacao e se o valor é do tipo especificado, caso contrario falso</returns>
        public static T ObterDadoAplicacao<T>()
        {
            if (!ExisteDadoAplicacao<T>())
            {
                DadoAplicacao = null;
                return default(T);
            }
            return (T)DadoAplicacao;
        }

        /// <summary>
        /// Obtem ou atribui coleção de pares nome/valor como dados para a splicacao.
        /// </summary>
        /// <remarks>
        /// Essa propriedade sempre retorna uma instancia de um <see cref="IDictionary`2"/>. Se <see cref="DadoAplicacao"/> nao possuir um <see cref="IDictionary`2"/> entao um novo é instanciado e atribuído a <see cref="DadoAplicacao"/>. 
        /// Se for necessario verificar se ja existe um <see cref="IDictionary`2"/> usar <c>ExisteDadoAplicacao<IDictionary<String, String>>()</c>.
        /// Em geral, onde essa solução cabe, outras soluções de navegação que não usam sessões também são recomendadas.
        /// </remarks>
        public static IDictionary<String, String> NomeValorDadoAplicacao
        {
            get
            {
                if (!ExisteDadoAplicacao<IDictionary<String, String>>())
                    DadoAplicacao = new Dictionary<String, String>();
                return ObterDadoAplicacao<IDictionary<String, String>>();
            }
            set
            {
                DadoAplicacao = value;
            }
        }


        //Vericar a possibilidade de usar Cache
        //Precisa de uma chave imbutida
        //public static bool ExisteDadoAplicacao<T>(string key)
        //{
        //    if (HttpContext.Current.Cache[key] == null)
        //        return false;
        //    if (HttpContext.Current.Cache[key].GetType() is T)
        //        return true;
        //    else
        //        return false;
        //}
        //public static T NomeValorDadoAplicacao<T>(string key)
        //{
        //    return (T)HttpContext.Current.Cache[key];
        //}
        //public static void InserirDadoAplicacao<T>(string key, T valor)
        //{
        //    if(HttpContext.Current.Cache[key] == null)
        //        HttpContext.Current.Cache[key] = valor;
        //    else
        //        HttpContext.Current.Cache.Insert(key,valor);
        //}
        //public static void RemoverDadoAplicacao(string key)
        //{
        //    HttpContext.Current.Cache.Remove(key);
        //}

        /// <summary>
        /// Obtem a mensagem armazenada em sessão e remove a mensagem da sessão
        /// </summary>
        /// <returns>Retorna <see cref="MensagemWeb"/> ou null se nao existir nenhuma mensagem</returns>
        public static MensagemWeb[] ObterMensagem()
        {
            return ObterMensagem(true);            
        }


        /// <summary>
        /// Obtem a mensagem armazenada em sessão
        /// </summary>
        /// <param name="remover">Indica se a mensagem deve ser removida da sessao ou mantida</param>
        /// <returns>Retorna <see cref="MensagemWeb"/> ou null se nao existir nenhuma mensagem</returns>
        public static MensagemWeb[] ObterMensagem(bool remover)
        {
            MensagemWeb[] msg = HttpContext.Current.Session[IDENTIFICADOR_MESSAGEM] as MensagemWeb[];
            if (remover)
            {
                HttpContext.Current.Session[IDENTIFICADOR_MESSAGEM] = null;
            }
            if (msg == null)
            {
                msg = new MensagemWeb[0];
            }
            return msg;           
        }

        /// <summary>
        /// coloca o objeto MensagemWeb no espaço definido para ser visualizado
        /// </summary>
        /// <param name="mensagemWeb"></param>
        public static void AtribuirMensagem(MensagemWeb mensagemWeb)
        {
            MensagemWeb[] msg = HttpContext.Current.Session[IDENTIFICADOR_MESSAGEM] as MensagemWeb[];
            if (msg == null)
            {
                msg = new MensagemWeb[1];
                msg[0] = mensagemWeb;
            }
            else
            {
                List<MensagemWeb> menssagens = new List<MensagemWeb>(msg);
                menssagens.Add(mensagemWeb);
                msg = menssagens.ToArray();                
            }
            HttpContext.Current.Session[IDENTIFICADOR_MESSAGEM] = msg;
        }

        /// <summary>
        /// Atribui uma menssagem no espaço definido paar ser visualizado
        /// </summary>
        /// <param name="mensagem"></param>
        public static void AtribuirMensagem(string mensagem)
        {
            AtribuirMensagem(mensagem, MensagemWeb.Tipo.Sucesso);
        }

        /// <summary>
        /// atribui uma menssagem e seu nivel no espaço definido para ser visualizado
        /// Este metodo pode ser usado usando a clase MenssagemWeb
        /// </summary>
        /// <param name="mensagem"></param>
        /// <param name="nivelMensagem"></param>
        public static void AtribuirMensagem(string mensagem, MensagemWeb.Tipo nivelMensagem)
        {
            AtribuirMensagem(new MensagemWeb(mensagem, nivelMensagem));
        }
        /// <summary>
        /// Atribui uma menssagem no espaço definido paar ser visualizado utilizando o .Message para recuperar a msg.
        /// </summary>
        /// <param name="secvException"></param>
        public static void AtribuirMensagem(SecvException secvException)
        {
            AtribuirMensagem(secvException.Message, MensagemWeb.Tipo.Erro);
        }
    }
}
