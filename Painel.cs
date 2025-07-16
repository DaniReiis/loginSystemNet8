using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoginSystemNet8
{
    public partial class Painel : Form
    {
        private Form? frmAtivo;

        public Painel()
        {
            InitializeComponent();
        }

        private void AbrirPainel(UserControl controle)
        {
            panelConteudo.Controls.Clear();
            controle.Dock = DockStyle.Fill;
            panelConteudo.Controls.Add(controle);
        }

        private void buttonCadastro_Click(object sender, EventArgs e)
        {
            AbrirPainel(new PainelCadastro());
        }

        private void buttonPerfil_Click(object sender, EventArgs e)
        {
            AbrirPainel(new PainelPerfil());
        }

        private void buttonUsuario_Click(object sender, EventArgs e)
        {
            AbrirPainel(new PainelUsuarios());
        }

        private void buttonFiltro_Click(object sender, EventArgs e)
        {
            AbrirPainel(new PainelFiltro());
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            this.Hide(); // Esconde o painel principal
            Form1 loginForm = new Form1();
            loginForm.Show(); // Reabre a tela de login
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            AbrirPainel(new PainelHome());
        }
    }
}