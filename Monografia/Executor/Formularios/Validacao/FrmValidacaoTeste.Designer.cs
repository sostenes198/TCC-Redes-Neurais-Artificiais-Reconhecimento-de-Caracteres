namespace Executor.Formularios.Validacao
{
    partial class FrmValidacaoTeste
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.painelValidacao1 = new Executor.Componente.PainelValidacao();
            this.SuspendLayout();
            // 
            // painelValidacao1
            // 
            this.painelValidacao1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.painelValidacao1.Location = new System.Drawing.Point(152, 43);
            this.painelValidacao1.Name = "painelValidacao1";
            this.painelValidacao1.Size = new System.Drawing.Size(228, 193);
            this.painelValidacao1.TabIndex = 0;
            // 
            // FrmValidacaoTeste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 377);
            this.Controls.Add(this.painelValidacao1);
            this.Name = "FrmValidacaoTeste";
            this.Text = "FrmValidacaoTeste";
            this.ResumeLayout(false);

        }

        #endregion

        private Componente.PainelValidacao painelValidacao1;


    }
}