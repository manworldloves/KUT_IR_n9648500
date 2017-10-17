using System;
using System.Collections.Generic; // for List<> object
using System.Linq; // for collection manipulation ie. ToArray()
using System.Windows.Forms;

namespace KUT_IR_n9648500
{
    public partial class frmResults : Form
    {
        private LuceneIREngine myIREngine = new LuceneIREngine();
        private int pageNumber;
        private int numResults;
        private string topicID;

        public frmResults()
        {
            InitializeComponent();
        }

        public frmResults(LuceneIREngine IREngine, string topic)
        {
            InitializeComponent();
            myIREngine = IREngine;
            topicID = topic;
            pageNumber = 0;

            // build the results collection
            numResults = myIREngine.BuildResults();

            // setup escape key actions
            this.CancelButton = btnClose;

            // setup datagrid
            SetupDataGrid(myIREngine.GetResultSummaryColDetails());
            PopulateDataGrid(0);

            // setup buttons
            btnPrevious.Enabled = false;
            if (numResults <= 10)
                btnNext.Enabled = false;
            
            // report total results and query time
            lblTotalResults.Text = "Total Results: " + numResults;
            lblQueryTime.Text = "Time to query: " + myIREngine.QueryTime + " sec";

        }

        // sets up the results datagrid with the col names and sizes
        // also sets up the interaction params for the datagrid
        private void SetupDataGrid(Dictionary<string, float> colDetails)
        {
            string[] colNames = colDetails.Keys.ToArray();
            int dgWidth = dgSearchResults.Width;
            dgSearchResults.ColumnCount = colNames.Length;
            for (int i = 0; i < colNames.Length; i++)
            {
                dgSearchResults.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgSearchResults.Columns[i].Name = colNames[i];
                dgSearchResults.Columns[i].Width = (int)(dgWidth * colDetails[colNames[i]]);
            }
			dgSearchResults.MultiSelect = false;
			dgSearchResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgSearchResults.ReadOnly = true;

        }

        // fills the datagrid with the results summary info
        private void PopulateDataGrid(int pageOffset)
        {
            dgSearchResults.Rows.Clear();
            int resultStart = pageOffset * 10;
            int resultEnd = resultStart + 10;

            // check that there are at least 10 more results to display
            if (numResults < resultEnd)
                resultEnd = numResults;

            // records to the datagrid
            for (int i = resultStart; i < resultEnd; i++)
            {
                string[] newRow = myIREngine.GetResultSummary(i);
                dgSearchResults.Rows.Add(newRow);
            }

            // update text
            lblResultRange.Text = (resultStart + 1) + " - " + resultEnd;
        }

        // opens the details view based on the selected record
        private void DisplayDetailedResults()
        {
			int rowSelection = dgSearchResults.SelectedCells[0].RowIndex;
			int docToDisplay = pageNumber * 10 + rowSelection;
			Form detailsForm = new frmDetail(myIREngine.GetResultDocument(docToDisplay));
			detailsForm.Show();
        }

        // allows the user to save the query results
        private void btnSaveResults_Click(object sender, EventArgs e)
        {
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.OverwritePrompt = false;
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.ShowDialog();
            string fileName = saveFileDialog.FileName;

            if (fileName != "")
            {
                myIREngine.WriteEvalFile(fileName, topicID);
            }
        }

        // navigates back through the results
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            pageNumber -= 1;
            PopulateDataGrid(pageNumber);
            CheckButtonsToEnable();
        }

        // navigates forward through the results
        private void btnNext_Click(object sender, EventArgs e)
        {
            pageNumber += 1;
            PopulateDataGrid(pageNumber);
            CheckButtonsToEnable();
        }

        // enables or disables the results navigation buttons
        private void CheckButtonsToEnable()
        {
            if (pageNumber == 0)
                btnPrevious.Enabled = false;
            else
                btnPrevious.Enabled = true;

            if (pageNumber * 10 + 10 >= numResults)
                btnNext.Enabled = false;
            else
                btnNext.Enabled = true;
        }

        private void dgSearchResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // do nothing
        }

        // open details view on double click of cell
        private void dgSearchResults_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplayDetailedResults();
        }

        // open details view on double click of row header
        private void dgSearchResults_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DisplayDetailedResults();
        }

        // open details view on clicking the open detail button
        private void btnOpenDetails_Click(object sender, EventArgs e)
        {
            DisplayDetailedResults();
        }

        private void dgSearchResults_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DisplayDetailedResults();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Right)
                btnNext.PerformClick();
            if (e.KeyCode == Keys.Left)
                btnPrevious.PerformClick();

        }

        // close this form
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
