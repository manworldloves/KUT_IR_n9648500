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
        private string topicID = "000";

        // the program doesn't like having 1400 autosuggestions so I have to do it manually...
        string[] allSugs;

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
            this.ActiveControl = txtQuery;

            // build query autocomplete options
            allSugs = myIREngine.GetQuerySuggestions();
            lbSuggestions.Visible = false;
            
            // this button was used for testing purposes
            btnAutoQuery.Visible = true;
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

            if (selectedFile != "")
            {
				// turn the file into a dictionary
				Dictionary<string, string> infoNeeds = InfoNeeds.GetInfoNeeds(selectedFile);

				// open form for user to select query
				frmQuerySelect getQuery = new frmQuerySelect(infoNeeds);
				
                // wait for user to select a query
                getQuery.ShowDialog();

				// set the query text if the use has selected a query
				topicID = getQuery.SelectedTopicID;
				if (topicID != null)
				{
					string stdQueryText = infoNeeds[topicID];
					txtQuery.Text = stdQueryText;
				}
            }
            else
            {
                MessageBox.Show("No query file selected!");
            }
        }

        private void btnAutoQuery_Click(object sender, EventArgs e)
        {
            // do stuff
            // this is for testing only
            string queryFile = "../../cran_information_needs.txt";
            //string now = DateTime.Now.ToString("yyyymmddhhmmss");
            string resultsFile = "../../autoquery_results.txt";

            Dictionary<string, string> infoNeeds = InfoNeeds.GetInfoNeeds(queryFile);

            myIREngine.AutoResults(resultsFile, infoNeeds, chkProcess.Checked);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtQuery_TextChanged(object sender, EventArgs e)
        {
            // reset the suggestions
            lbSuggestions.Items.Clear();
            
            // if something has been typed, update the suggestions and display
            if (txtQuery.Text != "")
            {
                // not null, matches the start of lowercase text, is distinct
                string[] qSugs = allSugs.Where(x => x != null && x.StartsWith(txtQuery.Text.ToLower())).Distinct().ToArray();
                if (qSugs.Length > 0)
                {
                    lbSuggestions.Items.AddRange(qSugs);
                    lbSuggestions.Visible = true;
                }
                else
                {
                    lbSuggestions.Visible = false;
                }


            }
            else
            {
                // else, hide the suggestions
                lbSuggestions.Visible = false;
            }

        }

        private void lbSuggestions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if the user selects a suggestions,
            // set it as the query text and hide suggestions
            txtQuery.Text = lbSuggestions.Text;
            lbSuggestions.Visible = false;
        }
    }
}
