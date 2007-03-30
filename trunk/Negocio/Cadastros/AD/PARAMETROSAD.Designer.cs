// Classe gerada automaticamente pela ferramenta GeradorCRUD
// em seg 15/01/2007 
// Por CENTRAL\thiago na máquina INFOAJU07PC 
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Acontep.Dado;
namespace Acontep.AD.Cadastros
{
[System.ComponentModel.DataObject(true)]
public static partial class ParametrosAD 
 {

	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_IX_PARAMETROS_ParametrosSistema (System.Byte Plt_Planta, System.Int16? Sis_CodSis){
string sql = @"
SELECT 
	
	Plt_Planta, 
	Par_NomPar, 
	Par_ValPar, 
	Par_ParFix, 
	Par_DatInc, 
	Par_DatHor, 
	Par_UsuTra, 
	Sis_CodSis, 
	Par_Descricao 
FROM 
	dbo.PARAMETROS
	  
 WHERE 
	( Plt_Planta LIKE @Plt_Planta ) AND ( Sis_CodSis LIKE @Sis_CodSis )";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Plt_Planta", System.Data.DbType.Byte, -1, Plt_Planta );
	bd.AdicionarParametro("@Sis_CodSis", System.Data.DbType.Int16, -1, Sis_CodSis );
      return bd.ObterDataTable();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_IX_PARAMETROS_Plt_Planta_Sis_CodSis_Par_NomPar (System.Byte Plt_Planta, System.Int16? Sis_CodSis, System.String Par_NomPar){
string sql = @"
SELECT 
	
	Plt_Planta, 
	Par_NomPar, 
	Par_ValPar, 
	Par_ParFix, 
	Par_DatInc, 
	Par_DatHor, 
	Par_UsuTra, 
	Sis_CodSis, 
	Par_Descricao 
FROM 
	dbo.PARAMETROS
	  
 WHERE 
	( Plt_Planta LIKE @Plt_Planta ) AND ( Sis_CodSis LIKE @Sis_CodSis ) AND ( Par_NomPar LIKE @Par_NomPar )";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Plt_Planta", System.Data.DbType.Byte, -1, Plt_Planta );
	bd.AdicionarParametro("@Sis_CodSis", System.Data.DbType.Int16, -1, Sis_CodSis );
	bd.AdicionarParametro("@Par_NomPar", System.Data.DbType.AnsiString, 20, Par_NomPar );
      return bd.ObterDataTable();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_PK_PARAMETROS (System.Byte Plt_Planta, System.String Par_NomPar){
string sql = @"
SELECT 
	
	Plt_Planta, 
	Par_NomPar, 
	Par_ValPar, 
	Par_ParFix, 
	Par_DatInc, 
	Par_DatHor, 
	Par_UsuTra, 
	Sis_CodSis, 
	Par_Descricao 
FROM 
	dbo.PARAMETROS
	  
 WHERE 
	( Plt_Planta LIKE @Plt_Planta ) AND ( Par_NomPar LIKE @Par_NomPar )";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Plt_Planta", System.Data.DbType.Byte, -1, Plt_Planta );
	bd.AdicionarParametro("@Par_NomPar", System.Data.DbType.String, 20, Par_NomPar );
      return bd.ObterDataTable();
    }




	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.Byte Plt_Planta, System.String Par_NomPar, System.String Par_ValPar, System.Boolean Par_ParFix, System.Int16? Sis_CodSis, System.String Par_Descricao){

      string sql = @"
INSERT INTO dbo.PARAMETROS
(
 Plt_Planta, 
 Par_NomPar, 
 Par_ValPar, 
 Par_ParFix, 
 Par_DatInc, 
 Par_DatHor, 
 Par_UsuTra, 
 Sis_CodSis, 
 Par_Descricao
)
    VALUES 
(
@Plt_Planta, 
@Par_NomPar, 
@Par_ValPar, 
@Par_ParFix, 
GetDate(), 
GetDate(), 
@Par_UsuTra, 
@Sis_CodSis, 
@Par_Descricao
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@Plt_Planta", System.Data.DbType.Byte, -1, Plt_Planta );
	bd.AdicionarParametro("@Par_NomPar", System.Data.DbType.String, 20, Par_NomPar );
	bd.AdicionarParametro("@Par_ValPar", System.Data.DbType.String, 40, Par_ValPar );
	bd.AdicionarParametro("@Par_ParFix", System.Data.DbType.Boolean, -1, Par_ParFix );
	bd.AdicionarParametro("@Par_UsuTra", System.Data.DbType.String, 15, System.Threading.Thread.CurrentPrincipal.Identity.Name );
	bd.AdicionarParametro("@Sis_CodSis", System.Data.DbType.Int16, -1, Sis_CodSis );
	bd.AdicionarParametro("@Par_Descricao", System.Data.DbType.String, 2147483647, Par_Descricao );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Byte original_Plt_Planta, System.String original_Par_NomPar, System.Byte Plt_Planta, System.String Par_NomPar, System.String Par_ValPar, System.Boolean Par_ParFix, System.Int16? Sis_CodSis, System.String Par_Descricao){
      string sql = 
@"UPDATE dbo.PARAMETROS SET
	Plt_Planta= @Plt_Planta
, 	Par_NomPar= @Par_NomPar
, 	Par_ValPar= @Par_ValPar
, 	Par_ParFix= @Par_ParFix
, 	Par_DatHor= GetDate()
, 	Par_UsuTra= @Par_UsuTra
, 	Sis_CodSis= @Sis_CodSis
, 	Par_Descricao= @Par_Descricao

    WHERE Plt_Planta = @original_Plt_Planta AND Par_NomPar = @original_Par_NomPar";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Plt_Planta", System.Data.DbType.Byte, -1, Plt_Planta );
	bd.AdicionarParametro("@Par_NomPar", System.Data.DbType.String, 20, Par_NomPar );
	bd.AdicionarParametro("@Par_ValPar", System.Data.DbType.String, 40, Par_ValPar );
	bd.AdicionarParametro("@Par_ParFix", System.Data.DbType.Boolean, -1, Par_ParFix );
	bd.AdicionarParametro("@Par_UsuTra", System.Data.DbType.String, 15, System.Threading.Thread.CurrentPrincipal.Identity.Name );
	bd.AdicionarParametro("@Sis_CodSis", System.Data.DbType.Int16, -1, Sis_CodSis );
	bd.AdicionarParametro("@Par_Descricao", System.Data.DbType.String, 2147483647, Par_Descricao );

	bd.AdicionarParametro("@original_Plt_Planta", System.Data.DbType.Byte, -1, original_Plt_Planta );
	bd.AdicionarParametro("@original_Par_NomPar", System.Data.DbType.String, 20, original_Par_NomPar );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Byte original_Plt_Planta, System.String original_Par_NomPar)
	{
      string sql = 
@"DELETE FROM dbo.PARAMETROS

    WHERE 
Plt_Planta = @original_Plt_Planta AND Par_NomPar = @original_Par_NomPar
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_Plt_Planta", System.Data.DbType.Byte, -1, original_Plt_Planta );
	bd.AdicionarParametro("@original_Par_NomPar", System.Data.DbType.String, 20, original_Par_NomPar );

      bd.ExecuteNonQuery();
    }


}
}

