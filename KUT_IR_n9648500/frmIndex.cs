using System;
using System.Windows.Forms;

namespace KUT_IR_n9648500
{
    public partial class frmIndex : Form
    {
        LuceneIREngine myIREngine = new LuceneIREngine();
        private string collectionFolder, indexFolder;

        public frmIndex()
        {
            InitializeComponent();
            btnCreateIndex.Enabled = false;

            // retrieve save directory info if available
            collectionFolder = Properties.Settings.Default.collectionDir;
            indexFolder = Properties.Settings.Default.indexDir;
            if (collectionFolder != "")
                lblCollection.Text = collectionFolder;

            if (indexFolder != "")
                lblIndex.Text = indexFolder;

            checkEnableCreateIndex();
		}

        // get the collection directory
        private void btnCollection_Click(object sender, EventArgs e)
        {
            collectionFolderBrowserDialog.ShowDialog();
            collectionFolder = collectionFolderBrowserDialog.SelectedPath;
            lblCollection.Text = collectionFolder;
            checkEnableCreateIndex();
        }

        // get the index directory
        private void btnIndex_Click(object sender, EventArgs e)
        {
            indexFolderBrowserDialog.ShowDialog();
            indexFolder = indexFolderBrowserDialog.SelectedPath;
            lblIndex.Text = indexFolder;
            checkEnableCreateIndex();
        }

        // build the index
        private void btnCreateIndex_Click(object sender, EventArgs e)
        {
            int numberDocs = myIREngine.CreateIndex(collectionFolder, indexFolder);

            if (numberDocs > 0)
            {
                // save collection and index locations to user settings
                Properties.Settings.Default.collectionDir = collectionFolder;
                Properties.Settings.Default.indexDir = indexFolder;
                Properties.Settings.Default.Save();

                float indexTime = myIREngine.IndexTime;
                MessageBox.Show(numberDocs + " documents added to index!\nTime to index: "
                                + indexTime + " seconds");

                // open query dialog
                Form queryForm = new frmQuery(myIREngine);
                queryForm.Show();
            }
            else
            {
                MessageBox.Show("There was a problem creating the index.\n" 
                               + "Try again.");
            }
        }

        // close this form (and the application)
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // check if there is enough info to build the index
        private void checkEnableCreateIndex()
        {
            if ((collectionFolder != "") && (indexFolder != ""))
            {
                btnCreateIndex.Enabled = true;
                this.AcceptButton = btnCreateIndex;
            }
                
        }

    }
}
