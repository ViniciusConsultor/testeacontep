<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="PlanoTrabalhoForm.aspx.cs" Inherits="Sistema_Contratos_Cadastros_PlanoTrabalhoForm"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel runat="server" ID="pnlPesquisa">
        <table>
            <tr>
                <td style="width: 20%">
                    Pesquisar Por:</td>
                <td style="width: 80%">
                    <asp:DropDownList ID="ddlTipoPesquisa" runat="server" OnSelectedIndexChanged="ddlTipoPesquisa_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="CP">C&#243;digo do plano de trabalho</asp:ListItem>
                        <asp:ListItem Value="CE">C&#243;digo da emenda</asp:ListItem>
                        <asp:ListItem Value="DC">Data de contrata&#231;&#227;o</asp:ListItem>
                        <asp:ListItem Value="DV">Data de vig&#234;ncia</asp:ListItem>
                        <asp:ListItem Value="DE">Data de empenho</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    Valor:</td>
                <td>
                    <ew:MaskedTextBox ID="tbxPesquisa" runat="server"></ew:MaskedTextBox>
                    <ew:CalendarPopup ID="tbxDataPesquisa" runat="server" Visible="False">
                    </ew:CalendarPopup>
                </td>
            </tr>
            <tr>
                <td class="BotaoAcao" colspan="2">
                    <asp:Button ID="btnPesquisar" runat="server" OnClick="btnPesquisar_Click" SkinID="Pesquisar" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <x:GridView ID="gvwItem" runat="server" AscImage="" AutoGenerateColumns="False" DescImage=""
                        GenerateColumnSelectIndex="0" ModelSelect="ColumnAndRowSelected" DataSourceID="odsPesquisar" DataKeyNames="IDPlanoTrabalho">
                        <Columns>
                            <asp:BoundField DataField="CodigoPlanoTrabalho" HeaderText="C&#243;digo" />
                            <asp:BoundField DataField="Acao" HeaderText="A&#231;&#227;o" />
                            <asp:BoundField ApplyFormatInEditMode="True" DataField="DataContratacao" DataFormatString="{0:d}"
                                HeaderText="Data de contrata&#231;&#227;o" HtmlEncode="False" />
                        </Columns>
                    </x:GridView>
                    <asp:ObjectDataSource ID="odsPesquisar" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="PesquisarPlanoTrabalho" TypeName="Acontep.Fn.Contratos.PlanoTrabalhoFn" OnSelecting="odsPesquisar_Selecting">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlTipoPesquisa" Name="tipoPesquisa" PropertyName="SelectedValue"
                                Type="String" />
                            <asp:ControlParameter ControlID="tbxPesquisa" Name="valor" PropertyName="Text" Type="String" />
                            <asp:Parameter Name="Data" Type="DateTime" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td class="BotaoAcao" colspan="2">
                    <asp:Button ID="btnIncluir" runat="server" OnClick="btnIncluir_Click" SkinID="Incluir" />
                    <asp:Button ID="btnAlterar" runat="server" SkinID="Alterar" />
                    <asp:Button ID="btnExcluir" runat="server" SkinID="Excluir" /></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlEdicao" runat="server" Width="100%">
        <asp:FormView ID="fvwItem" runat="server" DataSourceID="odsManutencao" DataKeyNames="IDPlanoTrabalho">
            <InsertItemTemplate>
                <table>
                    <tr>
                        <td style="width: 20%">
                            Código:</td>
                        <td style="width: 80%">
                            <ew:MaskedTextBox ID="tbxCodigo" runat="server" Mask="999.999-99/9999" Text='<%# Bind("CodigoPlanoTrabalho") %>'></ew:MaskedTextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Beneficiário:</td>
                        <td>
                            <asp:DropDownList ID="ddlCliente" runat="server" DataSourceID="odsClientes" DataTextField="Nome"
                                DataValueField="IDCliente" SelectedValue='<%# Bind("IDCliente") %>'>
                            </asp:DropDownList><asp:ObjectDataSource ID="odsClientes" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="ObterClientes" TypeName="Acontep.Fn.Contratos.PlanoTrabalhoFn"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Ação:</td>
                        <td>
                            <asp:TextBox ID="tbxAcao" runat="server" MaxLength="400" Text='<%# Bind("Acao") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Objeto:</td>
                        <td>
                            <asp:TextBox ID="tbxObjeto" runat="server" MaxLength="400" Text='<%# Bind("Objeto") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Situação Inst. Cadastral:</td>
                        <td>
                            <asp:TextBox ID="tbxSitInstCadastral" runat="server" MaxLength="255" Text='<%# Bind("SitInstCadastral") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Situação da Engenharia:</td>
                        <td>
                            <asp:TextBox ID="tbxSitEngenharia" runat="server" MaxLength="255" Text='<%# Bind("SitEngenharia") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Situação Social:</td>
                        <td>
                            <asp:TextBox ID="tbxSitSocial" runat="server" Text='<%# Bind("SitSocial") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Data Contratação:</td>
                        <td>
                            <ew:CalendarPopup ID="tbxDataContratacao" runat="server" Culture="Portuguese (Brazil)"
                                PostedDate="" SelectedDate="<%# DateTime.Now %>" SelectedValue='<%# Bind("DataContratacao") %>'
                                UpperBoundDate="12/31/9999 23:59:59" VisibleDate="04/09/2007 23:00:34">
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Data de Vigência:</td>
                        <td>
                            <ew:CalendarPopup ID="tbxDataVigencia" runat="server" Culture="Portuguese (Brazil)"
                                PostedDate="" SelectedDate="<%# DateTime.Now %>" SelectedValue='<%# Bind("DataVigencia") %>'
                                UpperBoundDate="12/31/9999 23:59:59" VisibleDate="04/09/2007 23:01:24">
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Data de Vigência Prorrogada 1:</td>
                        <td>
                            <ew:CalendarPopup ID="tbxDataVigenciaProrrog1" runat="server" Culture="Portuguese (Brazil)"
                                PostedDate="" SelectedDate="<%# DateTime.Now %>" SelectedValue='<%# Bind("DataVigenciaProrrog1") %>'
                                UpperBoundDate="12/31/9999 23:59:59" VisibleDate="04/09/2007 23:02:14">
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Data de Vigência Prorrogada 2:</td>
                        <td>
                            <ew:CalendarPopup ID="tbxDataVigenciaProrrog2" runat="server" Culture="Portuguese (Brazil)"
                                PostedDate="" SelectedDate="<%# DateTime.Now %>" SelectedValue='<%# Bind("DataVigenciaProrrog2") %>'
                                UpperBoundDate="12/31/9999 23:59:59" VisibleDate="04/09/2007 23:02:49">
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Valor do Repasse:</td>
                        <td>
                            <ew:NumericBox ID="tbxValorRepasse" runat="server" SkinID="Valor" Text='<%# Bind("ValorRepasse") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td>
                            Valor da Contra Partida:</td>
                        <td>
                            <ew:NumericBox ID="tbxValorContraPartida" runat="server" SkinID="Valor" Text='<%# Bind("ValorContraPartida") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td>
                            Valor da Contra Partida Engenharia:</td>
                        <td>
                            <ew:NumericBox ID="tbxValorContraPartidaEngenharia" runat="server" SkinID="Valor"
                                Text='<%# Bind("ValorContraPartidaEngenharia") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td>
                            Valor da Contra Partida Social:</td>
                        <td>
                            <ew:NumericBox ID="tbxValorContraPartidaSocial" runat="server" SkinID="Valor" Text='<%# Bind("ValorContraPartidaSocial") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td>
                            Emenda:</td>
                        <td>
                            <asp:DropDownList ID="ddlEmenda" runat="server" DataSourceID="odsEmenda" DataTextField="Numero"
                                DataValueField="IDEmenda" SelectedValue='<%# Bind("IDEmenda") %>'>
                            </asp:DropDownList><asp:ObjectDataSource ID="odsEmenda" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="ObterPor_UQ_Emenda" TypeName="Acontep.AD.Contratos.EmendaAD">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="%" Name="Numero" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Data de Reformulação do Plano de Trabalho:</td>
                        <td>
                            <ew:CalendarPopup ID="tbxDataReformulacaoPlanoTrabalho" runat="server" Culture="Portuguese (Brazil)"
                                PostedDate="" SelectedDate="<%# DateTime.Now %>" SelectedValue='<%# Bind("DataReformulacaoPlanoTrabalho") %>'
                                UpperBoundDate="12/31/9999 23:59:59" VisibleDate="04/09/2007 23:13:21">
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Reformulação do Plano de Trabalho:</td>
                        <td>
                            <asp:TextBox ID="tbxReformulacaoPlanoTrabalho" runat="server" Text='<%# Bind("ReformulacaoPlanoTrabalho") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Programa:</td>
                        <td>
                            <asp:TextBox ID="tbxPrograma" runat="server" Text='<%# Bind("Programa") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Cód. Empenho:</td>
                        <td>
                            <ew:NumericBox ID="tbxCodEmpenho" runat="server" SkinID="Inteiro" Text='<%# Bind("CodEmpenho") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td>
                            Data do Empenho:</td>
                        <td>
                            <ew:CalendarPopup ID="tbxDataEmpenho" runat="server" Culture="Portuguese (Brazil)"
                                PostedDate="" SelectedDate="<%# DateTime.Now %>" SelectedValue='<%# Bind("DataEmpenho") %>'
                                UpperBoundDate="12/31/9999 23:59:59" VisibleDate="04/09/2007 23:16:12">
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cód. do Empenho Invest.:</td>
                        <td>
                            <ew:NumericBox ID="NumericBox1" runat="server" Text='<%# Bind("CodEmpenhoInvest") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td>
                            Data do Empenho Invest.:</td>
                        <td>
                            <ew:CalendarPopup ID="tbxDataEmpenhoInvest" runat="server" Culture="Portuguese (Brazil)"
                                PostedDate="" SelectedDate="<%# DateTime.Now %>" SelectedValue='<%# Bind("DataEmpenhoInvest") %>'
                                UpperBoundDate="12/31/9999 23:59:59" VisibleDate="04/09/2007 23:18:06">
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cód. Empenho Custeio:</td>
                        <td>
                            <ew:NumericBox ID="tbxCodEmpenhoCusteio" runat="server"></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td>
                            Natureza da Despesa:</td>
                        <td>
                            <asp:TextBox ID="tbxNaturezaDespesa" runat="server" Text='<%# Bind("NaturezaDespesa") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Valor do Investimento:</td>
                        <td>
                            <ew:NumericBox ID="tbxValorInvestimento" runat="server" SkinID="Valor" Text='<%# Bind("ValorInvestimento") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <asp:Button ID="btnInserir" runat="server" CommandName="Insert" SkinID="Incluir" />
                            <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" SkinID="Cancelar" /></td>
                    </tr>
                </table>
            </InsertItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="odsManutencao" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="ObterPor_PK_PlanoTrabalho" TypeName="Acontep.AD.Contratos.PlanoTrabalhoAD" DeleteMethod="Apagar" InsertMethod="Inserir" UpdateMethod="Alterar">
            <DeleteParameters>
                <asp:Parameter Name="original_IDPlanoTrabalho" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="original_IDPlanoTrabalho" Type="Int32" />
                <asp:Parameter Name="CodigoPlanoTrabalho" Type="String" />
                <asp:Parameter Name="Acao" Type="String" />
                <asp:Parameter Name="Objeto" Type="String" />
                <asp:Parameter Name="SitInstCadastral" Type="String" />
                <asp:Parameter Name="SitEngenharia" Type="String" />
                <asp:Parameter Name="SitSocial" Type="String" />
                <asp:Parameter Name="DataContratacao" Type="DateTime" />
                <asp:Parameter Name="DataVigencia" Type="DateTime" />
                <asp:Parameter Name="DataVigenciaProrrog1" Type="DateTime" />
                <asp:Parameter Name="DataVigenciaProrrog2" Type="DateTime" />
                <asp:Parameter Name="ValorRepasse" Type="Decimal" />
                <asp:Parameter Name="ValorContraPartida" Type="Decimal" />
                <asp:Parameter Name="ValorContraPartidaEngenharia" Type="Decimal" />
                <asp:Parameter Name="ValorContraPartidaSocial" Type="Decimal" />
                <asp:Parameter Name="IDEmenda" Type="Int32" />
                <asp:Parameter Name="DataReformulacaoPlanoTrabalho" Type="DateTime" />
                <asp:Parameter Name="ReformulacaoPlanoTrabalho" Type="String" />
                <asp:Parameter Name="Programa" Type="String" />
                <asp:Parameter Name="DataEmpenho" Type="DateTime" />
                <asp:Parameter Name="CodEmpenho" Type="Int32" />
                <asp:Parameter Name="DataEmpenhoInvest" Type="DateTime" />
                <asp:Parameter Name="CodEmpenhoInvest" Type="Int32" />
                <asp:Parameter Name="DataEmpenhoCusteio" Type="DateTime" />
                <asp:Parameter Name="CodEmpenhoCusteio" Type="Int32" />
                <asp:Parameter Name="IDCliente" Type="Int32" />
                <asp:Parameter Name="NaturezaDespesa" Type="String" />
                <asp:Parameter Name="ValorInvestimento" Type="Decimal" />
            </UpdateParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="gvwItem" Name="IDPlanoTrabalho" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="CodigoPlanoTrabalho" Type="String" />
                <asp:Parameter Name="Acao" Type="String" />
                <asp:Parameter Name="Objeto" Type="String" />
                <asp:Parameter Name="SitInstCadastral" Type="String" />
                <asp:Parameter Name="SitEngenharia" Type="String" />
                <asp:Parameter Name="SitSocial" Type="String" />
                <asp:Parameter Name="DataContratacao" Type="DateTime" />
                <asp:Parameter Name="DataVigencia" Type="DateTime" />
                <asp:Parameter Name="DataVigenciaProrrog1" Type="DateTime" />
                <asp:Parameter Name="DataVigenciaProrrog2" Type="DateTime" />
                <asp:Parameter Name="ValorRepasse" Type="Decimal" />
                <asp:Parameter Name="ValorContraPartida" Type="Decimal" />
                <asp:Parameter Name="ValorContraPartidaEngenharia" Type="Decimal" />
                <asp:Parameter Name="ValorContraPartidaSocial" Type="Decimal" />
                <asp:Parameter Name="IDEmenda" Type="Int32" />
                <asp:Parameter Name="DataReformulacaoPlanoTrabalho" Type="DateTime" />
                <asp:Parameter Name="ReformulacaoPlanoTrabalho" Type="String" />
                <asp:Parameter Name="Programa" Type="String" />
                <asp:Parameter Name="DataEmpenho" Type="DateTime" />
                <asp:Parameter Name="CodEmpenho" Type="Int32" />
                <asp:Parameter Name="DataEmpenhoInvest" Type="DateTime" />
                <asp:Parameter Name="CodEmpenhoInvest" Type="Int32" />
                <asp:Parameter Name="DataEmpenhoCusteio" Type="DateTime" />
                <asp:Parameter Name="CodEmpenhoCusteio" Type="Int32" />
                <asp:Parameter Name="IDCliente" Type="Int32" />
                <asp:Parameter Name="NaturezaDespesa" Type="String" />
                <asp:Parameter Name="ValorInvestimento" Type="Decimal" />
            </InsertParameters>
        </asp:ObjectDataSource>
    </asp:Panel>
</asp:Content>
