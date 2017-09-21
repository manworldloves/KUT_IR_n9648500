using System;
using System.Collections.Generic;
using System.Reflection;
using Lucene.Net.Search;
using Lucene.Net.Documents;

namespace KUT_IR_n9648500
{
    public interface IRDocument
    {
        void AddToIndex(Lucene.Net.Index.IndexWriter writer);
        Dictionary<string, float> GetQueryParams();
        string[] GetResultSummary();
        string[] GetResultSummaryColNames();

    }

    public class IRCollection
    {
        private List<IRDocument> collectionDocs;

        /// <summary>
        /// Builds a custom document and returns it.
        /// Change the constructor below. eg JournalAbstract
        /// </summary>
        /// <returns>The document type.</returns>
        /// <param name="text">Text.</param>
        private IRDocument CustomDocType(string text)
        {
            return new JournalAbstract(text);
        }


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
                collection.Add(CustomDocType(text));
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

        public Dictionary<string, float> GetQueryParams()
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
