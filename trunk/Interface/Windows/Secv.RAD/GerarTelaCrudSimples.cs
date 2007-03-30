using System;
using System.Collections.Generic;
using System.Text;
using Acontep.Ng.Manutencao.Rad;
using System.Data;

namespace Acontep.RAD
{
    public class GerarTelaCrudSimples : GeradorSECV
    {
        public GerarTelaCrudSimples()
        {
            
        }
        public string GerarASPX(string MasterPage, EstruturaSaveRAD estruturaSaveRAD, EstruturaSaveRAD.ClasseCRUDRow ClasseSelecionada)
        {
            Conectar(ClasseSelecionada.StringConexao, ClasseSelecionada.Provider);
            EstruturaSaveRAD.MetodoRow MetodoCrud = CriarMetodoCRUD(estruturaSaveRAD, ClasseSelecionada);
            try{
            if( string.IsNullOrEmpty(MasterPage) )
                MasterPage = "~/MasterPages/Site.master";
            Tabela = ClasseSelecionada.Tabela;
            string Inherits = ClasseSelecionada.NameSpace + "_Manutencao." +  ClasseSelecionada.Tabela + "Form";
            Inherits = Inherits.Replace(".", "_");
            StringBuilder ASPX = new StringBuilder();
            ASPX.AppendFormat(@"<%@ Page Language=""C#"" MasterPageFile=""{0}"" AutoEventWireup=""true"" CodeFile=""Manutencao.{1}Form.aspx.cs"" Inherits=""{2}"" Title=""Untitled Page"" %>", MasterPage, ClasseSelecionada.Tabela, Inherits);
            ASPX.AppendLine();
            string Content = "Content1";
            string ContentPlaceHolderID = "cphPrincipal";
            ASPX.AppendFormat(@"<asp:Content ID=""{0}"" ContentPlaceHolderID=""{1}"" Runat=""Server"">", Content, ContentPlaceHolderID);
            ASPX.AppendLine();
            ASPX.Append(@"<asp:Panel ID=""pnlPesquisa"" runat=""server"" DefaultButton=""btnPesquisa"" GroupingText=""Opções de pesquisa"" Width=""100%"">");
            ASPX.AppendLine();
            ASPX.Append(MontarFiltrosParaHTML(ClasseSelecionada.ObterMetodoSelect()));
            ASPX.Append("<br />");
            ASPX.Append(@"<asp:Panel ID=""pnlItems"" runat=""server"" GroupingText=""Resultados"" Width=""100%"">");
            ASPX.AppendLine();
            ASPX.Append(MontarGridParaHTML(ClasseSelecionada));
            ASPX.AppendLine();
            ASPX.Append(MontarObjectDataSourceParaGridResultado(ClasseSelecionada));
            ASPX.AppendLine();

            ASPX.Append("</asp:Panel>");
            ASPX.AppendLine();
            ASPX.Append(@"<asp:Panel ID=""pnlBotoes"" runat=""server"" HorizontalAlign=""Right"" Width=""100%"">");
            ASPX.AppendLine();
            ASPX.Append(CriarBotaoParaHTML("Incluir", true, false, null));
            ASPX.Append(CriarBotaoParaHTML("Alterar", false, true, null));
            ASPX.Append(CriarBotaoParaHTML("Excluir", false, true, "Tem certeza que deseja realizar a exclusão do item?"));
            ASPX.Append("</asp:Panel>");
            ASPX.Append("</asp:Panel>");
            ASPX.Append("<br />");
            ASPX.Append(MontarObjectDataSourceParaFormView(ClasseSelecionada));
            ASPX.Append("</asp:Content>");
            return ASPX.ToString() ;
        }
        finally
        {
            MetodoCrud.Delete();
        }
        }

