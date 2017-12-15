namespace Executor.Formularios.Insercao
{
    partial class FormInsercaoValidacao
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
            this.painelDesenho1 = new Executor.Componente.PainelDesenho();
            this.SuspendLayout();
            // 
            // painelDesenho1
            // 
            this.painelDesenho1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.painelDesenho1.Location = new System.Drawing.Point(13, 13);
            this.painelDesenho1.Name = "painelDesenho1";
            this.painelDesenho1.Size = new System.Drawing.Size(228, 233);
            this.painelDesenho1.TabIndex = 0;
            this.painelDesenho1.TipoImagem = Executor.Classes.Global.TipoImagem.Validacao;
            // 
            // FormInsercaoValidacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 272);
            this.Controls.Add(this.painelDesenho1);
            this.Name = "FormInsercaoValidacao";
            this.Text = "Inserindo dados de Validação";
            this.ResumeLayout(false);

        }

        #endregion

        private Componente.PainelDesenho painelDesenho1;

    }
}