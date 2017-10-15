using Lucene.Net.Search;

namespace KUT_IR_n9648500
{
	public class CustomSimilarity : DefaultSimilarity
	{
        // Tf normally returns sqrt(freq)
        // This is not required for this application
        // as the document sizes are relatively similar so
        // freq does not have a wide range of values
        public override float Tf(float freq)
        {
            return freq;
        }

	}
}
