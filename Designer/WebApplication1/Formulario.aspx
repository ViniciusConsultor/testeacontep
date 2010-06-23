<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Formulario.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlViewingDashboard" SkinID="Accordion">
        <asp:Panel runat="server" ID="Panel2" SkinID="Accordion-Titulo">
            Botões</asp:Panel>
        <asp:Panel runat="server" ID="Panel1" SkinID="Accordion-Conteudo">
            <asp:LinkButton SkinID="Adicionar" ID="LinkButton2" Text="" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Adicionar" ID="LinkButton3" Text="Adicionar" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Atualizar" ID="LinkButton4" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Atualizar" ID="btnAvancar" Text="Atualizar" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Avancar" ID="LinkButton5" Text="" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Avancar" ID="LinkButton6" Text="Avançar" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Cancelar" ID="LinkButton7" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Cancelar" ID="LinkButton8" Text="Cancelar" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Confirmar" ID="LinkButton9" Text="" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Confirmar" ID="LinkButton10" Text="Confirmar" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Excluir" ID="LinkButton11" Text="" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Excluir" ID="LinkButton12" Text="Excluir" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Help" ID="LinkButton13" Text="" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Help" ID="LinkButton14" Text="Help" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Info" ID="LinkButton16" Text="" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Info" ID="LinkButton15" Text="Info" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Localizar" ID="LinkButton17" Text="" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Localizar" ID="LinkButton18" Text="Localizar" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Novo" ID="LinkButton19" Text="" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Novo" ID="LinkButton20" Text="Novo" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Relacionar" ID="LinkButton21" Text="" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Relacionar" ID="LinkButton22" Text="Relacionar" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Remover" ID="LinkButton23" Text="" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Remover" ID="LinkButton24" Text="Remover" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Salvar" ID="LinkButton25" Text="" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Salvar" ID="LinkButton26" Text="Salvar" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Voltar" ID="LinkButton27" Text="" runat="server"></asp:LinkButton>
            <asp:LinkButton SkinID="Voltar" ID="LinkButton28" Text="Voltar" runat="server"></asp:LinkButton>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel5" SkinID="Accordion">
        <asp:Panel runat="server" ID="Panel6" SkinID="Accordion-Titulo">
            Grid</asp:Panel>
        <asp:Panel runat="server" ID="Panel7" SkinID="Accordion-Conteudo">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="5">
                <Columns>
                    <asp:BoundField DataField="nome" HeaderText="Nome" />
                    <asp:BoundField DataField="email" HeaderText="email" />
                    <asp:BoundField DataField="telefone" HeaderText="telefone" />
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="nome" HeaderText="Nome" />
                    <asp:BoundField DataField="email" HeaderText="email" />
                    <asp:BoundField DataField="telefone" HeaderText="telefone" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel3" SkinID="Accordion">
        <asp:Panel runat="server" ID="Panel4" SkinID="Accordion-Titulo">
            Titulo</asp:Panel>
        <asp:Panel runat="server" ID="Panel8" SkinID="Accordion-Conteudo">
            <a class="btn ui-state-default ui-corner-all" id="dialog_link" href="#"><span class="ui-icon ui-icon-newwin">
            </span>Open Dialog</a>
            <br />
            <asp:Button runat="server" ID="btnSkinPadrao" Text="Botao - Skin Padrao" />
            <asp:LinkButton runat="server" ID="lkbBotaoComImagem" SkinID="BotaoComImagem" Text="Link Button - BotaoComImagem"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="lkbBotao" Text="Link Button - Skin Padrão"></asp:LinkButton>
            <br />
            <asp:Button runat="server" ID="btnNovo" Text="Botão" CssClass="btn_no_image ui-state-default ui-corner-all" />
            <asp:LinkButton runat="server" ID="LinkButton1" Text="Link Button 1" CssClass="btn ui-state-default ui-corner-all ui-icon ui-icon-newwin"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="lnkNovo" Text="Link Button" CssClass="btn_no_image ui-state-default ui-corner-all"></asp:LinkButton>
            <a class="btn ui-state-default ui-corner-all" id="A1" href="#"><span class="ui-icon ui-icon-newwin">
            </span>Open Dialog</a>
            <asp:Panel runat="server" ID="pnlTitulo" SkinID="Titulo">
                Título
            </asp:Panel>
            Texto qualquer
            <br />
            <asp:Label runat="server" ID="Label4" Text="Campo micro" SkinID="LabelDoCampo" />
            <br />
            <asp:TextBox runat="server" ID="TextBox4" SkinID="Micro" />
            <br />
            <asp:Label runat="server" ID="lblField" Text="Campo pequeno" SkinID="LabelDoCampo" />
            <br />
            <asp:TextBox runat="server" ID="tbxPequeno" />
            <br />
            <asp:Label runat="server" ID="Label3" Text="Medio" SkinID="LabelDoCampo" />
            <br />
            <asp:TextBox runat="server" ID="TextBox3" SkinID="Medio" />
            <br />
            <asp:Label runat="server" ID="Label1" Text="Grande" SkinID="LabelDoCampo" />
            <br />
            <asp:TextBox runat="server" ID="TextBox1" SkinID="Grande" />
            <br />
            <asp:TextBox runat="server" ID="TextBox5" SkinID="100%" />
            <br />
            <asp:Label runat="server" ID="Label2" Text="Multi linha" SkinID="LabelDoCampo" />
            <br />
            <asp:TextBox runat="server" ID="TextBox2" SkinID="Mlinha_Pequeno" TextMode="MultiLine" />
            <br />
            <asp:Literal ID="Literal1" runat="server" Text="literal"></asp:Literal>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="dropdownlist" SkinID="FundoVermelho">
        <asp:Panel runat="server" ID="dropdownlistTitulo" SkinID="Titulo">
            Listas
        </asp:Panel>
        <asp:DropDownList runat="server" ID="DropDownList1" SkinID="Pequeno">
            <asp:ListItem Text="Item 1"></asp:ListItem>
            <asp:ListItem Text="Item 2"></asp:ListItem>
            <asp:ListItem Text="Item 3"></asp:ListItem>
            <asp:ListItem Text="Item 4"></asp:ListItem>
            <asp:ListItem Text="Item 5"></asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:DropDownList runat="server" ID="DropDownList2" SkinID="Medio">
            <asp:ListItem Text="Item 1"></asp:ListItem>
            <asp:ListItem Text="Item 2"></asp:ListItem>
            <asp:ListItem Text="Item 3"></asp:ListItem>
            <asp:ListItem Text="Item 4"></asp:ListItem>
            <asp:ListItem Text="Item 5"></asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:DropDownList runat="server" ID="DropDownList3" SkinID="Grande">
            <asp:ListItem Text="Item 1"></asp:ListItem>
            <asp:ListItem Text="Item 2"></asp:ListItem>
            <asp:ListItem Text="Item 3"></asp:ListItem>
            <asp:ListItem Text="Item 4"></asp:ListItem>
            <asp:ListItem Text="Item 5"></asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:DropDownList runat="server" ID="ddl1" SkinID="100%">
            <asp:ListItem Text="Item 1"></asp:ListItem>
            <asp:ListItem Text="Item 2"></asp:ListItem>
            <asp:ListItem Text="Item 3"></asp:ListItem>
            <asp:ListItem Text="Item 4"></asp:ListItem>
            <asp:ListItem Text="Item 5"></asp:ListItem>
        </asp:DropDownList>
    </asp:Panel>
    <div class="title title-spacing">
        <h2>
            Form Example 1</h2>
        This form has all inputs set to large, so they will be 100% in width.
    </div>
    <ul>
        <li>
            <label class="desc">
                Name ( input class="small" )
            </label>
            <div>
                <input type="text" tabindex="1" maxlength="255" value="" class="field text small"
                    name="" />
            </div>
        </li>
        <li>
            <label class="desc">
                Input ( input class="medium" )
            </label>
            <div>
                <input type="text" tabindex="1" maxlength="255" value="" class="field text medium"
                    name="" />
            </div>
        </li>
        <li>
            <label class="desc">
                Input ( input class="large" )
            </label>
            <div>
                <input type="text" tabindex="1" maxlength="255" value="" class="field text large"
                    name="" />
            </div>
        </li>
        <li>
            <label class="desc">
                Textarea ( input class="small" )
            </label>
            <div>
                <textarea tabindex="2" cols="50" rows="5" class="field textarea small" name=""></textarea>
            </div>
        </li>
        <li>
            <label class="desc">
                Other
            </label>
            <div class="float-left">
                <span>
                    <input type="text" tabindex="6" value="" class="field text" name="" />
                    <label>
                        Example</label>
                </span>
            </div>
            <div class="float-left">
                <span>
                    <input type="text" tabindex="6" value="" class="field text" name="" />
                    <label>
                        Example</label>
                </span>
            </div>
            <div class="float-left">
                <span>
                    <input type="text" tabindex="6" value="" class="field text" name="" />
                    <label>
                        Example</label>
                </span>
            </div>
            <div class="float-left">
                <span>
                    <input type="text" tabindex="6" value="" class="field text" name="" />
                    <label>
                        Example</label>
                </span>
            </div>
        </li>
        <li>
            <label class="desc">
                Priority
            </label>
            <div>
                <select tabindex="3" class="field select large" name="">
                    <option value="Low">Low </option>
                    <option value="Medium">Medium </option>
                    <option value="High">High </option>
                </select>
            </div>
        </li>
        <li class="date">
            <label for="Field2" id="title2" class="desc">
                Due Date
            </label>
            <span>
                <input type="text" tabindex="5" maxlength="2" size="2" value="" class="field text"
                    name="" />
                <label>
                    MM</label>
            </span><span class="symbol">/</span> <span>
                <input type="text" tabindex="6" maxlength="2" size="2" value="" class="field text"
                    name="" />
                <label>
                    DD</label>
            </span><span class="symbol">/</span> <span>
                <input type="text" tabindex="7" maxlength="4" size="4" value="" class="field text"
                    name="" />
                <label>
                    YYYY</label>
            </span></li>
        <li>
            <label for="Field4" id="title4" class="desc">
                Status
            </label>
            <div class="col">
                <span>
                    <input type="checkbox" tabindex="8" value="Done." class="field checkbox" name="Field4"
                        id="Field4" />
                    <label for="Field4" class="choice">
                        Done.</label>
                </span>
            </div>
        </li>
        <li class="buttons">
            <input type="submit" value="Submit" class="submit" />
        </li>
    </ul>
    </form>
    <div class="clearfix">
    </div>
    <div class="title title-spacing">
        <h2>
            Form inside a box</h2>
        Lorem ipsum dolor sic amet.
    </div>
    <div class="portlet ui-widget ui-widget-content ui-helper-clearfix ui-corner-all form-container">
        <div class="portlet-header ui-widget-header">
            Form elements in box</div>
        <div class="portlet-content">
            <form action="#" method="post" enctype="multipart/form-data" class="forms" name="form">
            <ul>
                <li>
                    <label class="desc">
                        Name ( input class="small" )
                    </label>
                    <div>
                        <input type="text" tabindex="1" maxlength="255" value="" class="field text small"
                            name="" />
                    </div>
                </li>
                <li>
                    <label class="desc">
                        Input ( input class="medium" )
                    </label>
                    <div>
                        <input type="text" tabindex="1" maxlength="255" value="" class="field text medium"
                            name="" />
                    </div>
                </li>
                <li>
                    <label class="desc">
                        Input ( input class="large" )
                    </label>
                    <div>
                        <input type="text" tabindex="1" maxlength="255" value="" class="field text large"
                            name="" />
                    </div>
                </li>
                <li>
                    <label class="desc">
                        Textarea ( input class="small" )
                    </label>
                    <div>
                        <textarea tabindex="2" cols="50" rows="5" class="field textarea small" name=""></textarea>
                    </div>
                </li>
                <li>
                    <label class="desc">
                        Other
                    </label>
                    <div class="float-left">
                        <span>
                            <input type="text" tabindex="6" value="" class="field text" name="" />
                            <label>
                                Example</label>
                        </span>
                    </div>
                    <div class="float-left">
                        <span>
                            <input type="text" tabindex="6" value="" class="field text" name="" />
                            <label>
                                Example</label>
                        </span>
                    </div>
                    <div class="float-left">
                        <span>
                            <input type="text" tabindex="6" value="" class="field text" name="" />
                            <label>
                                Example</label>
                        </span>
                    </div>
                    <div class="float-left">
                        <span>
                            <input type="text" tabindex="6" value="" class="field text" name="" />
                            <label>
                                Example</label>
                        </span>
                    </div>
                </li>
                <li>
                    <label class="desc">
                        Priority
                    </label>
                    <div>
                        <select tabindex="3" class="field select large" name="">
                            <option value="Low">Low </option>
                            <option value="Medium">Medium </option>
                            <option value="High">High </option>
                        </select>
                    </div>
                </li>
                <li class="date">
                    <label for="Field2" id="title2" class="desc">
                        Due Date
                    </label>
                    <span>
                        <input type="text" tabindex="5" maxlength="2" size="2" value="" class="field text"
                            name="" />
                        <label>
                            MM</label>
                    </span><span class="symbol">/</span> <span>
                        <input type="text" tabindex="6" maxlength="2" size="2" value="" class="field text"
                            name="" />
                        <label>
                            DD</label>
                    </span><span class="symbol">/</span> <span>
                        <input type="text" tabindex="7" maxlength="4" size="4" value="" class="field text"
                            name="" />
                        <label>
                            YYYY</label>
                    </span></li>
                <li>
                    <label for="Field4" id="title4" class="desc">
                        Status
                    </label>
                    <div class="col">
                        <span>
                            <input type="checkbox" tabindex="8" value="" class="field checkbox" name="" />
                            <label class="choice">
                                Done.</label>
                        </span>
                    </div>
                </li>
                <li class="buttons">
                    <input type="submit" value="Submit" class="submit" />
                </li>
            </ul>
            </form>
        </div>
    </div>
    <div class="clearfix">
    </div>
    <i class="note">* Just a simple note here ...</i>
</asp:Content>
