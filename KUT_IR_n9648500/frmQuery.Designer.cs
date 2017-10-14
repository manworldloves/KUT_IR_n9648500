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
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnOpenQueryFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnAutoQuery = new System.Windows.Forms.Button();
            this.tbProcQuery = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lbSuggestions = new System.Windows.Forms.ListBox();
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
            this.txtQuery.TextChanged += new System.EventHandler(this.txtQuery_TextChanged);
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
            // btnAutoQuery
            // 
            this.btnAutoQuery.Location = new System.Drawing.Point(412, 12);
            this.btnAutoQuery.Name = "btnAutoQuery";
            this.btnAutoQuery.Size = new System.Drawing.Size(154, 23);
            this.btnAutoQuery.TabIndex = 5;
            this.btnAutoQuery.Text = "Run All Standard Queries";
            this.btnAutoQuery.UseVisualStyleBackColor = true;
            this.btnAutoQuery.Click += new System.EventHandler(this.btnAutoQuery_Click);
            // 
            // tbProcQuery
            // 
            this.tbProcQuery.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbProcQuery.Location = new System.Drawing.Point(22, 164);
            this.tbProcQuery.Multiline = true;
            this.tbProcQuery.Name = "tbProcQuery";
            this.tbProcQuery.ReadOnly = true;
            this.tbProcQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbProcQuery.Size = new System.Drawing.Size(544, 131);
            this.tbProcQuery.TabIndex = 6;
            this.tbProcQuery.Text = "query here";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(631, 244);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 51);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbSuggestions
            // 
            this.lbSuggestions.FormattingEnabled = true;
            this.lbSuggestions.Location = new System.Drawing.Point(22, 102);
            this.lbSuggestions.Name = "lbSuggestions";
            this.lbSuggestions.Size = new System.Drawing.Size(544, 147);
            this.lbSuggestions.TabIndex = 8;
            this.lbSuggestions.SelectedIndexChanged += new System.EventHandler(this.lbSuggestions_SelectedIndexChanged);
            // 
            // frmQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 307);
            this.Controls.Add(this.lbSuggestions);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tbProcQuery);
            this.Controls.Add(this.btnAutoQuery);
            this.Controls.Add(this.btnOpenQueryFile);
            this.Controls.Add(this.btnQuery);
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
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnOpenQueryFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnAutoQuery;
        private System.Windows.Forms.TextBox tbProcQuery;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListBox lbSuggestions;
    }
}