using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis; // for Analyser
using Lucene.Net.Documents; // for Document and Field
using Lucene.Net.Index; //for Index Writer
using Lucene.Net.Store; //for Directory
using Lucene.Net.Search; // for IndexSearcher
using Lucene.Net.QueryParsers;  // for QueryParser

namespace KUT_IR_n9648500
{
    public class LuceneIREngine
    {
		Lucene.Net.Store.Directory luceneIndexDirectory;
		Lucene.Net.Analysis.Analyzer analyzer;
		Lucene.Net.Index.IndexWriter writer;
		Lucene.Net.Search.IndexSearcher searcher;
		Lucene.Net.QueryParsers.QueryParser parser;

        public float indexTime;

		const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
		const string TEXT_FN = "Text";

        // class constructor
        public LuceneIREngine()
        {
			luceneIndexDirectory = null;
			writer = null;
			//ISet<string> stopWords = StopAnalyzer.ENGLISH_STOP_WORDS_SET;
			//analyzer = new SnowballAnalyzer(VERSION, "English", stopWords);
			analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION);
			parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, TEXT_FN, analyzer);
        }

        // helper function for CreateIndex()
        // sets up lucene index ready for adding documents
		private void InitIndex(string indexPath)
		{
			luceneIndexDirectory = Lucene.Net.Store.FSDirectory.Open(indexPath);
			IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
			writer = new Lucene.Net.Index.IndexWriter(luceneIndexDirectory, analyzer, true, mfl);
			//writer.SetSimilarity(newSimilarity);
		}

        // helper function for CreateIndex()
        // adds document to the index
		private void IndexText(string fieldName, string text)
		{
			Lucene.Net.Documents.Field field = new Field(fieldName, text, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES);
			Lucene.Net.Documents.Document doc = new Document();
			doc.Add(field);
			writer.AddDocument(doc);
		}
        // helper funciton for CreateIndex()
		private void CleanUpIndex()
		{
			writer.Optimize();
			writer.Flush(true, true, true);
			writer.Dispose();
		}

        // opens all of the files in a list 
        // and puts the contents of each file into a list
        private List<string> OpenCollectionFiles(List<string> fileNames)
        {
        	List<string> documents = new List<string>();

        	foreach (string fn in fileNames)
        	{
        		string document = FileHandling.ReadTextFile(fn);
        		if (document != "")
        		{
        			documents.Add(document);
        		}
        		else
        		{
                    System.Windows.Forms.MessageBox.Show("Problem opening file:\n\n" + fn);
        		}
        	}

            return documents;
        }

        public int CreateIndex(string collectionPath, string indexPath)
        {
            // start timer...
            DateTime start = System.DateTime.Now;

            // get all of the files names in the collection path
            List<string> filenames = FileHandling.GetFileNames(collectionPath, false);

            // get the text from all of the files
            List<string> collectionText = OpenCollectionFiles(filenames);

            // turn the raw text into JournalAbstract objects
            List<JournalAbstract> collectionDocs = new List<JournalAbstract>();
			foreach (string text in collectionText)
			{
				collectionDocs.Add(new JournalAbstract(text));
			}

            // initialise the index
            InitIndex(indexPath);

            //loop through text collectionDocs and add to index
            foreach (JournalAbstract doc in collectionDocs)
            {
                IndexText("words", doc.GetWords());
            }

            // close the index
            CleanUpIndex();

            // end timer and calculate total time
            DateTime end = System.DateTime.Now;
            TimeSpan duration = end - start;
            indexTime = duration.Milliseconds;

            return 0;
        }
    }
}
