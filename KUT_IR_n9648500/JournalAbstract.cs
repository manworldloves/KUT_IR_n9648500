using System;
using System.Collections.Generic; // used for List<> object
using Lucene.Net.Documents;

namespace KUT_IR_n9648500
{
    class JournalAbstract : IRDocument
    {
        private string docID;
        private string title;
        private string author;
        private string biblioInfo;
        private string words;

		public string DocID { get { return docID; } }
		public string Title { get { return title; } }
		public string Author { get { return author; } }
		public string BiblioInfo { get { return biblioInfo; } }
		public string Words { get { return words; } }

        public JournalAbstract()
        {
            // default constructor
        }

        public JournalAbstract(string[] docParts)
        {
            docID = docParts[0];
            title = docParts[1];
            author = docParts[2];
            biblioInfo = docParts[3];

            // title is also part of words - strip it out
            docParts[4] = docParts[4].Substring(title.Length+1);
            words = docParts[4].Trim();
        }

        // parses the input string and checks for correct structure
        public static JournalAbstract JAParse(string docText)
        {
            string[] delims = { ".I", ".T", ".A", ".B", ".W" };

            string[] docParts = docText.Split(delims, StringSplitOptions.RemoveEmptyEntries);

            // strip end of line chars from the fields
            for (int i = 0; i < docParts.Length-1; i++)
            {
                docParts[i] = docParts[i].Trim();
            }

            // check that the doc was structure correctly and has a docID that is an int
            int numParts = docParts.Length;
            int num = 0;
            if (numParts >= 5 && int.TryParse(docParts[0], out num))
            {
                return new JournalAbstract(docParts);    
            }
            else
            {
                return null;
            }
        }

        // addes the record to the index
        public override void AddToIndex(Lucene.Net.Index.IndexWriter writer)
        {
            // Custom add to index method for JournalAbstract class
            Field fieldID = new Field("docID", docID, Field.Store.YES,
                                            Field.Index.NOT_ANALYZED, Field.TermVector.NO);
            Field fieldTitle = new Field("title", title, Field.Store.NO,
                                            Field.Index.ANALYZED, Field.TermVector.NO);
            Field fieldAuthor = new Field("author", author, Field.Store.NO,
                                            Field.Index.NOT_ANALYZED, Field.TermVector.NO);
            Field fieldBibInfo = new Field("biblioInfo", biblioInfo, Field.Store.NO,
                                            Field.Index.NOT_ANALYZED, Field.TermVector.NO);
            Field fieldWords = new Field("words", words, Field.Store.NO,
                                            Field.Index.ANALYZED, Field.TermVector.NO);

            // add boosts
            fieldTitle.Boost = 5;

            // create document
            Document doc = new Document();
            doc.Add(fieldID);
            doc.Add(fieldTitle);
            doc.Add(fieldAuthor);
            doc.Add(fieldBibInfo);
            doc.Add(fieldWords);

            writer.AddDocument(doc);
        }

        // use the title as a query suggestion
        public override string GetQuerySuggestion()
        {
            return title;
        }

        // set the query parameters for this IRDocument type
        public override IRQueryParams GetQueryParams()
        {
            string[] fields = { "title", "words" };
            float[] fieldBoost = { 1.0f, 1.0f };
            bool removeStopWords = false;
            int nGrams = 3;
            float nGramBoost = 0.2f;
            bool addSynonyms = false;
            float synonymBoost = 0.5f;

            IRQueryParams querySettings = new IRQueryParams(fields, fieldBoost,
                                                           removeStopWords,
                                                           nGrams, nGramBoost,
                                                            addSynonyms, synonymBoost);

            return querySettings;
        }

        // provide the column names and size for results view
        public override Dictionary<string, float> GetResultSummaryColDetails()
        {
            Dictionary<string, float> colDetails = new Dictionary<string, float>();

            colDetails.Add("Title", 0.3f);
            colDetails.Add("Author", 0.1f);
            colDetails.Add("Bibliographic Info", 0.1f);
            colDetails.Add("Abstract", 0.5f);

            return colDetails;
        }

        // provide the results summary info for results view
        public override string[] GetResultSummary()
        {
            return new string[] { title, author, biblioInfo, TextProcessing.GetFirstSentence(words) };
        }

        // returns the docID
		public override string GetDocID()
		{
            return docID;
		}

        // override of ToString function - used for testing
        override public string ToString()
        {
            string outString = "";

            outString = outString + "DocId:\t\t" + docID + "\n";
            outString = outString + "Title:\t\t" + title + "\n";
            outString = outString + "Author:\t\t" + author + "\n";
            outString = outString + "Bibliographic Info:\t\t" + biblioInfo + "\n";
            outString = outString + "Abstract:\t\t" + words + "\n";

            return outString;
        }
    }
}
