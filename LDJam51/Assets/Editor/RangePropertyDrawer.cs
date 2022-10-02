using UnityEngine;
using UnityEditor;
using System;

public class NumericRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.LabelField(position, label);
        SerializedProperty minValue = property.FindPropertyRelative("min");
        SerializedProperty maxValue = property.FindPropertyRelative("max");

        
        Rect p = new Rect(position) { width = position.width - EditorGUIUtility.labelWidth, x = position.x + EditorGUIUtility.labelWidth };
        float s = p.width / 8;

        Rect pLabel = new Rect(p) { width = s };
        Rect pNumField = new Rect(pLabel) { x = pLabel.x + pLabel.width, width = s * 3 };

        EditorGUI.LabelField(pLabel, "Min");
        EditorGUI.PropertyField(pNumField, minValue, GUIContent.none, false);


        pLabel = new Rect(pLabel) { x = pLabel.x + pLabel.width * 4, width = s };
        pNumField = new Rect(pLabel) { x = pLabel.x + pLabel.width, width = s * 3 };

        EditorGUI.LabelField(pLabel, "Max");
        EditorGUI.PropertyField(pNumField, maxValue, GUIContent.none, false);
    }
}

[CustomPropertyDrawer(typeof(IntRange))]
public class IntRangeDrawer : NumericRangeDrawer
{
}

[CustomPropertyDrawer(typeof(FloatRange))]
public class FloatRangeDrawer : NumericRangeDrawer
{
}


[CustomPropertyDrawer(typeof(XRangeAttribute))]
public class RangeDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float baseHeight = base.GetPropertyHeight(property, label);
        
        if (fieldInfo.FieldType == typeof(FloatRange) || fieldInfo.FieldType == typeof(IntRange))
            return baseHeight * 2;
        return baseHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        XRangeAttribute range = attribute as XRangeAttribute;
        // Now draw the property as a Slider or an IntSlider based on whether it's a float or integer.
        if (property.propertyType == SerializedPropertyType.Float)
            EditorGUI.Slider(position, property, range.min, range.max, label);
        else if (property.propertyType == SerializedPropertyType.Integer)
            EditorGUI.IntSlider(position, property, (int)range.min, (int)range.max, label);
        else
        {
            
            if (fieldInfo.FieldType == typeof(FloatRange))
            {
                position = new Rect(position) { height = position.height / 2 };

                SerializedProperty minValue = property.FindPropertyRelative("min");
                SerializedProperty maxValue = property.FindPropertyRelative("max");
                float min = minValue.floatValue, max = maxValue.floatValue;
                EditorGUI.BeginChangeCheck();
                EditorGUI.MinMaxSlider(position, label, ref min, ref max, range.min, range.max);
                if (EditorGUI.EndChangeCheck())
                {
                    minValue.floatValue = min;
                    maxValue.floatValue = max;
                }

                position = new Rect(position) { y = position.y + position.height, x = position.x + EditorGUIUtility.labelWidth, width = position.width - EditorGUIUtility.labelWidth };

                Rect pMinLabel = new Rect(position) { width = 30 };
                float slideWidth = position.width - pMinLabel.width;

                Rect pMaxLabel = new Rect(pMinLabel) { x = pMinLabel.x + slideWidth };

                float slidingLabelWidth = 30;
                float dX = slideWidth / (range.max - range.min);

                EditorGUI.LabelField(new Rect(pMinLabel) { x = pMinLabel.x + dX * (min - range.min), width = slidingLabelWidth }, $"{min:F2}");
                EditorGUI.LabelField(new Rect(pMinLabel) { x = Mathf.Min(pMinLabel.x + dX * (max - range.min), pMaxLabel.x - slidingLabelWidth + pMinLabel.width), width = slidingLabelWidth }, $"{max:F2}");

                if (dX * (min - range.min) > pMinLabel.width)
                    EditorGUI.LabelField(pMinLabel, $"{range.min}");
                if (pMinLabel.x + dX * (max - range.min) < (pMaxLabel.x - slidingLabelWidth))
                    EditorGUI.LabelField(pMaxLabel, $"{range.max}");

            } 
            else if (fieldInfo.FieldType == typeof(IntRange))
            {
                position = new Rect(position) { height = position.height / 2 };

                SerializedProperty minValue = property.FindPropertyRelative("min");
                SerializedProperty maxValue = property.FindPropertyRelative("max");
                float min = minValue.intValue, max = maxValue.intValue;
                EditorGUI.BeginChangeCheck();
                EditorGUI.MinMaxSlider(position, label, ref min, ref max, range.min, range.max);
                if (EditorGUI.EndChangeCheck())
                {
                    minValue.intValue = Mathf.RoundToInt(min);
                    maxValue.intValue = Mathf.RoundToInt(max);
                }

                position = new Rect(position) { y = position.y + position.height , x = position.x + EditorGUIUtility.labelWidth };

            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use Range with float or int, FloatRange or IntRange.");
            }
        }
    }
}