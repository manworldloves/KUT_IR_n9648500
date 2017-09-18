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
            if (chkProcess.Checked==true)
            {
                MessageBox.Show("Preprocessing query.");
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            lblProcQuery.Text = txtQuery.Text;
            string queryText = txtQuery.Text;

            if (queryText!="")
            {
                myIREngine.RunQuery(queryText);
            }
            else
            {
                MessageBox.Show("No query entered.\nTry again.");
            }
        }
    }
}
