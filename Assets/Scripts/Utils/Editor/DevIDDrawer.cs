using Unity.Collections;
using UnityEditor;
using UnityEngine.UIElements;

namespace Utils.Editor
{
	[CustomPropertyDrawer(typeof(DevID))]
	internal class DevIDDrawer : PropertyDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			VisualElement container = new();

			TextField textField = new("EDITOR ID")
			{
				tooltip = "ID for internal usage is not compiled into the game. Only works on EDITOR"
			};
			FixedString32Bytes value = (FixedString32Bytes)property.FindPropertyRelative("id").boxedValue;
			textField.SetValueWithoutNotify(value.ToString());
			textField.maxLength = 15;
			textField.RegisterValueChangedCallback(evt =>
			{
				string value = evt.newValue.Replace(' ', '_');
				value = value.ToLower();
				textField.SetValueWithoutNotify(value);
				property.FindPropertyRelative("id").boxedValue = new FixedString32Bytes(value);
				property.serializedObject.ApplyModifiedProperties();
			});

			container.Add(textField);
			return container;
		}
	}
}