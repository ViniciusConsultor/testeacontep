// Classe gerada automaticamente pela ferramenta GeradorCRUD
// em qua 11/04/2007 
// Por FRANCISCONB\Chico na máquina FRANCISCONB 
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Acontep.Dado;
namespace Acontep.AD.Contratos
{
[System.ComponentModel.DataObject(true)]
public static partial class PlanoTrabalhoAD 
 {

	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_PK_PlanoTrabalho (System.Int32 IDPlanoTrabalho){
string sql = @"
SELECT 
	
	IDPlanoTrabalho, 
	CodigoPlanoTrabalho, 
	Acao, 
	Objeto, 
	SitInstCadastral, 
	SitEngenharia, 
	SitSocial, 
	DataContratacao, 
	DataVigencia, 
	DataVigenciaProrrog1, 
	DataVigenciaProrrog2, 
	ValorRepasse, 
	ValorContraPartida, 
	ValorContraPartidaEngenharia, 
	ValorContraPartidaSocial, 
	IDEmenda, 
	DataReformulacaoPlanoTrabalho, 
	ReformulacaoPlanoTrabalho, 
	Programa, 
	DataEmpenho, 
	CodEmpenho, 
	DataEmpenhoInvest, 
	CodEmpenhoInvest, 
	DataEmpenhoCusteio, 
	CodEmpenhoCusteio, 
	IDCliente, 
	NaturezaDespesa, 
	ValorInvestimento 
FROM 
	Contratos.PlanoTrabalho
	  
 WHERE 
	IDPlanoTrabalho = @IDPlanoTrabalho";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDPlanoTrabalho", System.Data.DbType.Int32, -1, IDPlanoTrabalho );
      return bd.ObterDataTable();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_UQ_PlanoTrabalho (System.String CodigoPlanoTrabalho){
string sql = @"
SELECT 
	
	IDPlanoTrabalho, 
	CodigoPlanoTrabalho, 
	Acao, 
	Objeto, 
	SitInstCadastral, 
	SitEngenharia, 
	SitSocial, 
	DataContratacao, 
	DataVigencia, 
	DataVigenciaProrrog1, 
	DataVigenciaProrrog2, 
	ValorRepasse, 
	ValorContraPartida, 
	ValorContraPartidaEngenharia, 
	ValorContraPartidaSocial, 
	IDEmenda, 
	DataReformulacaoPlanoTrabalho, 
	ReformulacaoPlanoTrabalho, 
	Programa, 
	DataEmpenho, 
	CodEmpenho, 
	DataEmpenhoInvest, 
	CodEmpenhoInvest, 
	DataEmpenhoCusteio, 
	CodEmpenhoCusteio, 
	IDCliente, 
	NaturezaDespesa, 
	ValorInvestimento 
FROM 
	Contratos.PlanoTrabalho
	  
 WHERE 
	( CodigoPlanoTrabalho LIKE @CodigoPlanoTrabalho + '%' )";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@CodigoPlanoTrabalho", System.Data.DbType.AnsiString, 20, CodigoPlanoTrabalho );
      return bd.ObterDataTable();
    }




	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.String CodigoPlanoTrabalho, System.String Acao, System.String Objeto, System.String SitInstCadastral, System.String SitEngenharia, System.String SitSocial, System.DateTime? DataContratacao, System.DateTime? DataVigencia, System.DateTime? DataVigenciaProrrog1, System.DateTime? DataVigenciaProrrog2, System.Decimal? ValorRepasse, System.Decimal? ValorContraPartida, System.Decimal? ValorContraPartidaEngenharia, System.Decimal? ValorContraPartidaSocial, System.Int32? IDEmenda, System.DateTime? DataReformulacaoPlanoTrabalho, System.String ReformulacaoPlanoTrabalho, System.String Programa, System.DateTime? DataEmpenho, System.Int32? CodEmpenho, System.DateTime? DataEmpenhoInvest, System.Int32? CodEmpenhoInvest, System.DateTime? DataEmpenhoCusteio, System.Int32? CodEmpenhoCusteio, System.Int32? IDCliente, System.String NaturezaDespesa, System.Decimal? ValorInvestimento){

      string sql = @"
INSERT INTO Contratos.PlanoTrabalho
(
 CodigoPlanoTrabalho, 
 Acao, 
 Objeto, 
 SitInstCadastral, 
 SitEngenharia, 
 SitSocial, 
 DataContratacao, 
 DataVigencia, 
 DataVigenciaProrrog1, 
 DataVigenciaProrrog2, 
 ValorRepasse, 
 ValorContraPartida, 
 ValorContraPartidaEngenharia, 
 ValorContraPartidaSocial, 
 IDEmenda, 
 DataReformulacaoPlanoTrabalho, 
 ReformulacaoPlanoTrabalho, 
 Programa, 
 DataEmpenho, 
 CodEmpenho, 
 DataEmpenhoInvest, 
 CodEmpenhoInvest, 
 DataEmpenhoCusteio, 
 CodEmpenhoCusteio, 
 IDCliente, 
 NaturezaDespesa, 
 ValorInvestimento
)
    VALUES 
(
@CodigoPlanoTrabalho, 
@Acao, 
@Objeto, 
@SitInstCadastral, 
@SitEngenharia, 
@SitSocial, 
@DataContratacao, 
@DataVigencia, 
@DataVigenciaProrrog1, 
@DataVigenciaProrrog2, 
@ValorRepasse, 
@ValorContraPartida, 
@ValorContraPartidaEngenharia, 
@ValorContraPartidaSocial, 
@IDEmenda, 
@DataReformulacaoPlanoTrabalho, 
@ReformulacaoPlanoTrabalho, 
@Programa, 
@DataEmpenho, 
@CodEmpenho, 
@DataEmpenhoInvest, 
@CodEmpenhoInvest, 
@DataEmpenhoCusteio, 
@CodEmpenhoCusteio, 
@IDCliente, 
@NaturezaDespesa, 
@ValorInvestimento
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@CodigoPlanoTrabalho", System.Data.DbType.AnsiString, 20, CodigoPlanoTrabalho );
	bd.AdicionarParametro("@Acao", System.Data.DbType.AnsiString, 400, Acao );
	bd.AdicionarParametro("@Objeto", System.Data.DbType.AnsiString, 400, Objeto );
	bd.AdicionarParametro("@SitInstCadastral", System.Data.DbType.AnsiString, 255, SitInstCadastral );
	bd.AdicionarParametro("@SitEngenharia", System.Data.DbType.AnsiString, 255, SitEngenharia );
	bd.AdicionarParametro("@SitSocial", System.Data.DbType.AnsiString, 255, SitSocial );
	bd.AdicionarParametro("@DataContratacao", System.Data.DbType.DateTime, -1, DataContratacao );
	bd.AdicionarParametro("@DataVigencia", System.Data.DbType.DateTime, -1, DataVigencia );
	bd.AdicionarParametro("@DataVigenciaProrrog1", System.Data.DbType.DateTime, -1, DataVigenciaProrrog1 );
	bd.AdicionarParametro("@DataVigenciaProrrog2", System.Data.DbType.DateTime, -1, DataVigenciaProrrog2 );
	bd.AdicionarParametro("@ValorRepasse", System.Data.DbType.Decimal, -1, ValorRepasse );
	bd.AdicionarParametro("@ValorContraPartida", System.Data.DbType.Decimal, -1, ValorContraPartida );
	bd.AdicionarParametro("@ValorContraPartidaEngenharia", System.Data.DbType.Decimal, -1, ValorContraPartidaEngenharia );
	bd.AdicionarParametro("@ValorContraPartidaSocial", System.Data.DbType.Decimal, -1, ValorContraPartidaSocial );
	bd.AdicionarParametro("@IDEmenda", System.Data.DbType.Int32, -1, IDEmenda );
	bd.AdicionarParametro("@DataReformulacaoPlanoTrabalho", System.Data.DbType.DateTime, -1, DataReformulacaoPlanoTrabalho );
	bd.AdicionarParametro("@ReformulacaoPlanoTrabalho", System.Data.DbType.AnsiString, 500, ReformulacaoPlanoTrabalho );
	bd.AdicionarParametro("@Programa", System.Data.DbType.AnsiString, 255, Programa );
	bd.AdicionarParametro("@DataEmpenho", System.Data.DbType.DateTime, -1, DataEmpenho );
	bd.AdicionarParametro("@CodEmpenho", System.Data.DbType.Int32, -1, CodEmpenho );
	bd.AdicionarParametro("@DataEmpenhoInvest", System.Data.DbType.DateTime, -1, DataEmpenhoInvest );
	bd.AdicionarParametro("@CodEmpenhoInvest", System.Data.DbType.Int32, -1, CodEmpenhoInvest );
	bd.AdicionarParametro("@DataEmpenhoCusteio", System.Data.DbType.DateTime, -1, DataEmpenhoCusteio );
	bd.AdicionarParametro("@CodEmpenhoCusteio", System.Data.DbType.Int32, -1, CodEmpenhoCusteio );
	bd.AdicionarParametro("@IDCliente", System.Data.DbType.Int32, -1, IDCliente );
	bd.AdicionarParametro("@NaturezaDespesa", System.Data.DbType.AnsiString, 500, NaturezaDespesa );
	bd.AdicionarParametro("@ValorInvestimento", System.Data.DbType.Decimal, -1, ValorInvestimento );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDPlanoTrabalho, System.String CodigoPlanoTrabalho, System.String Acao, System.String Objeto, System.String SitInstCadastral, System.String SitEngenharia, System.String SitSocial, System.DateTime? DataContratacao, System.DateTime? DataVigencia, System.DateTime? DataVigenciaProrrog1, System.DateTime? DataVigenciaProrrog2, System.Decimal? ValorRepasse, System.Decimal? ValorContraPartida, System.Decimal? ValorContraPartidaEngenharia, System.Decimal? ValorContraPartidaSocial, System.Int32? IDEmenda, System.DateTime? DataReformulacaoPlanoTrabalho, System.String ReformulacaoPlanoTrabalho, System.String Programa, System.DateTime? DataEmpenho, System.Int32? CodEmpenho, System.DateTime? DataEmpenhoInvest, System.Int32? CodEmpenhoInvest, System.DateTime? DataEmpenhoCusteio, System.Int32? CodEmpenhoCusteio, System.Int32? IDCliente, System.String NaturezaDespesa, System.Decimal? ValorInvestimento){
      string sql = 
@"UPDATE Contratos.PlanoTrabalho SET
	CodigoPlanoTrabalho= @CodigoPlanoTrabalho
, 	Acao= @Acao
, 	Objeto= @Objeto
, 	SitInstCadastral= @SitInstCadastral
, 	SitEngenharia= @SitEngenharia
, 	SitSocial= @SitSocial
, 	DataContratacao= @DataContratacao
, 	DataVigencia= @DataVigencia
, 	DataVigenciaProrrog1= @DataVigenciaProrrog1
, 	DataVigenciaProrrog2= @DataVigenciaProrrog2
, 	ValorRepasse= @ValorRepasse
, 	ValorContraPartida= @ValorContraPartida
, 	ValorContraPartidaEngenharia= @ValorContraPartidaEngenharia
, 	ValorContraPartidaSocial= @ValorContraPartidaSocial
, 	IDEmenda= @IDEmenda
, 	DataReformulacaoPlanoTrabalho= @DataReformulacaoPlanoTrabalho
, 	ReformulacaoPlanoTrabalho= @ReformulacaoPlanoTrabalho
, 	Programa= @Programa
, 	DataEmpenho= @DataEmpenho
, 	CodEmpenho= @CodEmpenho
, 	DataEmpenhoInvest= @DataEmpenhoInvest
, 	CodEmpenhoInvest= @CodEmpenhoInvest
, 	DataEmpenhoCusteio= @DataEmpenhoCusteio
, 	CodEmpenhoCusteio= @CodEmpenhoCusteio
, 	IDCliente= @IDCliente
, 	NaturezaDespesa= @NaturezaDespesa
, 	ValorInvestimento= @ValorInvestimento

    WHERE IDPlanoTrabalho = @original_IDPlanoTrabalho";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@CodigoPlanoTrabalho", System.Data.DbType.AnsiString, 20, CodigoPlanoTrabalho );
	bd.AdicionarParametro("@Acao", System.Data.DbType.AnsiString, 400, Acao );
	bd.AdicionarParametro("@Objeto", System.Data.DbType.AnsiString, 400, Objeto );
	bd.AdicionarParametro("@SitInstCadastral", System.Data.DbType.AnsiString, 255, SitInstCadastral );
	bd.AdicionarParametro("@SitEngenharia", System.Data.DbType.AnsiString, 255, SitEngenharia );
	bd.AdicionarParametro("@SitSocial", System.Data.DbType.AnsiString, 255, SitSocial );
	bd.AdicionarParametro("@DataContratacao", System.Data.DbType.DateTime, -1, DataContratacao );
	bd.AdicionarParametro("@DataVigencia", System.Data.DbType.DateTime, -1, DataVigencia );
	bd.AdicionarParametro("@DataVigenciaProrrog1", System.Data.DbType.DateTime, -1, DataVigenciaProrrog1 );
	bd.AdicionarParametro("@DataVigenciaProrrog2", System.Data.DbType.DateTime, -1, DataVigenciaProrrog2 );
	bd.AdicionarParametro("@ValorRepasse", System.Data.DbType.Decimal, -1, ValorRepasse );
	bd.AdicionarParametro("@ValorContraPartida", System.Data.DbType.Decimal, -1, ValorContraPartida );
	bd.AdicionarParametro("@ValorContraPartidaEngenharia", System.Data.DbType.Decimal, -1, ValorContraPartidaEngenharia );
	bd.AdicionarParametro("@ValorContraPartidaSocial", System.Data.DbType.Decimal, -1, ValorContraPartidaSocial );
	bd.AdicionarParametro("@IDEmenda", System.Data.DbType.Int32, -1, IDEmenda );
	bd.AdicionarParametro("@DataReformulacaoPlanoTrabalho", System.Data.DbType.DateTime, -1, DataReformulacaoPlanoTrabalho );
	bd.AdicionarParametro("@ReformulacaoPlanoTrabalho", System.Data.DbType.AnsiString, 500, ReformulacaoPlanoTrabalho );
	bd.AdicionarParametro("@Programa", System.Data.DbType.AnsiString, 255, Programa );
	bd.AdicionarParametro("@DataEmpenho", System.Data.DbType.DateTime, -1, DataEmpenho );
	bd.AdicionarParametro("@CodEmpenho", System.Data.DbType.Int32, -1, CodEmpenho );
	bd.AdicionarParametro("@DataEmpenhoInvest", System.Data.DbType.DateTime, -1, DataEmpenhoInvest );
	bd.AdicionarParametro("@CodEmpenhoInvest", System.Data.DbType.Int32, -1, CodEmpenhoInvest );
	bd.AdicionarParametro("@DataEmpenhoCusteio", System.Data.DbType.DateTime, -1, DataEmpenhoCusteio );
	bd.AdicionarParametro("@CodEmpenhoCusteio", System.Data.DbType.Int32, -1, CodEmpenhoCusteio );
	bd.AdicionarParametro("@IDCliente", System.Data.DbType.Int32, -1, IDCliente );
	bd.AdicionarParametro("@NaturezaDespesa", System.Data.DbType.AnsiString, 500, NaturezaDespesa );
	bd.AdicionarParametro("@ValorInvestimento", System.Data.DbType.Decimal, -1, ValorInvestimento );

	bd.AdicionarParametro("@original_IDPlanoTrabalho", System.Data.DbType.Int32, -1, original_IDPlanoTrabalho );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDPlanoTrabalho)
	{
      string sql = 
@"DELETE FROM Contratos.PlanoTrabalho

    WHERE 
IDPlanoTrabalho = @original_IDPlanoTrabalho
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDPlanoTrabalho", System.Data.DbType.Int32, -1, original_IDPlanoTrabalho );

      bd.ExecuteNonQuery();
    }


}
}

