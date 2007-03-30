using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Acontep.Dado;
using System.Data;
using Acontep.AD.Cadastros;
using Acontep.AD.Manutencao.Seguranca;


namespace Acontep.AD.Manutencao.Seguranca
{
    [Serializable]
    public class AuthenticationTicket
    {
        //private int _Cmp_ID;
        //private byte _Plt_Planta;
        private string _CodigoSistema;
        private string _CodigoModulo;
        private string _CodigoFuncao;
        private int _IDUsuario;
        private string _Login;
        private string _Usu_Nome;
        //private string _Cmp_Nome;
        //private string _Plt_Nome;
        private string _NomeSistema;
        private string _NomeModulo;
        private string _NomeFuncao;
        private bool _AcessoViaInternet;
        private bool _LogAcesso;
        private bool _LogAcessoSistema;

        /// <summary>
        /// Constante de indexe para a seção do ticket
        /// </summary>       

        private const string GUID_SECAO_TICKET = "4CF45368-B9FE-401E-9966-35A1A546D55E";

        /// <summary>
        /// Construtor do ticket de autenticacao
        /// </summary>
        /// <param name="usu_Id"></param>
        /// <param name="usu_Login"></param>
        /// <param name="usu_Nome"></param>
        /// <param name="acessoViaInternet"></param>
        private AuthenticationTicket(int usu_Id, string usu_Login, string usu_Nome, bool acessoViaInternet)
        {
            _IDUsuario = usu_Id;
            _Login = usu_Login;
            _Usu_Nome = usu_Nome;
            _AcessoViaInternet = acessoViaInternet;
        }

        /// <summary>
        /// Obter o codigo do usuario associado com o ticket de autenticacao        
        /// </summary>
        public int IDUsuario
        {
            get { return _IDUsuario; }
        }

        /// <summary>
        /// Obter o login do usuario associado com o ticket de autenticacao
        /// </summary>
        public string Login
        {
            get { return _Login; }
        }

        /// <summary>
        /// Obter o nome do usuario associado com o ticket de autenticacao
        /// </summary>
        public string Nome
        {
            get { return _Usu_Nome; }
        }

        ///// <summary>        
        ///// Obter o codigo da companhia que o usuario esta atualmente acessando
        ///// </summary>
        //public int Cmp_ID
        //{
        //    get { return _Cmp_ID; }
        //}

        ///// <summary>
        ///// Obter o nome da companhia que o usuario esta atualmente acessando
        ///// </summary>
        //public string Cmp_Nome
        //{
        //    get { return _Cmp_Nome; }
        //}

        ///// <summary>        
        ///// Obter o codigo da planta que o usuario esta atualmente acessando
        ///// </summary>
        //public byte Plt_Planta
        //{
        //    get { return _Plt_Planta; }
        //}

        ///// <summary>
        ///// Obter a descricao do planta que o usuario esta atualmente acessando
        ///// </summary>
        //public string Plt_Nome
        //{
        //    get { return _Plt_Nome; }
        //}

        /// <summary>        
        /// Obter o codigo do sistema que o usuario esta atualmente acessando
        /// </summary>
        public string CodigoSistema
        {
            get { return _CodigoSistema; }
        }

        /// <summary>
        /// Obter a descricao do sistema que o usuario esta atualmente acessando
        /// </summary>
        public string NomeSistema
        {
            get { return _NomeSistema; }            
        }

        /// <summary>
        /// Status se deve ser feito log de acesso do sistema
        /// </summary>
        public bool LogAcessoSistema
        {
            get { return _LogAcessoSistema; }            
        }

        /// <summary>        
        /// Obter o codigo do modulo que o usuario esta atualmente acessando
        /// </summary>
        public string CodigoModulo
        {
            get { return _CodigoModulo; }
        }

        /// <summary>
        /// Obter a descricao do modulo que o usuario esta atualmente acessando
        /// </summary>
        public string NomeModulo
        {
            get { return _NomeModulo; }
        }

        /// <summary>        
        /// Obter/Setar o codigo da funcao que o usuario esta atualmente acessando
        /// </summary>
        public string CodigoFuncao
        {
            get { return _CodigoFuncao; }
            internal set { _CodigoFuncao = value; }
        }

