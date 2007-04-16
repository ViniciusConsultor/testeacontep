// Classe gerada automaticamente pela ferramenta GeradorCRUD
// em seg 19/03/2007 
// Por FRANCISCONB\Chico na máquina FRANCISCONB 
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Acontep.Dado;
namespace Acontep.AD.Manutencao.Seguranca
{
[System.ComponentModel.DataObject(true)]
public static partial class SistemaAD 
 {



	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.String Nome, System.String Url, System.String Codigo){

      string sql = @"
INSERT INTO Permissao.Sistema
(
 Nome, 
 Url, 
 Codigo
)
    VALUES 
(
@Nome, 
@Url, 
@Codigo
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
	bd.AdicionarParametro("@Url", System.Data.DbType.AnsiString, 250, Url );
	bd.AdicionarParametro("@Codigo", System.Data.DbType.AnsiString, 5, Codigo );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDSistema, System.String Nome, System.String Url, System.String Codigo){
      string sql = 
@"UPDATE Permissao.Sistema SET
	Nome= @Nome
, 	Url= @Url
, 	Codigo= @Codigo

    WHERE IDSistema = @original_IDSistema";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
	bd.AdicionarParametro("@Url", System.Data.DbType.AnsiString, 250, Url );
	bd.AdicionarParametro("@Codigo", System.Data.DbType.AnsiString, 5, Codigo );

	bd.AdicionarParametro("@original_IDSistema", System.Data.DbType.Int32, -1, original_IDSistema );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDSistema)
	{
      string sql = 
@"DELETE FROM Permissao.Sistema

    WHERE 
IDSistema = @original_IDSistema
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDSistema", System.Data.DbType.Int32, -1, original_IDSistema );

      bd.ExecuteNonQuery();
    }


}
}

