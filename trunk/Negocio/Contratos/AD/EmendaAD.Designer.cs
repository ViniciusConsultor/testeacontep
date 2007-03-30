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
public static partial class EmendaAD 
 {

	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_PK_Emenda (System.Int32 IDEmenda){
string sql = @"
SELECT 
	
	IDEmenda, 
	Numero, 
	IDArea, 
	IDTipoRealizacao, 
	IDCidade, 
	NumeroFuncional, 
	Valor, 
	Autor 
FROM 
	Contratos.Emenda
	  
 WHERE 
	IDEmenda = @IDEmenda";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDEmenda", System.Data.DbType.Int32, -1, IDEmenda );
      return bd.ObterDataTable();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_UQ_Emenda (System.Int32 Numero){
string sql = @"
SELECT 
	
	IDEmenda, 
	Numero, 
	IDArea, 
	IDTipoRealizacao, 
	IDCidade, 
	NumeroFuncional, 
	Valor, 
	Autor 
FROM 
	Contratos.Emenda
	  
 WHERE 
	Numero = @Numero";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Numero", System.Data.DbType.Int32, -1, Numero );
      return bd.ObterDataTable();
    }




	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.Int32 Numero, System.Int32? IDArea, System.Int32? IDTipoRealizacao, System.Int32? IDCidade, System.Int32? NumeroFuncional, System.Decimal? Valor, System.String Autor){

      string sql = @"
INSERT INTO Contratos.Emenda
(
 Numero, 
 IDArea, 
 IDTipoRealizacao, 
 IDCidade, 
 NumeroFuncional, 
 Valor, 
 Autor
)
    VALUES 
(
@Numero, 
@IDArea, 
@IDTipoRealizacao, 
@IDCidade, 
@NumeroFuncional, 
@Valor, 
@Autor
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@Numero", System.Data.DbType.Int32, -1, Numero );
	bd.AdicionarParametro("@IDArea", System.Data.DbType.Int32, -1, IDArea );
	bd.AdicionarParametro("@IDTipoRealizacao", System.Data.DbType.Int32, -1, IDTipoRealizacao );
	bd.AdicionarParametro("@IDCidade", System.Data.DbType.Int32, -1, IDCidade );
	bd.AdicionarParametro("@NumeroFuncional", System.Data.DbType.Int32, -1, NumeroFuncional );
	bd.AdicionarParametro("@Valor", System.Data.DbType.Decimal, -1, Valor );
	bd.AdicionarParametro("@Autor", System.Data.DbType.AnsiString, 200, Autor );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDEmenda, System.Int32 Numero, System.Int32? IDArea, System.Int32? IDTipoRealizacao, System.Int32? IDCidade, System.Int32? NumeroFuncional, System.Decimal? Valor, System.String Autor){
      string sql = 
@"UPDATE Contratos.Emenda SET
	Numero= @Numero
, 	IDArea= @IDArea
, 	IDTipoRealizacao= @IDTipoRealizacao
, 	IDCidade= @IDCidade
, 	NumeroFuncional= @NumeroFuncional
, 	Valor= @Valor
, 	Autor= @Autor

    WHERE IDEmenda = @original_IDEmenda";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Numero", System.Data.DbType.Int32, -1, Numero );
	bd.AdicionarParametro("@IDArea", System.Data.DbType.Int32, -1, IDArea );
	bd.AdicionarParametro("@IDTipoRealizacao", System.Data.DbType.Int32, -1, IDTipoRealizacao );
	bd.AdicionarParametro("@IDCidade", System.Data.DbType.Int32, -1, IDCidade );
	bd.AdicionarParametro("@NumeroFuncional", System.Data.DbType.Int32, -1, NumeroFuncional );
	bd.AdicionarParametro("@Valor", System.Data.DbType.Decimal, -1, Valor );
	bd.AdicionarParametro("@Autor", System.Data.DbType.AnsiString, 200, Autor );

	bd.AdicionarParametro("@original_IDEmenda", System.Data.DbType.Int32, -1, original_IDEmenda );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDEmenda)
	{
      string sql = 
@"DELETE FROM Contratos.Emenda

    WHERE 
IDEmenda = @original_IDEmenda
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDEmenda", System.Data.DbType.Int32, -1, original_IDEmenda );

      bd.ExecuteNonQuery();
    }


}
}

