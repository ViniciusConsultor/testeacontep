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
public static partial class GrupoUsuarioAD 
 {



	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.Int32 IDGrupo, System.Int32 IDUsuario){

      string sql = @"
INSERT INTO Permissao.GrupoUsuario
(
 IDGrupo, 
 IDUsuario
)
    VALUES 
(
@IDGrupo, 
@IDUsuario
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@IDGrupo", System.Data.DbType.Int32, -1, IDGrupo );
	bd.AdicionarParametro("@IDUsuario", System.Data.DbType.Int32, -1, IDUsuario );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDGrupoUsuario, System.Int32 IDGrupo, System.Int32 IDUsuario){
      string sql = 
@"UPDATE Permissao.GrupoUsuario SET
	IDGrupo= @IDGrupo
, 	IDUsuario= @IDUsuario

    WHERE IDGrupoUsuario = @original_IDGrupoUsuario";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDGrupo", System.Data.DbType.Int32, -1, IDGrupo );
	bd.AdicionarParametro("@IDUsuario", System.Data.DbType.Int32, -1, IDUsuario );

	bd.AdicionarParametro("@original_IDGrupoUsuario", System.Data.DbType.Int32, -1, original_IDGrupoUsuario );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDGrupoUsuario)
	{
      string sql = 
@"DELETE FROM Permissao.GrupoUsuario

    WHERE 
IDGrupoUsuario = @original_IDGrupoUsuario
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDGrupoUsuario", System.Data.DbType.Int32, -1, original_IDGrupoUsuario );

      bd.ExecuteNonQuery();
    }


}
}

