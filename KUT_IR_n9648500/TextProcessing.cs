using System;
using System.Collections.Generic;
using System.Linq;

namespace KUT_IR_n9648500
{
    public class TextProcessing
    {
        // This class is really just for query pre-processing
        public TextProcessing()
        {

        }

        // split the string into tokens
		public static List<string> TokeniseString(string text)
		{
			char[] delims = { ' ', '(', ')', '[', ']', '{', '}', '<', '>',
				'-', ',', '.', '\'', '\"', ':', ';', '?', '!' };

			// put text in lower case
			string lc_text = text.ToLower();

			// split input string into string array of tokens
			string[] tokens = lc_text.Split(delims, StringSplitOptions.RemoveEmptyEntries);

			return tokens.ToList();
		}

        // create N-Grams from query input text
		public static List<string> getNGrams(List<string> tokens, int n)
		{
			//error checking so that n is between 0 and length of tokens
            if (n < 1 || n > tokens.Count)
			{
				System.Console.WriteLine("Invalid n value. Must be between 0 and length of input string.");
				return new List<string>();
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

        // get synonyms from query text
        public static List<string> getSynonyms(List<string> words)
        {
            List<string> synonyms = new List<string> { "gesicht" };

            return synonyms;
        }

        // remove stop words and small words from  token list
		public static List<string> RemoveStopWords(List<string> tokens)
		{
			// init stop words
			string[] stopWords = { "a", "an", "and", "are", "as", "at", "be", "but", "by",
				"for", "if", "in", "into", "is", "it", "no", "not", "of", "on", "or", "such",
				"that", "the", "their", "then", "there", "these", "they", "this", "to", "was",
				"will", "with" , "you", "nobody", "like"};


			// work backwards through list and remove any stopwords
            // or words less than 3 characters
			for (int i = tokens.Count - 1; i >= 0; i--)
			{
				if (stopWords.Contains(tokens[i]))
				{
					tokens.RemoveAt(i);
				}
				else if (tokens[i].Length < 3)
				{
					tokens.RemoveAt(i);
				}
			}

			return tokens;
		}
    }
}
