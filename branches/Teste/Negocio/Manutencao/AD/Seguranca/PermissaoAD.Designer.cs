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
public static partial class PermissaoAD 
 {



	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.Int32 IDGrupoUsuario, System.Int32 IDFuncao){

      string sql = @"
INSERT INTO Permissao.Permissao
(
 IDGrupoUsuario, 
 IDFuncao
)
    VALUES 
(
@IDGrupoUsuario, 
@IDFuncao
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@IDGrupoUsuario", System.Data.DbType.Int32, -1, IDGrupoUsuario );
	bd.AdicionarParametro("@IDFuncao", System.Data.DbType.Int32, -1, IDFuncao );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDPermissao, System.Int32 IDGrupoUsuario, System.Int32 IDFuncao){
      string sql = 
@"UPDATE Permissao.Permissao SET
	IDGrupoUsuario= @IDGrupoUsuario
, 	IDFuncao= @IDFuncao

    WHERE IDPermissao = @original_IDPermissao";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDGrupoUsuario", System.Data.DbType.Int32, -1, IDGrupoUsuario );
	bd.AdicionarParametro("@IDFuncao", System.Data.DbType.Int32, -1, IDFuncao );

	bd.AdicionarParametro("@original_IDPermissao", System.Data.DbType.Int32, -1, original_IDPermissao );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDPermissao)
	{
      string sql = 
@"DELETE FROM Permissao.Permissao

    WHERE 
IDPermissao = @original_IDPermissao
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDPermissao", System.Data.DbType.Int32, -1, original_IDPermissao );

      bd.ExecuteNonQuery();
    }


}
}

