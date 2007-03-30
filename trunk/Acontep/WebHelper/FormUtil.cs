using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web;
using System.Data;
using Secv.Dado;
using FredCK.FCKeditorV2;
using System.Configuration;
using System.IO;

namespace Secv.WebHelper
{
    /// <summary>
    /// Necessidade de tamanho do form para a funcao. Padrao eh <see cref="ConfigTamanhoForm.Pequeno"/>
    /// </summary>
    public enum ConfigTamanhoForm
    {
        /// <summary>
        /// 800x600
        /// </summary>
        Pequeno,
        /// <summary>
        /// 1024x768
        /// </summary>
        Grande,
        /// <summary>
        /// Se adapta a resolução do usuário
        /// </summary>
        Dinamico
    }

    /// <summary>
    /// 
    /// </summary>
    public class FormUtil
    {
        private const string _CONFIG_TAMANHO_FORM_GUID = "ConfigTamanhoForm";
        private const string _CONFIG_MENU_SISTEMA_GUID = "ConfigMenuSistema";

        /// <summary>
        /// Obtem ou atribui uma configuracao para o tamanho do form, se nao existir valor configurado retorna <see cref="ConfigTamanhoForm.Pequeno"/>
        /// </summary>
        public static ConfigTamanhoForm ConfigTamanhoForm
        {
            set
            {
                HttpContext.Current.Items[_CONFIG_TAMANHO_FORM_GUID] = value;
            }
            get
            {
                object value = HttpContext.Current.Items[_CONFIG_TAMANHO_FORM_GUID];
                if (value is ConfigTamanhoForm)
                {
                    return (ConfigTamanhoForm)value;
                }
                else
                {
                    return ConfigTamanhoForm.Pequeno;
                }
            }
        }

