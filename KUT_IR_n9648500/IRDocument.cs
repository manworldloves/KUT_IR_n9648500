using System;
using System.Collections.Generic;
using System.Reflection;

namespace KUT_IR_n9648500
{
    
    abstract class IRDocument
    {
        public abstract void AddToIndex(Lucene.Net.Index.IndexWriter writer);
        public abstract IDictionary<string, float> GetQueryParams();
    }

    class IRCollection
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

        public IDictionary<string, float> GetQueryParams()
        {
            return collectionDocs[0].GetQueryParams();
        }
    }


}
