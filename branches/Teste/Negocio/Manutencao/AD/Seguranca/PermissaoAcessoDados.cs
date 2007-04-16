using System;
using System.Collections.Generic;
using System.Text;
using Acontep;
using System.Data;
using Acontep.Dado;

namespace Acontep.AD.Manutencao.Seguranca
{
    public class PermissaoAcessoDados: AcessoDados
    {
        public bool PossuiPermissao(string CodigoSistema, string CodigoModulo, string CodigoFuncao, int IDUsuario)
        {
            BdUtil bdUtil = new BdUtil(@"
SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END
  FROM Permissao.PERMISSAO PERMISSAO
      JOIN Permissao.GRUPOUSUARIO GRUPOUSUARIO ON PERMISSAO.IDGrupoUsuario = GRUPOUSUARIO.IDGrupoUsuario
      JOIN PERMISSAO.Funcao Funcao on Funcao.IDFuncao = Permissao.IDFuncao
      JOIN Permissao.Modulo Modulo on Modulo.IDModulo = Funcao.IDModulo
      JOIN Permissao.Sistema Sistema on Modulo.IDSistema = Sistema.IDSistema
 WHERE Sistema.Codigo = @CodigoSistema AND       
       Modulo.Codigo = @CodigoModulo AND
       Funcao.Codigo = @CodigoFuncao AND
       GRUPOUSUARIO.IDUsuario = @IDUsuario
");
            bdUtil.AdicionarParametro("@CodigoSistema", DbType.AnsiString, CodigoSistema);
            bdUtil.AdicionarParametro("@CodigoModulo", DbType.AnsiString, CodigoModulo);
            bdUtil.AdicionarParametro("@CodigoFuncao", DbType.AnsiString, CodigoFuncao);
            bdUtil.AdicionarParametro("@IDUsuario", DbType.Int32, IDUsuario);
            return bdUtil.ExecuteScalar<int>() == 1;
        }

        public bool PossuiPermissao(string CodigoSistema, int IDUsuario)
        {
            BdUtil bdUtil = new BdUtil(@"
SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END
  FROM Permissao.PERMISSAO PERMISSAO
      JOIN Permissao.GRUPOUSUARIO GRUPOUSUARIO ON PERMISSAO.IDGrupoUsuario = GRUPOUSUARIO.IDGrupoUsuario
      JOIN PERMISSAO.Funcao Funcao on Funcao.IDFuncao = Permissao.IDFuncao
      JOIN Permissao.Modulo Modulo on Modulo.IDModulo = Funcao.IDModulo
      JOIN Permissao.Sistema Sistema on Modulo.IDSistema = Sistema.IDSistema
      
 WHERE Sistema.Codigo  = @CodigoSistema AND
       GRUPOUSUARIO.IDUsuario = @IDUsuario
");
            bdUtil.AdicionarParametro("@CodigoSistema", DbType.AnsiString, CodigoSistema);
            bdUtil.AdicionarParametro("@IDUsuario", DbType.Int32, IDUsuario);
            return bdUtil.ExecuteScalar<int>() == 1;
        }
    }
}
