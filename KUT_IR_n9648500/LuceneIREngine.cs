using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis; // for Analyser
using Lucene.Net.Analysis.Snowball;
using Lucene.Net.Documents; // for Document and Field
using Lucene.Net.Index; //for Index Writer
using Lucene.Net.Store; //for Directory
using Lucene.Net.Search; // for IndexSearcher
using Lucene.Net.QueryParsers;  // for QueryParser
using System.Windows.Forms;
using System.IO; // for file copy
using System.Diagnostics; // for running trec_eval

namespace KUT_IR_n9648500
{
    public class LuceneIREngine
    {
        Lucene.Net.Store.Directory luceneIndexDirectory;
        Lucene.Net.Analysis.Analyzer analyzer;
        Lucene.Net.Index.IndexWriter writer;
        Lucene.Net.Search.IndexSearcher searcher;
        Lucene.Net.QueryParsers.QueryParser parser;
        Similarity mySimilarity;

        private IRCollection myCollection;
        private TopDocs searchResults;
        private int maxResults = 0; // this is set when the collection is built

        public float indexTime;
        public float queryTime;

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;

        /// class constructor
        public LuceneIREngine()
        {
            luceneIndexDirectory = null;
            writer = null;
            ISet<string> stopWords = StopAnalyzer.ENGLISH_STOP_WORDS_SET;
            analyzer = new SnowballAnalyzer(VERSION, "English", stopWords);
            mySimilarity = new CustomSimilarity();
        }

        #region Index
        /// helper function for CreateIndex()
        // sets up lucene index ready for adding documents
        private void InitIndex(string indexPath)
        {
            luceneIndexDirectory = Lucene.Net.Store.FSDirectory.Open(indexPath);
            IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
            writer = new Lucene.Net.Index.IndexWriter(luceneIndexDirectory, analyzer, true, mfl);
            writer.SetSimilarity(mySimilarity);
        }

        /// helper funciton for CreateIndex()
		private void CleanUpIndex()
        {
            writer.Optimize();
            writer.Flush(true, true, true);
            writer.Dispose();
        }

        /// Helper function for CreateIndex
        /// This function implements threading to improve indexing speed.
        /// For each filename the method:
        /// 1. Read the file and store in a string.
        /// 2. Turn the text into IRDocument object and add to collection
        /// 3. Add the IRDocument to the index
        private IRCollection ReadAndProcessFiles(List<string> fileNames)
        {
            IRCollection collection = new IRCollection();

            // Lists are not thread safe so...
            // 1. need to create an array for the docs
            // 2. convert the array to a list
            // 3. add the list to the collection
            int docIndex;
            int numDocs = fileNames.Count;
            IRDocument[] docArray = new IRDocument[numDocs];

            Parallel.ForEach(fileNames, fn =>
            {
                string docText = FileHandling.ReadTextFile(fn);
                IRDocument doc = collection.GetNewDoc(docText);
                docIndex = int.Parse(doc.GetDocID())-1;
                docArray[docIndex] = doc;
                doc.AddToIndex(writer);
            });

            List<IRDocument> docList = docArray.ToList();

            collection.AddDocs(docList);
            maxResults = docList.Count;

            return collection;
        }

        /// Builds the index...
        public int CreateIndex(string collectionPath, string indexPath)
        {
            // start timer...
            DateTime start = DateTime.Now;

            // get all of the files names in the collection path
            List<string> filenames = FileHandling.GetFileNames(collectionPath, false);

            // initialise the index
            InitIndex(indexPath);

            // build the index
            // this method call does lots of things in parallel
            myCollection = ReadAndProcessFiles(filenames);

            // close the index
            CleanUpIndex();

            // end timer and calculate total time
            DateTime end = DateTime.Now;
            TimeSpan duration = end - start;
            indexTime = duration.Seconds + (float)duration.Milliseconds / 1000;

            return myCollection.Length();
        }
        #endregion

        #region Query
        /// helper function for RunQuery()
        // create the searcher object
        private void CreateSearcher()
        {
            searcher = new IndexSearcher(luceneIndexDirectory);
            searcher.Similarity = mySimilarity;
        }

        /// helper function for RunQuery()
        // closes the index after searching
        private void CleanUpSearcher()
        {
            searcher.Dispose();
        }

        private List<string> AddBoostToStringArray(List<string> tokens, float boost)
        {
            List<string> outputTokens = new List<string>();
            foreach (string token in tokens)
            {
                outputTokens.Add(token + '^' + boost);
            }

            return outputTokens;
        }

        // Method to take users query text as input
        // and does various things to it to produce
        // the actual text that is input to the searcher
        public Query PreprocessQuery(string origText, QueryParser parser)
        {
            // builds a boolean query
            // partA is just the original query text
            string partA = origText;

            // partB is bi- and tri-grams built from the original text
            // build ngrams
            int ngram_num = 3;
            List<string> tokens = TextProcessing.TokeniseString(origText);
            List<string> ngrams = TextProcessing.getNGrams(tokens, ngram_num);
            string partB = string.Join(" ", ngrams);

            // Build BooleanQuery
            BooleanQuery bQuery = new BooleanQuery();

            Query queryA = parser.Parse(partA);
            Query queryB = parser.Parse(partB);

            bQuery.Add(queryA, Occur.MUST);
            bQuery.Add(queryB, Occur.MUST);

            return bQuery;
        }

