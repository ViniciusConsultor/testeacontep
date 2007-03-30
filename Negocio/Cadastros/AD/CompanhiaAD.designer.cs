// Classe gerada automaticamente pela ferramenta GeradorCRUD
// em qui 03/08/2006 
// Por CENTRAL\thiago na máquina THIAGOPC 
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Acontep.Dado;
namespace Acontep.AD.Cadastros
{
[System.ComponentModel.DataObject(true)]
public static partial class CompanhiaAD
 {
    
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
    public static DataTable Obter()
    {
        string sql = @"
SELECT 
	
	ID, 
	Cmp_Nome, 
	Cmp_DatInc, 
	Cmp_DatHor, 
	Cmp_UsuTra 
FROM 
	COMPANHIA"; 
        BdUtil bd = new BdUtil(sql);        
        return bd.ObterDataTable();
    }

	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_PK_COMPANHIA (System.Int32 ID){
string sql = @"
SELECT 
	
	ID, 
	Cmp_Nome, 
	Cmp_DatInc, 
	Cmp_DatHor, 
	Cmp_UsuTra 
FROM 
	COMPANHIA
	  
 WHERE 
	ID = @ID";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@ID", System.Data.DbType.Int32, -1, ID );
      return bd.ObterDataTable();
    }




	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static void Inserir (System.Int32 ID, System.String Cmp_Nome, System.DateTime Cmp_DatInc, System.DateTime Cmp_DatHor, System.String Cmp_UsuTra){

      string sql = @"
INSERT INTO COMPANHIA
(
 Cmp_Nome, 
 Cmp_DatInc, 
 Cmp_DatHor, 
 Cmp_UsuTra
)
    VALUES 
(
@Cmp_Nome, 
@Cmp_DatInc, 
@Cmp_DatHor, 
@Cmp_UsuTra
)
";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Cmp_Nome", System.Data.DbType.String, 40, Cmp_Nome );
	bd.AdicionarParametro("@Cmp_DatInc", System.Data.DbType.DateTime, -1, Cmp_DatInc );
	bd.AdicionarParametro("@Cmp_DatHor", System.Data.DbType.DateTime, -1, Cmp_DatHor );
	bd.AdicionarParametro("@Cmp_UsuTra", System.Data.DbType.String, 15, Cmp_UsuTra );

	bd.ExecuteNonQuery();
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Int32 ID, System.String Cmp_Nome, System.DateTime Cmp_DatInc, System.DateTime Cmp_DatHor, System.String Cmp_UsuTra){
      string sql = 
@"UPDATE COMPANHIA SET
	Cmp_Nome= @Cmp_Nome
, 	Cmp_DatInc= @Cmp_DatInc
, 	Cmp_DatHor= @Cmp_DatHor
, 	Cmp_UsuTra= @Cmp_UsuTra

    WHERE ID = @ID";
      BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@ID", System.Data.DbType.Int32, -1, ID );
      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Int32 ID)
	{
      string sql = 
@"DELETE FROM COMPANHIA
    WHERE ID = @ID AND Cmp_Nome = @Cmp_Nome AND Cmp_DatInc = @Cmp_DatInc AND Cmp_DatHor = @Cmp_DatHor AND Cmp_UsuTra = @Cmp_UsuTra";      BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@ID", System.Data.DbType.Int32, -1, ID );	
      bd.ExecuteNonQuery();
    }


}
}

