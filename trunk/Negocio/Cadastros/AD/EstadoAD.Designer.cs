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
public static partial class EstadoAD 
 {

	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_PK_Estado (System.Int32 IDEstado){
string sql = @"
SELECT 
	
	IDEstado, 
	Sigla, 
	Nome, 
	DATINC, 
	USUINC, 
	DATHOR, 
	USUTRA 
FROM 
	Cadastros.Estado
	  
 WHERE 
	IDEstado = @IDEstado";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDEstado", System.Data.DbType.Int32, -1, IDEstado );
      return bd.ObterDataTable();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_UQ_Nome (System.String Nome){
string sql = @"
SELECT 
	
	IDEstado, 
	Sigla, 
	Nome, 
	DATINC, 
	USUINC, 
	DATHOR, 
	USUTRA 
FROM 
	Cadastros.Estado
	  
 WHERE 
	( Nome LIKE @Nome + '%' )";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
      return bd.ObterDataTable();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_UQ_Sigla (System.String Sigla){
string sql = @"
SELECT 
	
	IDEstado, 
	Sigla, 
	Nome, 
	DATINC, 
	USUINC, 
	DATHOR, 
	USUTRA 
FROM 
	Cadastros.Estado
	  
 WHERE 
	( Sigla LIKE @Sigla + '%' )";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Sigla", System.Data.DbType.AnsiString, 2, Sigla );
      return bd.ObterDataTable();
    }




	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.String Sigla, System.String Nome){

      string sql = @"
INSERT INTO Cadastros.Estado
(
 Sigla, 
 Nome, 
 DATINC, 
 USUINC, 
 DATHOR, 
 USUTRA
)
    VALUES 
(
@Sigla, 
@Nome, 
GetDate(), 
@USUINC, 
GetDate(), 
@USUTRA
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@Sigla", System.Data.DbType.AnsiString, 2, Sigla );
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
	bd.AdicionarParametro("@USUINC", System.Data.DbType.AnsiString, 50, System.Threading.Thread.CurrentPrincipal.Identity.Name );
	bd.AdicionarParametro("@USUTRA", System.Data.DbType.AnsiString, 50, System.Threading.Thread.CurrentPrincipal.Identity.Name );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDEstado, System.String Sigla, System.String Nome){
      string sql = 
@"UPDATE Cadastros.Estado SET
	Sigla= @Sigla
, 	Nome= @Nome
, 	DATHOR= GetDate()
, 	USUTRA= @USUTRA

    WHERE IDEstado = @original_IDEstado";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Sigla", System.Data.DbType.AnsiString, 2, Sigla );
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
	bd.AdicionarParametro("@USUTRA", System.Data.DbType.AnsiString, 50, System.Threading.Thread.CurrentPrincipal.Identity.Name );

	bd.AdicionarParametro("@original_IDEstado", System.Data.DbType.Int32, -1, original_IDEstado );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDEstado)
	{
      string sql = 
@"DELETE FROM Cadastros.Estado

    WHERE 
IDEstado = @original_IDEstado
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDEstado", System.Data.DbType.Int32, -1, original_IDEstado );

      bd.ExecuteNonQuery();
    }


}
}

