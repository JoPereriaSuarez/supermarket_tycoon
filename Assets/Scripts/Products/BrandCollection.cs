using UnityEngine;

namespace STycoon.Products
{
	[CreateAssetMenu(menuName = "Design/Brands", fileName = "BrandCollection")]
	public class BrandCollection : ScriptableObject
	{
		[SerializeField] private Brand[] brand;
	}
}