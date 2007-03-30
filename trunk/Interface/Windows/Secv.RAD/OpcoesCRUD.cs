using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Acontep.RAD
{
    public partial class OpcoesCRUD : UserControl
    {
        public OpcoesCRUD()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Retorna as opcoes para a classe crud
        /// </summary>
        /// <returns></returns>
        public string RetornarOpcoesCRUD()
        {
            string Opc = String.Empty;
            foreach( string opcChecked in chkOpcoesCrud.CheckedItems )
            {
                Opc += opcChecked[0].ToString();
            }
            return Opc;
        }

        public void SetarOpcoes(string opcoes)
        {
            for (int i = 0; i < chkOpcoesCrud.Items.Count; i++)
            {
                chkOpcoesCrud.SetItemChecked(i, opcoes.IndexOf(chkOpcoesCrud.Items[i].ToString()[0]) > -1);
            }
        }
    }
}
