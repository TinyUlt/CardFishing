using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePathEditorMgr : ScriptableObject
{
    
    public List<SinglePathEditorData> Paths;
    
}

[System.Serializable]
public class SinglePathEditorData
{
    [HideInInspector]
    public string pathName = "NewPath";
    [HideInInspector]
    public string description = "";
    [HideInInspector]
    public string copySource;
    [HideInInspector]
    public Vector3 offset;
    [HideInInspector]
    public bool foldX;
    [HideInInspector]
    public bool foldY;
    [HideInInspector]
    public bool foldZ;
    [HideInInspector]
    public float speedScale = 1f;
    [HideInInspector]
    public List<Vector3> pathPoints = new List<Vector3>();

    
    //路径点个数
    [HideInInspector]
    public int pointCount;

    //public List<ActionData> idleList;
    [HideInInspector]
    public bool CopyFromSource;

    [HideInInspector]
    [NonSerialized]
    public List<Transform> pathPointsTrans = new List<Transform>();

    [HideInInspector]
    [NonSerialized]
    public List<Vector3> pathListVec3;

    [HideInInspector]
    [NonSerialized]
    public List<int> nodeFrame;
}