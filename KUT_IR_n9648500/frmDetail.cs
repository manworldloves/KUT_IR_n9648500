using System;
using System.Windows.Forms;

namespace KUT_IR_n9648500
{
    public partial class frmDetail : Form
    {
        public frmDetail()
        {
            InitializeComponent();
        }

        // Display the details of the IRDocument
        // as this form is specific to the IRDocument type
        // this detail will need to be updated if he IRDocument
        // type is change for a different application
        public frmDetail(IRDocument doc)
        {
            InitializeComponent();
            JournalAbstract JAdoc = doc as JournalAbstract;
            tbAbstract.Text = JAdoc.Words;
            lblBib.Text = JAdoc.BiblioInfo;
            lblAuthor.Text = JAdoc.Author;
            lblTitle.Text = JAdoc.Title;

            // action for escape key
            this.CancelButton = btnOK;
        }

        // close this form
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
