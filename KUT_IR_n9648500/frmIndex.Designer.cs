namespace KUT_IR_n9648500
{
    partial class frmIndex
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
            this.lblStep1 = new System.Windows.Forms.Label();
            this.btnCollection = new System.Windows.Forms.Button();
            this.lblCollection = new System.Windows.Forms.Label();
            this.lblStep2 = new System.Windows.Forms.Label();
            this.btnIndex = new System.Windows.Forms.Button();
            this.lblIndex = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCreateIndex = new System.Windows.Forms.Button();
            this.collectionFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.indexFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // lblStep1
            // 
            this.lblStep1.AutoSize = true;
            this.lblStep1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep1.Location = new System.Drawing.Point(12, 65);
            this.lblStep1.Name = "lblStep1";
            this.lblStep1.Size = new System.Drawing.Size(322, 16);
            this.lblStep1.TabIndex = 0;
            this.lblStep1.Text = "Step 1: Select location of document collection";
            // 
            // btnCollection
            // 
            this.btnCollection.Location = new System.Drawing.Point(15, 93);
            this.btnCollection.Name = "btnCollection";
            this.btnCollection.Size = new System.Drawing.Size(106, 25);
            this.btnCollection.TabIndex = 1;
            this.btnCollection.Text = "Collection Location";
            this.btnCollection.UseVisualStyleBackColor = true;
            this.btnCollection.Click += new System.EventHandler(this.btnCollection_Click);
            // 
            // lblCollection
            // 
            this.lblCollection.AutoSize = true;
            this.lblCollection.Location = new System.Drawing.Point(127, 99);
            this.lblCollection.Name = "lblCollection";
            this.lblCollection.Size = new System.Drawing.Size(0, 13);
            this.lblCollection.TabIndex = 2;
            // 
            // lblStep2
            // 
            this.lblStep2.AutoSize = true;
            this.lblStep2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep2.Location = new System.Drawing.Point(12, 137);
            this.lblStep2.Name = "lblStep2";
            this.lblStep2.Size = new System.Drawing.Size(221, 16);
            this.lblStep2.TabIndex = 0;
            this.lblStep2.Text = "Step 2: Select location of index";
            // 
            // btnIndex
            // 
            this.btnIndex.Location = new System.Drawing.Point(15, 165);
            this.btnIndex.Name = "btnIndex";
            this.btnIndex.Size = new System.Drawing.Size(106, 25);
            this.btnIndex.TabIndex = 1;
            this.btnIndex.Text = "Index Location";
            this.btnIndex.UseVisualStyleBackColor = true;
            this.btnIndex.Click += new System.EventHandler(this.btnIndex_Click);
            // 
            // lblIndex
            // 
            this.lblIndex.AutoSize = true;
            this.lblIndex.Location = new System.Drawing.Point(127, 171);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(0, 13);
            this.lblIndex.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Step 3: Build the index";
            // 
            // btnCreateIndex
            // 
            this.btnCreateIndex.Location = new System.Drawing.Point(15, 236);
            this.btnCreateIndex.Name = "btnCreateIndex";
            this.btnCreateIndex.Size = new System.Drawing.Size(106, 25);
            this.btnCreateIndex.TabIndex = 1;
            this.btnCreateIndex.Text = "Build Index";
            this.btnCreateIndex.UseVisualStyleBackColor = true;
            this.btnCreateIndex.Click += new System.EventHandler(this.btnCreateIndex_Click);
            // 
            // collectionFolderBrowserDialog
            // 
            this.collectionFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // indexFolderBrowserDialog
            // 
            this.indexFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // frmIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 325);
            this.Controls.Add(this.lblIndex);
            this.Controls.Add(this.lblCollection);
            this.Controls.Add(this.btnCreateIndex);
            this.Controls.Add(this.btnIndex);
            this.Controls.Add(this.btnCollection);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblStep2);
            this.Controls.Add(this.lblStep1);
            this.Name = "frmIndex";
            this.Text = "Build Index";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStep1;
        private System.Windows.Forms.Button btnCollection;
        private System.Windows.Forms.Label lblCollection;
        private System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.Button btnIndex;
        private System.Windows.Forms.Label lblIndex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCreateIndex;
        private System.Windows.Forms.FolderBrowserDialog collectionFolderBrowserDialog;
        private System.Windows.Forms.FolderBrowserDialog indexFolderBrowserDialog;
    }
}

