// Classe Gerado automaticament pela ferramenta GeradorCRUD
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Acontep.Dado;

namespace Acontep.AD.Cadastros
{

    
[System.ComponentModel.DataObject(true)]
    public partial class PlantaAD
 {

	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]
	public static DataTable ObterPor_PK_Planta (System.Byte Plt_Planta){
string sql = @"
SELECT 
	
	Plt_Planta, 
	Plt_RazSoc, 
	Plt_CgcPlt, 
	Plt_InsEst, 
	Plt_Endere, 
	Plt_Bairro, 
	Plt_CEP, 
	Plt_Cidade, 
	Plt_Estado, 
	Plt_TelNum, 
	Plt_Ramal, 
	Plt_FaxNum, 
	Plt_DatInc, 
	Plt_DatHor, 
	Plt_UsuTra, 
	Cmp_ID, 
	Plt_Nome, 
	Tbm_ID 
FROM 
	PLANTA
	  
 WHERE 
	Plt_Planta = @Plt_Planta";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Plt_Planta", System.Data.DbType.Double, -1, Plt_Planta );
      return bd.ObterDataTable();
    }




	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]
	public static void Inserir (System.Byte Plt_Planta, System.String Plt_RazSoc, System.String Plt_CgcPlt, System.String Plt_InsEst, System.String Plt_Endere, System.String Plt_Bairro, System.String Plt_CEP, System.String Plt_Cidade, System.String Plt_Estado, System.String Plt_TelNum, System.String Plt_Ramal, System.String Plt_FaxNum, System.DateTime Plt_DatInc, System.DateTime Plt_DatHor, System.String Plt_UsuTra, System.Int32? Cmp_ID, System.String Plt_Nome, System.Int32? Tbm_ID){

      string sql = @"
INSERT INTO PLANTA
(
 Plt_Planta, 
 Plt_RazSoc, 
 Plt_CgcPlt, 
 Plt_InsEst, 
 Plt_Endere, 
 Plt_Bairro, 
 Plt_CEP, 
 Plt_Cidade, 
 Plt_Estado, 
 Plt_TelNum, 
 Plt_Ramal, 
 Plt_FaxNum, 
 Plt_DatInc, 
 Plt_DatHor, 
 Plt_UsuTra, 
 Cmp_ID, 
 Plt_Nome, 
 Tbm_ID
)
    VALUES 
(
@Plt_Planta, 
@Plt_RazSoc, 
@Plt_CgcPlt, 
@Plt_InsEst, 
@Plt_Endere, 
@Plt_Bairro, 
@Plt_CEP, 
@Plt_Cidade, 
@Plt_Estado, 
@Plt_TelNum, 
@Plt_Ramal, 
@Plt_FaxNum, 
@Plt_DatInc, 
@Plt_DatHor, 
@Plt_UsuTra, 
@Cmp_ID, 
@Plt_Nome, 
@Tbm_ID
)
";
	BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Plt_Planta", System.Data.DbType.Double, -1, Plt_Planta );
	bd.AdicionarParametro("@Plt_RazSoc", System.Data.DbType.String, 60, Plt_RazSoc );
	bd.AdicionarParametro("@Plt_CgcPlt", System.Data.DbType.String, 14, Plt_CgcPlt );
	bd.AdicionarParametro("@Plt_InsEst", System.Data.DbType.String, 20, Plt_InsEst );
	bd.AdicionarParametro("@Plt_Endere", System.Data.DbType.String, 60, Plt_Endere );
	bd.AdicionarParametro("@Plt_Bairro", System.Data.DbType.String, 40, Plt_Bairro );
	bd.AdicionarParametro("@Plt_CEP", System.Data.DbType.String, 8, Plt_CEP );
	bd.AdicionarParametro("@Plt_Cidade", System.Data.DbType.String, 40, Plt_Cidade );
	bd.AdicionarParametro("@Plt_Estado", System.Data.DbType.String, 2, Plt_Estado );
	bd.AdicionarParametro("@Plt_TelNum", System.Data.DbType.String, 15, Plt_TelNum );
	bd.AdicionarParametro("@Plt_Ramal", System.Data.DbType.String, 5, Plt_Ramal );
	bd.AdicionarParametro("@Plt_FaxNum", System.Data.DbType.String, 15, Plt_FaxNum );
	bd.AdicionarParametro("@Plt_DatInc", System.Data.DbType.DateTime, -1, Plt_DatInc );
	bd.AdicionarParametro("@Plt_DatHor", System.Data.DbType.DateTime, -1, Plt_DatHor );
	bd.AdicionarParametro("@Plt_UsuTra", System.Data.DbType.String, 15, Plt_UsuTra );
	bd.AdicionarParametro("@Cmp_ID", System.Data.DbType.Int32, -1, Cmp_ID );
	bd.AdicionarParametro("@Plt_Nome", System.Data.DbType.String, 17, Plt_Nome );
	bd.AdicionarParametro("@Tbm_ID", System.Data.DbType.Int32, -1, Tbm_ID );

	bd.ExecuteNonQuery();
	}


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]
public static void Alterar (System.Byte Plt_Planta, System.String Plt_RazSoc, System.String Plt_CgcPlt, System.String Plt_InsEst, System.String Plt_Endere, System.String Plt_Bairro, System.String Plt_CEP, System.String Plt_Cidade, System.String Plt_Estado, System.String Plt_TelNum, System.String Plt_Ramal, System.String Plt_FaxNum, System.DateTime Plt_DatInc, System.DateTime Plt_DatHor, System.String Plt_UsuTra, System.Int32? Cmp_ID, System.String Plt_Nome, System.Int32? Tbm_ID){
      string sql = 
@"UPDATE PLANTA SET
	Plt_RazSoc= @Plt_RazSoc
, 	Plt_CgcPlt= @Plt_CgcPlt
, 	Plt_InsEst= @Plt_InsEst
, 	Plt_Endere= @Plt_Endere
, 	Plt_Bairro= @Plt_Bairro
, 	Plt_CEP= @Plt_CEP
, 	Plt_Cidade= @Plt_Cidade
, 	Plt_Estado= @Plt_Estado
, 	Plt_TelNum= @Plt_TelNum
, 	Plt_Ramal= @Plt_Ramal
, 	Plt_FaxNum= @Plt_FaxNum
, 	Plt_DatInc= @Plt_DatInc
, 	Plt_DatHor= @Plt_DatHor
, 	Plt_UsuTra= @Plt_UsuTra
, 	Cmp_ID= @Cmp_ID
, 	Plt_Nome= @Plt_Nome
, 	Tbm_ID= @Tbm_ID

    WHERE Plt_Planta = @Plt_Planta";
      BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Plt_Planta", System.Data.DbType.Double, -1, Plt_Planta );
      bd.ExecuteNonQuery();
    }


	[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]
    public static void Apagar (System.Byte Plt_Planta)
	{
      string sql = 
@"DELETE FROM PLANTA
    WHERE Plt_Planta = @Plt_Planta AND Plt_RazSoc = @Plt_RazSoc AND Plt_CgcPlt = @Plt_CgcPlt AND Plt_InsEst = @Plt_InsEst AND Plt_Endere = @Plt_Endere AND Plt_Bairro = @Plt_Bairro AND Plt_CEP = @Plt_CEP AND Plt_Cidade = @Plt_Cidade AND Plt_Estado = @Plt_Estado AND Plt_TelNum = @Plt_TelNum AND Plt_Ramal = @Plt_Ramal AND Plt_FaxNum = @Plt_FaxNum AND Plt_DatInc = @Plt_DatInc AND Plt_DatHor = @Plt_DatHor AND Plt_UsuTra = @Plt_UsuTra AND Cmp_ID = @Cmp_ID AND Plt_Nome = @Plt_Nome AND Tbm_ID = @Tbm_ID";      BdUtil bd = new BdUtil(sql);
	bd.AdicionarParametro("@Plt_Planta", System.Data.DbType.Double, -1, Plt_Planta );
      bd.ExecuteNonQuery();
    }
}


