namespace LGroup.SysAmigos.UI.Windows.Modules.Amigos.Operadores_Consulta
{
    partial class frmListar
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
            this.dgvAmigos = new System.Windows.Forms.DataGridView();
            this.button8 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAmigos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAmigos
            // 
            this.dgvAmigos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAmigos.Location = new System.Drawing.Point(13, 70);
            this.dgvAmigos.Name = "dgvAmigos";
            this.dgvAmigos.Size = new System.Drawing.Size(882, 385);
            this.dgvAmigos.TabIndex = 21;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button8.Location = new System.Drawing.Point(293, 26);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(220, 23);
            this.button8.TabIndex = 22;
            this.button8.Text = "BOLADÃO DE QUERIES";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // frmListar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 468);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.dgvAmigos);
            this.Name = "frmListar";
            this.Text = "frmListar";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAmigos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAmigos;
        private System.Windows.Forms.Button button8;
    }
}