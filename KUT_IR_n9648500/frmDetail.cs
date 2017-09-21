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
    public partial class frmDetail : Form
    {
        public frmDetail()
        {
            InitializeComponent();
        }

        public frmDetail(IRDocument doc)
        {
            InitializeComponent();
            JournalAbstract JAdoc = doc as JournalAbstract;
            lblAbstract.Text = JAdoc.Words;
            lblBib.Text = JAdoc.BiblioInfo;
            lblAuthor.Text = JAdoc.Author;
            lblTitle.Text = JAdoc.Title;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
