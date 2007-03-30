// Classe gerada automaticamente pela ferramenta GeradorCRUD
// em seg 04/12/2006 
// Por CENTRAL\francisco na máquina FRANCISCOPC 
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Acontep.Dado;
namespace Acontep.AD.Manutencao
{
    public static partial class UsuarioAD
    {
        public static int ObterIDUsuarioLogado()
        {
            return ObterIDUsuario(System.Threading.Thread.CurrentPrincipal.Identity.Name);
        }
        /// <summary>
        /// Retorna o ID do usuário.
        /// </summary>
        /// <param name="Usu_Login">Login do usuário</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static int ObterIDUsuario(System.String Usu_Login)
        {
            string sql = @"
SELECT 	
	usu_id
FROM 
	USUARIO	  
 WHERE 
	( Usu_Login = @Usu_Login )";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Usu_Login", System.Data.DbType.String, 15, Usu_Login);
            return bd.ExecuteScalar<int>();
        }

        /// <summary>
        /// Retorna o usuário.
        /// </summary>
        /// <param name="Usu_Login">Login do usuário</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable ObterUsuario(int usu_ID)
        {
            string sql = @"
SELECT *
FROM USUARIO	  
 WHERE 
	( Usu_ID = @Usu_ID )";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Usu_ID", System.Data.DbType.Int32, usu_ID);
            return bd.ObterDataTable();
        }
        
        /// <summary>
        /// Retorna o usuário.
        /// </summary>
        /// <param name="Usu_Login">Login do usuário</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable ObterUsuario(int usu_ID, string usu_Nome)
        {
            string sql = @"
SELECT *
FROM USUARIO	  
WHERE (Usu_Nome Like @Usu_Nome or @Usu_Nome is null)
and   (Usu_ID Like @Usu_ID or @Usu_ID = 0)
Order By Usu_Nome
";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Usu_Nome", System.Data.DbType.String, usu_Nome);
            bd.AdicionarParametro("@Usu_ID", System.Data.DbType.Int32, usu_ID);
            return bd.ObterDataTable();
        }

        public static DataTable ObterUsuariosParaComboBox()
        {
            string sql = @"
SELECT Usu_ID, Usu_Nome
FROM USUARIO
Order By Usu_Nome	  
";
            BdUtil bd = new BdUtil(sql);
            return bd.ObterDataTable();
        }
    }
}
