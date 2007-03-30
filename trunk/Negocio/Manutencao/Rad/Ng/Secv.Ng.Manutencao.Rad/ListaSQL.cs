using System;
using System.IO;
namespace Acontep.Ng.Manutencao.Rad {


    partial class ListaSQL
    {
        public static ListaSQL.ProjetoRow ObterProjetoAtual()
        {
            ListaSQL listaSQL = EstruturaSaveRadUtil<ListaSQL>.ConteudoClasse;
            if (listaSQL.Projeto.Count == 0)
            {
                listaSQL.Projeto.AddProjetoRow(
                    "",
                    "",                    
                    "Novo Projeto",
                    "",
                    Guid.NewGuid(),
                    30);
            }
            if (listaSQL.Projeto[0].IsTimeOutNull())
            {
                listaSQL.Projeto[0].TimeOut = 30;
            }
            return listaSQL.Projeto[0];
        }
    }
}
