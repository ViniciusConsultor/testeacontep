using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Acontep.Dado;

namespace Acontep.Manutencao
{
    public static class UsuariosNg
    {
        public static DataTable ObterUsuario_PorGrupo(int grp_CodGrp)
        {
            string sql = @"
select 
	GU.Usu_Login ,
	GU.Grp_CodGrp,
	GU.Gru_DatInc,
	GU.Gru_DatHor,
	GU.Gru_UsuTra,
	U.Usu_Nome,    
	U.Usu_Email,   
	U.usu_id,      
	U.Usu_Ativo   
from grupo_usuario GU
join  usuario U on ( U.usu_Login = GU.usu_Login  )
where grp_CodGrp = @Grp_CodGrp
";
            BdUtil bd = new BdUtil( sql );
            bd.AdicionarParametro("@Grp_CodGrp", DbType.Int32, grp_CodGrp );

            return bd.ObterDataTable();
        }
    }
}
