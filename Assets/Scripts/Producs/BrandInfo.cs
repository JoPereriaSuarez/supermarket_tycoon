using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization;

namespace STycoon.Products
{
	[Serializable]
	public class BrandInfo
	{
		internal class Comparer : IComparer
		{
			public int Compare(object x, object y)
			{
				if (ReferenceEquals(x, y))
					return 0;

				BrandInfo xBrand = (BrandInfo)x;
				BrandInfo yBrand = (BrandInfo)y;
				if (ReferenceEquals(xBrand, null))
					return -1;
				if (ReferenceEquals(yBrand, null))
					return 1;

				return xBrand.id.CompareTo(yBrand.id);
			}
		}

		[SerializeField] private ushort id;
		[SerializeField] private LocalizedString title;
		[SerializeField] private LocalizedString description;
		[SerializeField] private Texture2D texture;

		public ushort ID => id;
		public string Title() => title.GetLocalizedString();
		public string Description() => description.GetLocalizedString();
		public Texture2D Texture => texture;

	}
}