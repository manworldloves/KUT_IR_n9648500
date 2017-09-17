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
        private string docID;
        private string title;
        private string author;
        private string biblioInfo;
        private string words;

        public JournalAbstract(string document)
        {
            string[] delims = { ".I", ".T", ".A", ".B", ".W" };

            string[] docParts = document.Split(delims, StringSplitOptions.None);
            docID = docParts[1];
            title = docParts[2];
            author = docParts[3];
            biblioInfo = docParts[4];
            
            // title is also part of words - strip it out
            words = docParts[5].Substring(title.Length);
        }

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
