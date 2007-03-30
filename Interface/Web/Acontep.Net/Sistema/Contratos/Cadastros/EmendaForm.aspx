<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmendaForm.aspx.cs" Inherits="Sistema_Contratos_Cadastros_EmendaForm"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlGeralEmendas" runat="server" Width="100%">
        <table width="100%">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td nowrap="nowrap" style="width: 25%">
                                Número:</td>
                            <td style="width: 25%">
                                <asp:TextBox ID="tbxNumero" runat="server"></asp:TextBox></td>
                            <td nowrap="nowrap" style="width: 25%">
                                N° Funcional:</td>
                            <td style="width: 25%">
                                <ew:MaskedTextBox ID="tbxNumeroFuncional" runat="server" Mask="99.999.9999.9999.9999"></ew:MaskedTextBox></td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                Área:</td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlArea" runat="server" AppendDataBoundItems="True" DataSourceID="odsArea"
                                    DataTextField="Nome" DataValueField="IDArea" OnDataBinding="TextoTodos_DataBinding">
                                </asp:DropDownList><asp:ObjectDataSource ID="odsArea" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="ObterPor_UQ_Area" TypeName="Acontep.AD.Contratos.AreaAD">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                Tipo Realização:</td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlTipoRealizacao" runat="server" AppendDataBoundItems="True"
                                    DataSourceID="odsTipoRealizacao" DataTextField="Nome" DataValueField="IDTipoRealizacao"
                                    OnDataBinding="TextoTodos_DataBinding">
                                </asp:DropDownList><asp:ObjectDataSource ID="odsTipoRealizacao" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="ObterPor_UQ_TipoRealizacao" TypeName="Acontep.AD.Contratos.TipoRealizacaoAD">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                Autor:</td>
                            <td colspan="2">
                                <asp:TextBox ID="tbxAutor" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="btnPesquisar" runat="server" SkinID="Pesquisar" OnClick="btnPesquisar_Click" /></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Panel ID="pnlEmendas" runat="server" GroupingText="Emendas:" Width="100%" Height="200px"
                                    ScrollBars="Vertical">
                                    <x:GridView ID="gvwEmenda" runat="server" AscImage="" AutoGenerateColumns="False"
                                        Width="100%" AutoGenerateColumnSelect="RadioButton" AutoGenerateColumnSelectAutoPostBack="True"
                                        DataKeyNames="IDEmenda" DataSourceID="odsEmenda" DescImage="" GenerateColumnSelectIndex="0"
                                        ModelSelect="ColumnAndRowSelected" AutoGenerateSelectButton="True">
                                        <Columns>
                                            <asp:BoundField DataField="Numero" HeaderText="N&#250;mero" />
                                            <asp:BoundField DataField="NumeroFuncional" HeaderText="N&#176; Funcional" />
                                            <asp:BoundField DataField="Autor" HeaderText="Autor" />
                                            <asp:BoundField DataField="NomeCidade" HeaderText="Cidade" />
                                        </Columns>
                                    </x:GridView>
                                </asp:Panel>
                                <asp:ObjectDataSource ID="odsEmenda" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="ObterEmenda" TypeName="Acontep.AD.Contratos.EmendaAD" DeleteMethod="Apagar" InsertMethod="Inserir" UpdateMethod="Alterar">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="tbxNumero" Name="Numero" PropertyName="Text" Type="Int32" />
                                        <asp:ControlParameter ControlID="tbxNumeroFuncional" Name="NumeroFuncional" PropertyName="Text"
                                            Type="Int32" />
                                        <asp:ControlParameter ControlID="ddlArea" Name="IDArea" PropertyName="SelectedValue"
                                            Type="Int32" />
                                        <asp:ControlParameter ControlID="ddlTipoRealizacao" Name="IDTipoRealizacao" PropertyName="SelectedValue"
                                            Type="Int32" />
                                        <asp:ControlParameter ControlID="tbxAutor" Name="Autor" PropertyName="Text" Type="String" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_IDEmenda" Type="Int32" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="original_IDEmenda" Type="Int32" />
                                        <asp:Parameter Name="Numero" Type="Int32" />
                                        <asp:Parameter Name="IDArea" Type="Int32" />
                                        <asp:Parameter Name="IDTipoRealizacao" Type="Int32" />
                                        <asp:Parameter Name="IDCidade" Type="Int32" />
                                        <asp:Parameter Name="NumeroFuncional" Type="Int32" />
                                        <asp:Parameter Name="Valor" Type="Decimal" />
                                        <asp:Parameter Name="Autor" Type="String" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="Numero" Type="Int32" />
                                        <asp:Parameter Name="IDArea" Type="Int32" />
                                        <asp:Parameter Name="IDTipoRealizacao" Type="Int32" />
                                        <asp:Parameter Name="IDCidade" Type="Int32" />
                                        <asp:Parameter Name="NumeroFuncional" Type="Int32" />
                                        <asp:Parameter Name="Valor" Type="Decimal" />
                                        <asp:Parameter Name="Autor" Type="String" />
                                    </InsertParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="BotaoAcao" colspan="4">
                                <asp:Button ID="btnIncluir" runat="server" SkinID="Incluir" OnClick="btnIncluir_Click" />
                                <asp:Button ID="btnAlterar" runat="server" SkinID="Alterar" OnClick="btnAlterar_Click" />
                                <asp:Button ID="btnExcluir" runat="server" SkinID="Excluir" OnClick="btnExcluir_Click"
                                    OnClientClick="return confirm('Deseja excluir o item selecionado?')" CommandName="Delete" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlAlteracao" runat="server" Width="100%">
        <asp:FormView ID="fvwItem" runat="server" DataKeyNames="IDEmenda" DataSourceID="odsItem" Width="100%">
            <InsertItemTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%">
                            Número :</td>
                        <td style="width: 80%">
                            <asp:TextBox ID="tbxNumero" runat="server" Text='<%# Bind("Numero") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            N° Funcional:</td>
                        <td>
                            <ew:MaskedTextBox ID="tbxNumeroFuncional" runat="server" Mask="99.999.9999.9999.9999"
                                Text='<%# Bind("NumeroFuncional") %>'></ew:MaskedTextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Valor:</td>
                        <td>
                            <ew:NumericBox ID="tbxValor" runat="server" SkinID="Valor" Text='<%# Bind("Valor") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td>
                            Autor:</td>
                        <td>
                            <asp:TextBox ID="tbxAutor" runat="server" Text='<%# Bind("Autor") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Área:</td>
                        <td>
                            <asp:DropDownList ID="ddlArea" runat="server" AppendDataBoundItems="True" DataSourceID="odsArea"
                                    DataTextField="Nome" DataValueField="IDArea" OnDataBinding="TextoNenumItemInformado" SelectedValue='<%# Bind("IDArea") %>'>
                            </asp:DropDownList><asp:ObjectDataSource ID="odsArea" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="ObterPor_UQ_Area" TypeName="Acontep.AD.Contratos.AreaAD">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Tipo de realização:</td>
                        <td>
                            <asp:DropDownList ID="ddlTipoRealizacao" runat="server" AppendDataBoundItems="True"
                                    DataSourceID="odsTipoRealizacao" DataTextField="Nome" DataValueField="IDTipoRealizacao"
                                    OnDataBinding="TextoNenumItemInformado" SelectedValue='<%# Bind("IDTipoRealizacao") %>'>
                            </asp:DropDownList><asp:ObjectDataSource ID="odsTipoRealizacao" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="ObterPor_UQ_TipoRealizacao" TypeName="Acontep.AD.Contratos.TipoRealizacaoAD">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Estado:</td>
                        <td>
                           <asp:DropDownList ID="ddlEstado" runat="server" AppendDataBoundItems="True"
                                    DataSourceID="odsEstado" DataTextField="Nome" DataValueField="IDEstado"
                                    OnDataBinding="TextoNenumItemInformado" SelectedValue='<%# IDEstadoPadrao %>' AutoPostBack="True">
                            </asp:DropDownList><asp:ObjectDataSource ID="odsEstado" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="ObterPor_UQ_Nome" TypeName="Acontep.AD.Cadastros.EstadoAD">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cidade:</td>
                        <td>
                            <asp:DropDownList ID="ddlCidade" runat="server" AppendDataBoundItems="True"
                                    DataSourceID="odsCidade" DataTextField="Nome" DataValueField="IDCidade"
                                    OnDataBinding="TextoNenumItemInformado">
                            </asp:DropDownList><asp:ObjectDataSource ID="odsCidade" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="ObterPor_IX_Municipio" TypeName="Acontep.AD.Cadastros.MunicipioAD">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlEstado" DefaultValue="" Name="IDEstado" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="BotaoAcao" colspan="2">
                            <asp:Button ID="btnConfirmar" runat="server" CommandName="Insert" SkinID="Confirmar" />
                            <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" SkinID="Cancelar" OnClick="btnCancelar_Click" /></td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <EditItemTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%">
                            Número :</td>
                        <td style="width: 80%">
                            <asp:TextBox ID="tbxNumero" runat="server" Text='<%# Bind("Numero") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            N° Funcional:</td>
                        <td>
                            <ew:MaskedTextBox ID="tbxNumeroFuncional" runat="server" Mask="99.999.9999.9999.9999"
                                Text='<%# Bind("NumeroFuncional") %>'></ew:MaskedTextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Valor:</td>
                        <td>
                            <ew:NumericBox ID="tbxValor" runat="server" SkinID="Valor" Text='<%# Bind("Valor") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td>
                            Autor:</td>
                        <td>
                            <asp:TextBox ID="tbxAutor" runat="server" Text='<%# Bind("Autor") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Área:</td>
                        <td>
                            <asp:DropDownList ID="ddlArea" runat="server" AppendDataBoundItems="True" DataSourceID="odsArea"
                                    DataTextField="Nome" DataValueField="IDArea" OnDataBinding="TextoNenumItemInformado" SelectedValue='<%# Bind("IDArea") %>'>
                            </asp:DropDownList><asp:ObjectDataSource ID="odsArea" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="ObterPor_UQ_Area" TypeName="Acontep.AD.Contratos.AreaAD">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Tipo de realização:</td>
                        <td>
                            <asp:DropDownList ID="ddlTipoRealizacao" runat="server" AppendDataBoundItems="True"
                                    DataSourceID="odsTipoRealizacao" DataTextField="Nome" DataValueField="IDTipoRealizacao"
                                    OnDataBinding="TextoNenumItemInformado" SelectedValue='<%# Bind("IDTipoRealizacao") %>'>
                            </asp:DropDownList><asp:ObjectDataSource ID="odsTipoRealizacao" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="ObterPor_UQ_TipoRealizacao" TypeName="Acontep.AD.Contratos.TipoRealizacaoAD">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Estado:</td>
                        <td>
                            <asp:DropDownList ID="ddlEstado" runat="server" AppendDataBoundItems="True"
                                    DataSourceID="odsEstado" DataTextField="Nome" DataValueField="IDEstado"
                                    OnDataBinding="TextoNenumItemInformado" SelectedValue='<%# Bind("IDEstado") %>' AutoPostBack="True">
                            </asp:DropDownList><asp:ObjectDataSource ID="odsEstado" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="ObterPor_UQ_Nome" TypeName="Acontep.AD.Cadastros.EstadoAD">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cidade:</td>
                        <td>
                            <asp:DropDownList ID="ddlCidade" runat="server" AppendDataBoundItems="True"
                                    DataSourceID="odsCidade" DataTextField="Nome" DataValueField="IDCidade"
                                    OnDataBinding="TextoNenumItemInformado">
                            </asp:DropDownList><asp:ObjectDataSource ID="odsCidade" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="ObterPor_IX_Municipio" TypeName="Acontep.AD.Cadastros.MunicipioAD">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlEstado" DefaultValue="" Name="IDEstado" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="BotaoAcao" colspan="2">
                            <asp:Button ID="btnConfirmar" runat="server" CommandName="Update" SkinID="Confirmar" />
                            <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" SkinID="Cancelar" OnClick="btnCancelar_Click" /></td>
                    </tr>
                </table>
            </EditItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="odsItem" runat="server" DeleteMethod="Apagar" InsertMethod="Inserir"
            OldValuesParameterFormatString="original_{0}" OnInserted="odsItem_Updated" OnInserting="odsManutencao_Inserting"
            OnUpdated="odsItem_Updated" OnUpdating="odsManutencao_Inserting" SelectMethod="ObterEmenda"
            TypeName="Acontep.AD.Contratos.EmendaAD" UpdateMethod="Alterar">
            <DeleteParameters>
                <asp:Parameter Name="original_IDEmenda" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="original_IDEmenda" Type="Int32" />
                <asp:Parameter Name="Numero" Type="Int32" />
                <asp:Parameter Name="IDArea" Type="Int32" />
                <asp:Parameter Name="IDTipoRealizacao" Type="Int32" />
                <asp:Parameter Name="IDCidade" Type="Int32" />
                <asp:Parameter Name="NumeroFuncional" Type="Int32" />
                <asp:Parameter Name="Valor" Type="Decimal" />
                <asp:Parameter Name="Autor" Type="String" />
            </UpdateParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="gvwEmenda" Name="IDEmenda" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="Numero" Type="Int32" />
                <asp:Parameter Name="IDArea" Type="Int32" />
                <asp:Parameter Name="IDTipoRealizacao" Type="Int32" />
                <asp:Parameter Name="IDCidade" Type="Int32" />
                <asp:Parameter Name="NumeroFuncional" Type="Int32" />
                <asp:Parameter Name="Valor" Type="Decimal" />
                <asp:Parameter Name="Autor" Type="String" />
            </InsertParameters>
        </asp:ObjectDataSource>
    </asp:Panel>
</asp:Content>
