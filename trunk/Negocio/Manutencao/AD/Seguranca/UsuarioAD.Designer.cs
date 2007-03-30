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
public static partial class UsuarioAD 
 {



	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.String Login, System.String Senha){

      string sql = @"
INSERT INTO Permissao.Usuario
(
 Login, 
 Senha
)
    VALUES 
(
@Login, 
@Senha
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@Login", System.Data.DbType.AnsiString, 50, Login );
	bd.AdicionarParametro("@Senha", System.Data.DbType.AnsiString, 4000, Senha );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDUsuario, System.String Login, System.String Senha){
      string sql = 
@"UPDATE Permissao.Usuario SET
	Login= @Login
, 	Senha= @Senha

    WHERE IDUsuario = @original_IDUsuario";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Login", System.Data.DbType.AnsiString, 50, Login );
	bd.AdicionarParametro("@Senha", System.Data.DbType.AnsiString, 4000, Senha );

	bd.AdicionarParametro("@original_IDUsuario", System.Data.DbType.Int32, -1, original_IDUsuario );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDUsuario)
	{
      string sql = 
@"DELETE FROM Permissao.Usuario

    WHERE 
IDUsuario = @original_IDUsuario
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDUsuario", System.Data.DbType.Int32, -1, original_IDUsuario );

      bd.ExecuteNonQuery();
    }


}
}

