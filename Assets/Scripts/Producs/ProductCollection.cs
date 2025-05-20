using System;
using UnityEngine;

namespace STycoon.Products
{
	[CreateAssetMenu(menuName = "Design/Products", fileName = "ProductCollection")]
	public class ProductCollection : ScriptableObject, ISortable
	{
		[SerializeField] private Product[] products;
		
		[ContextMenu(nameof(Sort))]
		public void Sort() => Array.Sort(products, new Product.Comparer());
	}
}