using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web;
using System.Data;
using Acontep.Dado;
using FredCK.FCKeditorV2;
using System.Configuration;
using System.IO;
using System.Web.Configuration;

namespace Acontep.UI.Web
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
                    string tamanhoForm = WebConfigurationManager.AppSettings["ConfigTamanhoForm"];
                    if (String.IsNullOrEmpty(tamanhoForm))
                        return ConfigTamanhoForm.Dinamico;
                    object configTamanhoForm = Enum.Parse(typeof(ConfigTamanhoForm), tamanhoForm);
                    if (configTamanhoForm != null)
                        return (ConfigTamanhoForm)configTamanhoForm;
                    else
                        return ConfigTamanhoForm.Dinamico;
                }
            }
        }

        /// <summary>
        /// Retorna se o menu de sistema deve ser escondido por configuração da função.
        /// Está propriedade não sera configurada em postback da pagina.
        /// </summary>
        public static bool? EsconderMenuSistemas
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
                    return null;
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
        /// Abre uma janela popup modal
        /// </summary>
        /// <param name="formularioQueChama"></param>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="resizable"></param>
        public static void AbrirPopUpModal(System.Web.UI.Page formularioQueChama, string url, int width, int height, bool topo, bool resizable, bool scrollbars)
        {
            string scriptAbrirPopUpModal = ObterScriptPopupModal(url, null, width, height, null, null, true, resizable, scrollbars);
            if (!formularioQueChama.ClientScript.IsStartupScriptRegistered("Startup"))
                formularioQueChama.ClientScript.RegisterStartupScript(formularioQueChama.GetType(), "Startup", scriptAbrirPopUpModal);
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
        /// Obtem script para abrir um popup modal.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="resizable">if set to <c>true</c> [resizable].</param>
        /// <returns></returns>
        public static string ObterFuncaoPopupModal(string url, int width, int height, bool resizable)
        {
            return "window.showModalDialog( \"" + url + "\", null, \"dialogHeight: " + width + "px; dialogWidth: " + height + "px; dialogTop: px; dialogLeft: px; edge: Raised; center: Yes; help: No; resizable: " + (resizable ? "yes" : "no") + "; status: No;\" ); ";
        }

        /// <summary>
        /// Obtem script para abrir um popup modal.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="centro"></param>
        /// <param name="resizable"></param>
        /// <param name="scrollbars"></param>
        /// <returns></returns>
        private static string ObterScriptPopupModal(string url, string target, int? width, int? height, int? topo, int? esquerda, bool centro, bool resizable, bool scrollbars)
        {
            // nao foi testado
            target = String.IsNullOrEmpty(target) ? "null" : "\"" + target + "\"";
            return String.Format(@"window.showModalDialog( ""{0}"", {1}, ""dialogHeight: {2}px; dialogWidth: {3}px; dialogTop: {4}px; dialogLeft: {5}px; edge: Raised; center: {6}; help: No; resizable: {7}; scrollbars: {8}; status: No;\",
                url,                        // {0}
                target,                     // {1}
                height.HasValue ? height.ToString() : "",         // {2}
                width.HasValue ? width.ToString() : "",         // {3}
                topo.HasValue ? topo.ToString() : "",           // {4}
                esquerda.HasValue ? esquerda.ToString() : "",   // {5}
                centro ? "Yes" : "No",      // {6}
                resizable ? "Yes" : "No",   // {7}
                scrollbars ? "Yes" : "No"   // {8}
                );
        }

        /// <summary>
        /// Localiza um controle dentro da coleção de controles passado
        /// </summary>
        /// <typeparam name="T">Tipo de controle</typeparam>
        /// <param name="controls">Coleção de controles</param>
        /// <param name="ID">ID do controle a ser pesquisado</param>
        /// <returns>Retorna o controle especificado pelo tipo T e pelo seu ID</returns>
        public static T ObterControle<T>(ControlCollection controls, string ID) where T : Control
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
        //    string caminho = FuncaoAD.ObterPor_PK_FUNCAO(AuthenticationTicket.Atual.Sis_CodSis,mod_CodMod,fun_CodFun);
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
            if (!string.IsNullOrEmpty(caminho))
                if (usarServerTransfer)
                    ((Page)(HttpContext.Current.CurrentHandler)).Server.Transfer(caminho);
                else
                    ((Page)(HttpContext.Current.CurrentHandler)).Response.Redirect(caminho);
            else
                throw new AcontepException("O acesso para a página requisitada foi negado.");
               
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
                throw new AcontepException("O acesso para a página requisitada foi negado.");
                
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
                else if (controle is DropDownList)
                {
                    ((DropDownList)controle).SelectedIndex = 0;
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
            if (editores == null) return;
            foreach (FCKeditor editor in editores)
            {
                editor.BasePath = "~/FCKeditor/";
                editor.ToolbarSet = "Acontep";
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
            idVazio = !idVazio.HasValue ? -1 : idVazio;
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
        /// Trasmiti o arquivo para usuario
        /// <remarks>caso o arquivo não exista ou seja nulo ele não faz nada</remarks>
        /// </summary>
        /// <param name="path">caminho para o arquivo</param>
        /// <param name="contentType">Tipo do arquivo. Veja http://www.imgx.com/hosting/mime.html para mais detealhes
        /// Tipos de MIME
        /// application/X-FSRecipe				fs 
        /// application/activexdocument			vbd
        /// application/astound					asn asz asd
        /// application/cprplayer				pqi
        /// application/datawindow 				psr
        /// application/dsptype					tsp
        /// application/exe						exe
        /// application/fml						fml ofml
        /// application/font-tdpfr				pfr
        /// application/freeloader				frl
        /// application/futuresplash			spl
        /// application/gzip					gz tgz
        /// application/hstu					stk 
        /// application/ips						ips
        /// application/listenup				ptlk 
        /// application/lit						lit
        /// application/mac-binhex40			hqx
        /// application/mbedlet					mbd
        /// application/mdb		   				mdb
        /// application/mirage					mfp
        /// application/msi						msi 
        /// application/msm						msm
        /// application/mspowerpoint			pot pps ppt ppz
        /// application/msword					doc
        /// application/n2p						n2p 
        /// application/octet-stream			bat BAT bin class dbf lha lzh lzx sjf
        /// application/octet-stream-bin 		seq
        /// application/oda						oda
        /// application/olescript				axs
        /// application/pcphoto					zpa
        /// application/pdf						pdf
        /// application/pls 	 				pls
        /// application/postscript				ai eps ps       
        /// application/presentations			shw
        /// application/quest					qrt
        /// application/rtc						rtc
        /// application/rtf						rtf             
        /// application/smil					smil smi sml
        /// application/studiom					smp
        /// application/tajima					dst
        /// application/talker					talk
        /// application/toolbook				tbk
        /// application/vnd.lotus-1-2-3 		123
        /// application/vnd.ms-project			mpp
        /// application/vnd.powerbuilder6		pbd 
        /// application/vnd.wap.wmlc			wmlc
        /// application/vnd.wap.wmlscriptc		wmlsc
        /// application/vocaltec-media-desc		vmd
        /// application/vocaltec-media-file		vmf
        /// application/vpa						vpa
        /// application/wordperfect5.1			wpd
        /// application/write					wri
        /// application/x-DemoShield			wid 
        /// application/x-InstallFromTheWeb		rrf
        /// application/x-InstallShield			wis
        /// application/x-NET-Install			ins
        /// application/x-Parable-Thing			tmv
        /// application/x-arj					arj
        /// application/x-asap					asp
        /// application/x-authorware-bin		aab
        /// application/x-authorware-map		aam aas
        /// application/x-bcpio					bcpio           
        /// application/x-cdlink				vcd
        /// application/x-cdr 					cdr
        /// application/x-chat					chat
        /// application/x-cnc					cnc
        /// application/x-coda					coda page
        /// application/x-compress				z
        /// application/x-connector				con
        /// application/x-cpio					cpio            
        /// application/x-cprplayer				pqf
        /// application/x-csh					csh             
        /// application/x-cu-seeme				cu csm
        /// application/x-director				dcr dir dxr swa
        /// application/x-dreamcast-vms			vms
        /// application/x-dreamcast-vms-info	vmi
        /// application/x-dvi					dvi             
        /// application/x-envoy					evy    
        /// application/x-expandedbook       	ebk
        /// application/x-gtar               	gtar            
        /// application/x-hdf                	hdf             
        /// application/x-hsp-erf            	erf
        /// application/x-httpd-imap         	map
        /// application/x-httpd-php          	php php4
        /// application/x-httpd-php3         	php3
        /// application/x-ica                	ica 
        /// application/x-icq                	uin
        /// application/x-ipix               	ipx
        /// application/x-ipscript           	ips
        /// application/x-javascript         	js
        /// application/x-latex              	latex           
        /// application/x-macbinary          	bin
        /// application/x-mif					mif
        /// application/x-mpire					mpl mpire
        /// application/x-ms-wmv 				wmv 
        /// application/x-msaddr				adr
        /// application/x-mswallet				wlt
        /// application/x-netcdf				nc cdf          
        /// application/x-netfpx				npx 
        /// application/x-nokia-9000-communicator-add-on-software aos
        /// application/x-nschat				nsc
        /// application/x-pgp-plugin			pgp
        /// application/x-quicktimeplayer		qtl
        /// application/x-sh					sh              
        /// application/x-shar					shar            
        /// application/x-shockwave-flash		swf 
        /// application/x-sprite				spr sprite
        /// application/x-stuffit				sit
        /// application/x-supercard				sca
        /// application/x-sv4cpio				sv4cpio         
        /// application/x-sv4crc				sv4crc          
        /// application/x-tar					tar             
        /// application/x-tcl					tcl             
        /// application/x-tex					tex             
        /// application/x-texinfo				texinfo texi   
        /// application/x-tlk					tlk 
        /// application/x-troff					t tr roff       
        /// application/x-troff-man				man             
        /// application/x-troff-me				me              
        /// application/x-troff-ms				ms              
        /// application/x-up-alert				alt
        /// application/x-up-cacheop			che
        /// application/x-ustar					ustar           
        /// application/x-wais-source			src             
        /// application/x-wordpro 				lwp
        /// application/x-x509-ca-cert			cacert
        /// application/x-x509-email-cert		ecert email
        /// application/x-x509-server-cert		scert servercert
        /// application/x-x509-user-cert   		ucert usercert
        /// application/x-zip-compressed 		zip
        /// application/xls						xls
        /// application/xlt						xlt
        /// application/zip						zip             
        /// audio/basic							au snd          
        /// audio/echospeech					es
        /// audio/gsm							gsm gsd
        /// audio/rmf							rmf
        /// audio/tsplayer						tsi    
        /// audio/voxware						vox 
        /// audio/wtx							wtx
        /// audio/x-aiff						aif aiff aifc
        /// audio/x-dspeech						cht dus
        /// audio/x-midi						mid midi
        /// audio/x-mpeg						mp2 mp3
        /// audio/x-mpegurl						m3u
        /// audio/x-ms-wma						wma
        /// audio/x-pn-realaudio				ram ra rm
        /// audio/x-pn-realaudio-plugin			rpm
        /// audio/x-qt-stream					stream 
        /// audio/x-rmf							rmf
        /// audio/x-twinvq						vqf     vql
        /// audio/x-twinvq-plugin				vqe
        /// audio/x-wav							wav             
        /// audio/x-wtx							wtx
        /// chemical/x-mdl-molfile				mol
        /// chemical/x-pdb						pdb
        /// drawing/x-dwf						dwf
        /// i-world/i-vrml						ivr
        /// image/cis-cod						cod
        /// image/cpi							cpi
        /// image/fif							fif
        /// image/gif							gif             
        /// image/ief							ief             
        /// image/jpeg							jpeg jpg jpe
        /// image/png							png
        /// image/rip							rip
        /// image/svh							svh
        /// image/tiff							tiff tif        
        /// image/vasa							mcf    
        /// image/vnd							svf dwg dxf
        /// image/vnd.wap.wbmp					wbmp
        /// image/vnd.xiff						xif
        /// image/wavelet						wi
        /// image/x-clp							clp CLP
        /// image/x-cmu-raster					ras
        /// image/x-cmx							cmx
        /// image/x-emf							emf EMF
        /// image/x-etf							etf
        /// image/x-fpx							fpx
        /// image/x-freehand					fh5 fh4 fhc    
        /// image/x-halo-cut					cut
        /// image/x-jps							jps
        /// image/x-mgx-dsf						dsf
        /// image/x-ms-bmp						bmp
        /// image/x-portable-anymap				pnm             
        /// image/x-portable-bitmap				pbm             
        /// image/x-portable-graymap			pgm             
        /// image/x-portable-pixmap				ppm             
        /// image/x-quicktime 					qti
        /// image/x-rgb							rgb
        /// image/x-wmf							wmf WMF
        /// image/x-xbitmap						xbm             
        /// image/x-xpixmap						xpm             
        /// image/x-xwindowdump					xwd             
        /// multipart/mixed						dig
        /// multipart/x-mixed-replace			push
        /// plugin/wanimate						wan waf
        /// text/ccs							ccs
        /// text/calendar						vcs
        /// text/css 							css	
        /// text/dss							dss
        /// text/html							htm html
        /// text/lsp							lsp
        /// text/parsnegar-document				pgr
        /// text/plain							htc
        /// text/plain							txt
        /// text/richtext						rtx             
        /// text/tab-separated-values			tsv             
        /// text/vnd.wap.wml					wml
        /// text/vnd.wap.wmlscript	 			wmls
        /// text/x-hdml							hdml
        /// text/x-setext						etx             
        /// text/x-speech						talk spc
        /// text/xml 							xml
        /// text/xml							xsl
        /// urdu/urdu98							u98
        /// video/animaflex						afl
        /// video/mpeg							mpeg mpg mpe    
        /// video/quicktime						qt mov          
        /// video/vnd.vivo						viv vivo    
        /// video/x-ms-asf						asf asx
        /// video/x-msvideo						avi             
        /// video/x-sgi-movie					movie           
        /// video/x-videogram					vgm vgx xdr
        /// video/x-videogram-plugin			vgp
        /// workbook/formulaone					vts vtts
        /// x-world/x-3dmf						3dmf 3dm qd3d qd3    
        /// x-world/x-svr						svr
        /// x-world/x-vrml						wrl wrz
        /// x-world/x-vrt						vrt
        /// audio/x-ms-wax						wax
        /// video/x-ms-wvx						wvx
        /// video/x-ms-wm						wm
        /// video/x-ms-wmx						wmx
        /// application/x-ms-wmz				wmz
        /// application/x-ms-wmd				wmd</param>
        public static void TransmitirArquivo(string path, string contentType)
        {
            if (path != null && File.Exists(path))
            {
                HttpContext.Current.Response.ContentType = contentType;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachament; filename=" + Path.GetFileName(path));
                HttpContext.Current.Response.TransmitFile(path);
            }
        }
        /// <summary>
        /// Trasmiti o arquivo para usuario
        /// <remarks>caso o arquivo não exista ou seja nulo ele não faz nada</remarks>
        /// </summary>
        /// <param name="path">caminho para o arquivo</param>
        public static void TransmitirArquivo(string path)
        {
            if (path != null && File.Exists(path))
            {
                string lExtensao = path.Substring(path.LastIndexOf(".") + 1).ToLower();
                //HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = RetornarMIMEType(lExtensao);
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachament; filename=" + Path.GetFileName(path));
                HttpContext.Current.Response.TransmitFile(path);
                HttpContext.Current.Response.End();
            }
        }
        public static string RetornarMIMEType(string pExtensao)
        {
            string lContentType;
            pExtensao = pExtensao.ToLower();
            switch (pExtensao)
            {
                case "fs":
                    lContentType = "application/X-FSRecipe";
                    break;
                case "vbd":
                    lContentType = "application/activexdocument";
                    break;
                case "asn":
                case "asz":
                case "asd":
                    lContentType = "application/astound";
                    break;
                case "pqi":
                    lContentType = "application/cprplayer";
                    break;
                case "xml":
                    lContentType = "text/xml";
                    break;
                case "ace":
                    lContentType = "application/ace";
                    break;
                case "rar":
                    lContentType = "application/rar";
                    break;
                case "zip":
                    lContentType = "application/x-zip-compressed";
                    break;
                case "exe":
                    lContentType = "application/octet-stream";
                    break;
                case "pot":
                case "pps":
                case "ppt":
                case "ppz":
                    lContentType = "application/mspowerpoint";
                    break;
                case "xls":
                    lContentType = "application/msexcel";
                    break;
                case "doc":
                    lContentType = "application/msword";
                    break;
                case "txt":
                case "htc":
                    lContentType = "text/plain";
                    break;
                default:
                    lContentType = "application/" + pExtensao;
                    break;
            }
            return lContentType;
        }
    }
}
