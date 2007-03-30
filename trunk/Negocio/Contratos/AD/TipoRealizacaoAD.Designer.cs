// Classe gerada automaticamente pela ferramenta GeradorCRUD
// em qua 21/03/2007 
// Por FRANCISCONB\Chico na máquina FRANCISCONB 
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Acontep.Dado;
namespace Acontep.AD.Contratos
{
[System.ComponentModel.DataObject(true)]
public static partial class TipoRealizacaoAD 
 {

	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_PK_TipoRealizacao (System.Int32 IDTipoRealizacao){
string sql = @"
SELECT 
	
	IDTipoRealizacao, 
	Nome 
FROM 
	Contratos.TipoRealizacao
	  
 WHERE 
	IDTipoRealizacao = @IDTipoRealizacao";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDTipoRealizacao", System.Data.DbType.Int32, -1, IDTipoRealizacao );
      return bd.ObterDataTable();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_UQ_TipoRealizacao (System.String Nome){
string sql = @"
SELECT 
	
	IDTipoRealizacao, 
	Nome 
FROM 
	Contratos.TipoRealizacao
	  
 WHERE 
	( Nome LIKE @Nome )";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
      return bd.ObterDataTable();
    }




	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.String Nome){

      string sql = @"
INSERT INTO Contratos.TipoRealizacao
(
 Nome
)
    VALUES 
(
@Nome
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDTipoRealizacao, System.String Nome){
      string sql = 
@"UPDATE Contratos.TipoRealizacao SET
	Nome= @Nome

    WHERE IDTipoRealizacao = @original_IDTipoRealizacao";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );

	bd.AdicionarParametro("@original_IDTipoRealizacao", System.Data.DbType.Int32, -1, original_IDTipoRealizacao );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDTipoRealizacao)
	{
      string sql = 
@"DELETE FROM Contratos.TipoRealizacao

    WHERE 
IDTipoRealizacao = @original_IDTipoRealizacao
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDTipoRealizacao", System.Data.DbType.Int32, -1, original_IDTipoRealizacao );

      bd.ExecuteNonQuery();
    }


}
}