        /// <summary>
        /// Obter/Setar a descricao da funcao que o usuario esta atualmente acessando
        /// </summary>
        public string NomeFuncao
        {
            get { return _NomeFuncao; }
            internal set { _NomeFuncao = value; }
        }

        /// <summary>
        /// Status se deve ser feito log de acesso da função
        /// </summary>
        public bool LogAcesso
        {
            get { return _LogAcesso; }            
        }


        /// <summary>
        /// Informa se o acesso do usuario esta sendo feito via internet ou via intranet
        /// </summary>
        public bool AcessoViaInternet
        {
            get { return _AcessoViaInternet; }
        }

        /// <summary>
        /// Atribui os valores correspondentes ao sistema/planta atualmente sendo acessados
        /// </summary>
        /// <param name="sis_CodSis"></param>
        /// <param name="plt_Planta"></param>
        public void AtribuirAcessoSistema(string CodigoSistema)
        {
            _CodigoSistema = CodigoSistema;
            //_Plt_Planta = plt_Planta;
            //Acontep.AD.Cadastros.PlantaAD plantaAcessoDados = new PlantaAD();            
            //SistemaAD sistemaAcessoDados = new SistemaAD();
            //DataRow drw = PlantaAD.ObterPor_PK_Planta(plt_Planta).Rows[0];
            //_Plt_Nome = drw["Plt_Nome"].ToString(); //PlantaAD.ObterPlantaNome(plt_Planta);
            //if(drw["Cmp_ID"] != DBNull.Value)
            //{
            //    _Cmp_ID = (int)drw["Cmp_ID"];
            //    drw = CompanhiaAD.ObterPor_PK_COMPANHIA(_Cmp_ID).Rows[0];
            //    _Cmp_Nome = drw["Cmp_Nome"].ToString();
            //}
            DataRow drw = SistemaAD.ObterSistema(CodigoSistema).Rows[0];
            _NomeSistema = drw["Nome"].ToString();
            _LogAcessoSistema = false;//(bool)drw["Sis_LogAcesso"];
        }

        /// <summary>
        /// Atribui os valores correspondentes ao modulo/funcao atualmente sendo acessados
        /// </summary>
        /// <param name="sis_CodSis"></param>
        /// <param name="mod_CodMod"></param>
        /// <param name="fun_CodFun"></param>
        public void AtribuirAcessoFuncao(string CodigoSistema, string CodigoModulo, string CodigoFuncao)
        {
            _CodigoSistema = CodigoSistema;
            _CodigoModulo = CodigoModulo;
            int IDSistema = Conversao.Para<int>(SistemaAD.ObterSistema(CodigoSistema).Rows[0]["IDSistema"], "Sistema");
            DataRow linha = ModuloAD.ObterPor_UQ_MODULO(IDSistema, CodigoModulo).Rows[0];
            _NomeModulo = linha["Nome"].ToString();
            int IDModulo = Conversao.Para<int>(linha["IDModulo"], "ID do módulo");
            _CodigoFuncao = CodigoFuncao;
            _NomeFuncao = FuncaoAD.ObterPor_UQ_FUNCAO(IDModulo, CodigoFuncao).Rows[0]["Nome"].ToString();
            _LogAcesso = true; //FuncaoAcessoDados.ObterFuncao(CodigoSistema, CodigoModulo, CodigoFuncao);            
        }

        /// <summary>
        /// Metodo para criar um novo ticket
        /// </summary>
        /// <param name="usu_Id"></param>
        /// <param name="usu_Login"></param>
        /// <param name="usu_Nome"></param>
        /// <param name="acessoViaInternet"></param>
        /// <returns></returns>
        public static AuthenticationTicket Criar(int usu_Id, string usu_Login, string usu_Nome, bool acessoViaInternet)
        {
            return Atual = new AuthenticationTicket(usu_Id, usu_Login, usu_Nome, acessoViaInternet);
        }

        /// <summary>
        /// Retorna o ticket de autenticação corrente
        /// </summary>
        public static AuthenticationTicket Atual
        {
            get
            {
                //if (HttpContext.Current.Session[GUID_SECAO_TICKET] == null)
                //{
                //}
                return (AuthenticationTicket)HttpContext.Current.Session[GUID_SECAO_TICKET];
            }
            private set
            {
                HttpContext.Current.Session[GUID_SECAO_TICKET] = value;
            }
        }
    }
}
