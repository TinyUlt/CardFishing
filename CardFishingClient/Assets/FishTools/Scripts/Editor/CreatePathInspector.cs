using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// [CustomEditor(typeof(CreatePath))]
 public class CreatePathInspector : Editor {
//     bool m_hasCopyParent = false;
//     Object m_sourceParent;
// 
//     CreatePath _target;
// 
//     bool ShowPathPoints = false;
// 
//     bool NeedRefreshPointName = false;
// 
//     protected  void OnEnable()
//     {
//         _target = target as CreatePath;
//     }
// 
     public override void OnInspectorGUI()
     {
//         //base.OnInspectorGUI();
//          serializedObject.Update();
//        var CopyFromSource = DrawProperty("拷贝源", serializedObject, "CopyFromSource");
//         if (CopyFromSource.boolValue)
//         {
//             DrawProperty("目标源", serializedObject, "copyObj");
// 
//         }
//         DrawProperty("偏移值", serializedObject, "offset");
//         DrawProperty("反转X", serializedObject, "foldX");
//         DrawProperty("反转Y", serializedObject, "foldY");
//         DrawProperty("反转Z", serializedObject, "foldZ");
//         DrawProperty("速度缩放", serializedObject, "speedScale");
//         if (!CopyFromSource.boolValue)
//         {
//             DrawProperty("路径点数量", serializedObject, "pointCount");
// 
//         }
        //         if(CopyFromSource.boolValue == false && _target.copyObj != null)
        //         {
        //             _target.copyObj = null;
        //             //UnityEditor.EditorUtility.SetDirty(this);
        //         }
        //         {
        //             DrawProperty("偏移值", serializedObject, "offset");
        //             DrawProperty("反转X", serializedObject, "foldX");
        //             DrawProperty("反转Y", serializedObject, "foldY");
        //             DrawProperty("反转Z", serializedObject, "foldZ");
        //             DrawProperty("速度缩放", serializedObject, "speedScale");
        //         }
        //         if (CopyFromSource.boolValue)
        //         {
        //             SerializedProperty sp = DrawProperty("源路径", serializedObject, "copyObj");
        //            
        //         }
        //         else
        //         {
        //             //DrawProperty("路径点", serializedObject, "paths");
        //            
        //             ShowPathPoints = EditorGUILayout.Foldout(ShowPathPoints, "路径点");
        //             if (ShowPathPoints)
        //             {
        //                 SerializedProperty sp = serializedObject.FindProperty("paths");
        //                 for (int i = 0; i < sp.arraySize; i++)
        //                 {
        //                     //EditorGUILayout.ObjectField(sp.GetArrayElementAtIndex(i));
        //                     
        //                     
        //                     EditorGUILayout.BeginHorizontal();
        //                     EditorGUILayout.LabelField(i.ToString() + ". ", GUILayout.Width(20));
        //                     SerializedProperty element = sp.GetArrayElementAtIndex(i);
        //                     EditorGUILayout.PropertyField(element, GUIContent.none, GUILayout.Width(300));
        //                     //EditorGUILayout.LabelField("元素" + i.ToString()+": ");
        //                     bool insert = GUILayout.Button("+");
        //                     if (insert)
        //                     {
        //                         sp.InsertArrayElementAtIndex(i);
        //                         serializedObject.ApplyModifiedProperties();
        //                         GameObject go = new GameObject("路径点."+ (sp.arraySize - 1).ToString());
        //                         go.transform.SetParent(_target.transform);
        //                         go.transform.localPosition = _target.paths[i + 1].localPosition;
        //                         _target.paths[i] = go.transform;
        //                         go.transform.SetSiblingIndex(i);
        // 
        //                     }
        //                     bool del = GUILayout.Button("-");
        //                     if (del)
        //                     {
        //                         Transform t = element.objectReferenceValue as Transform;
        //                         element.objectReferenceValue = null;
        //                         sp.DeleteArrayElementAtIndex(i);
        //                         if(t != null)
        //                         {
        //                             GameObject.DestroyImmediate(t.gameObject);
        //                         }
        // 
        //                     }
        //                     if (i != 0)
        //                     {
        //                         
        //                         bool moveUp = GUILayout.Button("↑");
        //                         if (moveUp)
        //                         {
        //                             SerializedProperty upelement = sp.GetArrayElementAtIndex(i-1);
        //                             
        //                             //((Transform)element.objectReferenceValue).name = "路径点." + (i - 1).ToString();
        //                             //((Transform)upelement.objectReferenceValue).name = "路径点." + (i).ToString();
        //                             ((Transform)element.objectReferenceValue).SetSiblingIndex(i - 1);
        //                             sp.MoveArrayElement(i, i - 1);
        //                         }
        //                     }
        //                     
        //                     if (i != sp.arraySize - 1)
        //                     {
        //                         bool moveDown = GUILayout.Button("↓");
        //                         if (moveDown)
        //                         {
        //                             SerializedProperty downelement = sp.GetArrayElementAtIndex(i + 1);
        //                             
        //                             //((Transform)element.objectReferenceValue).name = "路径点." + (i + 1).ToString();
        //                             //((Transform)downelement.objectReferenceValue).name = "路径点." + (i).ToString();
        //                             ((Transform)element.objectReferenceValue).SetSiblingIndex(i + 1);
        //                             sp.MoveArrayElement(i, i + 1);
        //                         }
        //                     }
        //                     EditorGUILayout.EndHorizontal();
        //                     EditorGUILayout.Space();
        //                 }
        //                 bool AddPoint = GUILayout.Button("增加路径点");
        //                 if (AddPoint)
        //                 {
        //                     sp.InsertArrayElementAtIndex(sp.arraySize);
        //                     serializedObject.ApplyModifiedProperties();
        //                     GameObject go = new GameObject("路径点."+(sp.arraySize -1).ToString());
        //                     go.transform.SetParent(_target.transform);
        //                     go.transform.localPosition = _target.paths[_target.paths.Length - 2].localPosition;
        //                     _target.paths[_target.paths.Length - 1] = go.transform;
        //                 }
        //             }
        //             DrawProperty("路径点数量", serializedObject, "pointCount");
        //         }
        // 
        // 
                serializedObject.ApplyModifiedProperties();
        // 
    }
// 
    static public SerializedProperty DrawProperty(string label, SerializedObject serializedObject, string property, params GUILayoutOption[] options)
    {
        return DrawProperty(label, serializedObject, property, false, options);
    }

    static public SerializedProperty DrawProperty(string label, SerializedObject serializedObject, string property, bool padding, params GUILayoutOption[] options)
    {
        SerializedProperty sp = serializedObject.FindProperty(property);
        
        if (sp != null)
        {

            if (padding) EditorGUILayout.BeginHorizontal();

            if (label != null) EditorGUILayout.PropertyField(sp, new GUIContent(label), options);
            else EditorGUILayout.PropertyField(sp, options);

            if (padding)
            {
                EditorGUILayout.EndHorizontal();
            }
        }
        return sp;
    }
 }
