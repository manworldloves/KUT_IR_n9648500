using System;
using System.Collections.Generic; // used for List<> object
using System.Linq; // used for colleciton conversion ie. ToList()

namespace KUT_IR_n9648500
{
    public static class TextProcessing
    {
        // split the string into tokens
        public static List<string> TokeniseString(string text)
		{
			char[] delims = { ' ', '(', ')', '[', ']', '{', '}', '<', '>',
				'-', ',', '.', '\'', '\"', ':', ';', '?', '!' ,'\n', '\r', '\t'};

			// put text in lower case
			string lc_text = text.ToLower();

			// split input string into string array of tokens
			string[] tokens = lc_text.Split(delims, StringSplitOptions.RemoveEmptyEntries);

			return tokens.ToList();
		}

        // finds the first sentence of an input string
        public static string GetFirstSentence(string paragraph)
        {
            char[] delims = { '.' };
            string firstSentence = paragraph.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0];
            firstSentence = firstSentence.TrimStart(new char[] { ' ' }) + '.';
            return firstSentence;
        }

        // create N-Grams from query input text
		public static List<string> getNGrams(List<string> tokens, int n)
		{
			//error checking so that n is between 0 and length of tokens
            if (n < 1 || n > tokens.Count)
			{
				System.Console.WriteLine("Invalid n value. Must be between 0 and length of input string.");
				return new List<string>() { "\"" + string.Join(" ", tokens) + "\"" };
			}

            List<string> nGrams = new List<string>();

            while (n > 1)
			{
				for (int i = 0; i < (tokens.Count - n + 1); i++)
				{
                    string tempToken = "\"";

                    for (int j = 0; j < n; j++)
					{
                        tempToken += tokens[i+j] + " ";
					}
                    tempToken = tempToken.Trim() + "\"";

                    nGrams.Add(tempToken);
				}

                n--;

			}

			return nGrams;
		}
    }
}
