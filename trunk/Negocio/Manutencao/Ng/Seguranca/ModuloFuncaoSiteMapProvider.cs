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
using Acontep.AD.Manutencao.Seguranca;

namespace Acontep.Manutencao.Seguranca
{
    /// <summary>
    /// Classe que implementa o SiteMapProvider
    /// </summary>
    public class ModuloFuncaoSiteMapProvider : AcontepSiteMapProvider
    {
        protected override NosSiteMapDataSet ConstruirSiteNodes()
        {
            //byte plt_Planta = AuthenticationTicket.Atual.Plt_Planta;
            string sis_CodSis = AuthenticationTicket.Atual.CodigoSistema;
            int usu_Id = AuthenticationTicket.Atual.IDUsuario;

            NosSiteMapDataSet nosSiteMapDataSet = new NosSiteMapDataSet();

            DataTable moduloTable;
            DataTable funcaoTable;

            SiteMapAcessoDados siteMapAD = new SiteMapAcessoDados();

            moduloTable = siteMapAD.ObterRaizModuloFuncaoParaMenu(sis_CodSis, usu_Id);
            funcaoTable = siteMapAD.ObterFolhaModuloFuncaoParaMenu(sis_CodSis, usu_Id);
            nosSiteMapDataSet.Raiz.Merge(moduloTable);
            nosSiteMapDataSet.No.Merge(funcaoTable);

            return nosSiteMapDataSet;
        }
    }
}