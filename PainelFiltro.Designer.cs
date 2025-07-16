namespace LoginSystemNet8
{
    partial class PainelFiltro
    {
        /// <summary>  
        /// Required designer variable.  
        /// </summary>  
        private System.ComponentModel.IContainer components = null;
        private Panel panel1;
        private Label label1; // Changed Control to Label to fix CS1061 error  

        /// <summary>  
        /// Clean up any resources being used.  
        /// </summary>  
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>  
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code  

        /// <summary>  
        /// Required method for Designer support - do not modify  
        /// the contents of this method with the code editor.  
        /// </summary>  
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label(); // Changed Control to Label to fix CS1061 error  
            panel1.SuspendLayout();
            SuspendLayout();
            //   
            // panel1  
            //   
            panel1.BackColor = Color.Maroon;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1195, 100);
            panel1.TabIndex = 0;
            //   
            // label1  
            //   
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(393, 32);
            label1.Name = "label1";
            label1.Size = new Size(429, 37);
            label1.TabIndex = 1;
            label1.Text = "Formulário de filtro de cadastros";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            //   
            // PainelCadastro  
            //   
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1195, 852);
            Controls.Add(panel1);
            Name = "PainelFiltro";
            Text = "PainelFiltro";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}