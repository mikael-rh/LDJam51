using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(PrefabOnlyAttribute))]
public class PrefabOnlyDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Now draw the property as a Slider or an IntSlider based on whether it's a float or integer.
        if (property.propertyType != SerializedPropertyType.ObjectReference)
        {
            EditorGUI.LabelField(position, label.text, "Use \"Prefab\" attribute with GameObject fields only!");
            return;
        }
        property.objectReferenceValue = EditorGUI.ObjectField(position, label.text, property.objectReferenceValue, typeof(GameObject), false);
        
    }
}