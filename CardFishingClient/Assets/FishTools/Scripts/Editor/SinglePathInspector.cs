using GtMsg;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SinglePathInspector {

    bool mToggleHasSource;
    bool mShowPathPoints;
    string mCopySource = "";

    Vector3 BornPosition = new Vector3(0, 0, 0);

    SinglePathEditorData mSelectPath;

    GameObject PathRoot;
    Transform PathParent;
    List<Transform> pathPointsTrans = new List<Transform>();
    Vector3[] pathPointsVec3;

    public void OnDestroy()
    {
        GameObject.DestroyImmediate(PathRoot);
        GameObject.DestroyImmediate(PathParent.gameObject);
    }

    public void OnSelectOnePath(SinglePathEditorData selectPath)
    {
        
        mSelectPath = selectPath;
        for (int i = 0; i < pathPointsTrans.Count; i++)
        {
            if(pathPointsTrans[i] != null)
            {
                GameObject.DestroyImmediate(pathPointsTrans[i].gameObject);
            }
           
        }
        pathPointsTrans.Clear();
        for (int i = 0; i < selectPath.pathPoints.Count; i++)
        {
            InsertOnePoint(pathPointsTrans.Count, selectPath.pathPoints[i]);
        }
    }

    public void SetGizmosPoints()
    {

        //pathPointsVec3 = new Vector3[pathPointsTrans.Count];
        for (int i = 0; i < mSelectPath.pathPoints.Count; i++)
        {
            
            var pos = pathPointsTrans[i].position;
            if (mSelectPath.foldX)
            {
                pos.x = 960 - pos.x;
            }
            if (mSelectPath.foldY)
            {
                pos.y = 640 - pos.y;
            }
            if (mSelectPath.foldZ)
            {
                pos.z = -pos.z;
            }
            pos.x += mSelectPath.offset.x;
            pos.y += mSelectPath.offset.y;
            pos.z += mSelectPath.offset.z;

            mSelectPath.pathPoints[i] = pos;
        }
    }

    public void DrawPathInScene()
    {
        SetGizmosPoints();
        if (mSelectPath != null && mSelectPath.pathPoints.Count > 1)
        {
            iTween.DrawPath(mSelectPath.pathPoints.ToArray(), Color.red);
            Gizmos.color = Color.blue;
            for (int i = 0; i < mSelectPath.pathPoints.Count; i++)
            {
                Gizmos.DrawWireSphere(mSelectPath.pathPoints[i], 4f);

            }
        }
    }

    public void DrawSinglePathInspector(SinglePathEditorData selectPath)
    {
        if (selectPath != null)
        {
            selectPath.pathName = EditorGUILayout.TextField("路径名", selectPath.pathName);
            EditorGUILayout.LabelField("描述");
            selectPath.description = EditorGUILayout.TextArea(selectPath.description, GUILayout.Height(50));
            //SerializedProperty CopyFromSource = DrawProperty("从源路径拷贝", serializedObject, "CopyFromSource");
            mToggleHasSource = GUILayout.Toggle(mToggleHasSource, "从源路径拷贝");

            if (mToggleHasSource)
            {
                mCopySource = EditorGUILayout.TextField("拷贝源",mCopySource);
                //GUILayout.ve
                //SerializedProperty sp = DrawProperty("源路径", serializedObject, "copyObj");
                //if (sp != null && sp.objectReferenceValue != null)
                //{
                //    DrawProperty("偏移值", serializedObject, "offset");
                //    DrawProperty("反转X", serializedObject, "foldX");
                //    DrawProperty("反转Y", serializedObject, "foldY");
                //    DrawProperty("反转Z", serializedObject, "foldZ");
                //    DrawProperty("速度缩放", serializedObject, "speedScale");
                //}
            }
            else
            {
                //DrawProperty("路径点", serializedObject, "paths");

                mShowPathPoints = EditorGUILayout.Foldout(mShowPathPoints, "路径点");
                if (mShowPathPoints)
                {
                    //SerializedProperty sp = serializedObject.FindProperty("paths");
                    int length = selectPath.pathPoints.Count;
                    for (int i = 0; i < length; i++)
                    {
                        //EditorGUILayout.ObjectField(sp.GetArrayElementAtIndex(i));


                        EditorGUILayout.BeginHorizontal();

                        if (GUILayout.Button("路径点"+ i.ToString(), GUILayout.Width(150)))
                        {
                            Selection.activeObject = pathPointsTrans[i];
                        }
                        //EditorGUILayout.LabelField("元素" + i.ToString()+": ");
                        bool insert = GUILayout.Button("+");
                        if (insert)
                        {
                            Vector3 newPoint = selectPath.pathPoints[i];
                            selectPath.pathPoints.Insert(i,newPoint);

                            InsertOnePoint(i, newPoint);

                        }
                        bool del = GUILayout.Button("-");
                        if (del)
                        {
                            selectPath.pathPoints.RemoveAt(i);
                            GameObject.DestroyImmediate(pathPointsTrans[i].gameObject);
                            pathPointsTrans.RemoveAt(i);
                            length -= 1;
                        }
                        if (i != 0)
                        {

                            bool moveUp = GUILayout.Button("↑");
                            if (moveUp)
                            {
                                Vector3 tempV = selectPath.pathPoints[i];
                                selectPath.pathPoints[i] = selectPath.pathPoints[i - 1];
                                selectPath.pathPoints[i - 1] = tempV;
                                Transform tempT = pathPointsTrans[i];
                                pathPointsTrans[i] = pathPointsTrans[i - 1];
                                pathPointsTrans[i - 1] = tempT;
                            }
                        }

                        if (i != selectPath.pathPoints.Count - 1)
                        {
                            bool moveDown = GUILayout.Button("↓");
                            if (moveDown)
                            {
                                Vector3 tempV = selectPath.pathPoints[i];
                                selectPath.pathPoints[i] = selectPath.pathPoints[i + 1];
                                selectPath.pathPoints[i + 1] = tempV;
                                Transform tempT = pathPointsTrans[i];
                                pathPointsTrans[i] = pathPointsTrans[i + 1];
                                pathPointsTrans[i + 1] = tempT;
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                    }
                    bool AddPoint = GUILayout.Button("增加路径点");
                    if (AddPoint)
                    {
                        Vector3 newPoint = selectPath.pathPoints.Count == 0 ? BornPosition : selectPath.pathPoints[selectPath.pathPoints.Count - 1];
                        selectPath.pathPoints.Add(newPoint);

                        InsertOnePoint(selectPath.pathPoints.Count, newPoint);
                    }
                }
                selectPath.pointCount = EditorGUILayout.IntField("路径点数量", selectPath.pointCount);
                //DrawProperty("路径点数量", serializedObject, "pointCount");
            }


        }
    }

    void InsertOnePoint(int insertIndex,Vector3 pos)
    {
        if (PathRoot == null)
        {
            PathRoot = GameObject.CreatePrimitive(PrimitiveType.Quad);
            PathRoot.name = "PathRoot";
            PathRoot.transform.localScale = new Vector3(960, 640);
            PathRoot.transform.position = new Vector3(480,320);
            PathRoot.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
            Material m = PathRoot.GetComponent<MeshRenderer>().sharedMaterial;
            m.shader = Shader.Find("Sprites/Diffuse");
            m.SetColor("_Color", new Color(0, 0, 0, 0.2f));
            GizmosDraw drawGizmo = PathRoot.AddComponent<GizmosDraw>();
            drawGizmo.CallGizmosDraw = DrawPathInScene;

            GameObject parent = new GameObject("PathParent");
            parent.hideFlags = HideFlags.HideInHierarchy;
            PathParent = parent.transform;
            //PathParent.SetParent(PathRoot.transform);

        }
        GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);//  new GameObject("Point");
        point.hideFlags = HideFlags.DontSave;
        point.transform.SetParent(PathParent.transform);
        point.transform.localScale = Vector3.one * 5;
        point.transform.position = pos;
        if(insertIndex < pathPointsTrans.Count)
        {
            pathPointsTrans.Insert(insertIndex, point.transform);
        }
        else
        {
            pathPointsTrans.Add(point.transform);
        }
        
    }
}
