namespace prjGIUnimage
{
    partial class frmExecutionStoredProcedure
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
            this.lblScenarios = new System.Windows.Forms.Label();
            this.lblInventory = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblScenarios
            // 
            this.lblScenarios.AutoSize = true;
            this.lblScenarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScenarios.Location = new System.Drawing.Point(94, 136);
            this.lblScenarios.Name = "lblScenarios";
            this.lblScenarios.Size = new System.Drawing.Size(599, 25);
            this.lblScenarios.TabIndex = 0;
            this.lblScenarios.Text = "Le scénario est en cours de création, veuillez patienter.";
            // 
            // lblInventory
            // 
            this.lblInventory.AutoSize = true;
            this.lblInventory.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInventory.Location = new System.Drawing.Point(81, 161);
            this.lblInventory.Name = "lblInventory";
            this.lblInventory.Size = new System.Drawing.Size(625, 25);
            this.lblInventory.TabIndex = 1;
            this.lblInventory.Text = "L\'inventaire est en cours de génération, veuillez patienter.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(194, 313);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(399, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "(Ce traitement peut prendre quelques minutes...)";
            // 
            // frmExecutionStoredProcedure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 565);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblInventory);
            this.Controls.Add(this.lblScenarios);
            this.Name = "frmExecutionStoredProcedure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmExecutionStoredProcedure";
            this.Load += new System.EventHandler(this.frmExecutionStoredProcedure_Load);
            this.Shown += new System.EventHandler(this.frmExecutionStoredProcedure_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblScenarios;
        private System.Windows.Forms.Label lblInventory;
        private System.Windows.Forms.Label label1;
    }
}