// Classe gerada automaticamente pela ferramenta GeradorCRUD
// em qui 01/02/2007 
// Por CENTRAL\igorleonardo na máquina INFOAJU01PC 
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Secv.Dado;
namespace Secv.AD
{
    public static partial class HierarquiaUsuarioAD
    {

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable Obter_Usuario_Por_Hierarquia(string hie_ID)
        {
            string sql = @"
SELECT 	
	Hus_Id,
	Hie_Id,
	u.Usu_Id,
    u.Usu_Nome,
	Hus_Alcada,
	u1.Usu_Nome Supervisor
FROM CadeiaAutorizacao.HierarquiaUsuario hu 
join USUARIO u on hu.Usu_Id = u.usu_id
left join Usuario u1 on hu.Usu_IdSuperior = u1.usu_id
WHERE ( Hie_Id LIKE @Hie_Id )
";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Hie_Id", System.Data.DbType.Int32, -1, hie_ID);
            return bd.ObterDataTable();
        }
    }
}
