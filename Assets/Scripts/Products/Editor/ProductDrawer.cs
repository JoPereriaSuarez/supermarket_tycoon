using System;
using STycoon.Barcodes.Tools;
using STycoon.Products;
using Unity.Collections;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Producs.Editor
{
	[CustomPropertyDrawer(typeof(Product))]
	public class ProductDrawer : PropertyDrawer
	{
		private bool hasException = false;
		private TextElement debugText;
		private PropertyField barcodeField;

		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			debugText = new() { visible = hasException, };
			UpdateBarcode(property);
			VisualElement container = new();
			PropertyField codeField = new(property.FindPropertyRelative("code"));
			PropertyField typeField = new(property.FindPropertyRelative("type"));
			PropertyField brandIdField = new(property.FindPropertyRelative("brandId"));
			barcodeField = new(property.FindPropertyRelative("barcode"));
			PropertyField dimensionsField = new(property.FindPropertyRelative("dimensions"));
			PropertyField editorIDProperty = new(property.FindPropertyRelative("editor_id"));

			codeField.RegisterValueChangeCallback(_ => UpdateBarcode(property));
			brandIdField.RegisterValueChangeCallback(_ => UpdateBarcode(property));
			typeField.RegisterValueChangeCallback(_ => UpdateBarcode(property));

			Button randomCodeButton = new(() =>
			{
				Product product = (Product)property.boxedValue;
				product.code = Product.RandomizeCode();
				property.FindPropertyRelative("code").intValue = product.code;
				property.serializedObject.ApplyModifiedProperties();
			})
			{
				text = "Randomize"
			};
			container.Add(editorIDProperty);
			container.Add(codeField);
			container.Add(randomCodeButton);
			container.Add(typeField);
			container.Add(brandIdField);
			container.Add(barcodeField);
			container.Add(dimensionsField);
			container.Add(debugText);

			return container;
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 18 * 8;

		private void UpdateBarcode(SerializedProperty property)
		{
			Product product = (Product)property.boxedValue;
			if (product.code / 10_000 <= 0)
			{
				debugText.text = $"{product.code} is not a valid product's code";
				hasException = true;
				return;
			}

			if (product.brandId / 10_000 <= 0)
			{
				debugText.text = $"{product.brandId} is not a valid brand value";
				hasException = true;
				return;
			}

			try
			{
				product.barcode = BarcodeTools.Generate((byte)product.type, product.brandId, product.code);
				hasException = false;
				property.FindPropertyRelative("barcode").ulongValue = product.barcode;
				property.serializedObject.ApplyModifiedProperties();
			}
			catch (ArgumentException e)
			{
				hasException = true;
				debugText.text = e.Message;
			}
		}
	}
}