// Classe Gerado automaticament pela ferramenta GeradorCRUD
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Acontep.Dado;

namespace Acontep.AD.Cadastros
{
    public partial class PlantaAD
    {
        public static DataTable Obter()
        {
            string sql =
      @"SELECT Cmp_ID, Plt_Bairro, Plt_CEP, Plt_CgcPlt, Plt_Cidade, Plt_DatHor, Plt_DatInc, Plt_Endere, Plt_Estado, Plt_FaxNum, Plt_InsEst, Plt_Nome, Plt_Planta, Plt_Ramal, Plt_RazSoc, Plt_TelNum, Plt_UsuTra, Tbm_ID
    FROM PLANTA
        order by Plt_Nome";
            BdUtil bd = new BdUtil(sql);
            return bd.ObterDataTable();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable ObterPor_Cmp_ID(System.Int32 Cmp_ID)
        {
            string sql =
      @"SELECT Cmp_ID, Plt_Bairro, Plt_CEP, Plt_CgcPlt, Plt_Cidade, Plt_DatHor, Plt_DatInc, Plt_Endere, Plt_Estado, Plt_FaxNum, Plt_InsEst, Plt_Nome, Plt_Planta, Plt_Ramal, Plt_RazSoc, Plt_TelNum, Plt_UsuTra, Tbm_ID
    FROM PLANTA
    WHERE (Cmp_ID = @Cmp_ID)
        order by Plt_Nome";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Cmp_ID", DbType.Int32, Cmp_ID);
            return bd.ObterDataTable();
        }

        public static string ObterPlantaNome(byte Plt_Planta)
        {
            string sql = @"Select Plt_Nome from PLANTA
                           WHERE (PLANTA.Plt_Planta = @Plt_Planta)";
            BdUtil bdUtil = new BdUtil(sql);
            bdUtil.AdicionarParametro("@Plt_Planta", DbType.Byte, Plt_Planta);
            return bdUtil.ExecuteScalar<string>();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable Obter_ParaComboBox()
        {
            string sql = @"
SELECT Plt_Nome, Plt_Planta, (Cast(Plt_Planta as VarChar(4)) + ' - ' + Plt_Nome) as Plt_Descri
FROM PLANTA    
    order by Plt_Planta, Plt_Nome";
            BdUtil bd = new BdUtil(sql);
            return bd.ObterDataTable();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable Obter_ParaComboBoxPor_Cmp_ID(int Cmp_ID)
        {
            string sql = @"
SELECT Plt_Nome, Plt_Planta, (Cast(Plt_Planta as VarChar(4)) + ' - ' + Plt_Nome) as Plt_Descri
    FROM PLANTA
    WHERE (Cmp_ID = @Cmp_ID)
        order by Plt_Planta";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Cmp_ID", DbType.Int32, Cmp_ID);
            return bd.ObterDataTable();
        }
        /// <summary>
        /// Caso a planta não tenha Compahia (Planta 99) o retorno é 0
        /// </summary>
        /// <param name="Plt_Planta"></param>
        /// <returns></returns>
        public static int ObterCompanhiaPor_Planta(System.Byte Plt_Planta)
        {
            string sql = @"
SELECT 	
	Cmp_ID
FROM 
	PLANTA	  
 WHERE 
	Plt_Planta = @Plt_Planta";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Plt_Planta", System.Data.DbType.Double, -1, Plt_Planta);
            Object retorno = bd.ExecuteScalar();
            return (int)((retorno is DBNull || retorno == null)  ? 0 : retorno);
        }
    }
}