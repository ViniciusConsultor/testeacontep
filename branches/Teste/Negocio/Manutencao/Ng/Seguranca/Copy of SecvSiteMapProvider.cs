using System;
using System.Text;
using System.Web;

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;

using System.IO;

using System.Security.Permissions;
using System.Data;
using Secv.Sistema.Dado;
using Secv.AcessoDado.Manutencao.Seguranca;
using System.Configuration;
using Secv.Sistema;

namespace Secv.Manutencao.Seguranca
{
    /// <summary>
    /// Classe que implementa o SiteMapProvider
    /// </summary>
    public abstract class SecvSiteMapProvider : StaticSiteMapProvider
    {
        public SecvSiteMapProvider()
            : base()
        {
            base.EnableLocalization = true;
        }
        TimeSpan _TempoCache = new TimeSpan(0, 2, 0);
        /// <summary>
        /// Intervalo de tempo para atualização do cache
        /// </summary>
        public TimeSpan TempoCache
        {
            get { return _TempoCache; }
            set { _TempoCache = value; }
        }
        SiteMapNode _SiteMapNodeBase;
        /// <summary>
        /// Nó raiz
        /// </summary>
        public SiteMapNode SiteMapNodeBase
        {
            get { return _SiteMapNodeBase; }
            protected set { _SiteMapNodeBase = value; }
        }

        string _PaginaBase = "/";
        /// <summary>
        /// Qual a pagina que será configurada no nó raiz
        /// </summary>
        public string PaginaBase
        {
            get { return _PaginaBase; }
            set { _PaginaBase = value; }
        }

        /// <summary>
        /// Inicializa as configurações lendo do web.config
        /// </summary>
        /// <param name="name"></param>
        /// <param name="config"></param>
        public override void Initialize(string name, NameValueCollection config)
        {
            // Verify that config isn't null
            if (config == null)
                throw new ArgumentNullException("config");

            // Assign the provider a default name if it doesn't have one
            if (String.IsNullOrEmpty(name))
                name = "SecvSiteMap";

            // Add a default "description" attribute to config if the
            // attribute doesn’t exist or is empty
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Secv site map provider");
            }


            // Call the base class's Initialize method
            base.Initialize(name, config);

            if (string.IsNullOrEmpty(config["TimeCache"]))
            {
                TempoCache = new TimeSpan(0, 2, 0);
            }
            else
            {
                TimeSpan tempoLido = TempoCache;
                if (!TimeSpan.TryParse(config["TimeCache"], out tempoLido))
                    throw new ProviderException("O campo TimeCache tem que ser um TimeSpan");
                TempoCache = tempoLido;
                config.Remove("TimeCache");
            }
            if (!string.IsNullOrEmpty(config["PaginaBase"]))
            {
                PaginaBase = config["PaginaBase"].ToString();
                Uri.IsWellFormedUriString(PaginaBase, UriKind.Relative);
                config.Remove("PaginaBase");
            }


            // In beta 2, SiteMapProvider processes the securityTrimmingEnabled
            // attribute but fails to remove it. Remove it now so we can check
            // for unrecognized configuration attributes.

            if (config["securityTrimmingEnabled"] != null)
                config.Remove("securityTrimmingEnabled");

            // Throw an exception if unrecognized attributes remain
            while (config.Count > 0)
            {
                string attr = config.GetKey(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException("Unrecognized attribute: " + attr);
                config.Remove(attr);
            }
        }

        /// <summary>
        /// Constroi o mapa do site baseada na permissão do usuário logado
        /// </summary>
        /// <returns></returns>
        public override SiteMapNode BuildSiteMap()
        {
            if (SecvAuthenticationTicket.Atual == null)
                return SiteMapNodeBase;

            // Indica se o acesso esta sendo feito via internet ou intranet
            bool acessoViaInternet = SecvAuthenticationTicket.Atual.AcessoViaInternet;

            // Inicia com um estado limpo
            Clear();

            SiteMapNodeBase = new SiteMapNode(this, String.Empty, "~/", "Início");
            SiteMapNodeBase.Roles = new string[] { "*" };
            AddNode(SiteMapNodeBase, null);
            
            NosSiteMapDataSet nosSiteMapDataSet;
            using (Escopo escopo = new Escopo())
            {
                nosSiteMapDataSet = ConstruirNos();
            }
            // TODO Colocar codigo para testar se o acesso eh externo ou interno
            foreach (NosSiteMapDataSet.RaizRow raizRow in nosSiteMapDataSet.Raiz)
            {
                if (acessoViaInternet && !raizRow.PermiteAcessoInternet)
                    continue;
                SiteMapNode raizMapNode = CriarRaizSiteMapNode(raizRow);
                foreach (NosSiteMapDataSet.NoRow noRow in raizRow.GetChildRows("Raiz_No"))
                {
                    if (acessoViaInternet && !noRow.PermiteAcessoInternet)
                        continue;
                    SiteMapNode noMapNode = CriarNoSiteMapNode(raizMapNode, noRow);
                    AddNode(noMapNode, raizMapNode);
                }
                AddNode(raizMapNode, SiteMapNodeBase);
            }
            return SiteMapNodeBase;
        }


        protected abstract NosSiteMapDataSet ConstruirNos();

        /// <summary>
        /// Cria os nós do que correspondem ao Módulo
        /// </summary>
        /// <param name="raizRow">Raiz</param>
        /// <returns></returns>
        private SiteMapNode CriarRaizSiteMapNode(NosSiteMapDataSet.RaizRow raizRow)
        {
            SiteMapNode node = new SiteMapNode(
                this,
                raizRow.Codigo,
                String.Empty,
                raizRow.Nome,
                raizRow.IsDescricaoNull() ? String.Empty : raizRow.Descricao
            );
            node.Roles = new string[] { "*" };
            return node;
        }

        /// <summary>
        /// Cria os nós que representam as funções
        /// </summary>
        /// <param name="RaizMapNode">Módulo que a função pertence</param>
        /// <param name="noRow">Nó do raiz</param>
        /// <returns></returns>
        private SiteMapNode CriarNoSiteMapNode(SiteMapNode RaizMapNode, NosSiteMapDataSet.NoRow noRow)
        {
            SiteMapNode node = new SiteMapNode(
                            this,
                            noRow.Codigo,
                            noRow.IsUrlNull() ? String.Empty : noRow.Url,
                            noRow.Nome,
                            noRow.IsDescricaoNull() ? String.Empty : noRow.Descricao
                            );
            node.Roles = new string[] { "*" };
            return node;
        }

        /// <summary>
        /// Retorna o mapa do site criado.
        /// </summary>
        /// <returns></returns>
        protected override SiteMapNode GetRootNodeCore()
        {
            if (SiteMapNodeBase == null)
                BuildSiteMap();
            return this.SiteMapNodeBase;
        }
    }
}