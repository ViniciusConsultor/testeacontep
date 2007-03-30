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
	Reponsavel, 
	Email, 
	IDBanco, 
	Conta, 
	DigConta, 
	Agencia, 
	DigAgencia, 
	NumeroFuncional, 
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
	public static DataTable ObterPor_UQ_Empreiteira (System.Int32 CNPJ){
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
	Reponsavel, 
	Email, 
	IDBanco, 
	Conta, 
	DigConta, 
	Agencia, 
	DigAgencia, 
	NumeroFuncional, 
	TipoOperacao 
FROM 
	Contratos.Empreiteira
	  
 WHERE 
	CNPJ = @CNPJ";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@CNPJ", System.Data.DbType.Int32, -1, CNPJ );
      return bd.ObterDataTable();
    }




	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.Int32 CNPJ, System.String RazaoSocial, System.String Fantasia, System.String Logradouro, System.Int32? Bairro, System.Int32? IDCidade, System.Int32? Cep, System.String Telefone, System.String Fax, System.String Reponsavel, System.String Email, System.Int32? IDBanco, System.Int32? Conta, System.String DigConta, System.Int32? Agencia, System.String DigAgencia, System.Int32? NumeroFuncional, System.String TipoOperacao){

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
 Reponsavel, 
 Email, 
 IDBanco, 
 Conta, 
 DigConta, 
 Agencia, 
 DigAgencia, 
 NumeroFuncional, 
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
@Reponsavel, 
@Email, 
@IDBanco, 
@Conta, 
@DigConta, 
@Agencia, 
@DigAgencia, 
@NumeroFuncional, 
@TipoOperacao
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@CNPJ", System.Data.DbType.Int32, -1, CNPJ );
	bd.AdicionarParametro("@RazaoSocial", System.Data.DbType.AnsiString, 200, RazaoSocial );
	bd.AdicionarParametro("@Fantasia", System.Data.DbType.AnsiString, 200, Fantasia );
	bd.AdicionarParametro("@Logradouro", System.Data.DbType.AnsiString, 200, Logradouro );
	bd.AdicionarParametro("@Bairro", System.Data.DbType.Int32, -1, Bairro );
	bd.AdicionarParametro("@IDCidade", System.Data.DbType.Int32, -1, IDCidade );
	bd.AdicionarParametro("@Cep", System.Data.DbType.Int32, -1, Cep );
	bd.AdicionarParametro("@Telefone", System.Data.DbType.AnsiString, 10, Telefone );
	bd.AdicionarParametro("@Fax", System.Data.DbType.AnsiString, 10, Fax );
	bd.AdicionarParametro("@Reponsavel", System.Data.DbType.AnsiString, 200, Reponsavel );
	bd.AdicionarParametro("@Email", System.Data.DbType.AnsiString, 200, Email );
	bd.AdicionarParametro("@IDBanco", System.Data.DbType.Int32, -1, IDBanco );
	bd.AdicionarParametro("@Conta", System.Data.DbType.Int32, -1, Conta );
	bd.AdicionarParametro("@DigConta", System.Data.DbType.AnsiString, 1, DigConta );
	bd.AdicionarParametro("@Agencia", System.Data.DbType.Int32, -1, Agencia );
	bd.AdicionarParametro("@DigAgencia", System.Data.DbType.AnsiString, 1, DigAgencia );
	bd.AdicionarParametro("@NumeroFuncional", System.Data.DbType.Int32, -1, NumeroFuncional );
	bd.AdicionarParametro("@TipoOperacao", System.Data.DbType.AnsiString, 3, TipoOperacao );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDEmpreiteira, System.Int32 CNPJ, System.String RazaoSocial, System.String Fantasia, System.String Logradouro, System.Int32? Bairro, System.Int32? IDCidade, System.Int32? Cep, System.String Telefone, System.String Fax, System.String Reponsavel, System.String Email, System.Int32? IDBanco, System.Int32? Conta, System.String DigConta, System.Int32? Agencia, System.String DigAgencia, System.Int32? NumeroFuncional, System.String TipoOperacao){
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
, 	Reponsavel= @Reponsavel
, 	Email= @Email
, 	IDBanco= @IDBanco
, 	Conta= @Conta
, 	DigConta= @DigConta
, 	Agencia= @Agencia
, 	DigAgencia= @DigAgencia
, 	NumeroFuncional= @NumeroFuncional
, 	TipoOperacao= @TipoOperacao

    WHERE IDEmpreiteira = @original_IDEmpreiteira";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@CNPJ", System.Data.DbType.Int32, -1, CNPJ );
	bd.AdicionarParametro("@RazaoSocial", System.Data.DbType.AnsiString, 200, RazaoSocial );
	bd.AdicionarParametro("@Fantasia", System.Data.DbType.AnsiString, 200, Fantasia );
	bd.AdicionarParametro("@Logradouro", System.Data.DbType.AnsiString, 200, Logradouro );
	bd.AdicionarParametro("@Bairro", System.Data.DbType.Int32, -1, Bairro );
	bd.AdicionarParametro("@IDCidade", System.Data.DbType.Int32, -1, IDCidade );
	bd.AdicionarParametro("@Cep", System.Data.DbType.Int32, -1, Cep );
	bd.AdicionarParametro("@Telefone", System.Data.DbType.AnsiString, 10, Telefone );
	bd.AdicionarParametro("@Fax", System.Data.DbType.AnsiString, 10, Fax );
	bd.AdicionarParametro("@Reponsavel", System.Data.DbType.AnsiString, 200, Reponsavel );
	bd.AdicionarParametro("@Email", System.Data.DbType.AnsiString, 200, Email );
	bd.AdicionarParametro("@IDBanco", System.Data.DbType.Int32, -1, IDBanco );
	bd.AdicionarParametro("@Conta", System.Data.DbType.Int32, -1, Conta );
	bd.AdicionarParametro("@DigConta", System.Data.DbType.AnsiString, 1, DigConta );
	bd.AdicionarParametro("@Agencia", System.Data.DbType.Int32, -1, Agencia );
	bd.AdicionarParametro("@DigAgencia", System.Data.DbType.AnsiString, 1, DigAgencia );
	bd.AdicionarParametro("@NumeroFuncional", System.Data.DbType.Int32, -1, NumeroFuncional );
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