        /// <summary>
        /// Retorna se o menu de sistema deve ser escondido por configuração da função.
        /// Está propriedade não sera configurada em postback da pagina.
        /// </summary>
        public static bool EsconderMenuSistemas
        {
            get
            {                
                object value = HttpContext.Current.Items[_CONFIG_MENU_SISTEMA_GUID];
                if (value is bool)
                {
                    if ((bool)value == true)
                    {
                        value = false;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            set
            {
                if (!((Page)HttpContext.Current.CurrentHandler).IsPostBack)
                    HttpContext.Current.Items[_CONFIG_MENU_SISTEMA_GUID] = value;
            }
        }

        /// <summary>
        /// Abre uma janela PopUp que não pode ser redimencionada 
        /// </summary>
        /// <param name="pFormularioQueChama">Formulário que quer abrir o popup</param>
        /// <param name="pUrl">url do popup</param>
        /// <param name="pWidth">largura do popup</param>
        /// <param name="pHeight">altura do popup</param>
        /// <param name="scrollbars">habilitar scrollbar</param>
        public static void AbrirPopUp(System.Web.UI.Page pFormularioQueChama, string pUrl, int pWidth, int pHeight, bool scrollbars)
        {
            AbrirPopUp(pFormularioQueChama, pUrl, pWidth, pHeight, false, scrollbars);
        }

        /// <summary>
        /// Abre uma janela PopUp que não pode ser redimencionada 
        /// </summary>
        /// <param name="pFormularioQueChama">Formulário que quer abrir o popup</param>
        /// <param name="pUrl">url do popup</param>
        /// <param name="pWidth">largura do popup</param>
        /// <param name="pTopo">Topo da pagina?</param>
        /// <param name="pHeight">altura do popup</param>
        /// <param name="scrollbars">habilitar scrollbar</param>
        public static void AbrirPopUp(System.Web.UI.Page pFormularioQueChama, string pUrl, int pWidth, int pHeight, bool pTopo,bool scrollbars)
        {
            
            string FuncaoPopUp = ObterFuncaoPopup(pUrl, pWidth, pHeight, pTopo, scrollbars);
            //"window.open \"" + pUrl + "\", null, \"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=0,width=" + pWidth.ToString() + ",height=" + pHeight.ToString() + "\");\n"; //+ 
            //"End Sub";

            StringBuilder Script = new StringBuilder();
            Script.Append("\n\n\n");
            Script.Append("<SCRIPT FOR=window EVENT=onload LANGUAGE=\"JScript\">");
            Script.Append("\n\n");
            Script.Append(FuncaoPopUp);
            Script.Append("\n\n");
            Script.Append("</script>");
            Script.Append("\n\n");

            if (!pFormularioQueChama.ClientScript.IsStartupScriptRegistered("Startup"))
                pFormularioQueChama.ClientScript.RegisterStartupScript(pFormularioQueChama.GetType(), "Startup", Script.ToString());
        }

        /// <summary>
        /// Retorna a função popup.
        /// </summary>
        /// <param name="pUrl">URL</param>
        /// <param name="pWidth">Width </param>
        /// <param name="pHeight">Height</param>
        /// <param name="pTopo">if set to <c>true</c> topo.</param>
        /// <param name="scrollbars">if set to <c>true</c> [scrollbars].</param>
        /// <returns></returns>
        public static string ObterFuncaoPopup(string pUrl, int pWidth, int pHeight, bool pTopo, bool scrollbars)
        {
            string scroll = "no";
            if (scrollbars)
                scroll = "yes";

            string FuncaoPopUp; //"Sub Window_onload()" + "\n" +				
            if (!pTopo)
            {
                FuncaoPopUp = string.Format("window.open(\"{0}\",null,\"height={1}px,width={2}px,status=yes,toolbar=no,menubar=no,location=no,scrollbars={3} center=true\");", pUrl, pHeight.ToString(), pWidth.ToString(), scroll);
            }
            else
            {
                FuncaoPopUp = string.Format("window.open(\"{0}\",null,\"height={1}px,width={2}px,status=yes,toolbar=no,menubar=no,location=no,scrollbars={3} top=0; left= 0\");", pUrl, pHeight.ToString(), pWidth.ToString(), scroll);
            }
            return FuncaoPopUp;
        }
        /// <summary>
        /// retorna a funcao popup modal.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="resizable">if set to <c>true</c> [resizable].</param>
        /// <returns></returns>
        public static string ObterFuncaoPopupModal(string url, int width, int height, bool resizable)
        {
            return "window.showModalDialog( \"" + url + "\", null, \"dialogHeight: " + width + "px; dialogWidth: " + height + "px; dialogTop: px; dialogLeft: px; edge: Raised; center: Yes; help: No; resizable: " + ( resizable ? "yes" : "no") + "; status: No;\" ); ";
        }

        /// <summary>
        /// Localiza um controle dentro da coleção de controles passado
        /// </summary>
        /// <typeparam name="T">Tipo de controle</typeparam>
        /// <param name="controls">Coleção de controles</param>
        /// <param name="ID">ID do controle a ser pesquisado</param>
        /// <returns>Retorna o controle especificado pelo tipo T e pelo seu ID</returns>
        public static T ObterControle<T>(ControlCollection controls, string ID) where T: Control
        {
            foreach (Control controle in controls)
            {
                if (controle.Controls.Count > 0)
                {
                    T obj = ObterControle<T>(controle.Controls, ID);
                    if (obj != null)
                        return obj;
                }
                if (controle is T)
                {
                    if (controle.ID == ID)
                        return (T)controle;
                    //else  // Ver necessidade o return externo resolve
                        //return null;
                }
            }
            return null;
        }

        ///// <summary>
        ///// Localiza um controle dentro da coleção de controles passado
        ///// </summary>
        ///// <typeparam name="T">Tipo de controle</typeparam>
        ///// <param name="controls">Coleção de controles</param>
        ///// <returns>Retorna o primeiro controle do tipo especificado</returns>
        //public static T ObterControle<T>(ControlCollection controls) where T : Control
        //{
        //    foreach (Control controle in controls)
        //    {
        //        if (controle.Controls.Count > 0)
        //        {
        //            T obj = ObterControle<T>(controle.Controls);
        //            if (obj != null)
        //                return obj;
        //        }
        //        if (controle is T)
        //            return (T)controle;
        //    }
        //    return null;
        //}

        //public static T ObterControle<T>(Array controls, string ID) where T : Control
        //{
        //    foreach (Control control in controls)
        //    {
        //        if (control is T && control.ID == ID)
        //            return (T)control;
        //    }
        //    return null;            
        //}
        /// <summary>
        /// Localiza controles do tipo especificado
        /// </summary>
        /// <typeparam name="T">Tipo de controle</typeparam>
        /// <param name="controls">Coleção de controles</param>
        /// <returns>Retorna a lista de controles do tipo especificado</returns>
        public static T[] ObterControle<T>(ControlCollection controls) where T : Control            
        {            
            List<T> items = new List<T>();
            //ArrayList controles = new ArrayList();
            //foreach (Control controle in controls)
            //{
            //    if (controls is WebControl)
            //        controles.Add(controle);
            //}
            foreach (T controle in controls)
            {
                if (controle.Controls.Count > 0)
                {
                    T[] obj = ObterControle<T>(controle.Controls);
                    if (obj != null)
                    {
                        foreach (T i in obj)
                        {
                            //if (items.BinarySearch(i) == -1)
                            items.Add(i);
                        }
                    }
                }
                if (controle is T)
                    items.Add(controle);
            }
            if (items.Count == 0)
                return null;
            else
                return items.ToArray();
        }

        /// <summary>
        /// localiza um controle dentro da coleção de controles passado
        /// </summary> 
        /// <param name="controls">coleção de controles</param>
        /// <param name="ID"> ID do controle a ser localizado</param>
        /// <returns>retorna o controle com o id especificado</returns>
        public static Control ObterControle(ControlCollection controls, string ID)
        {
            foreach (Control controle in controls)
            {
                if (controle.Controls.Count > 0)
                {
                    Control obj = ObterControle(controle.Controls, ID);
                    if (obj != null)
                        return obj;
                }
                if (controle.ID == ID)
                    return controle;
            }
            return null;
        }


        ///// <summary>
        ///// Redireciona para o modulo e a função informados, dentro do mesmo sistema
        ///// </summary>
        ///// <param name="mod_CodMod">Código do módulo</param>
        ///// <param name="fun_CodFun">Código da função</param>
        ///// <param name="usarServerTransfer">Usar o metodo de redirecionamento Server.Trasnfer</param>
        //public static void Redirecionar(short mod_CodMod, byte fun_CodFun, bool usarServerTransfer)
        //{
        //    // Precisa fazer acesso a dados (quebrar WebUtil em outra dll?)
        //    string caminho = FuncaoAD.ObterPor_PK_FUNCAO(SecvAuthenticationTicket.Atual.Sis_CodSis,mod_CodMod,fun_CodFun);
        //    if(dte.Rows.Count>0)
        //    {
        //        if (usarServerTransfer)
        //            ((Page)(HttpContext.Current.CurrentHandler)).Server.Transfer(caminho);
        //        else
        //            ((Page)(HttpContext.Current.CurrentHandler)).Response.Redirect(caminho);
        //    }
        //    else
        //        MensagemUtil.AcessoInvalidoException();
        //}

        /// <summary>
        /// Retorna o caminho da função solicitada
        /// </summary>
        /// <param name="sis_CodSis"></param>
        /// <param name="mod_CodMod"></param>
        /// <param name="fun_CodFun"></param>
        /// <returns></returns>
        private static string ObterCaminhoFuncao(short sis_CodSis, short mod_CodMod, byte fun_CodFun)
        {
            string sql = @"
SELECT 	
	Fun_Url 	 
FROM 
	FUNCAO
	  
 WHERE 
	( Sis_CodSis LIKE @Sis_CodSis ) AND ( Mod_CodMod LIKE @Mod_CodMod ) AND ( Fun_CodFun LIKE @Fun_CodFun )
    and (Fun_Ativo = 1)";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Sis_CodSis", System.Data.DbType.Int16, -1, sis_CodSis);
            bd.AdicionarParametro("@Mod_CodMod", System.Data.DbType.Int16, -1, mod_CodMod);
            bd.AdicionarParametro("@Fun_CodFun", System.Data.DbType.Byte, -1, fun_CodFun);
            return bd.ExecuteScalar<string>();
        }

        /// <summary>
        /// Redireciona para o sistema, módulo e função informados com opção de usar ServerTransfer
        /// </summary>
        /// <param name="sis_CodSis">Código do sistema</param>
        /// <param name="mod_CodMod">Código do módulo</param>
        /// <param name="fun_CodFun">Código da função</param>
        /// <param name="usarServerTransfer">Usar o metodo de redirecionamento Server.Trasnfer</param>
        public static void Redirecionar(short sis_CodSis, short mod_CodMod, byte fun_CodFun, bool usarServerTransfer)
        {
            // Testar este metodo quando acessando sistemas legados
            // Precisa fazer acesso a dados (quebrar WebUtil em outra dll?)
            
            string caminho = ObterCaminhoFuncao(sis_CodSis, mod_CodMod, fun_CodFun);
            if(!string.IsNullOrEmpty(caminho))
                if (usarServerTransfer)
                    ((Page)(HttpContext.Current.CurrentHandler)).Server.Transfer(caminho);
                else
                    ((Page)(HttpContext.Current.CurrentHandler)).Response.Redirect(caminho);
            else
                MensagemUtil.AcessoInvalidoException();
        }

        /// <summary>
        /// Redireciona para o sistema, módulo e função informados
        /// </summary>
        /// <param name="sis_CodSis">Código do sistema</param>
        /// <param name="mod_CodMod">Código do módulo</param>
        /// <param name="fun_CodFun">Código da função</param>
        /// <param name="usarServerTransfer">Usar o metodo de redirecionamento Server.Trasnfer</param>
        public static void Redirecionar(short sis_CodSis, short mod_CodMod, byte fun_CodFun)
        {
            // Testar este metodo quando acessando sistemas legados
            // Precisa fazer acesso a dados (quebrar WebUtil em outra dll?)

            string caminho = ObterCaminhoFuncao(sis_CodSis, mod_CodMod, fun_CodFun);
            if (!string.IsNullOrEmpty(caminho))
                    ((Page)(HttpContext.Current.CurrentHandler)).Response.Redirect(caminho);
            else
                MensagemUtil.AcessoInvalidoException();
        }

        /// <summary>
        /// Percorre todos os WebControls da pagina e faz um reset de seus valores
        /// </summary>
        /// <param name="controles"></param>
        public static void LimparControles(ControlCollection controles)
        {
            foreach (Control controle in controles)
            {
                // Se controle for um container limpa os seus valores tambem 
                if (controle.Controls.Count > 0)
                    LimparControles(controle.Controls);

                if (controle is TextBox)
                {
                    ((TextBox)controle).Text = String.Empty;
                }
                else if (controle is ListControl)
                {
                    ((ListControl)controle).SelectedIndex = -1;
                }
                else if (controle is CheckBox)
                {
                    ((CheckBox)controle).Checked = false;
                }
            }
        }
        /// <summary>
        /// Configura o FkEditor com o formato definido como padrão
        /// </summary>
        /// <remarks> Devido a a classe do FKEditor não ser temavel</remarks>
        /// <param name="editores">Editores a serem configurados</param>
        public static void ConfigurarFCKEditor(params FCKeditor[] editores)
        {
            ConfigurarFCKEditor(350, editores);
        }
        /// <summary>
        /// Configura o FkEditor com o formato definido como padrão apenas ajustando o tamanho
        /// </summary>
        /// <param name="Height">Altura do Editor</param>
        /// <param name="editores">Editores a serem configurados</param>
        public static void ConfigurarFCKEditor(int Height, params FCKeditor[] editores)
        {
            if ( editores == null ) return;
            foreach (FCKeditor editor in editores)
            {
                editor.BasePath = "~/FCKeditor/";
                editor.ToolbarSet = "Secv";
                editor.Height = Unit.Pixel(Height);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="value"></param>
        /// <param name="idVazio"></param>
        /// <param name="throwOnNotFound"></param>
        /// <returns></returns>
        public static int MarcarLista(ListControl lista, string value, int? idVazio, bool throwOnNotFound)
        {
            lista.ClearSelection();
            idVazio = ! idVazio.HasValue? -1: idVazio;            
            for (int i = 0; i < lista.Items.Count; i++)
            {
                if (string.IsNullOrEmpty(lista.Items[i].Value))
                {
                    idVazio = i;
                    if (string.IsNullOrEmpty(value))
                    {
                        lista.Items[i].Selected = true;
                        return idVazio.Value;
                    }
                }
                else if (lista.Items[i].Value.Equals(value))
                {
                    lista.Items[i].Selected = true;
                    return i;
                }
            }
            if (!throwOnNotFound)
            {
                lista.SelectedIndex = idVazio.Value;
                return idVazio.Value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("value");
            }
        }
       
        /// <summary>
        /// Retorna o diretorio onde estão guardados os arquivos para transferência.
        /// Todos os arquivos nessa pasta estão passíveis de um "delete".
        /// Caso não exista, é criado o diretório.
        /// </summary>
        /// <returns></returns>
        public static string ObterPathRepositorioTransfer()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["PathRepositorioTransfer"];
            CriarPathSeNaoExitir(path);
            return path;
        }
        /// <summary>
        /// Retorna o diretorio onde estão guardados os arquivos de cada sistema.
        /// Todos os arquivos nessa pasta sofreão backup.
        /// Caso não exista, é criado o diretório.
        /// </summary>
        /// <returns></returns>
        public static string ObterPathRepositorioSistemas(int sis_CodSis)
        {
            string path = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["PathRepositorioSistemas"], sis_CodSis.ToString());
            CriarPathSeNaoExitir(path);
            return path;
        }    
        /// <summary>
        /// Retorna o diretorio onde estão guardados os arquivos de cada tabela. Por padrão, o nome do arquivo será o ID do registro da tabela.
        /// Todos os arquivos nessa pasta sofreão backup.
        /// Caso não exista, é criado o diretório.
        /// </summary>
        /// <returns></returns>
        public static string ObterPathRepositorioTabelas(string nomeTabela)
        {
            string path = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["PathRepositorioTabelas"], nomeTabela);
            CriarPathSeNaoExitir(path);
            return path;
        }

        /// <summary>
        /// Retorna o path do arquivo. Caso não exista, retorna nulo.
        /// </summary>
        /// <param name="tabela">The tabela.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static string ObterPathArquivo(string tabela, int id)
        {
            string path = ObterPathRepositorioTabelas(tabela);
            foreach (string arquivo in Directory.GetFiles(path, id + ".*"))
            {
                return arquivo;
            }
            return null;
        }

        /// <summary>
        /// Apaga o arquivo caso ele exista
        /// </summary>
        /// <param name="tabela">nome da tabela</param>
        /// <param name="id">id da tabela</param>
        public static void ApagarArquivo(string tabela, int id)
        {
            string path = ObterPathArquivo( tabela, id);
            if (path != null && File.Exists(path ) )
            {
                File.Delete(path);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="id"></param>
        /// <param name="original_fileName"></param>
        /// <param name="conteudo"></param>
        public static void AtualizarArquivo(string tabela, int id, string original_fileName, byte[] conteudo)
        {
            ApagarArquivo(tabela, id);
            string diretorio = Secv.WebHelper.FormUtil.ObterPathRepositorioTabelas(tabela);
            string caminho = Path.Combine(diretorio, id + Path.GetExtension(original_fileName));
            using (Stream file = new StreamWriter(caminho).BaseStream)
            {
                file.Write(conteudo, 0, conteudo.Length);
            }
        }
        private static void CriarPathSeNaoExitir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

    }
}
