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
    public static partial class HierarquiaAD
    {

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable ObterPor_PK_Hierarquia(System.Int32 Hie_Id)
        {
            string sql = @"
SELECT 
	
	Hie_Id, 
	Hie_Descri, 
	Hie_Funcao, 
	Hie_DatInc, 
	Hie_UsuInc, 
	Hie_UsuTra, 
	Hie_DatHor 
FROM 
	CadeiaAutorizacao.Hierarquia
	  
 WHERE 
	( Hie_Id LIKE @Hie_Id )";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Hie_Id", System.Data.DbType.Int32, -1, Hie_Id);
            return bd.ObterDataTable();
        }




        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert)]
        public static int Inserir(System.String Hie_Descri, System.String Hie_Funcao)
        {

            string sql = @"
INSERT INTO CadeiaAutorizacao.Hierarquia
(
 Hie_Descri, 
 Hie_Funcao, 
 Hie_DatInc, 
 Hie_UsuInc, 
 Hie_UsuTra, 
 Hie_DatHor
)
    VALUES 
(
@Hie_Descri, 
@Hie_Funcao, 
GetDate(), 
@Hie_UsuInc, 
@Hie_UsuTra, 
GetDate()
); SELECT SCOPE_IDENTITY();
";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Hie_Descri", System.Data.DbType.String, 50, Hie_Descri);
            bd.AdicionarParametro("@Hie_Funcao", System.Data.DbType.String, 100, Hie_Funcao);
            bd.AdicionarParametro("@Hie_UsuInc", System.Data.DbType.String, 15, System.Threading.Thread.CurrentPrincipal.Identity.Name);
            bd.AdicionarParametro("@Hie_UsuTra", System.Data.DbType.String, 15, System.Threading.Thread.CurrentPrincipal.Identity.Name);

            object objRetorno = bd.ExecuteScalar();
            return (objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
        }


        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        public static void Alterar(System.Int32 original_Hie_Id, System.String Hie_Descri, System.String Hie_Funcao)
        {
            string sql =
      @"UPDATE CadeiaAutorizacao.Hierarquia SET
	Hie_Descri= @Hie_Descri
, 	Hie_Funcao= @Hie_Funcao
, 	Hie_UsuTra= @Hie_UsuTra
, 	Hie_DatHor= GetDate()

    WHERE Hie_Id = @original_Hie_Id";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Hie_Descri", System.Data.DbType.String, 50, Hie_Descri);
            bd.AdicionarParametro("@Hie_Funcao", System.Data.DbType.String, 100, Hie_Funcao);
            bd.AdicionarParametro("@Hie_UsuTra", System.Data.DbType.String, 15, System.Threading.Thread.CurrentPrincipal.Identity.Name);

            bd.AdicionarParametro("@original_Hie_Id", System.Data.DbType.Int32, -1, original_Hie_Id);

            bd.ExecuteNonQuery();
        }


        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public static void Apagar(System.Int32 original_Hie_Id)
        {
            string sql =
      @"DELETE FROM CadeiaAutorizacao.Hierarquia

    WHERE 
Hie_Id = @original_Hie_Id
"; BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@original_Hie_Id", System.Data.DbType.Int32, -1, original_Hie_Id);

            bd.ExecuteNonQuery();
        }


    }
}