        /// Executes the query.
        //  Preprocesses the query text entered by the user
        //  and queries the index.
        //  Calculates the total time to run the query
        //  and sets some text variables for later use.
        public int RunQuery(string text, bool preproc, out string qText)
        {
            // start timer...
            DateTime start = DateTime.Now;

            // get the query settings from the collection
            IRQueryParams queryParams = myCollection.GetQueryParams();
            string[] queryFields = queryParams.Fields;
            float[] queryFieldBoosts = queryParams.FieldBoosts;

            // build field boost dictionary
            IDictionary<string, float> boosts = new Dictionary<string, float>();
            for (int i = 0; i < queryFields.Length; i++)
            {
                boosts.Add(queryFields[i], queryFieldBoosts[i]);
            }

            // setup query and searcher
            CreateSearcher();
            Query query;
            parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30,
                                    queryFields, analyzer, boosts);
            
            // preprocess query (if required)
            if (preproc == true)
            {
                query = PreprocessQuery(text, parser);
            }
            else
            {
				// no preprocessing
                query = parser.Parse(text);
			}

            // print query text to form
            qText = query.ToString();

            // execute the search
			searchResults = searcher.Search(query, maxResults);

            // end timer and calculate total time
            DateTime end = DateTime.Now;
            TimeSpan duration = end - start;
            queryTime = duration.Seconds + (float)duration.Milliseconds / 1000;

            CleanUpSearcher();

            return searchResults.TotalHits;
        }
        #endregion

        /// Builds an IRCollection from the search results.
        //  This is used to display the search results.
        public IRCollection BuildResults()
        {
            // rewrite this to use IRCollection object rather than index
            CreateSearcher();

            IRCollection resultDocs = new IRCollection(myCollection, searcher, searchResults);

            CleanUpSearcher();

            return resultDocs;
        }

        /// Writes a trec evaluation file from the search results.
        public int WriteEvalFile(string fileName, string topicID, IRCollection results)
        {
            List<string> evalList = new List<string>();

            bool appendFlag = true;
            // check if the file exists
            if (System.IO.File.Exists(fileName) == true)
            {
                // prompt for append
                DialogResult append = MessageBox.Show("Do you want to append to the existing file?",
                                                      "Confirm",
                                                      MessageBoxButtons.YesNo);

                if (append == DialogResult.Yes)
                {
                    appendFlag = true;
                }
                else
                {
                    // if overwrite confirm
                    DialogResult ruSure = MessageBox.Show("Are you sure you want to overwrite the file?",
                                                          "Confirm",
                                                          MessageBoxButtons.YesNo);
                    if (ruSure == DialogResult.Yes)
                    {
                        appendFlag = false;
                    }
                }
            }

            // this is fixed
            string groupName = "09648500_NathanOnly";

            // structure TopicID QO DocID rank score group
            string tempString = "";
            for (int i = 0; i < results.Length(); i++)
            {
                IRDocument doc = results.GetIRDocument(i);
                tempString = topicID + "\tQ0\t";
                tempString += doc.GetDocID() + "\t";
                tempString += doc.Rank + "\t";
                tempString += doc.Score + "\t";
                tempString += groupName + "\n";

                evalList.Add(tempString);
            }

            // write file
            FileHandling.WriteTextFile(evalList, fileName, appendFlag);

            return 0;
        }

		// this is for testing only
		public void AutoResults (string filename, Dictionary<string, string> queries, bool preproc)
        {
			string dontcare = "";

			bool appendFlag = false;
            foreach (KeyValuePair<string,string> q in queries)
            {
                // execute query
                string topicID = q.Key;
                RunQuery(q.Value, preproc, out dontcare);

                // get results
                IRCollection results = BuildResults();

				// write to file
				string groupName = "09648500_NathanOnly";

                List<string> evalList = new List<string>();

				// structure TopicID QO DocID rank score group
				string tempString = "";
				for (int i = 0; i < results.Length(); i++)
				{
					IRDocument doc = results.GetIRDocument(i);
					tempString = topicID + "\tQ0\t";
					tempString += doc.GetDocID() + "\t";
					tempString += doc.Rank + "\t";
					tempString += doc.Score + "\t";
					tempString += groupName + "\n";

					evalList.Add(tempString);
				}

				// write file
				FileHandling.WriteTextFile(evalList, filename, appendFlag);

                appendFlag = true;
            }

            string trecpath = "../../../../results/";
            if (File.Exists(trecpath + Path.GetFileName(filename)))
            {
                File.Delete(trecpath + Path.GetFileName(filename));
            }

            File.Move(filename, trecpath + Path.GetFileName(filename));

            // from MSDN
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = trecpath + "trec_eval";
            p.StartInfo.Arguments = "-q " +trecpath + "cranqrel.txt " + trecpath + "autoquery_results.txt";
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            Console.WriteLine(output);
        }
    }
}