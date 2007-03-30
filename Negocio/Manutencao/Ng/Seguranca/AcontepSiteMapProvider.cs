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
using Acontep.Dado;
//using Acontep.AD.Manutencao.Seguranca;
using System.Configuration;
using Acontep;
using System.Runtime.CompilerServices;
using System.Web.Caching;
using Acontep.AD.Manutencao.Seguranca;

namespace Acontep.Manutencao.Seguranca
{
    /// <summary>
    /// Classe que implementa o SiteMapProvider
    /// </summary>
    public abstract class AcontepSiteMapProvider : StaticSiteMapProvider
    {
        private bool _RebuildSiteMap;
        private SiteMapNode _NoRaiz;

        public AcontepSiteMapProvider()
        {
        }

        /// <summary>
        /// Cria os nós do que correspondem ao Sistema/Módulo
        /// </summary>
        /// <param name="raizRow">Raiz</param>
        /// <returns></returns>
        private SiteMapNode CriarRaizSiteMapNode(NosSiteMapDataSet.RaizRow raizRow)
        {
            SiteMapNode node = new SiteMapNode(
                this,
                raizRow.Codigo,
                raizRow.IsUrlNull() ? String.Empty : raizRow.Url,
                raizRow.Nome,
                raizRow.IsDescricaoNull() ? String.Empty : raizRow.Descricao
            );
            node.Roles = new string[] { "*" };
            return node;
        }

        /// <summary>
        /// Cria os nós que representam as Planta/Função
        /// </summary>
        /// <param name="noRow">Nó do raiz</param>
        /// <returns></returns>
        private SiteMapNode CriarNoSiteMapNode(NosSiteMapDataSet.NoRow noRow)
        {
            SiteMapNode node = new SiteMapNode(
                            this,
                            noRow.Codigo,
                            //Real
                            noRow.IsUrlNull() ? String.Empty : noRow.Url,
                            //noRow.IsUrlNull() ? String.Empty : noRow.Url + "?sis=" + noRow.CodigoRaiz.ToString() + "&plt=" + noRow.Codigo.Split('|')[1],                            
                            noRow.Nome,
                            noRow.IsDescricaoNull() ? String.Empty : noRow.Descricao
                            );
            node.Roles = new string[] { "*" };
            return node;
        }

        /// <summary>
        /// Inicializa as configurações lendo do web.config
        /// </summary>
        /// <param name="name"></param>
        /// <param name="config"></param>
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
        }

        private bool EhParaConstruirSiteMap()
        {
            return AuthenticationTicket.Atual != null && (this._RebuildSiteMap || this._NoRaiz == null);
        }

        /// <summary>
        /// Constroi o mapa do site baseada na permissão do usuário logado
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public override SiteMapNode BuildSiteMap()
        {
            if (EhParaConstruirSiteMap())
            {
                Clear();
                _NoRaiz = new SiteMapNode(this, Guid.NewGuid().ToString(), "~/", String.Empty);
                _NoRaiz.Roles = new string[] { "*" };
                AddNode(_NoRaiz);

                bool acessoViaInternet = AuthenticationTicket.Atual.AcessoViaInternet;
                NosSiteMapDataSet nosSiteMapDataSet;
                using (Escopo escopo = new Escopo())
                {
                    nosSiteMapDataSet = ConstruirSiteNodes();
                }
                MapearNosSiteMapDataSetParaSiteMapNode(nosSiteMapDataSet, acessoViaInternet);
                this._RebuildSiteMap = false;
            }
            return this._NoRaiz;
        }

        private void MapearNosSiteMapDataSetParaSiteMapNode(NosSiteMapDataSet nosSiteMapDataSet, bool acessoViaInternet)
        {
            foreach (NosSiteMapDataSet.RaizRow raizRow in nosSiteMapDataSet.Raiz)
            {
                if (acessoViaInternet && !raizRow.PermiteAcessoInternet)
                    continue;
                raizRow.Nome = raizRow.Nome.Trim();
                SiteMapNode raizMapNode = CriarRaizSiteMapNode(raizRow);                
                foreach (NosSiteMapDataSet.NoRow noRow in raizRow.GetChildRows("Raiz_No"))
                {
                    if (acessoViaInternet && !noRow.PermiteAcessoInternet)
                        continue;

                    SiteMapNode noMapNode = CriarNoSiteMapNode(noRow);
                    AddNode(noMapNode, raizMapNode);
                }
                AddNode(raizMapNode, _NoRaiz);
            }
        }

        protected abstract NosSiteMapDataSet ConstruirSiteNodes();

        /// <summary>
        /// Retorna o mapa do site criado.
        /// </summary>
        /// <returns></returns>
        protected override SiteMapNode GetRootNodeCore()
        {
            if (AuthenticationTicket.Atual == null)
            {
                Clear();
                _NoRaiz = new SiteMapNode(this, "S" + Guid.NewGuid(), "~/", "Inicio");
                _NoRaiz.Roles = new string[] { "*" };
                AddNode(_NoRaiz);
            }
            else
            {
                _NoRaiz = null;
                BuildSiteMap();
            }
            return this._NoRaiz;
        }
    }
}