//    public partial class PlantaAD
//    {

//        public static void Inserir(System.Int32 Cmp_ID, System.String Plt_Bairro, System.String Plt_CEP, System.String Plt_CgcPlt, System.String Plt_Cidade, System.DateTime Plt_DatHor, System.DateTime Plt_DatInc, System.String Plt_Endere, System.String Plt_Estado, System.String Plt_FaxNum, System.String Plt_InsEst, System.String Plt_Nome, System.Byte Plt_Planta, System.String Plt_Ramal, System.String Plt_RazSoc, System.String Plt_TelNum, System.String Plt_UsuTra, System.Int32 Tbm_ID)
//        {

//            // VALIDAÇÃO DOS CAMPOS
//            //validarCampos(Cmp_ID, Plt_Bairro, Plt_CEP, Plt_CgcPlt, Plt_Cidade, Plt_DatHor, Plt_DatInc, Plt_Endere, Plt_Estado, Plt_FaxNum, Plt_InsEst, Plt_Nome, Plt_Planta, Plt_Ramal, Plt_RazSoc, Plt_TelNum, Plt_UsuTra, Tbm_ID);

//            string sql =
//      @"INSERT INTO PLANTA(Cmp_ID, Plt_Bairro, Plt_CEP, Plt_CgcPlt, Plt_Cidade, Plt_DatHor, Plt_DatInc, Plt_Endere, Plt_Estado, Plt_FaxNum, Plt_InsEst, Plt_Nome, Plt_Planta, Plt_Ramal, Plt_RazSoc, Plt_TelNum, Plt_UsuTra, Tbm_ID)
//    VALUES (@Cmp_ID, @Plt_Bairro, @Plt_CEP, @Plt_CgcPlt, @Plt_Cidade, @Plt_DatHor, @Plt_DatInc, @Plt_Endere, @Plt_Estado, @Plt_FaxNum, @Plt_InsEst, @Plt_Nome, @Plt_Planta, @Plt_Ramal, @Plt_RazSoc, @Plt_TelNum, @Plt_UsuTra, @Tbm_ID)";
//            BdUtil bd = new BdUtil(sql);
//            bd.AdicionarParametro("@Cmp_ID", DbType.Int32, -1, Cmp_ID);
//            bd.AdicionarParametro("@Plt_Bairro", DbType.String, 40, Plt_Bairro);
//            bd.AdicionarParametro("@Plt_CEP", DbType.String, 8, Plt_CEP);
//            bd.AdicionarParametro("@Plt_CgcPlt", DbType.String, 14, Plt_CgcPlt);
//            bd.AdicionarParametro("@Plt_Cidade", DbType.String, 40, Plt_Cidade);
//            bd.AdicionarParametro("@Plt_DatHor", DbType.DateTime, -1, Plt_DatHor);
//            bd.AdicionarParametro("@Plt_DatInc", DbType.DateTime, -1, Plt_DatInc);
//            bd.AdicionarParametro("@Plt_Endere", DbType.String, 60, Plt_Endere);
//            bd.AdicionarParametro("@Plt_Estado", DbType.String, 2, Plt_Estado);
//            bd.AdicionarParametro("@Plt_FaxNum", DbType.String, 15, Plt_FaxNum);
//            bd.AdicionarParametro("@Plt_InsEst", DbType.String, 20, Plt_InsEst);
//            bd.AdicionarParametro("@Plt_Nome", DbType.String, 17, Plt_Nome);
//            bd.AdicionarParametro("@Plt_Planta", DbType.Byte, -1, Plt_Planta);
//            bd.AdicionarParametro("@Plt_Ramal", DbType.String, 5, Plt_Ramal);
//            bd.AdicionarParametro("@Plt_RazSoc", DbType.String, 60, Plt_RazSoc);
//            bd.AdicionarParametro("@Plt_TelNum", DbType.String, 15, Plt_TelNum);
//            bd.AdicionarParametro("@Plt_UsuTra", DbType.String, 15, Plt_UsuTra);
//            bd.AdicionarParametro("@Tbm_ID", DbType.Int32, -1, Tbm_ID);
//            bd.ExecuteNonQuery();
//        }


