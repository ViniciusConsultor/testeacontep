<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
    }

    void Application_ReleaseRequestState(object sender, EventArgs e)
    {
        
    }
    
    void Application_PreRequestHandlerExecute(object sender, EventArgs e)
    {
        if (((System.Web.HttpApplication)(sender)).Context.Items["AspSession"] != null)
        {
            // Limpa o valor configurado da funcao antes de comecar a executar o handler
            Acontep.Manutencao.Seguranca.Permissao.SairFuncao();
            // Se o handler for um Page injeta o evento de validacao do evento no Init
            Page page = Context.Handler as Page;
            if (page != null)
            {
                page.Init += new EventHandler(page_ValidarPermissao);
            }
        }
    }

    /// <summary>
    /// Evento plugado no Init do page sendo executado para validar permissao configurada no PreInit da pagina
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void page_ValidarPermissao(object sender, EventArgs e)
    {
        Acontep.Manutencao.Seguranca.Permissao.ValidarPermissao();
    }

    void Application_Error(object sender, EventArgs e)
    {
        #region Tratamento genérico de erro
        //
        // Toda exceção não tratada será submetida a um tratamento automático
        //
        string mensagem;
        bool EhParaTratar = true;
        try
        {
            // Get the Web application configuration.
            System.Configuration.Configuration configuration =
                System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(this.Request.ApplicationPath);


            // Get the section.
            System.Web.Configuration.CustomErrorsSection customErrorsSection =
              (System.Web.Configuration.CustomErrorsSection)configuration.GetSection(
              "system.web/customErrors");

            EhParaTratar = customErrorsSection.Mode == System.Web.Configuration.CustomErrorsMode.On ||
                (customErrorsSection.Mode == System.Web.Configuration.CustomErrorsMode.RemoteOnly &&
                    Request.UserHostAddress != Request.ServerVariables["LOCAL_ADDR"].ToString()
                );
        }
        catch
        {
            
        }
        //EhParaTratar = true;
        if (EhParaTratar)
        {
            bool tratou = Acontep.TratamentoExcessao.Tratar(Server.GetLastError().InnerException, out mensagem);
            // Caso a exceção seja tratada com sucesso a mensagem será colocada na sessão,
            // a exceção será limpa e o usuário direcionado para a página de erro
            if (tratou)
            {
                Acontep.UI.Web.DadosWeb.AtribuirMensagem(mensagem, Acontep.UI.Web.MensagemWeb.Tipo.Erro);
                Server.ClearError();
                //Response.Clear();
                //Server.Transfer("ConfiguracaoResumoRotaForm.aspx",true);
                Response.Redirect("~/Erro/Erro.aspx", true);
            }
        }
        //Habilite a linha abaixo para não mostrar os erro não tratados
        //Response.Redirect("~/Erro/Default.htm", true);
        
        //
        // Obs: Caso a exceção não seja tratada o IIS irá direcionar o usuário para 
        // a página de erro padrão (estática)
        //
        #endregion

        //Code that runs when an unhandled error occurs
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
