using System;
using System.Collections;

namespace STycoon.Products
{
	[Serializable]
	public struct Brand
	{
		internal class Comparer : IComparer
		{
			public int Compare(object x, object y)
			{
				if (ReferenceEquals(x, y))
					return 0;

				if (x is not Brand xBrand)
					throw new NullReferenceException(nameof(xBrand));
				if (y is not Brand yBrand)
					throw new NullReferenceException(nameof(yBrand));

				return xBrand.code.CompareTo(yBrand.code);
			}
		}

		public ushort code;
	}
}