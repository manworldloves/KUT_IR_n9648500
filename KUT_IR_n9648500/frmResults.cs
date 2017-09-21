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

        public frmResults()
        {
            InitializeComponent();
        }

        public frmResults(LuceneIREngine IREngine)
        {
            InitializeComponent();
            myIREngine = IREngine;

            /// good tutorial
            /// https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/walkthrough-creating-an-unbound-windows-forms-datagridview-control
            /// 
            //List<IRDocument> 
            //TopDocs results =
            myResultCollection = myIREngine.BuildResults();

            // setup datagrid
            SetupDataGrid(myResultCollection.GetIRDocument(0).GetResultSummaryColNames());
            PopulateDataGrid();
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

        private void PopulateDataGrid()
        {
            //dgSearchResults.Rows.Clear();
     
            for (int i = 0; i < myResultCollection.Length(); i++)
            {
                string[] newRow = myResultCollection.GetIRDocument(i).GetResultSummary();
                dgSearchResults.Rows.Add(newRow);
            }
        }

        private void btnSaveResults_Click(object sender, EventArgs e)
        {

        }

        private void btnClearResults_Click(object sender, EventArgs e)
        {

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void dgSearchResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgSearchResults_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowSelection = dgSearchResults.SelectedCells[0].RowIndex;
            //MessageBox.Show("Selected row: " + rowSelection);
            Form detailsForm = new frmDetail(myResultCollection.GetIRDocument(rowSelection));
            detailsForm.Show();
        }

        private void dgSearchResults_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowSelection = dgSearchResults.SelectedCells[0].RowIndex;
            //MessageBox.Show("Header Selected row: " + rowSelection);
        }
    }
}
