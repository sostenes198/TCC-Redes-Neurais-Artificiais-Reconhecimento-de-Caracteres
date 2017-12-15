namespace Executor.Componente
{
    partial class PainelValidacao 
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLimpar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAcao = new System.Windows.Forms.Button();
            this.txtCodigoTreinamento = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.controleDesenho = new Executor.Componente.ControleDesenho();
            this.SuspendLayout();
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(158, 4);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(64, 38);
            this.btnLimpar.TabIndex = 1;
            this.btnLimpar.Text = "Limpar Desenho";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Desenhe no quadrado abaixo";
            // 
            // btnAcao
            // 
            this.btnAcao.Location = new System.Drawing.Point(147, 166);
            this.btnAcao.Name = "btnAcao";
            this.btnAcao.Size = new System.Drawing.Size(75, 24);
            this.btnAcao.TabIndex = 6;
            this.btnAcao.Text = "Isso e um...";
            this.btnAcao.UseVisualStyleBackColor = true;
            this.btnAcao.Click += new System.EventHandler(this.btnAcao_Click);
            // 
            // txtCodigoTreinamento
            // 
            this.txtCodigoTreinamento.Location = new System.Drawing.Point(7, 164);
            this.txtCodigoTreinamento.Name = "txtCodigoTreinamento";
            this.txtCodigoTreinamento.Size = new System.Drawing.Size(68, 20);
            this.txtCodigoTreinamento.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Treinamento:";
            // 
            // controleDesenho
            // 
            this.controleDesenho.BackColor = System.Drawing.Color.White;
            this.controleDesenho.Location = new System.Drawing.Point(64, 45);
            this.controleDesenho.MaximumSize = new System.Drawing.Size(100, 100);
            this.controleDesenho.MinimumSize = new System.Drawing.Size(100, 100);
            this.controleDesenho.Name = "controleDesenho";
            this.controleDesenho.Size = new System.Drawing.Size(100, 100);
            this.controleDesenho.TabIndex = 0;
            // 
            // PainelValidacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCodigoTreinamento);
            this.Controls.Add(this.btnAcao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.controleDesenho);
            this.Name = "PainelValidacao";
            this.Size = new System.Drawing.Size(228, 193);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ControleDesenho controleDesenho;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAcao;
        private System.Windows.Forms.TextBox txtCodigoTreinamento;
        private System.Windows.Forms.Label label2;
    }
}
