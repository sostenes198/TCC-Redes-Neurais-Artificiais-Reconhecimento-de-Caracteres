﻿namespace Executor.Componente
{
    partial class ControleDesenho
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
            this.desenho = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // desenho
            // 
            this.desenho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.desenho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.desenho.Location = new System.Drawing.Point(0, 0);
            this.desenho.Name = "desenho";
            this.desenho.Size = new System.Drawing.Size(100, 100);
            this.desenho.TabIndex = 0;
            this.desenho.Paint += new System.Windows.Forms.PaintEventHandler(this.desenho_Paint);
            this.desenho.MouseDown += new System.Windows.Forms.MouseEventHandler(this.desenho_MouseDown);
            this.desenho.MouseEnter += new System.EventHandler(this.desenho_MouseEnter);
            this.desenho.MouseLeave += new System.EventHandler(this.desenho_MouseLeave);
            this.desenho.MouseMove += new System.Windows.Forms.MouseEventHandler(this.desenho_MouseMove);
            this.desenho.MouseUp += new System.Windows.Forms.MouseEventHandler(this.desenho_MouseUp);
            // 
            // ControleDesenho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.desenho);
            this.MaximumSize = new System.Drawing.Size(100, 100);
            this.MinimumSize = new System.Drawing.Size(60, 100);
            this.Name = "ControleDesenho";
            this.Size = new System.Drawing.Size(100, 100);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel desenho;
    }
}
