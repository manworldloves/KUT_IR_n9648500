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
    public partial class frmResults : Form
    {
        private LuceneIREngine myIREngine = new LuceneIREngine();
        //IRCollection myResultCollection;
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

            // setup datagrid
            SetupDataGrid(myIREngine.GetResultSummaryColDetails());
            PopulateDataGrid(0);

            // setup buttons
            btnPrevious.Enabled = false;
            if (numResults <= 10)
                btnNext.Enabled = false;
            
            // report total results and query time
            lblTotalResults.Text = "Total Results: " + numResults;
            lblQueryTime.Text = "Time to query: " + myIREngine.queryTime + " sec";

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

        private void DisplayDetailedResults()
        {
			int rowSelection = dgSearchResults.SelectedCells[0].RowIndex;
			int docToDisplay = pageNumber * 10 + rowSelection;
			Form detailsForm = new frmDetail(myIREngine.GetResultDocument(docToDisplay));
			detailsForm.Show();
        }

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

        private void btnClearResults_Click(object sender, EventArgs e)
        {
 
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            pageNumber -= 1;
            PopulateDataGrid(pageNumber);
            CheckButtonsToEnable();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            pageNumber += 1;
            PopulateDataGrid(pageNumber);
            CheckButtonsToEnable();
        }

        private void CheckButtonsToEnable()
        {
            if (pageNumber == 0)
                btnPrevious.Enabled = false;
            else
                btnPrevious.Enabled = true;

            if (pageNumber * 10 + 10 > numResults)
                btnNext.Enabled = false;
            else
                btnNext.Enabled = true;
        }

        private void dgSearchResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // do nothing
        }

        private void dgSearchResults_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplayDetailedResults();
        }

        private void dgSearchResults_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DisplayDetailedResults();
        }

        private void btnOpenDetails_Click(object sender, EventArgs e)
        {
            DisplayDetailedResults();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
