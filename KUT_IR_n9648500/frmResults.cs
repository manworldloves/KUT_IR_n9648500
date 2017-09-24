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
        LuceneIREngine myIREngine = new LuceneIREngine();
        IRCollection myResultCollection;
        int pageNumber;

        public frmResults()
        {
            InitializeComponent();
        }

        public frmResults(LuceneIREngine IREngine)
        {
            InitializeComponent();
            myIREngine = IREngine;
            pageNumber = 0;

            /// good tutorial
            /// https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/walkthrough-creating-an-unbound-windows-forms-datagridview-control
            /// 
            //List<IRDocument> 
            //TopDocs results =
            myResultCollection = myIREngine.BuildResults();

            // setup datagrid
            SetupDataGrid(myResultCollection.GetIRDocument(0).GetResultSummaryColNames());
            PopulateDataGrid(0);
            btnPrevious.Enabled = false;
            if (myResultCollection.Length() <= 10)
                btnNext.Enabled = false;
            dgSearchResults.MultiSelect = false;
            dgSearchResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgSearchResults.ReadOnly = true;

            JournalAbstract toprank = myResultCollection.GetIRDocument(0) as JournalAbstract;
            MessageBox.Show("Top doc is docID: " + toprank.DocID);
        }

        private void SetupDataGrid(string[] colNames)
        {
            dgSearchResults.ColumnCount = colNames.Length;
            for (int i = 0; i < colNames.Length; i++)
            {
                dgSearchResults.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgSearchResults.Columns[i].Name = colNames[i];
            }

        }

        private void PopulateDataGrid(int pageOffset)
        {
            dgSearchResults.Rows.Clear();
            int resultStart = pageOffset * 10;
            int resultEnd = resultStart + 10;

            // check that there are at least 10 more results to display
            if (myResultCollection.Length() < resultEnd)
                resultEnd = myResultCollection.Length();

            // records to the datagrid
            for (int i = resultStart; i < resultEnd; i++)
            {
                string[] newRow = myResultCollection.GetIRDocument(i).GetResultSummary();
                dgSearchResults.Rows.Add(newRow);
            }

            // update text
            lblResultRange.Text = "Displaying Results " + 
                (resultStart + 1) + " - " + resultEnd;
        }

        private void DisplayDetailedResults()
        {
			int rowSelection = dgSearchResults.SelectedCells[0].RowIndex;
			//MessageBox.Show("Selected row: " + rowSelection);
			int docToDisplay = pageNumber * 10 + rowSelection;
			Form detailsForm = new frmDetail(myResultCollection.GetIRDocument(docToDisplay));
			detailsForm.Show();
        }

        private void btnSaveResults_Click(object sender, EventArgs e)
        {
            myIREngine.WriteEvalFile(@"../../nathan_eval.txt", myResultCollection);
        }

        private void btnClearResults_Click(object sender, EventArgs e)
        {
            // temporarily here because of mac
            DisplayDetailedResults();
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

            if (pageNumber * 10 + 10 > myResultCollection.Length())
                btnNext.Enabled = false;
            else
                btnNext.Enabled = true;
        }

        private void dgSearchResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgSearchResults_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplayDetailedResults();
        }

        private void dgSearchResults_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DisplayDetailedResults();
        }
    }
}