        private string MontarObjectDataSourceParaFormView(EstruturaSaveRAD.ClasseCRUDRow tabela)
        {
            string NomeTabela = tabela.Tabela.Replace(".", "_");
            StringBuilder objectDataSource = new StringBuilder();
            objectDataSource.AppendFormat(@"<asp:ObjectDataSource ID=""{0}fvw"" runat=""server"" DeleteMethod=""Apagar""", NomeTabela);
            objectDataSource.Append(@" InsertMethod=""Inserir"" OldValuesParameterFormatString=""original_{0}"" ");
            objectDataSource.AppendFormat(@" OnInserted=""{0}fvw_Inserted"" OnUpdated=""{0}fvw_Updated"" SelectMethod=""ObterPor_PK_{0}""",
                    NomeTabela);
            objectDataSource.AppendLine();

            objectDataSource.AppendFormat(@"TypeName=""{0}.{1}"" UpdateMethod=""Alterar"">",
                tabela.NameSpace,
                tabela.NomeClasse
                );
            objectDataSource.AppendLine();
            foreach (EstruturaSaveRAD.MetodoRow metodo in tabela.GetMetodoRows())
            {
                for (int i = 0; i < metodo.Opcoes.ToCharArray().Length; i++)
                {
                    objectDataSource.AppendFormat("<{0}>\n{1}\n</{0}>\n",
                        RetornarTipoParametroObjectDataSource(metodo),
                        CriarParametroObjectDataSourceHTML(metodo)
                    );
                    objectDataSource.AppendLine();
                }
            }
            objectDataSource.AppendFormat("</asp:ObjectDataSource>");
            return objectDataSource.ToString();
        }
        private List<DataColumn> RetornarListaParaParametros(EstruturaSaveRAD.MetodoRow mrw)
        {
            List<DataColumn> Colunas = new List<DataColumn>();

            if (mrw.Opcoes.IndexOfAny("UDS".ToCharArray()) > -1)
            {
                Colunas.AddRange(base.PrimaryKey);
            }

            if (mrw.Opcoes.IndexOfAny("IUS".ToCharArray()) > -1 )
            {
                foreach (DataColumn Filtro in base.RetornarColunas(mrw.GetListaCamposRows()))
                {
                    if (Filtro.AutoIncrement)
                        continue;

                    if (EhCampoTransacional(Filtro))
                        continue;
                    Colunas.Add(Filtro);
                }
            }
            return Colunas;
        }
        private string CriarBotaoParaHTML(string NomeSemPrefixo, bool Habilitado, bool CausarValidacao, string MensagemOnClientClick)
        {
            StringBuilder Botao = new StringBuilder();
            Botao.AppendFormat(@"<asp:Button ID=""btn{0}"" runat=""server"" CausesValidation=""{1}"" OnClick=""btn{0}_Click"" SkinID=""{0}"" ", NomeSemPrefixo, CausarValidacao);
            if (!string.IsNullOrEmpty(MensagemOnClientClick))
                Botao.AppendFormat("OnClientClick=\"return confirm('{0}')\" Enabled=\"{1}\"", MensagemOnClientClick, Habilitado);
            Botao.Append("/>");
            Botao.AppendLine();
            return Botao.ToString();
        }
        private string RetornarTipoParametroObjectDataSource(EstruturaSaveRAD.MetodoRow mrw)
        {
            string TipoParametro = String.Empty;
            if (mrw.Opcoes.IndexOf("S") > -1)
            {
                TipoParametro = "SelectParameters";
            }
            else if (mrw.Opcoes.IndexOf("D") > -1)
            {
                TipoParametro = "DeleteParameters";
            }
            else if (mrw.Opcoes.IndexOf("U") > -1)
            {
                TipoParametro = "UpdateParameters";
            }
            else if (mrw.Opcoes.IndexOf("I") > -1)
            {
                TipoParametro = "InsertParameters";
            }
            return TipoParametro;
        }

