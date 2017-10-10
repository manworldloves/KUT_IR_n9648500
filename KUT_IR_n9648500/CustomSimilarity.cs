using System;
using Lucene.Net.Search;
using FieldInvertState = Lucene.Net.Index.FieldInvertState;

namespace KUT_IR_n9648500
{
	public class CustomSimilarity : DefaultSimilarity
	{
        /*public override float Coord(int a, int b)
		{
            return (float)(a / Math.Sqrt(b));
		}*/

        public override float Tf(float freq)
        {
            return freq;
        }

	}
}
