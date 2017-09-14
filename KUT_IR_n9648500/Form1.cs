using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace KUT_IR_n9648500
{
    public partial class frmIndex : Form
    {
        private string collectionFolder, indexFolder;

        LuceneIREngine myIREngine = new LuceneIREngine();

        public frmIndex()
        {
            InitializeComponent();
            btnCreateIndex.Enabled = false;
        }

        private void btnCollection_Click(object sender, EventArgs e)
        {
            collectionFolderBrowserDialog.ShowDialog();
            collectionFolder = collectionFolderBrowserDialog.SelectedPath;
            lblCollection.Text = collectionFolder;
            checkEnableCreateIndex();
        }

        private void btnIndex_Click(object sender, EventArgs e)
        {
            indexFolderBrowserDialog.ShowDialog();
            indexFolder = indexFolderBrowserDialog.SelectedPath;
            lblIndex.Text = indexFolder;
            checkEnableCreateIndex();
        }

        private void btnCreateIndex_Click(object sender, EventArgs e)
        {
            int success = myIREngine.CreateIndex(collectionFolder, indexFolder);

            if (success == 0)
            {
                float indexTime = myIREngine.indexTime;
                MessageBox.Show("Index created successfully!\nTime to index: "
                                + indexTime + " msec");
            }
            else
            {
                MessageBox.Show("There was a problem creating the index.\n" 
                               + "Try again.");
            }

        }

        private void checkEnableCreateIndex()
        {
            if (!(collectionFolder is null || indexFolder is null))
                btnCreateIndex.Enabled = true;
        }

    }
}
