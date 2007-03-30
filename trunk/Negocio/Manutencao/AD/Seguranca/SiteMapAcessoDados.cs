using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Acontep.Dado;

namespace Acontep.AD.Manutencao.Seguranca
{
    public class SiteMapAcessoDados
    {
        public DataTable ObterRaizSistemaPlantaParaMenu(int IDUsuario)
        {
            string sql = @"
SELECT DISTINCT Sistema.Codigo AS Codigo, 
       Sistema.Codigo as Sis_CodSis,
       Sistema.Nome, 
       '' AS Descricao, 
       Cast( '1' as bit ) AS PermiteAcessoInternet       
  FROM PERMISSAO.PERMISSAO PERMISSAO
        JOIN PERMISSAO.GRUPOUSUARIO GRUPOUSUARIO ON PERMISSAO.IDGrupoUsuario = GRUPOUSUARIO.IDGrupoUsuario
        JOIN PERMISSAO.FUNCAO FUNCAO ON FUNCAO.IDFuncao = Permissao.IDFuncao
        JOIN Permissao.Modulo Modulo on Modulo.IDModulo = Funcao.IDModulo
        JOIN PERMISSAO.SISTEMA SISTEMA ON Sistema.IDSistema = SISTEMA.IDSistema
WHERE (GRUPOUSUARIO.IDUsuario = @IDUsuario) AND 
      (FUNCAO.Ativo = 1) AND
      (FUNCAO.Menu = 1)
ORDER BY Sistema.Nome";

            BdUtil bdUtil = new BdUtil(sql);
            bdUtil.AdicionarParametro("@IDUsuario", DbType.Int32, IDUsuario);
            return bdUtil.ObterDataTable();
        }

        public DataTable ObterFolhaSistemaPlantaParaMenu(int IDUsuario)
        {
            return new DataTable();
//            string sql = @"
//SELECT DISTINCT CAST(PERMISSAO.Sis_CodSis AS VARCHAR(3)) + '|' + CAST(PERMISSAO.Plt_Planta AS CHAR(2)) AS Codigo, 
//       PERMISSAO.Sis_CodSis,
//       PERMISSAO.Plt_Planta,
//       CAST(PERMISSAO.Sis_CodSis AS VARCHAR(3)) AS CodigoRaiz,
//       CAST(PLANTA.Plt_Planta AS CHAR(2)) + '-' + Plt_Nome AS Nome, 
//       CAST(NULL AS CHAR(1)) AS Descricao, 
//       Sis_Url as Url, 
//       SISTEMA.Sis_Externo AS PermiteAcessoInternet
//  FROM PERMISSAO 
//        JOIN GRUPO_USUARIO ON PERMISSAO.IDGrupoUsuario = GRUPO_USUARIO.IDGrupoUsuario
//        JOIN USUARIO ON GRUPO_USUARIO.Usu_Login = USUARIO.Usu_Login 
//        JOIN SISTEMA ON PERMISSAO.Sis_CodSis = SISTEMA.Sis_CodSis
//        JOIN PLANTA ON PERMISSAO.Plt_Planta = PLANTA.Plt_Planta
//        JOIN FUNCAO ON FUNCAO.Sis_CodSis = PERMISSAO.Sis_CodSis AND 
//                       FUNCAO.Mod_CodMod = PERMISSAO.Mod_CodMod AND 
//                       FUNCAO.Fun_CodFun = PERMISSAO.Fun_CodFun 
//WHERE (USUARIO.IDUsuario = @IDUsuario) AND 
//      (FUNCAO.Fun_Ativo = 1) AND
//      (FUNCAO.Fun_Menu = 1)
//ORDER BY PERMISSAO.Sis_CodSis, PERMISSAO.Plt_Planta
//";

//            BdUtil bdUtil = new BdUtil(sql);

//            bdUtil.AdicionarParametro("@IDUsuario", DbType.Int32, IDUsuario);

//            return bdUtil.ObterDataTable();
        }

        public DataTable ObterRaizModuloFuncaoParaMenu(string Codigo, int IDUsuario)
        {
            string sql = @"
SELECT DISTINCT Sistema.Codigo + '|' + Modulo.Codigo AS Codigo, 
       Sistema.Codigo Sis_CodSis,
       Modulo.Codigo Mod_CodMod,
       MODULO.Nome AS Nome, 
       '' AS Descricao, 
       Cast( '1' as bit ) AS PermiteAcessoInternet

FROM PERMISSAO.PERMISSAO PERMISSAO
        JOIN PERMISSAO.GRUPOUSUARIO GRUPOUSUARIO ON PERMISSAO.IDGrupoUsuario = GRUPOUSUARIO.IDGrupoUsuario
        JOIN PERMISSAO.FUNCAO FUNCAO ON FUNCAO.IDFuncao = Permissao.IDFuncao
        JOIN Permissao.Modulo Modulo on Modulo.IDModulo = Funcao.IDModulo
        JOIN PERMISSAO.SISTEMA SISTEMA ON Sistema.IDSistema = SISTEMA.IDSistema

WHERE (GRUPOUSUARIO.IDUsuario = @IDUsuario) AND 
      (Sistema.Codigo = @Codigo) AND 
      (FUNCAO.Ativo = 1) AND
      (FUNCAO.Menu = 1)
--ORDER BY Sis_CodSis, Nome";

            BdUtil bdUtil = new BdUtil(sql);

            bdUtil.AdicionarParametro("@Codigo", DbType.AnsiString, Codigo);
            bdUtil.AdicionarParametro("@IDUsuario", DbType.Int32, IDUsuario);

            return bdUtil.ObterDataTable();
        }

        public DataTable ObterFolhaModuloFuncaoParaMenu(string Codigo, int IDUsuario)
        {
            string sql = @"SELECT DISTINCT Sistema.Codigo + '|' + Modulo.Codigo  + '|' + Funcao.Codigo AS Codigo, 
       Sistema.Codigo,
       Modulo.Codigo,
       Funcao.Codigo,
       Sistema.Codigo + '|' + Modulo.Codigo AS CodigoRaiz, 
       FUNCAO.Nome AS Nome, 
       '' AS Descricao, 
       Funcao.Url AS Url,
       Cast( '1' as bit ) AS PermiteAcessoInternet
FROM PERMISSAO.PERMISSAO PERMISSAO
        JOIN PERMISSAO.GRUPOUSUARIO GRUPOUSUARIO ON PERMISSAO.IDGrupoUsuario = GRUPOUSUARIO.IDGrupoUsuario
        JOIN PERMISSAO.FUNCAO FUNCAO ON FUNCAO.IDFuncao = Permissao.IDFuncao
        JOIN Permissao.Modulo Modulo on Modulo.IDModulo = Funcao.IDModulo
        JOIN PERMISSAO.SISTEMA SISTEMA ON Sistema.IDSistema = SISTEMA.IDSistema

WHERE (GRUPOUSUARIO.IDUsuario = @IDUsuario) AND 
      (SISTEMA.Codigo = @Codigo) AND 
      (FUNCAO.Ativo = 1) AND
      (FUNCAO.Menu = 1)
ORDER BY Sistema.Codigo, Modulo.COdigo, Funcao.Codigo";

            BdUtil bdUtil = new BdUtil(sql);
            bdUtil.AdicionarParametro("@Codigo", DbType.AnsiString, Codigo);
            bdUtil.AdicionarParametro("@IDUsuario", DbType.Int32, IDUsuario);

            return bdUtil.ObterDataTable();
        }
    }
}
