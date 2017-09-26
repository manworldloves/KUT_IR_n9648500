using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent; // for threading
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

        public string originalQuery = "";
        public string processedQuery = "";

        private TopDocs searchResults;
        private const int maxResults = 1400; // 1400 docs in provided collection

        public float indexTime;
        public float queryTime;

        // things to get from collection that is not indexed
        IRQueryParams queryParams;

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

        #region Index
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

            var parFilenames = fileNames.AsParallel();

            parFilenames.ForAll (fn =>
        	{
        		string document = FileHandling.ReadTextFile(fn);
        		if (document != "")
        		{
        			documents.Add(document);
        		}
        		else
        		{
                    MessageBox.Show("Problem opening file:\n\n" + fn);
        		}
            });

            return documents;
        }

        /// Builds the index...
        public int CreateIndex(string collectionPath, string indexPath)
        {
            // start timer...
            DateTime start = System.DateTime.Now;

            // get all of the files names in the collection path
            List<string> filenames = FileHandling.GetFileNames(collectionPath, false);

            // THREADING - try doing these two tasks in parallel
            // need 
            // get the text from all of the files
            //List<string> collectionText = OpenCollectionFiles(filenames);

            // turn the raw text into a Collection of objects
            //IRCollection collection = new IRCollection(collectionText);
            DateTime mid = System.DateTime.Now;

            // initialise the index
            InitIndex(indexPath);

            DateTime index1 = DateTime.Now;

			// build the index
			//collection.IndexCollection(writer);
			IRCollection collection = ReadAndProcessFiles(filenames);
            //collection.IndexCollection(writer);
			// get the query parameters of the collection (to be used later)
			queryParams = collection.GetQueryParams();

            DateTime index2 = DateTime.Now;

            // close the index
            CleanUpIndex();

            // end timer and calculate total time
            DateTime end = System.DateTime.Now;
            TimeSpan duration = end - start;
            indexTime = duration.Seconds + (float)duration.Milliseconds/1000;

            TimeSpan readSpan = mid - start;
            float midTime = readSpan.Seconds + (float)readSpan.Milliseconds / 1000;

            TimeSpan index1span = index1 - mid;
            float index1time = index1span.Seconds + (float)index1span.Milliseconds / 1000;

			TimeSpan index2span = index2 - index1;
			float index2time = index2span.Seconds + (float)index2span.Milliseconds / 1000;

            TimeSpan writeSpan = end - index2;
            float writeTime = writeSpan.Seconds + (float)writeSpan.Milliseconds / 1000;

            string timeText = "Time to read files: " + midTime +
                "\nTime to init index: " + index1time +
                "\nTime to build index: " + index2time +
                "\nTime to cleanup index: " + writeTime +
                "\nTotal time to create index: " + indexTime;

            System.Windows.Forms.MessageBox.Show(timeText);

            return 0;
        }
        #endregion

        #region Query
        /// helper function for RunQuery()
        // create the searcher object
        private void CreateSearcher()
		{
			searcher = new IndexSearcher(luceneIndexDirectory);
			//searcher.Similarity = newSimilarity;
		}

		/// helper function for RunQuery()
		// execute the query
		private TopDocs SearchText(string querytext)
		{

			//System.Console.WriteLine("Searching for " + querytext);
			querytext = querytext.ToLower();
			Query query = parser.Parse(querytext);

			TopDocs results = searcher.Search(query, maxResults);

            return results;
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
        public string PreprocessQuery(string origText)
        {
            //string[] origTokens = TextProcessing.TokeniseString(origText);

            List<string> origTokens = TextProcessing.TokeniseString(origText).ToList();

            // Query pre processing steps.
			// 1. remove stop words if required
			if (queryParams.RemoveStopWords)
			{
                origTokens = TextProcessing.RemoveStopWords(origTokens);
			}

            List<string> preprocTokens = origTokens;

			// 2. get ngrams if required
			int n = queryParams.NGrams;
			if (n > 1)
			{
				List<string> nGrams = TextProcessing.getNGrams(preprocTokens, n);
				float nGramBoost = queryParams.NGramBoost;
				List<string> nGramsWithBoost = AddBoostToStringArray(nGrams, nGramBoost);
                preprocTokens.AddRange(nGramsWithBoost);
			}

			// 3. get synonums if required
			if (queryParams.AddSynonyms == true)
			{
				List<string> synonyms = TextProcessing.getSynonyms(origTokens);
				float synBoost = queryParams.SynonymBoost;
				List<string> synWithBoost = AddBoostToStringArray(synonyms, synBoost);
                preprocTokens.AddRange(synWithBoost);
			}

            // 4. turn the list back to a string
            string preprocString = "";
            foreach (string token in preprocTokens)
            {
                preprocString += token + " ";
            }
            preprocString.Trim();

            return preprocString;
        }

        /// Executes the query.
        //  Preprocesses the query text entered by the user
        //  and queries the index.
        //  Calculates the total time to run the query
        //  and sets some text variables for later use.
        public int RunQuery(string text, bool preproc)
        {
			// start timer...
			DateTime start = System.DateTime.Now;

            originalQuery = text;

            CreateSearcher();

            // preprocess query
            if (preproc == true)
            {
                text = PreprocessQuery(text);
                processedQuery = text;
            }

			/// other options...
			// DefaultOperator - AND / OR
			// BooleanQuery - combine queries in different ways

			// get the query settings from the collection
			string[] queryFields = queryParams.Fields;
			float[] queryFieldBoosts = queryParams.FieldBoosts;

			// build field boost dictionary
			IDictionary<string, float> boosts = new Dictionary<string, float>();
			for (int i = 0; i < queryFields.Length; i++)
			{
				boosts.Add(queryFields[i], queryFieldBoosts[i]);
			}

            parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30,
                                               queryFields, analyzer, boosts);

            searchResults = SearchText(text);

			// end timer and calculate total time
			DateTime end = System.DateTime.Now;
			TimeSpan duration = end - start;
			queryTime = duration.Seconds + (float)duration.Milliseconds/1000;

            //MessageBox.Show("Time to query: " + queryTime + " seconds.");
            //MessageBox.Show("Number of results is " + searchResults.TotalHits);

            // dodgy results display
            //DisplaySearchResults(searchResults);

            CleanUpSearcher();

			return searchResults.TotalHits;
        }
