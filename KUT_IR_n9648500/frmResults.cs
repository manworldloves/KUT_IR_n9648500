﻿using System;
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
        string topicID;

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

            /// good tutorial
            /// https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/walkthrough-creating-an-unbound-windows-forms-datagridview-control
            ///
            myResultCollection = myIREngine.BuildResults();

            // setup datagrid
            SetupDataGrid(myResultCollection.GetIRDocument(0).GetResultSummaryColDetails());
            PopulateDataGrid(0);

            // setup buttons
            btnPrevious.Enabled = false;
            if (myResultCollection.Length() <= 10)
                btnNext.Enabled = false;
            
            // report total results and query time
            lblTotalResults.Text = "Total Results: " + myResultCollection.Length();
            lblQueryTime.Text = "Time to query: " + myIREngine.queryTime + " sec";

            //JournalAbstract toprank = myResultCollection.GetIRDocument(0) as JournalAbstract;
            //MessageBox.Show("Top doc is docID: " + toprank.DocID);

        }

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
            if (myResultCollection.Length() < resultEnd)
                resultEnd = myResultCollection.Length();

            // records to the datagrid
            for (int i = resultStart; i < resultEnd; i++)
            {
                string[] newRow = myResultCollection.GetIRDocument(i).GetResultSummary();
                dgSearchResults.Rows.Add(newRow);
            }

            // update text
            lblResultRange.Text = (resultStart + 1) + " - " + resultEnd;
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
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.OverwritePrompt = false;
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.ShowDialog();
            string fileName = saveFileDialog.FileName;

            if (fileName != "")
            {
                myIREngine.WriteEvalFile(fileName, topicID, myResultCollection);
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

            if (pageNumber * 10 + 10 > myResultCollection.Length())
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
