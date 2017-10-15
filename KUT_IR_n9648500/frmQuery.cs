using System;
using System.Collections.Generic; // for List<> object
using System.Linq; // for collection manipulation ie. ToArray()
using System.Windows.Forms;

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
            btnAutoQuery.Visible = false;
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

                // display the processed query text
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

        // opens these info needs file and gets the user to
        // select a standard query
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

        // closes this form
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // builds and displays the query suggestion box
        private void txtQuery_TextChanged(object sender, EventArgs e)
        {
            // reset the suggestions
            lbSuggestions.Items.Clear();
            
            // if something has been typed, update the suggestions and display
            if (txtQuery.Text != "")
            {
                // not null, matches the start of lowercase text, is distinct
                string[] qSugs = allSugs.Where(x => x != null && x.StartsWith(txtQuery.Text.ToLower(), StringComparison.CurrentCulture))
                                        .Distinct().ToArray();
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

        // allows the user to select a suggestion
        private void lbSuggestions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if the user selects a suggestions,
            // set it as the query text and hide suggestions
            txtQuery.Text = lbSuggestions.Text;
            lbSuggestions.Visible = false;
        }

        // for testing purposes only
        // runs all of the standard queries
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
    }
}
