using System;
using System.Collections.Generic; // used for List<> object
namespace KUT_IR_n9648500
{
    public static class InfoNeeds
    {
        // Parses a standard information needs file
        public static Dictionary<string, string> GetInfoNeeds(string fileName)
        {
            Dictionary<string, string> iNeeds = new Dictionary<string, string>();

            // open file and dump into a string
            string document = FileHandling.ReadTextFile(fileName);

            // split string based on ".I" and ".D" delimiters
            string[] delims = { ".I", ".D" };
            string[] docParts = document.Split(delims, StringSplitOptions.RemoveEmptyEntries);

            if (docParts.Length > 2)
            {
                // build dicationary from string array
                for (int i = 0; i < docParts.Length; i++)
                {
                    iNeeds.Add(docParts[i].Trim(), docParts[i + 1].Trim());

                    // inc i so that it goes up 2 each iteration
                    i++;
                }

                return iNeeds;
            }
            else
            {
                return null;
            }
            
            
        }
    }
}
