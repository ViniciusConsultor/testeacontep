// Classe gerada automaticamente pela ferramenta GeradorCRUD
// em seg 19/03/2007 
// Por FRANCISCONB\Chico na m�quina FRANCISCONB 
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Acontep.Dado;
namespace Acontep.AD.Manutencao.Seguranca
{
[System.ComponentModel.DataObject(true)]
public static partial class ModuloAD 
 {


    public static DataTable ObterPor_UQ_MODULO(int IDSistema, String Codigo)
    {
        string sql = "Select * From Permissao.Modulo where IDSistema = @IDSistema and Codigo = @Codigo";
        BdUtil bd = new BdUtil(sql);
        bd.AdicionarParametro("@IDSistema", System.Data.DbType.Int32, -1, IDSistema);
        bd.AdicionarParametro("@Codigo", System.Data.DbType.AnsiString, 50, Codigo);

        return bd.ObterDataTable();
    }
	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.Int32 IDSistema, System.String Nome, System.String Url, System.String Codigo){

      string sql = @"
INSERT INTO Permissao.Modulo
(
 IDSistema, 
 Nome, 
 Url, 
 Codigo
)
    VALUES 
(
@IDSistema, 
@Nome, 
@Url, 
@Codigo
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@IDSistema", System.Data.DbType.Int32, -1, IDSistema );
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
	bd.AdicionarParametro("@Url", System.Data.DbType.AnsiString, 250, Url );
	bd.AdicionarParametro("@Codigo", System.Data.DbType.AnsiString, 5, Codigo );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDModulo, System.Int32 IDSistema, System.String Nome, System.String Url, System.String Codigo){
      string sql = 
@"UPDATE Permissao.Modulo SET
	IDSistema= @IDSistema
, 	Nome= @Nome
, 	Url= @Url
, 	Codigo= @Codigo

    WHERE IDModulo = @original_IDModulo";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDSistema", System.Data.DbType.Int32, -1, IDSistema );
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
	bd.AdicionarParametro("@Url", System.Data.DbType.AnsiString, 250, Url );
	bd.AdicionarParametro("@Codigo", System.Data.DbType.AnsiString, 5, Codigo );

	bd.AdicionarParametro("@original_IDModulo", System.Data.DbType.Int32, -1, original_IDModulo );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDModulo)
	{
      string sql = 
@"DELETE FROM Permissao.Modulo

    WHERE 
IDModulo = @original_IDModulo
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDModulo", System.Data.DbType.Int32, -1, original_IDModulo );

      bd.ExecuteNonQuery();
    }


}
}

