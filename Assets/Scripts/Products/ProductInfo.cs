using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization;
using Utils;

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

				return xInfo.barcode.CompareTo(yInfo.barcode);
			}
		}

		#if UNITY_EDITOR
		[SerializeField] private STycoon.Utils.DevID devID;
		#endif
		
		[SerializeField] private ulong barcode;
		[SerializeField] private LocalizedString name;
		[SerializeField] private LocalizedString description;
		[SerializeField] private AssetReference barcodeImage;
		[SerializeField] private AssetReference prefab;

		public ulong ID => barcode;
		public string GetName() => name.GetLocalizedString();
		public string GetDescription() => description.GetLocalizedString();
		// public Texture2D Barcode => barcode;
		// public GameObject Prefab => prefab;

		#if UNITY_EDITOR
		public void EDITOR_SetCode(ulong barcode) => this.barcode = barcode;
		#endif
	}
}