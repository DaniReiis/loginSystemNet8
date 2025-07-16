using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LoginSystemNet8
{
    public partial class Cadastro : Form
    {
        public Cadastro()
        {
            InitializeComponent();
            ConnectKeyEvents(); // Adicione esta linha
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await ExecuteCadastro();
        }

        private async Task ExecuteCadastro()
        {

            // Validação dos campos
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Por favor, digite o nome de usuário.", "Campo Obrigatório",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Por favor, digite seu email.", "Campo Obrigatório",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Por favor, digite a senha.", "Campo Obrigatório",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }


            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Por favor, digite sua senha novamente.", "Campo Obrigatório",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("As senhas não coincidem.", "Erro de Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return;
            }

            // Desabilita o botão durante o login
            button1.Enabled = false;
            button1.Text = "Cadastrando...";
            button1.BackColor = Color.Gray;


            try
            {
                // Mostra que está tentando cadastrar
                this.Text = "Login System - Cadastrando...";

                // Tenta registrar o usuário no banco de dados
                bool success = await DatabaseConnection.RegisterUserAsync(
                    textBox1.Text.Trim(),
                    textBox2.Text.Trim(),
                    textBox3.Text
                );

                if (success)
                {
                    MessageBox.Show("Cadastro realizado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpa os campos
                    ClearFields();

                    // Fecha o formulário após o cadastro
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuário ou email já existe. Tente com dados diferentes.",
                        "Erro no Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro durante o cadastro de usuario:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Reabilita o botão
                button1.Enabled = true;
                button1.Text = "Login";
                button1.BackColor = Color.Maroon;
            }
        }

        private void ClearFields()
        {
            textBox1?.Clear();
            textBox2?.Clear();
            textBox3?.Clear();
            textBox4?.Clear();
        }

        private void ConnectKeyEvents()
        {
            // Conectar eventos de navegação com Enter
            textBox1.KeyPress += textBox1_KeyPress;
            textBox2.KeyPress += textBox2_KeyPress;
            textBox3.KeyPress += textBox3_KeyPress;
            textBox4.KeyPress += textBox4_KeyPress;
        }

        // Eventos de navegação
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                textBox3.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                textBox4.Focus();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                _ = ExecuteCadastro();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var voltarLogin = new Form1();
            this.Hide(); // Esconde o form atual
            voltarLogin.Show();
            voltarLogin.FormClosed += (s, args) =>
            {
                this.Show();
                this.BringToFront();
            };
        }
    }
}
