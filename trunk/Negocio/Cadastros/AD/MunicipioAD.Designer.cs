// Classe gerada automaticamente pela ferramenta GeradorCRUD
// em sáb 07/04/2007 
// Por FRANCISCONB\Chico na máquina FRANCISCONB 
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Acontep.Dado;
namespace Acontep.AD.Cadastros
{
[System.ComponentModel.DataObject(true)]
public static partial class MunicipioAD 
 {

	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_IX_Municipio (System.Int32 IDEstado, System.String Nome){
string sql = @"
SELECT 
	
	IDCidade, 
	IDEstado, 
	Nome, 
	NomeCompleto, 
	Logradouro, 
	Bairro, 
	Cep, 
	Telefone, 
	Fax, 
	Email, 
	IDPrefeito 
FROM 
	Cadastros.Municipio
	  
 WHERE 
	IDEstado = @IDEstado AND ( Nome LIKE @Nome + '%' )";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDEstado", System.Data.DbType.Int32, -1, IDEstado );
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
      return bd.ObterDataTable();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_PK_Municipio (System.Int32 IDCidade){
string sql = @"
SELECT 
	
	IDCidade, 
	IDEstado, 
	Nome, 
	NomeCompleto, 
	Logradouro, 
	Bairro, 
	Cep, 
	Telefone, 
	Fax, 
	Email, 
	IDPrefeito 
FROM 
	Cadastros.Municipio
	  
 WHERE 
	IDCidade = @IDCidade";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDCidade", System.Data.DbType.Int32, -1, IDCidade );
      return bd.ObterDataTable();
    }




	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static int Inserir (System.Int32 IDEstado, System.String Nome, System.String NomeCompleto, System.String Logradouro, System.String Bairro, System.String Cep, System.String Telefone, System.String Fax, System.String Email, System.Int32? IDPrefeito){

      string sql = @"
INSERT INTO Cadastros.Municipio
(
 IDEstado, 
 Nome, 
 NomeCompleto, 
 Logradouro, 
 Bairro, 
 Cep, 
 Telefone, 
 Fax, 
 Email, 
 IDPrefeito
)
    VALUES 
(
@IDEstado, 
@Nome, 
@NomeCompleto, 
@Logradouro, 
@Bairro, 
@Cep, 
@Telefone, 
@Fax, 
@Email, 
@IDPrefeito
); SELECT SCOPE_IDENTITY();
";
	BdUtil bd = new BdUtil(sql); 
	bd.AdicionarParametro("@IDEstado", System.Data.DbType.Int32, -1, IDEstado );
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
	bd.AdicionarParametro("@NomeCompleto", System.Data.DbType.AnsiString, 100, NomeCompleto );
	bd.AdicionarParametro("@Logradouro", System.Data.DbType.AnsiString, 200, Logradouro );
	bd.AdicionarParametro("@Bairro", System.Data.DbType.AnsiString, 200, Bairro );
	bd.AdicionarParametro("@Cep", System.Data.DbType.AnsiString, 8, Cep );
	bd.AdicionarParametro("@Telefone", System.Data.DbType.AnsiString, 15, Telefone );
	bd.AdicionarParametro("@Fax", System.Data.DbType.AnsiString, 15, Fax );
	bd.AdicionarParametro("@Email", System.Data.DbType.AnsiString, 200, Email );
	bd.AdicionarParametro("@IDPrefeito", System.Data.DbType.Int32, -1, IDPrefeito );

	 object objRetorno = bd.ExecuteScalar();
	 return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 original_IDCidade, System.Int32 IDEstado, System.String Nome, System.String NomeCompleto, System.String Logradouro, System.String Bairro, System.String Cep, System.String Telefone, System.String Fax, System.String Email, System.Int32? IDPrefeito){
      string sql = 
@"UPDATE Cadastros.Municipio SET
	IDEstado= @IDEstado
, 	Nome= @Nome
, 	NomeCompleto= @NomeCompleto
, 	Logradouro= @Logradouro
, 	Bairro= @Bairro
, 	Cep= @Cep
, 	Telefone= @Telefone
, 	Fax= @Fax
, 	Email= @Email
, 	IDPrefeito= @IDPrefeito

    WHERE IDCidade = @original_IDCidade";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@IDEstado", System.Data.DbType.Int32, -1, IDEstado );
	bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome );
	bd.AdicionarParametro("@NomeCompleto", System.Data.DbType.AnsiString, 100, NomeCompleto );
	bd.AdicionarParametro("@Logradouro", System.Data.DbType.AnsiString, 200, Logradouro );
	bd.AdicionarParametro("@Bairro", System.Data.DbType.AnsiString, 200, Bairro );
	bd.AdicionarParametro("@Cep", System.Data.DbType.AnsiString, 8, Cep );
	bd.AdicionarParametro("@Telefone", System.Data.DbType.AnsiString, 15, Telefone );
	bd.AdicionarParametro("@Fax", System.Data.DbType.AnsiString, 15, Fax );
	bd.AdicionarParametro("@Email", System.Data.DbType.AnsiString, 200, Email );
	bd.AdicionarParametro("@IDPrefeito", System.Data.DbType.Int32, -1, IDPrefeito );

	bd.AdicionarParametro("@original_IDCidade", System.Data.DbType.Int32, -1, original_IDCidade );

      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 original_IDCidade)
	{
      string sql = 
@"DELETE FROM Cadastros.Municipio

    WHERE 
IDCidade = @original_IDCidade
";	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@original_IDCidade", System.Data.DbType.Int32, -1, original_IDCidade );

      bd.ExecuteNonQuery();
    }


}
}

