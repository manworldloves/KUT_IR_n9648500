using System;
using System.Collections.Generic;
using System.Reflection;
using Lucene.Net.Search;
using Lucene.Net.Documents;

namespace KUT_IR_n9648500
{
    public abstract class IRDocument
    {
        protected int rank;
        protected float score;

        public int Rank { get { return rank; } }
        public float Score { get { return score; } }

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

        
        /// <summary>
        /// Update the custom doc type in the constructors
        /// </summary>
        /// <param name="searcher">Searcher.</param>
        /// <param name="results">Results.</param>
        public IRCollection(IndexSearcher searcher, TopDocs results)
        {
            List<IRDocument> collection = new List<IRDocument>();

            int rank;
            float score;
            for (int i = 0; i < results.TotalHits; i++)
            {
                rank = i + 1;
                score = results.ScoreDocs[i].Score;
                Document doc = searcher.Doc(results.ScoreDocs[i].Doc);
                collection.Add(new JournalAbstract(doc, rank, score));

                collectionDocs = collection;
            }
        }


        public IRCollection(List<String> collectionText)
        {
            List<IRDocument> collection = new List<IRDocument>();
            foreach (string text in collectionText)
            {
                collection.Add(new JournalAbstract(text));
            }

            collectionDocs = collection;
        }

        public void IndexCollection(Lucene.Net.Index.IndexWriter writer)
        {
            foreach (IRDocument doc in collectionDocs)
            {
                doc.AddToIndex(writer);
            }    
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


        public void BuildResults(IndexSearcher searcher, TopDocs results)
        {
            int rank;
            float score;
            for (int i = 0; i < results.TotalHits; i++)
            {
                rank = i + 1;
                score = results.ScoreDocs[i].Score;
                Document doc = searcher.Doc(results.ScoreDocs[i].Doc);
                collectionDocs.Add(new JournalAbstract(doc, rank, score));
            }
        }
    }

}
