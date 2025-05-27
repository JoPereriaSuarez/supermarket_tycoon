using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Utils.Editor
{
	[CustomPropertyDrawer(typeof(ReadonlyAttribute))]
	internal class ReadonlyPropertyDrawer : PropertyDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			VisualElement container = new();

			PropertyField field = new(property) { enabledSelf = false };
			container.Add(field);
			
			return container;
		}
	}
}