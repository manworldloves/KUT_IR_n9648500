using System;
using System.Collections.Generic;
using System.Reflection;

namespace KUT_IR_n9648500
{
    
    abstract class IRDocument
    {
        public abstract void AddToIndex(Lucene.Net.Index.IndexWriter writer);
    }

    class IRCollection
    {
        private List<IRDocument> collectionDocs;

        public IRCollection(List<String> collectionText)
        {
            List<IRDocument> collection = new List<IRDocument>();
            foreach (string text in collectionText)
            {
                // choose which type of source doc type here eg JournalAbstract
                // change the constructor used here for other doc types.
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
    }


}
