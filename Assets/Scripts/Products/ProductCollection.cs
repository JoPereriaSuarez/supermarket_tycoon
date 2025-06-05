using System;
using System.Linq;
using UnityEngine;

namespace STycoon.Products
{
	[CreateAssetMenu(menuName = "Design/Products/ProductCollection", fileName = "ProductCollection")]
	public class ProductCollection : ScriptableObject, ISortable
	{
		[SerializeField] private Product[] products;
		
		[ContextMenu(nameof(Sort))]
		public void Sort() => Array.Sort(products, new Product.Comparer());

		#if UNITY_EDITOR	
		public Product EDITOR_Find(string devID)
		{
			if (string.IsNullOrEmpty(devID))
				return default;
			return products.FirstOrDefault(p => p.editor_id.id.Value == devID);
		}
		#endif
	}
}