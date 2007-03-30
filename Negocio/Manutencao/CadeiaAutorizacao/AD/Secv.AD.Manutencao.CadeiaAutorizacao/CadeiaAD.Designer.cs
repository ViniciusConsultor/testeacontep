// Classe gerada automaticamente pela ferramenta GeradorCRUD
// em ter 02/01/2007 
// Por CENTRAL\tsousa na máquina INFOAJU02PC 
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Secv.Dado;
namespace Secv.AD.Manutencao.CadeiaAutorizacao
{
[System.ComponentModel.DataObject(true)]
public static partial class CadeiaAD 
 {

	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_PK_CadeiaAutorizacao (System.Int32 Cau_Id){
string sql = @"
SELECT 
	
	Cau_Id, 
	Cau_Descri, 
	Cau_TipoAprovacao, 
	Cau_UrlAprovacao, 
	Hie_Id, 
	Cau_ClasseRetorno, 
	Cau_MetodoRetorno 
FROM 
	CadeiaAutorizacao.Cadeia
	  
 WHERE 
	( Cau_Id LIKE @Cau_Id )";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Cau_Id", System.Data.DbType.Int32, -1, Cau_Id );
      return bd.ObterDataTable();
    }




	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.String Cau_Descri, System.Int32 Cau_TipoAprovacao, System.String Cau_UrlAprovacao, System.Int32? Hie_Id, System.String Cau_ClasseRetorno, System.String Cau_MetodoRetorno){

      string sql = @"
INSERT INTO CadeiaAutorizacao.Cadeia
(
 Cau_Descri, 
 Cau_TipoAprovacao, 
 Cau_UrlAprovacao, 
 Hie_Id, 
 Cau_ClasseRetorno, 
 Cau_MetodoRetorno
)
    VALUES 
(
@Cau_Descri, 
@Cau_TipoAprovacao, 
@Cau_UrlAprovacao, 
@Hie_Id, 
@Cau_ClasseRetorno, 
@Cau_MetodoRetorno
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@Cau_Descri", System.Data.DbType.String, 50, Cau_Descri );
	bd.AdicionarParametro("@Cau_TipoAprovacao", System.Data.DbType.Int32, -1, Cau_TipoAprovacao );
	bd.AdicionarParametro("@Cau_UrlAprovacao", System.Data.DbType.String, 100, Cau_UrlAprovacao );
	bd.AdicionarParametro("@Hie_Id", System.Data.DbType.Int32, -1, Hie_Id );
	bd.AdicionarParametro("@Cau_ClasseRetorno", System.Data.DbType.String, 100, Cau_ClasseRetorno );
	bd.AdicionarParametro("@Cau_MetodoRetorno", System.Data.DbType.String, 50, Cau_MetodoRetorno );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_Cau_Id, System.String Cau_Descri, System.Int32 Cau_TipoAprovacao, System.String Cau_UrlAprovacao, System.Int32? Hie_Id, System.String Cau_ClasseRetorno, System.String Cau_MetodoRetorno){
      string sql = 
@"UPDATE CadeiaAutorizacao.Cadeia SET
	Cau_Descri= @Cau_Descri
, 	Cau_TipoAprovacao= @Cau_TipoAprovacao
, 	Cau_UrlAprovacao= @Cau_UrlAprovacao
, 	Hie_Id= @Hie_Id
, 	Cau_ClasseRetorno= @Cau_ClasseRetorno
, 	Cau_MetodoRetorno= @Cau_MetodoRetorno

    WHERE Cau_Id = @Cau_Id";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Cau_Id", System.Data.DbType.Int32, -1, original_Cau_Id );
	bd.AdicionarParametro("@Cau_Descri", System.Data.DbType.String, 50, Cau_Descri );
	bd.AdicionarParametro("@Cau_TipoAprovacao", System.Data.DbType.Int32, -1, Cau_TipoAprovacao );
	bd.AdicionarParametro("@Cau_UrlAprovacao", System.Data.DbType.String, 100, Cau_UrlAprovacao );
	bd.AdicionarParametro("@Hie_Id", System.Data.DbType.Int32, -1, Hie_Id );
	bd.AdicionarParametro("@Cau_ClasseRetorno", System.Data.DbType.String, 100, Cau_ClasseRetorno );
	bd.AdicionarParametro("@Cau_MetodoRetorno", System.Data.DbType.String, 50, Cau_MetodoRetorno );
      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_Cau_Id)
	{
      string sql = 
@"DELETE FROM CadeiaAutorizacao.Cadeia
    WHERE Cau_Id = @Cau_Id";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Cau_Id", System.Data.DbType.Int32, -1, original_Cau_Id );
      bd.ExecuteNonQuery();
    }


}
}

