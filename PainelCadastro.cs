using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginSystemNet8
{
    public partial class PainelCadastro : UserControl
    {
        public PainelCadastro()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.Resize += panel1_Resize;
        }

        private void CentralizarLabel()
        {
            if (label1 != null && panel1 != null)
            {
                label1.Left = (panel1.Width - label1.Width) / 2;
                label1.Top = (panel1.Height - label1.Height) / 2;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CentralizarLabel();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            CentralizarLabel();
        }
    }
}
