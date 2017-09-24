namespace KUT_IR_n9648500
{
    partial class frmQuerySelect
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
            this.lbQueries = new System.Windows.Forms.ListBox();
            this.lblQuerySelectTitle = new System.Windows.Forms.Label();
            this.btnGetQuery = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbQueries
            // 
            this.lbQueries.FormattingEnabled = true;
            this.lbQueries.Location = new System.Drawing.Point(12, 51);
            this.lbQueries.Name = "lbQueries";
            this.lbQueries.Size = new System.Drawing.Size(260, 147);
            this.lbQueries.TabIndex = 0;
            // 
            // lblQuerySelectTitle
            // 
            this.lblQuerySelectTitle.AutoSize = true;
            this.lblQuerySelectTitle.Location = new System.Drawing.Point(13, 13);
            this.lblQuerySelectTitle.Name = "lblQuerySelectTitle";
            this.lblQuerySelectTitle.Size = new System.Drawing.Size(117, 13);
            this.lblQuerySelectTitle.TabIndex = 1;
            this.lblQuerySelectTitle.Text = "Select a Query Number";
            // 
            // btnGetQuery
            // 
            this.btnGetQuery.Location = new System.Drawing.Point(74, 205);
            this.btnGetQuery.Name = "btnGetQuery";
            this.btnGetQuery.Size = new System.Drawing.Size(140, 44);
            this.btnGetQuery.TabIndex = 2;
            this.btnGetQuery.Text = "Get Query";
            this.btnGetQuery.UseVisualStyleBackColor = true;
            // 
            // frmQuerySelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnGetQuery);
            this.Controls.Add(this.lblQuerySelectTitle);
            this.Controls.Add(this.lbQueries);
            this.Name = "frmQuerySelect";
            this.Text = "frmQuerySelect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbQueries;
        private System.Windows.Forms.Label lblQuerySelectTitle;
        private System.Windows.Forms.Button btnGetQuery;
    }
}