namespace Executor.Formularios.Geracao
{
    partial class FrmGerarTreinamento
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.txtCodigoDadosRedeNeural = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtExemplo10 = new System.Windows.Forms.RadioButton();
            this.rbtExemplo50 = new System.Windows.Forms.RadioButton();
            this.rbtExemplo30 = new System.Windows.Forms.RadioButton();
            this.nudTaxaAprendizadoFrequencia = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudMomentumFrequencia = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudTaxaAprendizadoFinal = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.nudTaxaAprendizadoInicial = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudMomentumFinal = new System.Windows.Forms.NumericUpDown();
            this.nudMomentumInicial = new System.Windows.Forms.NumericUpDown();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaxaAprendizadoFrequencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMomentumFrequencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaxaAprendizadoFinal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaxaAprendizadoInicial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMomentumFinal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMomentumInicial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtCodigoDadosRedeNeural);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.nudTaxaAprendizadoFrequencia);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.nudMomentumFrequencia);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.nudTaxaAprendizadoFinal);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.nudTaxaAprendizadoInicial);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.nudMomentumFinal);
            this.panel1.Controls.Add(this.nudMomentumInicial);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 112);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(653, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 39);
            this.button1.TabIndex = 24;
            this.button1.Text = "Adicionar Resilients propagation";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCodigoDadosRedeNeural
            // 
            this.txtCodigoDadosRedeNeural.Location = new System.Drawing.Point(662, 25);
            this.txtCodigoDadosRedeNeural.Name = "txtCodigoDadosRedeNeural";
            this.txtCodigoDadosRedeNeural.Size = new System.Drawing.Size(100, 20);
            this.txtCodigoDadosRedeNeural.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(659, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Código Rede Neural";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtExemplo10);
            this.groupBox1.Controls.Add(this.rbtExemplo50);
            this.groupBox1.Controls.Add(this.rbtExemplo30);
            this.groupBox1.Location = new System.Drawing.Point(470, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(167, 41);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Quantidade de Exemplos";
            // 
            // rbtExemplo10
            // 
            this.rbtExemplo10.AutoSize = true;
            this.rbtExemplo10.Location = new System.Drawing.Point(30, 18);
            this.rbtExemplo10.Name = "rbtExemplo10";
            this.rbtExemplo10.Size = new System.Drawing.Size(37, 17);
            this.rbtExemplo10.TabIndex = 18;
            this.rbtExemplo10.TabStop = true;
            this.rbtExemplo10.Text = "10";
            this.rbtExemplo10.UseVisualStyleBackColor = true;
            // 
            // rbtExemplo50
            // 
            this.rbtExemplo50.AutoSize = true;
            this.rbtExemplo50.Location = new System.Drawing.Point(116, 18);
            this.rbtExemplo50.Name = "rbtExemplo50";
            this.rbtExemplo50.Size = new System.Drawing.Size(37, 17);
            this.rbtExemplo50.TabIndex = 20;
            this.rbtExemplo50.TabStop = true;
            this.rbtExemplo50.Text = "30";
            this.rbtExemplo50.UseVisualStyleBackColor = true;
            // 
            // rbtExemplo30
            // 
            this.rbtExemplo30.AutoSize = true;
            this.rbtExemplo30.Location = new System.Drawing.Point(73, 18);
            this.rbtExemplo30.Name = "rbtExemplo30";
            this.rbtExemplo30.Size = new System.Drawing.Size(37, 17);
            this.rbtExemplo30.TabIndex = 19;
            this.rbtExemplo30.TabStop = true;
            this.rbtExemplo30.Text = "20";
            this.rbtExemplo30.UseVisualStyleBackColor = true;
            // 
            // nudTaxaAprendizadoFrequencia
            // 
            this.nudTaxaAprendizadoFrequencia.DecimalPlaces = 4;
            this.nudTaxaAprendizadoFrequencia.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nudTaxaAprendizadoFrequencia.Location = new System.Drawing.Point(315, 79);
            this.nudTaxaAprendizadoFrequencia.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTaxaAprendizadoFrequencia.Name = "nudTaxaAprendizadoFrequencia";
            this.nudTaxaAprendizadoFrequencia.Size = new System.Drawing.Size(120, 20);
            this.nudTaxaAprendizadoFrequencia.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(312, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Frequencia";
            // 
            // nudMomentumFrequencia
            // 
            this.nudMomentumFrequencia.DecimalPlaces = 4;
            this.nudMomentumFrequencia.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nudMomentumFrequencia.Location = new System.Drawing.Point(315, 29);
            this.nudMomentumFrequencia.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMomentumFrequencia.Name = "nudMomentumFrequencia";
            this.nudMomentumFrequencia.Size = new System.Drawing.Size(120, 20);
            this.nudMomentumFrequencia.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(312, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Frequencia";
            // 
            // nudTaxaAprendizadoFinal
            // 
            this.nudTaxaAprendizadoFinal.DecimalPlaces = 4;
            this.nudTaxaAprendizadoFinal.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nudTaxaAprendizadoFinal.Location = new System.Drawing.Point(162, 79);
            this.nudTaxaAprendizadoFinal.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTaxaAprendizadoFinal.Name = "nudTaxaAprendizadoFinal";
            this.nudTaxaAprendizadoFinal.Size = new System.Drawing.Size(120, 20);
            this.nudTaxaAprendizadoFinal.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Taxa Aprendizado Final";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(505, 76);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Adicionar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // nudTaxaAprendizadoInicial
            // 
            this.nudTaxaAprendizadoInicial.DecimalPlaces = 4;
            this.nudTaxaAprendizadoInicial.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nudTaxaAprendizadoInicial.Location = new System.Drawing.Point(16, 79);
            this.nudTaxaAprendizadoInicial.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTaxaAprendizadoInicial.Name = "nudTaxaAprendizadoInicial";
            this.nudTaxaAprendizadoInicial.Size = new System.Drawing.Size(120, 20);
            this.nudTaxaAprendizadoInicial.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Taxa Aprendizado Inicial";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(162, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Momentum Final";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Momentum Inicial";
            // 
            // nudMomentumFinal
            // 
            this.nudMomentumFinal.DecimalPlaces = 4;
            this.nudMomentumFinal.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nudMomentumFinal.Location = new System.Drawing.Point(162, 29);
            this.nudMomentumFinal.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMomentumFinal.Name = "nudMomentumFinal";
            this.nudMomentumFinal.Size = new System.Drawing.Size(120, 20);
            this.nudMomentumFinal.TabIndex = 2;
            // 
            // nudMomentumInicial
            // 
            this.nudMomentumInicial.DecimalPlaces = 4;
            this.nudMomentumInicial.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nudMomentumInicial.Location = new System.Drawing.Point(13, 30);
            this.nudMomentumInicial.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMomentumInicial.Name = "nudMomentumInicial";
            this.nudMomentumInicial.Size = new System.Drawing.Size(120, 20);
            this.nudMomentumInicial.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 112);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(801, 352);
            this.dataGridView1.TabIndex = 2;
            // 
            // FrmGerarTreinamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 464);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmGerarTreinamento";
            this.Text = "Gerar Treinamento";
            this.Load += new System.EventHandler(this.FrmGerarTreinamento_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaxaAprendizadoFrequencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMomentumFrequencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaxaAprendizadoFinal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaxaAprendizadoInicial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMomentumFinal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMomentumInicial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudMomentumFinal;
        private System.Windows.Forms.NumericUpDown nudMomentumInicial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudTaxaAprendizadoInicial;
        private System.Windows.Forms.NumericUpDown nudTaxaAprendizadoFrequencia;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudMomentumFrequencia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudTaxaAprendizadoFinal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtExemplo10;
        private System.Windows.Forms.RadioButton rbtExemplo50;
        private System.Windows.Forms.RadioButton rbtExemplo30;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtCodigoDadosRedeNeural;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
    }
}