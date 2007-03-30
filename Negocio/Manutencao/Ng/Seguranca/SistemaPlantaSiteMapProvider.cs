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
    public class SistemaPlantaSiteMapProvider : AcontepSiteMapProvider
    {
        protected override NosSiteMapDataSet ConstruirSiteNodes()
        {

            int usu_Id = AuthenticationTicket.Atual.IDUsuario;

            NosSiteMapDataSet nosSiteMapDataSet = new NosSiteMapDataSet();

            DataTable sistemaTable;
            //DataTable plantaTable;

            SiteMapAcessoDados siteMapAD = new SiteMapAcessoDados();

            sistemaTable = siteMapAD.ObterRaizSistemaPlantaParaMenu(usu_Id);
            //plantaTable = siteMapAD.ObterFolhaSistemaPlantaParaMenu(usu_Id);
            //foreach (DataRow row in plantaTable.Rows)
            //{
            //    row["Url"] = String.Format("~/Default.aspx?sis={0}", row["CodigoRaiz"], row["Codigo"].ToString().Split('|')[1]);
            //}
            nosSiteMapDataSet.Raiz.Merge(sistemaTable);
            foreach (DataRow row in nosSiteMapDataSet.Raiz.Rows)
            {
                row["Url"] = String.Format("~/Default.aspx?sis={0}", row["Codigo"]);
            }
            //nosSiteMapDataSet.No.Merge(plantaTable);
            foreach (NosSiteMapDataSet.RaizRow raizRow in nosSiteMapDataSet.Raiz)
            {
                DataRow[] noRows = raizRow.GetChildRows("Raiz_No");
                if (noRows.Length == 1)
                {
                    raizRow.Url = noRows[0]["Url"].ToString();
                    noRows[0].Delete();
                }
            }
            nosSiteMapDataSet.AcceptChanges();

            return nosSiteMapDataSet;
        }
    }
}