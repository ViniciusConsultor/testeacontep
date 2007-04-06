// Classe gerada automaticamente pela ferramenta GeradorCRUD
// em sex 06/04/2007 
// Por FRANCISCONB\Chico na máquina FRANCISCONB 
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Acontep.Dado;
namespace Acontep.AD.Contratos
{
[System.ComponentModel.DataObject(true)]
public static partial class EmpreiteiraAD 
 {

	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_PK_Empreiteira (System.Int32 IDEmpreiteira){
string sql = @"
SELECT 
	
	IDEmpreiteira, 
	CNPJ, 
	RazaoSocial, 
	Fantasia, 
	Logradouro, 
	Bairro, 
	IDCidade, 
	Cep, 
	Telefone, 
	Fax, 
	Responsavel, 
	Email, 
	IDBanco, 
	Conta, 
	Agencia, 
	TipoOperacao 
FROM 
	Contratos.Empreiteira
	  
 WHERE 
	IDEmpreiteira = @IDEmpreiteira";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDEmpreiteira", System.Data.DbType.Int32, -1, IDEmpreiteira );
      return bd.ObterDataTable();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_UQ_Empreiteira (System.String CNPJ){
string sql = @"
SELECT 
	
	IDEmpreiteira, 
	CNPJ, 
	RazaoSocial, 
	Fantasia, 
	Logradouro, 
	Bairro, 
	IDCidade, 
	Cep, 
	Telefone, 
	Fax, 
	Responsavel, 
	Email, 
	IDBanco, 
	Conta, 
	Agencia, 
	TipoOperacao 
FROM 
	Contratos.Empreiteira
	  
 WHERE 
	( CNPJ LIKE @CNPJ + '%' )";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@CNPJ", System.Data.DbType.AnsiString, 14, CNPJ );
      return bd.ObterDataTable();
    }




	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.String CNPJ, System.String RazaoSocial, System.String Fantasia, System.String Logradouro, System.String Bairro, System.Int32? IDCidade, System.Int32? Cep, System.String Telefone, System.String Fax, System.String Responsavel, System.String Email, System.String IDBanco, System.String Conta, System.String Agencia, System.String TipoOperacao){

      string sql = @"
INSERT INTO Contratos.Empreiteira
(
 CNPJ, 
 RazaoSocial, 
 Fantasia, 
 Logradouro, 
 Bairro, 
 IDCidade, 
 Cep, 
 Telefone, 
 Fax, 
 Responsavel, 
 Email, 
 IDBanco, 
 Conta, 
 Agencia, 
 TipoOperacao
)
    VALUES 
(
@CNPJ, 
@RazaoSocial, 
@Fantasia, 
@Logradouro, 
@Bairro, 
@IDCidade, 
@Cep, 
@Telefone, 
@Fax, 
@Responsavel, 
@Email, 
@IDBanco, 
@Conta, 
@Agencia, 
@TipoOperacao
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@CNPJ", System.Data.DbType.AnsiString, 14, CNPJ );
	bd.AdicionarParametro("@RazaoSocial", System.Data.DbType.AnsiString, 200, RazaoSocial );
	bd.AdicionarParametro("@Fantasia", System.Data.DbType.AnsiString, 200, Fantasia );
	bd.AdicionarParametro("@Logradouro", System.Data.DbType.AnsiString, 200, Logradouro );
	bd.AdicionarParametro("@Bairro", System.Data.DbType.AnsiString, 100, Bairro );
	bd.AdicionarParametro("@IDCidade", System.Data.DbType.Int32, -1, IDCidade );
	bd.AdicionarParametro("@Cep", System.Data.DbType.Int32, -1, Cep );
	bd.AdicionarParametro("@Telefone", System.Data.DbType.AnsiString, 15, Telefone );
	bd.AdicionarParametro("@Fax", System.Data.DbType.AnsiString, 15, Fax );
	bd.AdicionarParametro("@Responsavel", System.Data.DbType.AnsiString, 200, Responsavel );
	bd.AdicionarParametro("@Email", System.Data.DbType.AnsiString, 200, Email );
	bd.AdicionarParametro("@IDBanco", System.Data.DbType.AnsiString, 3, IDBanco );
	bd.AdicionarParametro("@Conta", System.Data.DbType.AnsiString, 50, Conta );
	bd.AdicionarParametro("@Agencia", System.Data.DbType.AnsiString, 50, Agencia );
	bd.AdicionarParametro("@TipoOperacao", System.Data.DbType.AnsiString, 3, TipoOperacao );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDEmpreiteira, System.String CNPJ, System.String RazaoSocial, System.String Fantasia, System.String Logradouro, System.String Bairro, System.Int32? IDCidade, System.Int32? Cep, System.String Telefone, System.String Fax, System.String Responsavel, System.String Email, System.String IDBanco, System.String Conta, System.String Agencia, System.String TipoOperacao){
      string sql = 
@"UPDATE Contratos.Empreiteira SET
	CNPJ= @CNPJ
, 	RazaoSocial= @RazaoSocial
, 	Fantasia= @Fantasia
, 	Logradouro= @Logradouro
, 	Bairro= @Bairro
, 	IDCidade= @IDCidade
, 	Cep= @Cep
, 	Telefone= @Telefone
, 	Fax= @Fax
, 	Responsavel= @Responsavel
, 	Email= @Email
, 	IDBanco= @IDBanco
, 	Conta= @Conta
, 	Agencia= @Agencia
, 	TipoOperacao= @TipoOperacao

    WHERE IDEmpreiteira = @original_IDEmpreiteira";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@CNPJ", System.Data.DbType.AnsiString, 14, CNPJ );
	bd.AdicionarParametro("@RazaoSocial", System.Data.DbType.AnsiString, 200, RazaoSocial );
	bd.AdicionarParametro("@Fantasia", System.Data.DbType.AnsiString, 200, Fantasia );
	bd.AdicionarParametro("@Logradouro", System.Data.DbType.AnsiString, 200, Logradouro );
	bd.AdicionarParametro("@Bairro", System.Data.DbType.AnsiString, 100, Bairro );
	bd.AdicionarParametro("@IDCidade", System.Data.DbType.Int32, -1, IDCidade );
	bd.AdicionarParametro("@Cep", System.Data.DbType.Int32, -1, Cep );
	bd.AdicionarParametro("@Telefone", System.Data.DbType.AnsiString, 15, Telefone );
	bd.AdicionarParametro("@Fax", System.Data.DbType.AnsiString, 15, Fax );
	bd.AdicionarParametro("@Responsavel", System.Data.DbType.AnsiString, 200, Responsavel );
	bd.AdicionarParametro("@Email", System.Data.DbType.AnsiString, 200, Email );
	bd.AdicionarParametro("@IDBanco", System.Data.DbType.AnsiString, 3, IDBanco );
	bd.AdicionarParametro("@Conta", System.Data.DbType.AnsiString, 50, Conta );
	bd.AdicionarParametro("@Agencia", System.Data.DbType.AnsiString, 50, Agencia );
	bd.AdicionarParametro("@TipoOperacao", System.Data.DbType.AnsiString, 3, TipoOperacao );

	bd.AdicionarParametro("@original_IDEmpreiteira", System.Data.DbType.Int32, -1, original_IDEmpreiteira );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDEmpreiteira)
	{
      string sql = 
@"DELETE FROM Contratos.Empreiteira

    WHERE 
IDEmpreiteira = @original_IDEmpreiteira
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDEmpreiteira", System.Data.DbType.Int32, -1, original_IDEmpreiteira );

      bd.ExecuteNonQuery();
    }


}
}