        private string CriarParametroObjectDataSourceHTML(EstruturaSaveRAD.MetodoRow mrw)
        {
            StringBuilder objectDataSource = new StringBuilder();
            
            foreach (DataColumn coluna in RetornarListaParaParametros(mrw))
            {
                objectDataSource.AppendFormat(@"<asp:Parameter Name=""original_{0}"" Type=""{1}"" />",
                    coluna.ColumnName,
                    coluna.DataType.Name
                );
                objectDataSource.AppendLine();
            }
            return objectDataSource.ToString();
        }

        
        private string MontarObjectDataSourceParaGridResultado(EstruturaSaveRAD.ClasseCRUDRow tabela)
        {
            //<asp:ObjectDataSource ID="odsItems" runat="server" OldValuesParameterFormatString="original_{0}"
            //            SelectMethod="ObterPor_IX_GrupoEmpresarial_Gre_Nome" TypeName="SisCompras.ManutencaoCadastros.Fn.GrupoEmpresarialFn">
            //            <SelectParameters>
            //                <asp:ControlParameter ControlID="tbxPesquisa" Name="Gre_Nome" PropertyName="Text"
            //                    Type="String" />
            //            </SelectParameters>
            //        </asp:ObjectDataSource>

            StringBuilder objectDataSource = new StringBuilder();
            objectDataSource.Append(@"<asp:ObjectDataSource ID=""odsItems"" runat=""server"" OldValuesParameterFormatString=""original_{0}""
                        SelectMethod=""ObterPor_TodosOsFiltros""");
            objectDataSource.AppendLine();
            objectDataSource.AppendFormat(@"TypeName=""{0}.{1}"" UpdateMethod=""Alterar"">",
                tabela.NameSpace,
                tabela.NomeClasse
                );
            objectDataSource.AppendLine();
            EstruturaSaveRAD.MetodoRow metodo = tabela.ObterMetodoSelect();
            
                objectDataSource.AppendFormat("<{0}>\n{1}\n</{0}>\n",
                    RetornarTipoParametroObjectDataSource(metodo),
                    CriarControlParameterHTML("TextBox", metodo)
                );
                objectDataSource.AppendLine();
            
            objectDataSource.AppendFormat("</asp:ObjectDataSource>");
            return objectDataSource.ToString();
        }
        /// <summary>
        /// Cria algo do tipo
        /// <SelectParameters>
        ///    <asp:ControlParameter ControlID="tbxPesquisa" Name="Gre_Nome" PropertyName="Text"
        ///        Type="String" />
        ///</SelectParameters>
        /// </summary>
        /// <param name="TipoControle">Tipo do controle que será relacionado o parametro</param>
        /// <returns></returns>
        private string CriarControlParameterHTML(string TipoControle, EstruturaSaveRAD.MetodoRow mrw)
        {
            StringBuilder objectDataSource = new StringBuilder();
            string PrefixoControle = String.Empty;
            string PropertyName = String.Empty;
            switch (TipoControle)
            {
                case "TextBox":
                    PrefixoControle = "tbx";
                    PropertyName = "Text";
                    break;
                case "GridView":
                    PrefixoControle = "grv";
                    PropertyName = "SelectedDataKey";
                    break;
                default:
                    throw new NotSupportedException("não foi definido relacionamento para o tipo do controle");
            }
            foreach (DataColumn coluna in RetornarListaParaParametros(mrw).ToArray())
            {
                objectDataSource.AppendFormat(@"<asp:ControlParameter Name=""{0}""  Type=""{1}"" ControlID=""{2}"" PropertyName=""{3}"" />",
                    coluna.ColumnName,
                    coluna.DataType.Name,
                    PrefixoControle,
                    PropertyName
                );
                objectDataSource.AppendLine();
            
            }
            return objectDataSource.ToString();
        
        }
        private string MontarGridParaHTML(EstruturaSaveRAD.ClasseCRUDRow tabela)
        {
            StringBuilder Grid = new StringBuilder();
            Grid.AppendFormat(@"<x:GridView ID=""gvwItems"" runat=""server"" AutoGenerateColumns=""False"" DataKeyNames=""{0}""
            DataSourceID=""odsItems"" EnableViewState=""False"" OnSelectedIndexChanged=""gvwItems_SelectedIndexChanged""
            Width=""100%"" AscImage="""" AutoGenerateColumnSelect=""RadioButton"" DescImage="""" GenerateColumnSelectIndex=""0"" ModelSelect=""ColumnAndRowSelected""
            WebControlCustomColection=""wccPesquisa"" OnDataBound=""gvwItems_DataBound"">",
                 string.Join(", ", base.PrimaryKeys));
            Grid.AppendLine();
            Grid.AppendFormat("<Columns>\n{0}\n</Columns>\n", CriarBoundFields(tabela.ObterMetodoSelect()));

            Grid.Append(@"</x:GridView>
                    <x:WebControlCustomColection ID=""wccPesquisa"" runat=""server"" WebControl-Capacity=""2""> 
                    <x:WebControlCustom Enable=""False"" Text=""btnIncluir"" Visible=""False"" />
                    <x:WebControlCustom Enable=""True"" Text=""btnAlterar"" Visible=""False"" />
                    </x:WebControlCustomColection>");
            Grid.AppendLine();

            return Grid.ToString();
        }

        private string CriarBoundFields(EstruturaSaveRAD.MetodoRow mrw)
        {
            if (mrw == null) return "";
            string Layout = @"<asp:BoundField DataField=""{0}"" HeaderText=""{1}""></asp:BoundField>";
            StringBuilder ListaBoundFields = new StringBuilder();
            foreach (DataColumn coluna in RetornarListaParaParametros(mrw).ToArray())
            {
                if (EhCampoTransacional(coluna)) 
                    continue;
                ListaBoundFields.AppendFormat(Layout,
                    coluna.ColumnName,
                    ObterNomeColunaSemPrefixo(coluna)
                );
                ListaBoundFields.AppendLine();
            }
            return ListaBoundFields.ToString();
        }

        private string MontarFiltrosParaHTML(EstruturaSaveRAD.MetodoRow mrw)
        {
            StringBuilder FiltrosHtml = new StringBuilder();
            FiltrosHtml.Append(@"<asp:Label ID=""lblPesquisa"" runat=""server"" Text=""Pesquisar:""></asp:Label>");
            FiltrosHtml.AppendLine();

            FiltrosHtml.Append(@"<table width=""100%"">");
            FiltrosHtml.AppendLine();
            if (mrw == null) return "";
            string Layout = @"<tr><td style=""Width:20%"">
                                       <asp:Label ID=""lbl{0}"" Text={1} runat=""server"" ></asp:Label>
                                  </td>
                                  <td style=""Width:80%"">
                                       <asp:TextBox ID=""tbx{0}"" runat=""server"" ></asp:TextBox>
                                  </td>
                              </tr>";
            foreach (DataColumn coluna in RetornarListaParaParametros(mrw).ToArray())
            {
                if (EhCampoTransacional(coluna))
                    continue;
                FiltrosHtml.AppendFormat(Layout,
                    coluna.ColumnName,
                    ObterNomeColunaSemPrefixo(coluna)
                );
            }

            FiltrosHtml.Append(@"</table>");
            FiltrosHtml.AppendLine();
            return FiltrosHtml.ToString();
            //        <tr>
            //            <td>
            //                <asp:TextBox ID="tbxPesquisa" runat="server" Width="281px"></asp:TextBox></td>
            //            <td style="width: 90px">
            //                &nbsp;<asp:Button ID="btnPesquisa" runat="server" CausesValidation="False" EnableViewState="False"
            //                    SkinID="Pesquisar" Text="Pesquisar" OnClick="btnPesquisa_Click" /></td>
            //        </tr>
            //    </table>
        }
 
    }
}
