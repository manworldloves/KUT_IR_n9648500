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

        public frmQuery()
        {
            // default constructor
            InitializeComponent();
        }

        public frmQuery(LuceneIREngine IREngine)
        {
            InitializeComponent();
            myIREngine = IREngine;
        }

        private void chkProcess_CheckedChanged(object sender, EventArgs e)
        {
			if (chkProcess.Checked == true)
			{
				queryText = myIREngine.PreprocessQuery(txtQuery.Text);
			}
			else
			{
				queryText = txtQuery.Text;
			}

			lblProcQuery.Text = queryText;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtQuery.Text !="")
            {
                // execute the query
                int numberOfResults = myIREngine.RunQuery(txtQuery.Text, 
                                                          chkProcess.Checked);

                // open query dialog
                if (numberOfResults > 0)
                {
                    Form resultsForm = new frmResults(myIREngine);
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
            openFileDialog.ShowDialog();
            string selectedFile = openFileDialog.FileName;

            MessageBox.Show(selectedFile);
        }
    }
}
