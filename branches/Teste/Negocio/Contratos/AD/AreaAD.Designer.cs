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
    public static partial class AreaAD
    {

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable ObterPor_PK_Area(System.Int32 IDArea)
        {
            string sql = @"
SELECT 
	
	IDArea, 
	Nome 
FROM 
	Contratos.Area
	  
 WHERE 
	IDArea = @IDArea";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IDArea", System.Data.DbType.Int32, -1, IDArea);
            return bd.ObterDataTable();
        }


        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable ObterPor_UQ_Area(System.String Nome)
        {
            string sql = @"
SELECT 
	
	IDArea, 
	Nome 
FROM 
	Contratos.Area
	  
 WHERE 
	( Nome LIKE @Nome + '%' )";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome);
            return bd.ObterDataTable();
        }




        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert)]
        public static int Inserir(System.String Nome)
        {

            string sql = @"
INSERT INTO Contratos.Area
(
 Nome
)
    VALUES 
(
@Nome
); SELECT SCOPE_IDENTITY();
";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome);

            object objRetorno = bd.ExecuteScalar();
            return (objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);
        }


        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        public static void Alterar(System.Int32 original_IDArea, System.String Nome)
        {
            string sql =
      @"UPDATE Contratos.Area SET
	Nome= @Nome

    WHERE IDArea = @original_IDArea";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome);

            bd.AdicionarParametro("@original_IDArea", System.Data.DbType.Int32, -1, original_IDArea);

            bd.ExecuteNonQuery();
        }


        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public static void Apagar(System.Int32 original_IDArea)
        {
            string sql =
      @"DELETE FROM Contratos.Area

    WHERE 
IDArea = @original_IDArea
"; BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@original_IDArea", System.Data.DbType.Int32, -1, original_IDArea);

            bd.ExecuteNonQuery();
        }


    }
}

