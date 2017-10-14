using System;
using System.Collections.Generic;
using System.Reflection;
using Lucene.Net.Search;
using Lucene.Net.Documents;
using System.Linq;

namespace KUT_IR_n9648500
{
    public abstract class IRDocument
    {
        protected int rank;
        protected float score;

        public int Rank { get { return rank; } set { rank = value; }}
        public float Score { get { return score; } set { score = value; } }

        public abstract void AddToIndex(Lucene.Net.Index.IndexWriter writer);
        public abstract IRQueryParams GetQueryParams();
        public abstract string[] GetResultSummary();
        public abstract Dictionary<string, float> GetResultSummaryColDetails();
        public abstract string GetDocID();

    }

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

        ///
        /// Update the custom doc constructors in these methods:
        /// 1. cstr IRCollection(IndexSearch, TopDocs)
        /// 2. IRDocument Add(string)
        /// 
        public IRCollection()
        {
            //default constructor
            collectionDocs = new List<IRDocument>();
        }

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

        public void AddDoc(IRDocument doc)
        {
            collectionDocs.Add(doc);
        }

        public void AddDocs(List<IRDocument> docs)
        {
            collectionDocs.AddRange(docs);
        }

        public IRDocument GetNewDoc(string docText)
        {
            IRDocument doc = JournalAbstract.JAParse(docText);

            return doc;
        }

        public IRQueryParams GetQueryParams()
        {
            return collectionDocs[0].GetQueryParams();
        }

        public IRDocument GetIRDocument(int index)
        {
            return collectionDocs[index];
        }

        public int Length()
        {
            return collectionDocs.Count;
        }

    }

}
