namespace Acontep.Ng.Manutencao.Rad {


    partial class EstruturaSaveRAD
    {
        partial class ClasseCRUDDataTable
        {
        
        }
        partial class ClasseCRUDRow
        {
            public EstruturaSaveRAD.MetodoRow ObterMetodoSelect()
            {
                foreach (MetodoRow metodo in GetMetodoRows())
                {
                    if (metodo.Opcoes.IndexOf("C") > -1)
                        return metodo;
                }
                return null;
            }
        }

        partial class VersaoDataTable
        {
        }
    }
}