//        public static DataTable Obter(System.Byte Plt_Planta)
//        {
//            string sql =
//      @"SELECT Cmp_ID, Plt_Bairro, Plt_CEP, Plt_CgcPlt, Plt_Cidade, Plt_DatHor, Plt_DatInc, Plt_Endere, Plt_Estado, Plt_FaxNum, Plt_InsEst, Plt_Nome, Plt_Planta, Plt_Ramal, Plt_RazSoc, Plt_TelNum, Plt_UsuTra, Tbm_ID
//    FROM PLANTA
//    WHERE Plt_Planta = @Plt_Planta";
//            BdUtil bd = new BdUtil(sql);
//            bd.AdicionarParametro("@Plt_Planta", DbType.Byte, -1, Plt_Planta);
//            return bd.ObterDataTable();
//        }

        

//        public static void Alterar(System.Int32 Cmp_ID, System.String Plt_Bairro, System.String Plt_CEP, System.String Plt_CgcPlt, System.String Plt_Cidade, System.DateTime Plt_DatHor, System.DateTime Plt_DatInc, System.String Plt_Endere, System.String Plt_Estado, System.String Plt_FaxNum, System.String Plt_InsEst, System.String Plt_Nome, System.Byte Plt_Planta, System.String Plt_Ramal, System.String Plt_RazSoc, System.String Plt_TelNum, System.String Plt_UsuTra, System.Int32 Tbm_ID)
//        {

//            // VALIDAÇÃO DOS CAMPOS
//            //validarCampos(Cmp_ID, Plt_Bairro, Plt_CEP, Plt_CgcPlt, Plt_Cidade, Plt_DatHor, Plt_DatInc, Plt_Endere, Plt_Estado, Plt_FaxNum, Plt_InsEst, Plt_Nome, Plt_Planta, Plt_Ramal, Plt_RazSoc, Plt_TelNum, Plt_UsuTra, Tbm_ID);

