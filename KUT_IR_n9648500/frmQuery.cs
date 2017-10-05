using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lucene.Net.Search;

namespace KUT_IR_n9648500
{
    public partial class frmQuery : Form
    {
        LuceneIREngine myIREngine = new LuceneIREngine();
        string queryText;
        string topicID = "000";

        public frmQuery()
        {
            // default constructor
            InitializeComponent();
        }

        public frmQuery(LuceneIREngine IREngine)
        {
            InitializeComponent();
            myIREngine = IREngine;
            chkProcess.Checked = true;
            tbProcQuery.Text = "";

        }

        private void chkProcess_CheckedChanged(object sender, EventArgs e)
        {
            // nothing to do
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtQuery.Text !="")
            {
                // execute the query
                string qText;
                int numberOfResults = myIREngine.RunQuery(txtQuery.Text, 
                                                          chkProcess.Checked, out qText);

                tbProcQuery.Text = qText;

                // open query dialog
                if (numberOfResults > 0)
                {
                    Form resultsForm = new frmResults(myIREngine, topicID);
                    resultsForm.Show();
                }
                else
                {
                    MessageBox.Show("No results found.\nTry again.");
                }
                
            }
            else
            {
                MessageBox.Show("No query entered.\nTry again.");
            }
        }

        private void btnOpenQueryFile_Click(object sender, EventArgs e)
        {
            // get the infoneeds txt file
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            openFileDialog.FileName = "";
            openFileDialog.ShowDialog();
            string selectedFile = openFileDialog.FileName;

            // turn the file into a dictionary
            Dictionary<string, string> infoNeeds = InfoNeeds.GetInfoNeeds(selectedFile);

            // open form for user to select query
            frmQuerySelect getQuery = new frmQuerySelect(infoNeeds);
            getQuery.ShowDialog();
            
            // wait for user to select a query

            // set the query text if the use has selected a query
            topicID = getQuery.SelectedTopicID;
            if (topicID != "000")
            {
                string stdQueryText = infoNeeds[topicID];
                txtQuery.Text = stdQueryText;
            }

        }

        private void btnAutoQuery_Click(object sender, EventArgs e)
        {
            // do stuff
        }
    }
}
