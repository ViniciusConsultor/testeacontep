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
    [System.ComponentModel.DataObject(true)]
    public static partial class HierarquiaUsuarioAD
    {

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable ObterPor_PK_HierarquiaUsuario(System.Int32 Hus_Id)
        {
            string sql = @"
SELECT 
	
	Hus_Id, 
	Hie_Id, 
	Usu_Id, 
	Hus_Alcada, 
	Usu_IdSuperior, 
	Hus_DatInc, 
	Hus_UsuInc, 
	Hus_DatHor, 
	Hus_UsuTra 
FROM 
	CadeiaAutorizacao.HierarquiaUsuario
	  
 WHERE 
	( Hus_Id LIKE @Hus_Id )";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Hus_Id", System.Data.DbType.Int32, -1, Hus_Id);
            return bd.ObterDataTable();
        }




        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert)]
        public static int Inserir(System.Int32 Hie_Id, System.Int32 Usu_Id, System.Decimal? Hus_Alcada, System.Int32? Usu_IdSuperior)
        {

            string sql = @"
INSERT INTO CadeiaAutorizacao.HierarquiaUsuario
(
 Hie_Id, 
 Usu_Id, 
 Hus_Alcada, 
 Usu_IdSuperior, 
 Hus_DatInc, 
 Hus_UsuInc, 
 Hus_DatHor, 
 Hus_UsuTra
)
    VALUES 
(
@Hie_Id, 
@Usu_Id, 
@Hus_Alcada, 
@Usu_IdSuperior, 
GetDate(), 
@Hus_UsuInc, 
GetDate(), 
@Hus_UsuTra
); SELECT SCOPE_IDENTITY();
";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Hie_Id", System.Data.DbType.Int32, -1, Hie_Id);
            bd.AdicionarParametro("@Usu_Id", System.Data.DbType.Int32, -1, Usu_Id);
            bd.AdicionarParametro("@Hus_Alcada", System.Data.DbType.Decimal, -1, Hus_Alcada);
            bd.AdicionarParametro("@Usu_IdSuperior", System.Data.DbType.Int32, -1, Usu_IdSuperior);
            bd.AdicionarParametro("@Hus_UsuInc", System.Data.DbType.String, 15, System.Threading.Thread.CurrentPrincipal.Identity.Name);
            bd.AdicionarParametro("@Hus_UsuTra", System.Data.DbType.String, 15, System.Threading.Thread.CurrentPrincipal.Identity.Name);

            object objRetorno = bd.ExecuteScalar();
            return (objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
        }


        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        public static void Alterar(System.Int32 original_Hus_Id, System.Int32 Hie_Id, System.Int32 Usu_Id, System.Decimal? Hus_Alcada, System.Int32? Usu_IdSuperior)
        {
            string sql =
      @"UPDATE CadeiaAutorizacao.HierarquiaUsuario SET
	Hie_Id= @Hie_Id
, 	Usu_Id= @Usu_Id
, 	Hus_Alcada= @Hus_Alcada
, 	Usu_IdSuperior= @Usu_IdSuperior
, 	Hus_DatHor= GetDate()
, 	Hus_UsuTra= @Hus_UsuTra

    WHERE Hus_Id = @original_Hus_Id";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Hie_Id", System.Data.DbType.Int32, -1, Hie_Id);
            bd.AdicionarParametro("@Usu_Id", System.Data.DbType.Int32, -1, Usu_Id);
            bd.AdicionarParametro("@Hus_Alcada", System.Data.DbType.Decimal, -1, Hus_Alcada);
            bd.AdicionarParametro("@Usu_IdSuperior", System.Data.DbType.Int32, -1, Usu_IdSuperior);
            bd.AdicionarParametro("@Hus_UsuTra", System.Data.DbType.String, 15, System.Threading.Thread.CurrentPrincipal.Identity.Name);

            bd.AdicionarParametro("@original_Hus_Id", System.Data.DbType.Int32, -1, original_Hus_Id);

            bd.ExecuteNonQuery();
        }


        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public static void Apagar(System.Int32 original_Hus_Id)
        {
            string sql =
      @"DELETE FROM CadeiaAutorizacao.HierarquiaUsuario

    WHERE 
Hus_Id = @original_Hus_Id
"; BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@original_Hus_Id", System.Data.DbType.Int32, -1, original_Hus_Id);

            bd.ExecuteNonQuery();
        }


    }
}

