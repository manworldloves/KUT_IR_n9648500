using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Documents;

namespace KUT_IR_n9648500
{
    class JournalAbstract : IRDocument
    {
        // from source
        private string docID;
        private string title;
        private string author;
        private string biblioInfo;
        private string words;

        public JournalAbstract(string document)
        {
            string[] delims = { ".I", ".T", ".A", ".B", ".W" };

            string[] docParts = document.Split(delims, StringSplitOptions.RemoveEmptyEntries);

            // strip end of line chars from the fields
            for (int i = 0; i < 4; i++)
            {
                docParts[i] = docParts[i].Trim();
            }

            docID = docParts[0];
            title = docParts[1];
            author = docParts[2];
            biblioInfo = docParts[3];

            // title is also part of words - strip it out
            docParts[4] = docParts[4].Substring(title.Length+1);
            words = docParts[4].Trim();
        }

        public JournalAbstract(Document doc, int rank, float score)
        {
            docID = doc.Get("docID");
            title = doc.Get("title");
            author = doc.Get("author");
            biblioInfo = doc.Get("biblioInfo");
            words = doc.Get("words");
            this.rank = rank;
            this.score = score;
        }

        public string DocID { get { return docID; } }
        public string Title { get { return title; } }
        public string Author { get { return author; } }
        public string BiblioInfo { get { return biblioInfo; } }
        public string Words { get { return words; } }

        public override void AddToIndex(Lucene.Net.Index.IndexWriter writer)
        {
            // Custom add to index method for JournalAbstract class
            Field fieldID = new Field("docID", docID, Field.Store.YES,
                                            Field.Index.NOT_ANALYZED, Field.TermVector.NO);
            Field fieldTitle = new Field("title", title, Field.Store.YES,
                                            Field.Index.ANALYZED, Field.TermVector.NO);
            Field fieldAuthor = new Field("author", author, Field.Store.YES,
                                            Field.Index.NOT_ANALYZED, Field.TermVector.NO);
            Field fieldBibInfo = new Field("biblioInfo", biblioInfo, Field.Store.YES,
                                            Field.Index.NOT_ANALYZED, Field.TermVector.NO);
            Field fieldWords = new Field("words", words, Field.Store.YES,
                                            Field.Index.ANALYZED, Field.TermVector.YES);

            Document doc = new Document();
            doc.Add(fieldID);
            doc.Add(fieldTitle);
            doc.Add(fieldAuthor);
            doc.Add(fieldBibInfo);
            doc.Add(fieldWords);

            writer.AddDocument(doc);
        }

        public override Dictionary<string, float> GetQueryParams()
        {
            Dictionary<string, float> querySettings = new Dictionary<string, float> { };

            querySettings.Add("title", 2.5f);
            querySettings.Add("words", 1.0f);

            return querySettings;
        }

        public override string[] GetResultSummaryColNames()
        {
            return new string[] { "Title", "Author", "Bibliographic Info", "Abstract" };
        }
        
        public override string[] GetResultSummary()
        {
            return new string[] { title, author, biblioInfo, GetFirstSentence(words) };
        }

        private string GetFirstSentence(string paragraph)
        {
            char[] delims = { '.' };
            string firstSentence = paragraph.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0];
            firstSentence = firstSentence.TrimStart(new char[] { ' ' }) + '.';
            return firstSentence;
        }

		public override string GetDocID()
		{
            return docID;
		}

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
