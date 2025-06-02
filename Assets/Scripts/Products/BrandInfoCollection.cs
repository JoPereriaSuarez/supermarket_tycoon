using System;
using System.Linq;
using UnityEngine;

namespace STycoon.Products
{
	[CreateAssetMenu(menuName = "Design/Brands", fileName = "BrandInfoCollection")]
	public class BrandInfoCollection : ScriptableObject, ISortable
	{
		public BrandInfo[] brands;

		[ContextMenu(nameof(Sort))]
		public void Sort() => Array.Sort(brands, new BrandInfo.Comparer());
		
		
		#if UNITY_EDITOR
		public ushort EDITOR_GetId(string editorName)
		{
			BrandInfo result = brands
				.FirstOrDefault(b => string.Equals(b.EditorName(), editorName, StringComparison.OrdinalIgnoreCase)) ;
			if (result == null)
				return 0;
			return result.ID;
		}
		#endif
	}
}