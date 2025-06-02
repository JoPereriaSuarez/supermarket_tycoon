using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization;

namespace STycoon.Products
{
	[Serializable]
	public class ProductInfo
	{
		internal class Comparer : IComparer
		{
			public int Compare(object x, object y)
			{
				if (ReferenceEquals(x, y))
					return 0;

				if (x is not ProductInfo xInfo)
					throw new NullReferenceException(nameof(xInfo));
				if (y is not ProductInfo yInfo)
					throw new NullReferenceException(nameof(yInfo));

				return xInfo.code.CompareTo(yInfo.code);
			}
		}

		[SerializeField] private ushort code;
		[SerializeField] private LocalizedString name;
		[SerializeField] private LocalizedString description;
		[SerializeField] private Texture2D texture;
		[SerializeField] private Texture2D barcode;
		[SerializeField] private GameObject prefab;

		public ushort ID => code;
		public string GetName() => name.GetLocalizedString();
		public string GetDescription() => description.GetLocalizedString();
		public Texture2D Texture => texture;
		public Texture2D Barcode => barcode;
		public GameObject Prefab => prefab;
	}
}