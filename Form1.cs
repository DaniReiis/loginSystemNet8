using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginSystemNet8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento executado quando o form é carregado
        /// </summary>
        private async void Form1_Load(object sender, EventArgs e)
        {
            await TestDatabaseConnection();
        }

        /// <summary>
        /// Testa a conexão com o banco de dados
        /// </summary>
        private async Task TestDatabaseConnection()
        {
            try
            {
                // Mostra que está testando
                this.Text = "Login System - Testando conexão...";

                bool isConnected = await DatabaseConnection.TestConnectionAsync();

                if (isConnected)
                {
                    this.Text = "Login System - Conectado ✓";
                    // Não mostra MessageBox aqui para não incomodar o usuário
                }
                else
                {
                    this.Text = "Login System - Erro de Conexão ✗";
                    MessageBox.Show("Não foi possível conectar ao banco de dados!\nVerifique sua conexão com a internet.",
                        "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                this.Text = "Login System - Erro ✗";
                MessageBox.Show($"Erro ao testar conexão: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento do botão de login
        /// </summary>
        private async void button1_Click(object sender, EventArgs e)
        {
            await ExecuteLogin();
        }

        /// <summary>
        /// Executa o processo de login
        /// </summary>
        private async Task ExecuteLogin()
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
                MessageBox.Show("Por favor, digite a senha.", "Campo Obrigatório",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            // Desabilita o botão durante o login
            button1.Enabled = false;
            button1.Text = "Verificando...";
            button1.BackColor = Color.Gray;

            try
            {
                // Valida o usuário
                var (success, username, email) = await DatabaseConnection.ValidateUserAsync(
                    textBox1.Text.Trim(),
                    textBox2.Text);

                if (success)
                {
                    // Login bem-sucedido
                    MessageBox.Show($"✅ Bem-vindo, {username}!\n📧 Email: {email}", "Login Bem-sucedido",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Obtém informações completas do usuário
                    var (found, id, user, userEmail, dataCreated) = await DatabaseConnection.GetUserInfoAsync(username);

                    if (found)
                    {
                        Painel painel = new Painel();
                        this.Hide(); // Esconde o form atual
                        painel.Show();

                    }

                    // Limpa os campos após login bem-sucedido
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                }
                else
                {
                    // Login falhou
                    MessageBox.Show("❌ Usuário ou senha incorretos.\nVerifique seus dados e tente novamente.", "Login Falhou",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Limpa apenas a senha e foca no usuário
                    textBox2.Clear();
                    textBox1.Focus();
                    textBox1.SelectAll(); // Seleciona todo o texto para facilitar correção
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro durante o login:\n{ex.Message}", "Erro",
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

        /// <summary>
        /// Permite login com Enter no campo senha
        /// </summary>
        private async void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Impede o "beep" do Windows
                await ExecuteLogin();
            }
        }

        /// <summary>
        /// Permite mover para o campo senha com Enter
        /// </summary>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Impede o "beep" do Windows
                textBox2.Focus();
            }
        }

        /// <summary>
        /// Mudar o form quando clica em "Cadastre-se"
        /// </summary>
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Cadastro cadastro = new Cadastro();
            this.Hide(); // Esconde o form atual
            cadastro.Show();
            cadastro.FormClosed += (s, args) =>
            {
                this.Show();
                this.BringToFront();
            };

        }


        /// <summary>
        /// Mudar o form quando clica em "Esqueci a senha"
        /// </summary>
        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RecuperarSenha recuperarSenha = new RecuperarSenha();
            this.Hide(); // Esconde o form atual
            recuperarSenha.Show();
            recuperarSenha.FormClosed += (s, args) =>
            {
                this.Show();
                this.BringToFront();
            };
        }
    }
}