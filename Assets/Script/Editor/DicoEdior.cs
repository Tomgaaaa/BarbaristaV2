using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class DicoEdior : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Texture iconBoisson = Resources.Load<Texture>( "test");

        SerializedProperty key = property.FindPropertyRelative("key");
        SerializedProperty value = property.FindPropertyRelative("value");

        Rect rectKey = new Rect (position.x, position.y, position.width / 2, position.height);
        Rect rectValue = new Rect (position.x + position.width / 2 + 10, position.y, position.width / 2, position.height);

        string e = "ratio";
        EditorGUI.PropertyField(rectKey, key, label);
        EditorGUI.PropertyField(rectValue, value, new GUIContent(e,iconBoisson));
    }
}
