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
public static partial class GrupoAD 
 {



	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.String Nome){

      string sql = @"
INSERT INTO Permissao.Grupo
(
 Nome, 
 DatInc, 
 UsuTra, 
 DatHor, 
 UsuInc
)
    VALUES 
(
@Nome, 
GetDate(), 
@UsuTra, 
GetDate(), 
@UsuInc
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
	bd.AdicionarParametro("@UsuTra", System.Data.DbType.AnsiString, 50, System.Threading.Thread.CurrentPrincipal.Identity.Name );
	bd.AdicionarParametro("@UsuInc", System.Data.DbType.AnsiString, 50, System.Threading.Thread.CurrentPrincipal.Identity.Name );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDGrupo, System.String Nome){
      string sql = 
@"UPDATE Permissao.Grupo SET
	Nome= @Nome
, 	UsuTra= @UsuTra
, 	DatHor= GetDate()

    WHERE IDGrupo = @original_IDGrupo";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
	bd.AdicionarParametro("@UsuTra", System.Data.DbType.AnsiString, 50, System.Threading.Thread.CurrentPrincipal.Identity.Name );

	bd.AdicionarParametro("@original_IDGrupo", System.Data.DbType.Int32, -1, original_IDGrupo );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDGrupo)
	{
      string sql = 
@"DELETE FROM Permissao.Grupo

    WHERE 
IDGrupo = @original_IDGrupo
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDGrupo", System.Data.DbType.Int32, -1, original_IDGrupo );

      bd.ExecuteNonQuery();
    }


}
}