//            string sql =
//      @"UPDATE PLANTA SET
//    Cmp_ID = @Cmp_ID,
//    Plt_Bairro = @Plt_Bairro,
//    Plt_CEP = @Plt_CEP,
//    Plt_CgcPlt = @Plt_CgcPlt,
//    Plt_Cidade = @Plt_Cidade,
//    Plt_DatHor = @Plt_DatHor,
//    Plt_DatInc = @Plt_DatInc,
//    Plt_Endere = @Plt_Endere,
//    Plt_Estado = @Plt_Estado,
//    Plt_FaxNum = @Plt_FaxNum,
//    Plt_InsEst = @Plt_InsEst,
//    Plt_Nome = @Plt_Nome,
//    Plt_Ramal = @Plt_Ramal,
//    Plt_RazSoc = @Plt_RazSoc,
//    Plt_TelNum = @Plt_TelNum,
//    Plt_UsuTra = @Plt_UsuTra,
//    Tbm_ID = @Tbm_ID
//    WHERE Plt_Planta = @Plt_Planta";
//            BdUtil bd = new BdUtil(sql);
//            bd.AdicionarParametro("@Cmp_ID", DbType.Int32, -1, Cmp_ID);
//            bd.AdicionarParametro("@Plt_Bairro", DbType.String, 40, Plt_Bairro);
//            bd.AdicionarParametro("@Plt_CEP", DbType.String, 8, Plt_CEP);
//            bd.AdicionarParametro("@Plt_CgcPlt", DbType.String, 14, Plt_CgcPlt);
//            bd.AdicionarParametro("@Plt_Cidade", DbType.String, 40, Plt_Cidade);
//            bd.AdicionarParametro("@Plt_DatHor", DbType.DateTime, -1, Plt_DatHor);
//            bd.AdicionarParametro("@Plt_DatInc", DbType.DateTime, -1, Plt_DatInc);
//            bd.AdicionarParametro("@Plt_Endere", DbType.String, 60, Plt_Endere);
//            bd.AdicionarParametro("@Plt_Estado", DbType.String, 2, Plt_Estado);
//            bd.AdicionarParametro("@Plt_FaxNum", DbType.String, 15, Plt_FaxNum);
//            bd.AdicionarParametro("@Plt_InsEst", DbType.String, 20, Plt_InsEst);
//            bd.AdicionarParametro("@Plt_Nome", DbType.String, 17, Plt_Nome);
//            bd.AdicionarParametro("@Plt_Planta", DbType.Byte, -1, Plt_Planta);
//            bd.AdicionarParametro("@Plt_Ramal", DbType.String, 5, Plt_Ramal);
//            bd.AdicionarParametro("@Plt_RazSoc", DbType.String, 60, Plt_RazSoc);
//            bd.AdicionarParametro("@Plt_TelNum", DbType.String, 15, Plt_TelNum);
//            bd.AdicionarParametro("@Plt_UsuTra", DbType.String, 15, Plt_UsuTra);
//            bd.AdicionarParametro("@Tbm_ID", DbType.Int32, -1, Tbm_ID);
//            bd.ExecuteNonQuery();
//        }


//        public static void Apagar(System.Byte Plt_Planta)
//        {
//            string sql =
//      @"DELETE FROM PLANTA
//    WHERE Plt_Planta = @Plt_Planta)";
//            BdUtil bd = new BdUtil(sql);
//            bd.AdicionarParametro("@Plt_Planta", DbType.Byte, -1, Plt_Planta);
//            bd.ExecuteNonQuery();
//        }


//        //MÉTODOS DE PESQUISA POR ÍNDICES


//        public static DataTable ObterPor_PK_Planta(System.Byte Plt_Planta)
//        {
//            string sql =
//      @"SELECT Cmp_ID, Plt_Bairro, Plt_CEP, Plt_CgcPlt, Plt_Cidade, Plt_DatHor, Plt_DatInc, Plt_Endere, Plt_Estado, Plt_FaxNum, Plt_InsEst, Plt_Nome, Plt_Planta, Plt_Ramal, Plt_RazSoc, Plt_TelNum, Plt_UsuTra, Tbm_ID
//    FROM PLANTA
//    WHERE Plt_Planta = @Plt_Planta";
//            BdUtil bd = new BdUtil(sql);
//            bd.AdicionarParametro("@Plt_Planta", DbType.Byte, -1, Plt_Planta);
//            return bd.ObterDataTable();
//        }

//    }
}