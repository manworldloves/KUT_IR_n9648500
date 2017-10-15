using System.Collections.Generic; // used for List<> object
using Lucene.Net.Search;
using Lucene.Net.Documents;


namespace KUT_IR_n9648500
{
    // abstract class to enable abstraction of document types
    public abstract class IRDocument
    {
        protected int rank;
        protected float score;

        public int Rank { get { return rank; } set { rank = value; }}
        public float Score { get { return score; } set { score = value; } }

        public abstract void AddToIndex(Lucene.Net.Index.IndexWriter writer);
        public abstract IRQueryParams GetQueryParams();
        public abstract string GetQuerySuggestion();
        public abstract string[] GetResultSummary();
        public abstract Dictionary<string, float> GetResultSummaryColDetails();
        public abstract string GetDocID();

    }

    // class of query parameters that might be used
    public class IRQueryParams
    {
        private string[] fields;
        private float[] fieldBoosts;
        private bool removeStopWords;
        private int nGrams;
        private float nGramBoost;
        private bool addSynonyms;
        private float synonymBoost;

		public string[] Fields { get { return fields; } }
        public float[] FieldBoosts { get { return fieldBoosts; }}
        public bool RemoveStopWords { get { return removeStopWords; }}
        public int NGrams { get { return nGrams; }}
        public float NGramBoost { get { return nGramBoost; }}
        public bool AddSynonyms { get { return addSynonyms; }}
        public float SynonymBoost { get { return synonymBoost; }}

        public IRQueryParams(string[] fs, float[] fboosts, bool remSW,
                            int ngs, float ngboost, bool addsyn, float synboost)
        {
            fields = fs;
            fieldBoosts = fboosts;
            removeStopWords = remSW;
            nGrams = ngs;
            nGramBoost = ngboost;
            addSynonyms = addsyn;
            synonymBoost = synboost;
        }
	}

    public class IRCollection
    {
        private List<IRDocument> collectionDocs;

        public IRCollection()
        {
            //default constructor
            collectionDocs = new List<IRDocument>();
        }

        // this is used to build an IRCollection from an original IRCollection 
        // and a set of results
		public IRCollection(IRCollection origCollection, IndexSearcher searcher, TopDocs results)
		{
			List<IRDocument> resultCollection = new List<IRDocument>();

			int rank;
			float score;
			for (int i = 0; i < results.TotalHits; i++)
			{
				rank = i + 1;
				score = results.ScoreDocs[i].Score;
				Document doc = searcher.Doc(results.ScoreDocs[i].Doc);
                string docID = doc.Get("docID");
                IRDocument newDoc = origCollection.collectionDocs.Find(x => x.GetDocID() == docID);
                newDoc.Rank = rank;
                newDoc.Score = score;
				resultCollection.Add(newDoc);

				collectionDocs = resultCollection;
			}
		}

        // Add one IRDocument to the collection
        public void AddDoc(IRDocument doc)
        {
            collectionDocs.Add(doc);
        }

        // Add a set of IRDocuments to the collection
        public void AddDocs(List<IRDocument> docs)
        {
            collectionDocs.AddRange(docs);
        }

        // returns the query parameters for the IRDocument type
        public IRQueryParams GetQueryParams()
        {
            return collectionDocs[0].GetQueryParams();
        }

        // returns the query suggestions for the IRDocument type
        public string[] GetQuerySuggestions()
        {
            string[] qSugs = new string[collectionDocs.Count];
            
            for (int i = 0; i < collectionDocs.Count-1; i++)
            {
                qSugs[i] = collectionDocs[i].GetQuerySuggestion();
            }

            return qSugs;
        }

        // returns the results table setup info
        public Dictionary<string, float> GetResultSummaryColDetails()
        {
            if (collectionDocs.Count > 0)
                return collectionDocs[0].GetResultSummaryColDetails();
            else
                return null;
        }

        // returns an IRDocument at an index
        public IRDocument GetIRDocument(int index)
        {
            return collectionDocs[index];
        }

        // returns the number of IRDocuments in the collection
        public int Length()
        {
            return collectionDocs.Count;
        }

    }

}
