namespace KUT_IR_n9648500
{
    partial class frmQuery
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
            this.chkProcess = new System.Windows.Forms.CheckBox();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.lblProcQuery = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnOpenQueryFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // chkProcess
            // 
            this.chkProcess.AutoSize = true;
            this.chkProcess.Location = new System.Drawing.Point(22, 128);
            this.chkProcess.Name = "chkProcess";
            this.chkProcess.Size = new System.Drawing.Size(110, 17);
            this.chkProcess.TabIndex = 0;
            this.chkProcess.Text = "Preprocess Query";
            this.chkProcess.UseVisualStyleBackColor = true;
            this.chkProcess.CheckedChanged += new System.EventHandler(this.chkProcess_CheckedChanged);
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(22, 76);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(544, 20);
            this.txtQuery.TabIndex = 1;
            // 
            // lblProcQuery
            // 
            this.lblProcQuery.AutoSize = true;
            this.lblProcQuery.Location = new System.Drawing.Point(22, 178);
            this.lblProcQuery.Name = "lblProcQuery";
            this.lblProcQuery.Size = new System.Drawing.Size(0, 13);
            this.lblProcQuery.TabIndex = 2;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(201, 128);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(161, 23);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "Search";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnOpenQueryFile
            // 
            this.btnOpenQueryFile.Location = new System.Drawing.Point(412, 128);
            this.btnOpenQueryFile.Name = "btnOpenQueryFile";
            this.btnOpenQueryFile.Size = new System.Drawing.Size(154, 23);
            this.btnOpenQueryFile.TabIndex = 4;
            this.btnOpenQueryFile.Text = "Open Standard Queries";
            this.btnOpenQueryFile.UseVisualStyleBackColor = true;
            this.btnOpenQueryFile.Click += new System.EventHandler(this.btnOpenQueryFile_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // frmQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 307);
            this.Controls.Add(this.btnOpenQueryFile);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.lblProcQuery);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.chkProcess);
            this.Name = "frmQuery";
            this.Text = "Submit Query";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkProcess;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Label lblProcQuery;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnOpenQueryFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}