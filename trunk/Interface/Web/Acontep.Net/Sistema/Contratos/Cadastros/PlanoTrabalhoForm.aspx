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
                    <asp:DropDownList ID="ddlTipoPesquisa" runat="server" OnSelectedIndexChanged="ddlTipoPesquisa_SelectedIndexChanged">
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
                <td class="BotaoAcao" colspan="2">
                    <x:GridView ID="gvwItem" runat="server" AscImage="" AutoGenerateColumns="False" DescImage=""
                        GenerateColumnSelectIndex="0" ModelSelect="ColumnAndRowSelected" WebControlCustomColection="">
                        <Columns>
                            <asp:BoundField DataField="CodigoPlanoTrabalho" HeaderText="C&#243;digo" />
                            <asp:BoundField DataField="Acao" HeaderText="A&#231;&#227;o" />
                            <asp:BoundField ApplyFormatInEditMode="True" DataField="DataContratacao" DataFormatString="{0:d}"
                                HeaderText="Data de contrata&#231;&#227;o" HtmlEncode="False" />
                        </Columns>
                    </x:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlEdicao" runat="server" Width="100%">
        <asp:FormView ID="fvwItem" runat="server" DataSourceID="odsManutencao">
            <InsertItemTemplate>
                <table>
                    <tr>
                        <td style="width: 100px">
                            Código:</td>
                        <td style="width: 100px">
                            <ew:MaskedTextBox ID="tbxCodigo" runat="server" Mask="999.999-99/9999" Text='<%# Bind("CodigoPlanoTrabalho") %>'></ew:MaskedTextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            Beneficiário:</td>
                        <td style="width: 100px">
                            <asp:DropDownList ID="ddlCliente" runat="server" DataSourceID="odsClientes" DataTextField="Nome"
                                DataValueField="IDCliente" SelectedValue='<%# Bind("IDCliente") %>'>
                            </asp:DropDownList><asp:ObjectDataSource ID="odsClientes" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="ObterClientes" TypeName="Acontep.Fn.Contratos.PlanoTrabalhoFn"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            Ação:</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="tbxAcao" runat="server" MaxLength="400" Text='<%# Bind("Acao") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            Objeto:</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="tbxObjeto" runat="server" MaxLength="400" Text='<%# Bind("Objeto") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            SitInstCadastral:</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="tbxSitInstCadastral" runat="server" MaxLength="255" Text='<%# Bind("SitInstCadastral") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            SitEngenharia:</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="tbxSitEngenharia" runat="server" MaxLength="255" Text='<%# Bind("SitEngenharia") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            SitSocial:</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="tbxSitSocial" runat="server" Text='<%# Bind("SitSocial") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            DataContratacao:</td>
                        <td style="width: 100px">
                            <ew:CalendarPopup ID="tbxDataContratacao" runat="server" Culture="Portuguese (Brazil)"
                                PostedDate="" SelectedDate="<%# DateTime.Now %>" SelectedValue='<%# Bind("DataContratacao") %>'
                                UpperBoundDate="12/31/9999 23:59:59" VisibleDate="04/09/2007 23:00:34">
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            DataVigencia:</td>
                        <td style="width: 100px">
                            <ew:CalendarPopup ID="tbxDataVigencia" runat="server" Culture="Portuguese (Brazil)"
                                PostedDate="" SelectedDate="<%# DateTime.Now %>" SelectedValue='<%# Bind("DataVigencia") %>'
                                UpperBoundDate="12/31/9999 23:59:59" VisibleDate="04/09/2007 23:01:24">
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            DataVigenciaProrrog1</td>
                        <td style="width: 100px">
                            <ew:CalendarPopup ID="tbxDataVigenciaProrrog1" runat="server" Culture="Portuguese (Brazil)"
                                PostedDate="" SelectedDate="<%# DateTime.Now %>" SelectedValue='<%# Bind("DataVigenciaProrrog1") %>'
                                UpperBoundDate="12/31/9999 23:59:59" VisibleDate="04/09/2007 23:02:14">
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            DataVigenciaProrrog2</td>
                        <td style="width: 100px">
                            <ew:CalendarPopup ID="tbxDataVigenciaProrrog2" runat="server" Culture="Portuguese (Brazil)"
                                PostedDate="" SelectedDate="<%# DateTime.Now %>" SelectedValue='<%# Bind("DataVigenciaProrrog2") %>'
                                UpperBoundDate="12/31/9999 23:59:59" VisibleDate="04/09/2007 23:02:49">
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            ValorRepasse</td>
                        <td style="width: 100px">
                            <ew:NumericBox ID="tbxValorRepasse" runat="server" SkinID="Valor" Text='<%# Bind("ValorRepasse") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            ValorContraPartida</td>
                        <td style="width: 100px">
                            <ew:NumericBox ID="tbxValorContraPartida" runat="server" SkinID="Valor" Text='<%# Bind("ValorContraPartida") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            ValorContraPartidaEngenharia</td>
                        <td style="width: 100px">
                            <ew:NumericBox ID="tbxValorContraPartidaEngenharia" runat="server" SkinID="Valor"
                                Text='<%# Bind("ValorContraPartidaEngenharia") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            ValorContraPartidaSocial</td>
                        <td style="width: 100px">
                            <ew:NumericBox ID="tbxValorContraPartidaSocial" runat="server" SkinID="Valor" Text='<%# Bind("ValorContraPartidaSocial") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            Emenda:</td>
                        <td style="width: 100px">
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
                        <td style="width: 100px">
                            DataReformulacaoPlanoTrabalho</td>
                        <td style="width: 100px">
                            <ew:CalendarPopup ID="tbxDataReformulacaoPlanoTrabalho" runat="server" Culture="Portuguese (Brazil)"
                                PostedDate="" SelectedDate="<%# DateTime.Now %>" SelectedValue='<%# Bind("DataReformulacaoPlanoTrabalho") %>'
                                UpperBoundDate="12/31/9999 23:59:59" VisibleDate="04/09/2007 23:13:21">
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            ReformulacaoPlanoTrabalho</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="tbxReformulacaoPlanoTrabalho" runat="server" Text='<%# Bind("ReformulacaoPlanoTrabalho") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            Programa:</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="tbxPrograma" runat="server" Text='<%# Bind("Programa") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            CodEmpenho</td>
                        <td style="width: 100px">
                            <ew:NumericBox ID="tbxCodEmpenho" runat="server" SkinID="Inteiro" Text='<%# Bind("CodEmpenho") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            DataEmpenho</td>
                        <td style="width: 100px">
                            <ew:CalendarPopup ID="tbxDataEmpenho" runat="server" Culture="Portuguese (Brazil)"
                                PostedDate="" SelectedDate="<%# DateTime.Now %>" SelectedValue='<%# Bind("DataEmpenho") %>'
                                UpperBoundDate="12/31/9999 23:59:59" VisibleDate="04/09/2007 23:16:12">
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            CodEmpenhoInvest</td>
                        <td style="width: 100px">
                            <ew:NumericBox ID="NumericBox1" runat="server" Text='<%# Bind("CodEmpenhoInvest") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            DataEmpenhoInvest</td>
                        <td style="width: 100px">
                            <ew:CalendarPopup ID="tbxDataEmpenhoInvest" runat="server" Culture="Portuguese (Brazil)"
                                PostedDate="" SelectedDate="<%# DateTime.Now %>" SelectedValue='<%# Bind("DataEmpenhoInvest") %>'
                                UpperBoundDate="12/31/9999 23:59:59" VisibleDate="04/09/2007 23:18:06">
                            </ew:CalendarPopup>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            CodEmpenhoCusteio</td>
                        <td style="width: 100px">
                            <ew:NumericBox ID="tbxCodEmpenhoCusteio" runat="server"></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            NaturezaDespesa</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="tbxNaturezaDespesa" runat="server" Text='<%# Bind("NaturezaDespesa") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            ValorInvestimento</td>
                        <td style="width: 100px">
                            <ew:NumericBox ID="tbxValorInvestimento" runat="server" SkinID="Valor" Text='<%# Bind("ValorInvestimento") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="odsManutencao" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="ObterClientes" TypeName="Acontep.Fn.Contratos.PlanoTrabalhoFn"></asp:ObjectDataSource>
    </asp:Panel>
</asp:Content>
