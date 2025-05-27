using System.Collections.Generic;
using System.Linq;
using STycoon.Products;
using STycoon.Products.Utils;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Producs.Editor
{
	[CustomPropertyDrawer(typeof(BrandAttribute))]
	internal class BrandPropertyDrawer : PropertyDrawer
	{
		private const string ASSET_PATH = "Assets/Desing/BrandInfoCollection.asset";
		private static readonly BrandInfoCollection collection;
		
		static BrandPropertyDrawer()
		{
			collection = AssetDatabase.LoadAssetAtPath<BrandInfoCollection>(ASSET_PATH);
			if (collection == null)
			{
				BrandInfoCollection obj = ScriptableObject.CreateInstance<BrandInfoCollection>();
				AssetDatabase.CreateAsset(obj, ASSET_PATH);
				collection = AssetDatabase.LoadAssetAtPath<BrandInfoCollection>(ASSET_PATH);
			}
		}
		
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			VisualElement container = new();

			List<string> brands = collection.brands.Select(b => b.EditorName()).ToList();
			DropdownField field = new(property.displayName, brands, 0);
			field.RegisterValueChangedCallback(evt =>
			{
				property.intValue = collection.EDITOR_GetId(evt.newValue);
				property.serializedObject.ApplyModifiedProperties();
			});
			PropertyField propertyField = new(property) { enabledSelf = false };
			
			container.Add(field);
			container.Add(propertyField);
			return container;
		}
	}
}