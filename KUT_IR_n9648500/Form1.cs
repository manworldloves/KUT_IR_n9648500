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

            float indexTime;
            if (success == 0)
            {
                indexTime = myIREngine.indexTime;
                MessageBox.Show("Index created successfully!\nTime to index: "
                                + indexTime + " msec");
            }
            else
            {
                MessageBox.Show("There was a problem creating the index.\n" 
                               + "Try again.");
            }

            //// this code needs to be moved - it shouldn't sit in the form class
            //List<string> filenames = FileHandling.GetFileNames(collectionFolder, false);
            //List<string> documents = new List<string>();

            //foreach (string fn in filenames)
            //{
            //    string document = FileHandling.ReadTextFile(fn);
            //    if (document != "")
            //    {
            //        documents.Add(document);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Problem opening file:\n\n" + fn);
            //    }
            //}

            //List<JournalAbstract> allAbstracts = new List<JournalAbstract>();
            //foreach (string doc in documents)
            //{
            //    allAbstracts.Add(new JournalAbstract(doc));
            //}

            //System.Diagnostics.Debug.WriteLine(allAbstracts[0]);
            //System.Diagnostics.Debug.WriteLine("number of docs: " + allAbstracts.Count);
        }

        private void checkEnableCreateIndex()
        {
            if (!(collectionFolder is null || indexFolder is null))
                btnCreateIndex.Enabled = true;
        }

    }
}
