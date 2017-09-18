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
    public partial class frmQuery : Form
    {
        LuceneIREngine myIREngine = new LuceneIREngine();
        string queryText;

        public frmQuery()
        {
            InitializeComponent();
        }

        public frmQuery(LuceneIREngine IREngine)
        {
            InitializeComponent();
            myIREngine = IREngine;
        }

        public void TestMethod()
        {
            myIREngine.RunQuery("help!");
        }

        private void chkProcess_CheckedChanged(object sender, EventArgs e)
        {
            UpdateQueryText();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            UpdateQueryText();

            if (queryText!="")
            {
                myIREngine.RunQuery(queryText);
            }
            else
            {
                MessageBox.Show("No query entered.\nTry again.");
            }
        }

        private void UpdateQueryText()
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
    }
}
