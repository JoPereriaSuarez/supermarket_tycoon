using System;
using UnityEngine;

namespace STycoon.Products
{
	[CreateAssetMenu(menuName = "Design/Products", fileName = "ProductInfoCollection")]
	public class ProductInfoCollection : ScriptableObject, ISortable
	{
		public ProductInfo[] products;

		[ContextMenu(nameof(Sort))]
		public void Sort() => Array.Sort(products, new ProductInfo.Comparer());
	}
}