namespace KUT_IR_n9648500
{
    partial class frmResults
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgSearchResults = new System.Windows.Forms.DataGridView();
            this.btnSaveResults = new System.Windows.Forms.Button();
            this.btnClearResults = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.lblResultRange = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnOpenDetails = new System.Windows.Forms.Button();
            this.lblTotalResults = new System.Windows.Forms.Label();
            this.lblQueryTime = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgSearchResults)).BeginInit();
            this.SuspendLayout();
            // 
            // dgSearchResults
            // 
            this.dgSearchResults.AllowUserToAddRows = false;
            this.dgSearchResults.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSearchResults.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgSearchResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgSearchResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgSearchResults.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgSearchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSearchResults.Location = new System.Drawing.Point(13, 13);
            this.dgSearchResults.Name = "dgSearchResults";
            this.dgSearchResults.Size = new System.Drawing.Size(1251, 664);
            this.dgSearchResults.TabIndex = 0;
            this.dgSearchResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSearchResults_CellContentClick);
            this.dgSearchResults.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSearchResults_CellContentDoubleClick);
            this.dgSearchResults.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgSearchResults_RowHeaderMouseDoubleClick);
            // 
            // btnSaveResults
            // 
            this.btnSaveResults.Location = new System.Drawing.Point(909, 695);
            this.btnSaveResults.Name = "btnSaveResults";
            this.btnSaveResults.Size = new System.Drawing.Size(95, 54);
            this.btnSaveResults.TabIndex = 1;
            this.btnSaveResults.Text = "Save Results to File";
            this.btnSaveResults.UseVisualStyleBackColor = true;
            this.btnSaveResults.Click += new System.EventHandler(this.btnSaveResults_Click);
            // 
            // btnClearResults
            // 
            this.btnClearResults.Location = new System.Drawing.Point(1028, 695);
            this.btnClearResults.Name = "btnClearResults";
            this.btnClearResults.Size = new System.Drawing.Size(95, 54);
            this.btnClearResults.TabIndex = 2;
            this.btnClearResults.Text = "Clear Results File";
            this.btnClearResults.UseVisualStyleBackColor = true;
            this.btnClearResults.Click += new System.EventHandler(this.btnClearResults_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(409, 695);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(67, 54);
            this.btnPrevious.TabIndex = 3;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // lblResultRange
            // 
            this.lblResultRange.AutoSize = true;
            this.lblResultRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultRange.Location = new System.Drawing.Point(500, 707);
            this.lblResultRange.Name = "lblResultRange";
            this.lblResultRange.Size = new System.Drawing.Size(276, 25);
            this.lblResultRange.TabIndex = 4;
            this.lblResultRange.Text = "Displaying Results 1 - 10";
            this.lblResultRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(802, 695);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(67, 54);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnOpenDetails
            // 
            this.btnOpenDetails.Location = new System.Drawing.Point(13, 695);
            this.btnOpenDetails.Name = "btnOpenDetails";
            this.btnOpenDetails.Size = new System.Drawing.Size(94, 54);
            this.btnOpenDetails.TabIndex = 6;
            this.btnOpenDetails.Text = "Open Details";
            this.btnOpenDetails.UseVisualStyleBackColor = true;
            this.btnOpenDetails.Click += new System.EventHandler(this.btnOpenDetails_Click);
            // 
            // lblTotalResults
            // 
            this.lblTotalResults.AutoSize = true;
            this.lblTotalResults.Location = new System.Drawing.Point(128, 695);
            this.lblTotalResults.Name = "lblTotalResults";
            this.lblTotalResults.Size = new System.Drawing.Size(69, 13);
            this.lblTotalResults.TabIndex = 7;
            this.lblTotalResults.Text = "Total Resuls:";
            // 
            // lblQueryTime
            // 
            this.lblQueryTime.AutoSize = true;
            this.lblQueryTime.Location = new System.Drawing.Point(128, 719);
            this.lblQueryTime.Name = "lblQueryTime";
            this.lblQueryTime.Size = new System.Drawing.Size(74, 13);
            this.lblQueryTime.TabIndex = 8;
            this.lblQueryTime.Text = "Time to query:";
            // 
            // frmResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 761);
            this.Controls.Add(this.lblQueryTime);
            this.Controls.Add(this.lblTotalResults);
            this.Controls.Add(this.btnOpenDetails);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lblResultRange);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnClearResults);
            this.Controls.Add(this.btnSaveResults);
            this.Controls.Add(this.dgSearchResults);
            this.Name = "frmResults";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Results";
            ((System.ComponentModel.ISupportInitialize)(this.dgSearchResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgSearchResults;
        private System.Windows.Forms.Button btnSaveResults;
        private System.Windows.Forms.Button btnClearResults;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label lblResultRange;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnOpenDetails;
        private System.Windows.Forms.Label lblTotalResults;
        private System.Windows.Forms.Label lblQueryTime;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}