using System.IO;
using STycoon.Barcodes.Tools;
using STycoon.Products;
using STycoon.Utils;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Producs.Editor
{
	[CustomPropertyDrawer(typeof(ProductInfo))]
	internal class ProductInfoDrawer : PropertyDrawer
	{
		private const string BARCODES_PATH = "Assets/Desing/BarcodesImages";
		private const string PRODUCTS_PATH = "Assets/Desing/ProductCollection.asset";

		private static readonly ProductCollection products;
		private Product product;
		private bool isProductValid;
		private VisualElement container;
		private PropertyField codeField;
		private PropertyField nameField;
		private PropertyField descriptionField;
		private PropertyField barcodeImageField;
		private PropertyField prefabField;
		private Button createBarcodeButton;
		private SerializedProperty codeProperty;
		
		static ProductInfoDrawer()
		{
			products = AssetDatabase.LoadAssetAtPath<ProductCollection>(PRODUCTS_PATH);
		}
		
		
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			container = new();
			SerializedProperty devIDProperty = property.FindPropertyRelative("devID");
			codeProperty = property.FindPropertyRelative("barcode");
			PropertyField devIDField = new(devIDProperty);
			FindProduct(devIDProperty.boxedValue.ToString());
			devIDField.RegisterValueChangeCallback(evt =>
			{
				DevID devID = (DevID)evt.changedProperty.boxedValue;
				FindProduct(devID.ToString());
				codeField.visible = isProductValid;
				nameField.visible = isProductValid;
				descriptionField.visible = isProductValid;
				barcodeImageField.visible = isProductValid;
				prefabField.visible = isProductValid;
				createBarcodeButton.visible = isProductValid;

				codeProperty.ulongValue = product.barcode;
				property.serializedObject.ApplyModifiedProperties();
			});

			createBarcodeButton = new(CreateBarcodeImage)
			{
				text = "Generate Image",
				visible = isProductValid
			};
			codeField = new(property.FindPropertyRelative("barcode"))
			{
				enabledSelf = false,
				visible = isProductValid,
			};
			((ProductInfo)property.boxedValue).EDITOR_SetCode(product.barcode);
			property.serializedObject.ApplyModifiedProperties();
			nameField = new(property.FindPropertyRelative("name"))
			{
				visible = isProductValid
			};
			descriptionField = new(property.FindPropertyRelative("description"))
			{
				visible = isProductValid
			};
			barcodeImageField = new(property.FindPropertyRelative("barcodeImage"))
			{
				// enabledSelf = false,
				visible = isProductValid
			};
			prefabField = new(property.FindPropertyRelative("prefab"))
			{
				visible = isProductValid
			};
			
			container.Add(devIDField);	
			container.Add(codeField);
			container.Add(nameField);
			container.Add(descriptionField);
			container.Add(barcodeImageField);
			container.Add(createBarcodeButton);
			container.Add(prefabField);
			return container;
		}

		// //FIXME: This Unity method is not being called
		// public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		// {
		// 	int amount = isProductValid ? 3 : 2;
		// 	return 18 * amount;
		// }
		private void FindProduct(string devID)
		{
			product = products.EDITOR_Find(devID);
			isProductValid = product.IsValid();
		}
		private void CreateBarcodeImage()
		{
			if (!isProductValid)
			{
				Debug.LogError("[ERROR] Cannot Validate barcode");
				return;
			}

			string path = Path.Combine(BARCODES_PATH, $"{product.barcode}.png");
			if (!BarcodeTools.GenerateSprite(path, product.barcode))
				Debug.LogError("[ERROR] Cannot Create Barcode Sprite");
		}

		
	}
}