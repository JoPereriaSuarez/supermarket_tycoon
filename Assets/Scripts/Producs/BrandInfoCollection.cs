using System;
using UnityEngine;

namespace STycoon.Products
{
	[CreateAssetMenu(menuName = "Design/Brands", fileName = "BrandInfoCollection")]
	public class BrandInfoCollection : ScriptableObject, ISortable
	{
		public BrandInfo[] brands;

		[ContextMenu(nameof(Sort))]
		public void Sort() => Array.Sort(brands, new BrandInfo.Comparer());
	}
}