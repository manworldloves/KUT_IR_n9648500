using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUT_IR_n9648500
{
    class JournalAbstract
    {
        private string docID;
        private string title;
        private string author;
        private string biblioInfo;
        private string words;

        public JournalAbstract(string document)
        {
            string[] delims = { ".I", ".T", ".A", ".B", ".W" };

            string[] docParts = document.Split(delims, StringSplitOptions.None);
            docID = docParts[1];
            title = docParts[2];
            author = docParts[3];
            biblioInfo = docParts[4];
            
            // title is also part of words - strip it out
            words = docParts[5].Substring(title.Length);
        }

        override public string ToString()
        {
            string outString = "";

            outString = outString + "DocId:\t\t" + docID + "\n";
            outString = outString + "Title:\t\t" + title + "\n";
            outString = outString + "Author:\t\t" + author + "\n";
            outString = outString + "Bibliographic Info:\t\t" + biblioInfo + "\n";
            outString = outString + "Abstract:\t\t" + words + "\n";

            return outString;
        }


    }
}
