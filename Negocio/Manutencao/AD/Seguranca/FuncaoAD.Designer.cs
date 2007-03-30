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
public static partial class FuncaoAD 
 {
    public static DataTable ObterPor_UQ_FUNCAO(int IDModulo, String Codigo)
    {
        string sql = "Select * From Permissao.Funcao where IDModulo = @IDModulo and Codigo = @Codigo";
        BdUtil bd = new BdUtil(sql);
        bd.AdicionarParametro("@IDModulo", System.Data.DbType.Int32, -1, IDModulo);
        bd.AdicionarParametro("@Codigo", System.Data.DbType.AnsiString, 50, Codigo);

        return bd.ObterDataTable();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.Int32 IDModulo, System.String Nome, System.String Url, System.String Codigo, System.Boolean Ativo, System.Boolean? Menu){

      string sql = @"
INSERT INTO Permissao.Funcao
(
 IDModulo, 
 Nome, 
 Url, 
 Codigo, 
 Ativo, 
 Menu
)
    VALUES 
(
@IDModulo, 
@Nome, 
@Url, 
@Codigo, 
@Ativo, 
@Menu
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@IDModulo", System.Data.DbType.Int32, -1, IDModulo );
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
	bd.AdicionarParametro("@Url", System.Data.DbType.AnsiString, 250, Url );
	bd.AdicionarParametro("@Codigo", System.Data.DbType.AnsiString, 5, Codigo );
	bd.AdicionarParametro("@Ativo", System.Data.DbType.Boolean, -1, Ativo );
	bd.AdicionarParametro("@Menu", System.Data.DbType.Boolean, -1, Menu );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDFuncao, System.Int32 IDModulo, System.String Nome, System.String Url, System.String Codigo, System.Boolean Ativo, System.Boolean? Menu){
      string sql = 
@"UPDATE Permissao.Funcao SET
	IDModulo= @IDModulo
, 	Nome= @Nome
, 	Url= @Url
, 	Codigo= @Codigo
, 	Ativo= @Ativo
, 	Menu= @Menu

    WHERE IDFuncao = @original_IDFuncao";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDModulo", System.Data.DbType.Int32, -1, IDModulo );
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
	bd.AdicionarParametro("@Url", System.Data.DbType.AnsiString, 250, Url );
	bd.AdicionarParametro("@Codigo", System.Data.DbType.AnsiString, 5, Codigo );
	bd.AdicionarParametro("@Ativo", System.Data.DbType.Boolean, -1, Ativo );
	bd.AdicionarParametro("@Menu", System.Data.DbType.Boolean, -1, Menu );

	bd.AdicionarParametro("@original_IDFuncao", System.Data.DbType.Int32, -1, original_IDFuncao );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDFuncao)
	{
      string sql = 
@"DELETE FROM Permissao.Funcao

    WHERE 
IDFuncao = @original_IDFuncao
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDFuncao", System.Data.DbType.Int32, -1, original_IDFuncao );

      bd.ExecuteNonQuery();
    }


}
}

