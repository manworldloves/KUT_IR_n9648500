using System;
using System.Collections.Generic; // for List<> object
using System.Linq; // for collection manipulation ie. ToArray()
using System.Windows.Forms;

namespace KUT_IR_n9648500
{
    public partial class frmQuerySelect : Form
    {
        private string selectedTopicID;

        public string SelectedTopicID { get { return selectedTopicID; } }

        public frmQuerySelect()
        {
            // default constructor
            InitializeComponent();
        }

		public frmQuerySelect(Dictionary<string, string> infoNeeds)
		{
			InitializeComponent();
            lbQueries.SelectionMode = SelectionMode.One;
            lbQueries.Items.AddRange(infoNeeds.Keys.ToArray());
		}

        // returns the TopicID selected by the user
        private void btnGetQuery_Click(object sender, EventArgs e)
        {
            selectedTopicID = lbQueries.SelectedItem.ToString();
            this.Close();
        }
    }
}
