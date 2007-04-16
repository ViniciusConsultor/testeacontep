// Classe gerada automaticamente pela ferramenta GeradorCRUD
// em sáb 07/04/2007 
// Por FRANCISCONB\Chico na máquina FRANCISCONB 
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Acontep.Dado;
namespace Acontep.AD.Contratos
{
[System.ComponentModel.DataObject(true)]
public static partial class PrefeitoAD 
 {

	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_PK_Prefeito (System.Int32 IDPrefeito){
string sql = @"
SELECT 
	
	IDPrefeito, 
	Nome, 
	RG, 
	OrgaoExpedidor, 
	IDEstadoExpedidor, 
	DataExpedicao, 
	Logradouro, 
	Bairro, 
	IDCidade, 
	CEP, 
	Nascimento, 
	Email, 
	Telefone, 
	Celular, 
	NomeConjuge, 
	DataNascimentoConjuge 
FROM 
	Contratos.Prefeito
	  
 WHERE 
	IDPrefeito = @IDPrefeito";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDPrefeito", System.Data.DbType.Int32, -1, IDPrefeito );
      return bd.ObterDataTable();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_UQ_Prefeito (System.String Nome){
string sql = @"
SELECT 
	
	IDPrefeito, 
	Nome, 
	RG, 
	OrgaoExpedidor, 
	IDEstadoExpedidor, 
	DataExpedicao, 
	Logradouro, 
	Bairro, 
	IDCidade, 
	CEP, 
	Nascimento, 
	Email, 
	Telefone, 
	Celular, 
	NomeConjuge, 
	DataNascimentoConjuge 
FROM 
	Contratos.Prefeito
	  
 WHERE 
	( Nome LIKE @Nome + '%' )";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 80, Nome );
      return bd.ObterDataTable();
    }




	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.String Nome, System.String RG, System.String OrgaoExpedidor, System.Int32? IDEstadoExpedidor, System.DateTime? DataExpedicao, System.String Logradouro, System.String Bairro, System.Int32? IDCidade, System.String CEP, System.DateTime? Nascimento, System.String Email, System.String Telefone, System.String Celular, System.String NomeConjuge, System.DateTime? DataNascimentoConjuge){

      string sql = @"
INSERT INTO Contratos.Prefeito
(
 Nome, 
 RG, 
 OrgaoExpedidor, 
 IDEstadoExpedidor, 
 DataExpedicao, 
 Logradouro, 
 Bairro, 
 IDCidade, 
 CEP, 
 Nascimento, 
 Email, 
 Telefone, 
 Celular, 
 NomeConjuge, 
 DataNascimentoConjuge
)
    VALUES 
(
@Nome, 
@RG, 
@OrgaoExpedidor, 
@IDEstadoExpedidor, 
@DataExpedicao, 
@Logradouro, 
@Bairro, 
@IDCidade, 
@CEP, 
@Nascimento, 
@Email, 
@Telefone, 
@Celular, 
@NomeConjuge, 
@DataNascimentoConjuge
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 80, Nome );
	bd.AdicionarParametro("@RG", System.Data.DbType.AnsiString, 15, RG );
	bd.AdicionarParametro("@OrgaoExpedidor", System.Data.DbType.AnsiString, 10, OrgaoExpedidor );
	bd.AdicionarParametro("@IDEstadoExpedidor", System.Data.DbType.Int32, -1, IDEstadoExpedidor );
	bd.AdicionarParametro("@DataExpedicao", System.Data.DbType.DateTime, -1, DataExpedicao );
	bd.AdicionarParametro("@Logradouro", System.Data.DbType.AnsiString, 100, Logradouro );
	bd.AdicionarParametro("@Bairro", System.Data.DbType.AnsiString, 80, Bairro );
	bd.AdicionarParametro("@IDCidade", System.Data.DbType.Int32, -1, IDCidade );
	bd.AdicionarParametro("@CEP", System.Data.DbType.AnsiString, 9, CEP );
	bd.AdicionarParametro("@Nascimento", System.Data.DbType.DateTime, -1, Nascimento );
	bd.AdicionarParametro("@Email", System.Data.DbType.AnsiString, 300, Email );
	bd.AdicionarParametro("@Telefone", System.Data.DbType.AnsiString, 15, Telefone );
	bd.AdicionarParametro("@Celular", System.Data.DbType.AnsiString, 15, Celular );
	bd.AdicionarParametro("@NomeConjuge", System.Data.DbType.AnsiString, 80, NomeConjuge );
	bd.AdicionarParametro("@DataNascimentoConjuge", System.Data.DbType.DateTime, -1, DataNascimentoConjuge );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDPrefeito, System.String Nome, System.String RG, System.String OrgaoExpedidor, System.Int32? IDEstadoExpedidor, System.DateTime? DataExpedicao, System.String Logradouro, System.String Bairro, System.Int32? IDCidade, System.String CEP, System.DateTime? Nascimento, System.String Email, System.String Telefone, System.String Celular, System.String NomeConjuge, System.DateTime? DataNascimentoConjuge){
      string sql = 
@"UPDATE Contratos.Prefeito SET
	Nome= @Nome
, 	RG= @RG
, 	OrgaoExpedidor= @OrgaoExpedidor
, 	IDEstadoExpedidor= @IDEstadoExpedidor
, 	DataExpedicao= @DataExpedicao
, 	Logradouro= @Logradouro
, 	Bairro= @Bairro
, 	IDCidade= @IDCidade
, 	CEP= @CEP
, 	Nascimento= @Nascimento
, 	Email= @Email
, 	Telefone= @Telefone
, 	Celular= @Celular
, 	NomeConjuge= @NomeConjuge
, 	DataNascimentoConjuge= @DataNascimentoConjuge

    WHERE IDPrefeito = @original_IDPrefeito";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 80, Nome );
	bd.AdicionarParametro("@RG", System.Data.DbType.AnsiString, 15, RG );
	bd.AdicionarParametro("@OrgaoExpedidor", System.Data.DbType.AnsiString, 10, OrgaoExpedidor );
	bd.AdicionarParametro("@IDEstadoExpedidor", System.Data.DbType.Int32, -1, IDEstadoExpedidor );
	bd.AdicionarParametro("@DataExpedicao", System.Data.DbType.DateTime, -1, DataExpedicao );
	bd.AdicionarParametro("@Logradouro", System.Data.DbType.AnsiString, 100, Logradouro );
	bd.AdicionarParametro("@Bairro", System.Data.DbType.AnsiString, 80, Bairro );
	bd.AdicionarParametro("@IDCidade", System.Data.DbType.Int32, -1, IDCidade );
	bd.AdicionarParametro("@CEP", System.Data.DbType.AnsiString, 9, CEP );
	bd.AdicionarParametro("@Nascimento", System.Data.DbType.DateTime, -1, Nascimento );
	bd.AdicionarParametro("@Email", System.Data.DbType.AnsiString, 300, Email );
	bd.AdicionarParametro("@Telefone", System.Data.DbType.AnsiString, 15, Telefone );
	bd.AdicionarParametro("@Celular", System.Data.DbType.AnsiString, 15, Celular );
	bd.AdicionarParametro("@NomeConjuge", System.Data.DbType.AnsiString, 80, NomeConjuge );
	bd.AdicionarParametro("@DataNascimentoConjuge", System.Data.DbType.DateTime, -1, DataNascimentoConjuge );

	bd.AdicionarParametro("@original_IDPrefeito", System.Data.DbType.Int32, -1, original_IDPrefeito );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDPrefeito)
	{
      string sql = 
@"DELETE FROM Contratos.Prefeito

    WHERE 
IDPrefeito = @original_IDPrefeito
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDPrefeito", System.Data.DbType.Int32, -1, original_IDPrefeito );

      bd.ExecuteNonQuery();
    }


}
}

