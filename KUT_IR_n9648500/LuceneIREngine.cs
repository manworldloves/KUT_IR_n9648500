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
using System.Windows.Forms;

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

        // things to get from collection that is not indexed
        IDictionary<string, float> queryParams;

		const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;

        /// class constructor
        public LuceneIREngine()
        {
			luceneIndexDirectory = null;
			writer = null;
			//ISet<string> stopWords = StopAnalyzer.ENGLISH_STOP_WORDS_SET;
			//analyzer = new SnowballAnalyzer(VERSION, "English", stopWords);
			analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION);
        }

        /// helper function for CreateIndex()
        // sets up lucene index ready for adding documents
		private void InitIndex(string indexPath)
		{
			luceneIndexDirectory = Lucene.Net.Store.FSDirectory.Open(indexPath);
			IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
			writer = new Lucene.Net.Index.IndexWriter(luceneIndexDirectory, analyzer, true, mfl);
			//writer.SetSimilarity(newSimilarity);
		}

        /// helper funciton for CreateIndex()
		private void CleanUpIndex()
		{
			writer.Optimize();
			writer.Flush(true, true, true);
			writer.Dispose();
		}

		/// helper funciton for CreateIndex()
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

            // turn the raw text into a Collection of objects
            IRCollection collection = new IRCollection(collectionText);

            // get the query parameters of the collection (to be used later)
            queryParams = collection.GetQueryParams();

            // initialise the index
            InitIndex(indexPath);

            // build the index
            collection.IndexCollection(writer);

            // close the index
            CleanUpIndex();

            // end timer and calculate total time
            DateTime end = System.DateTime.Now;
            TimeSpan duration = end - start;
            indexTime = duration.Milliseconds;

            return 0;
        }

        /// helper function for RunQuery()
        // create the searcher object
		private void CreateSearcher()
		{
			searcher = new IndexSearcher(luceneIndexDirectory);
			//searcher.Similarity = newSimilarity;
		}

		/// helper function for RunQuery()
		// execute the query
		private void SearchText(string querytext)
		{

			//System.Console.WriteLine("Searching for " + querytext);
			querytext = querytext.ToLower();
			Query query = parser.Parse(querytext);

			TopDocs results = searcher.Search(query, 100);
			MessageBox.Show("Number of results is " + results.TotalHits);
			int rank = 0;
			foreach (ScoreDoc scoreDoc in results.ScoreDocs)
			{
				rank++;
				Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
				string myFieldValue = doc.Get("title").ToString();

                string msgText = "Rank " + rank + " Title: " + myFieldValue +
								  ". Score: (" + scoreDoc.Score + ")";

                var keepBrowsing = MessageBox.Show(msgText, "Results", 
                                                   MessageBoxButtons.OKCancel, 
                                                   MessageBoxIcon.Information);

                if (keepBrowsing == DialogResult.Cancel) break;

				//Explanation exp = searcher.Explain(query, scoreDoc.Doc);
				//Console.WriteLine(exp);

			}
		}

		/// helper function for RunQuery()
		// closes the index after searching
		private void CleanUpSearcher()
		{
			searcher.Dispose();
		}

        public void RunQuery(string text)
        {
            CreateSearcher();

            // get the query settings from the collection
            var test = queryParams.Keys;
            string[] queryFields = queryParams.Keys.ToArray();

            /// other options...
            // DefaultOperator - AND / OR
            // BooleanQuery - combine queries in different ways

            parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, 
                                               queryFields, analyzer, queryParams);

            SearchText(text);

            CleanUpSearcher();
        }
    }
}