#endregion

        /// Builds an IRCollection from the search results.
        //  This is used to display the search results.
        public IRCollection BuildResults()
		{
			CreateSearcher();

			IRCollection resultDocs = new IRCollection(searcher, searchResults);

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

        /*
        private IRCollection ReadAndProcessFiles1(List<string> fileNames)
        {
			IRCollection collection = new IRCollection();

            int midpoint = fileNames.Count / 2;
            List<string> fn1 = fileNames.GetRange(0, midpoint);
            List<string> fn2 = fileNames.GetRange(midpoint, fileNames.Count - midpoint);

            //Thread first = new Thread(new ThreadStart();

            return collection;
		}

        private IRCollection BuildIRCollection(List<string> filenames)
        {
            return new IRCollection(filenames);
        }*/

        private IRCollection ReadAndProcessFiles(List<string> fileNames)
        {
			IRCollection collection = new IRCollection();
            //var paraFN = fileNames.AsParallel();

            Parallel.ForEach(fileNames, fn =>
            {
                string docText = FileHandling.ReadTextFile(fn);
                IRDocument doc = new JournalAbstract(docText);
                doc.AddToIndex(writer);
                collection.Add(doc);

            });
                     
            return collection;
		}

        private IRCollection ReadAndProcessFiles1(List<string> fileNames)
        {
            IRCollection collection = new IRCollection();
            var paraFN = fileNames.AsParallel();

            // Our thread-safe collection used for the handover.
            var files = new BlockingCollection<string>();

            // Build the pipeline.
            var readFile = Task.Run(() =>
            {
                try
                {
                    foreach (string fn in fileNames)
                    //paraFN.ForAll(fn =>
                    //Parallel.ForEach(fileNames, fn =>
                    {
                        string document = FileHandling.ReadTextFile(fn);
                        if (document != "")
                        {
                            files.Add(document);
                        }
                        else
                        {
                            MessageBox.Show("Problem opening file:\n\n" + fn);
                        }
                    //});
                    }
                }
                finally
                {
                    files.CompleteAdding();
                }
            });

            var processFile = Task.Run(() =>
            {
                // Process lines on a ThreadPool thread
                // as soon as they become available.
                foreach (var file in files.GetConsumingEnumerable())
                {
                    //collection.Add(file);
                }
            });

            // Block until both tasks have completed.
            // This makes this method prone to deadlocking.
            // Consider using 'await Task.WhenAll' instead.
            Task.WaitAll(readFile, processFile);

            return collection;
        }
    }
}
