// Classe gerada automaticamente pela ferramenta GeradorCRUD
// em seg 26/03/2007 
// Por FRANCISCONB\Chico na máquina FRANCISCONB 
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Acontep.Dado;
namespace Acontep.AD.Cadastros
{
[System.ComponentModel.DataObject(true)]
public static partial class MunicipioAD 
 {

	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_IX_Municipio (System.Int32 IDEstado, System.String Nome){
string sql = @"
SELECT 
	
	IDCidade, 
	IDEstado, 
	Nome, 
	DATINC, 
	DATHOR, 
	USUINC, 
	USUTRA 
FROM 
	Cadastros.Municipio
	  
 WHERE 
	IDEstado = @IDEstado AND ( Nome LIKE @Nome + '%' )";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDEstado", System.Data.DbType.Int32, -1, IDEstado );
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
      return bd.ObterDataTable();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_PK_Municipio (System.Int32 IDCidade){
string sql = @"
SELECT 
	
	IDCidade, 
	IDEstado, 
	Nome, 
	DATINC, 
	DATHOR, 
	USUINC, 
	USUTRA 
FROM 
	Cadastros.Municipio
	  
 WHERE 
	IDCidade = @IDCidade";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDCidade", System.Data.DbType.Int32, -1, IDCidade );
      return bd.ObterDataTable();
    }




	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.Int32 IDEstado, System.String Nome){

      string sql = @"
INSERT INTO Cadastros.Municipio
(
 IDEstado, 
 Nome, 
 DATINC, 
 DATHOR, 
 USUINC, 
 USUTRA
)
    VALUES 
(
@IDEstado, 
@Nome, 
GetDate(), 
GetDate(), 
@USUINC, 
@USUTRA
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@IDEstado", System.Data.DbType.Int32, -1, IDEstado );
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
	bd.AdicionarParametro("@USUINC", System.Data.DbType.AnsiString, 50, System.Threading.Thread.CurrentPrincipal.Identity.Name );
	bd.AdicionarParametro("@USUTRA", System.Data.DbType.AnsiString, 50, System.Threading.Thread.CurrentPrincipal.Identity.Name );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDCidade, System.Int32 IDEstado, System.String Nome){
      string sql = 
@"UPDATE Cadastros.Municipio SET
	IDEstado= @IDEstado
, 	Nome= @Nome
, 	DATHOR= GetDate()
, 	USUTRA= @USUTRA

    WHERE IDCidade = @original_IDCidade";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDEstado", System.Data.DbType.Int32, -1, IDEstado );
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
	bd.AdicionarParametro("@USUTRA", System.Data.DbType.AnsiString, 50, System.Threading.Thread.CurrentPrincipal.Identity.Name );

	bd.AdicionarParametro("@original_IDCidade", System.Data.DbType.Int32, -1, original_IDCidade );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDCidade)
	{
      string sql = 
@"DELETE FROM Cadastros.Municipio

    WHERE 
IDCidade = @original_IDCidade
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDCidade", System.Data.DbType.Int32, -1, original_IDCidade );

      bd.ExecuteNonQuery();
    }


}
}

