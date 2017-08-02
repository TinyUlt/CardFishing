using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using GtMsg;

public class CreatePath : MonoBehaviour {

    public GameObject copyObj;
    public Vector3 offset;
    public bool foldX;
    public bool foldY;
    public bool foldZ;
    public float speedScale = 1f;
    

    [HideInInspector]
    public Transform[] paths;
    [HideInInspector]
    public Vector3[] vec3s;
	//路径点个数
	public int pointCount;


	[HideInInspector]
	public List<Vector3> pathListVec3;
    [HideInInspector]
    public List<int> nodeFrame;


    [HideInInspector]
	public List<ActionData> idleList;

    [HideInInspector]
    [SerializeField]
	void Start()
	{
		
	}
    public void ffff(Transform tran, bool setLine)
    {
        var count = tran.childCount;
        vec3s = new Vector3[count];
        if (setLine)
        {
            paths = new Transform[count];
        }
        
        for (int i = 0; i < count; i++)
        {
            var t = tran.GetChild(i);
            if (setLine)
            {
                paths[i] = t;
            }

            var pos = t.position;
            if (foldX)
            {
                pos.x = 960 - pos.x;
            }
            if (foldY)
            {
                pos.y = 640 - pos.y;
            }
            if (foldZ)
            {
                pos.z = -pos.z;
            }
            pos.x += offset.x;
            pos.y += offset.y;
            pos.z += offset.z;

            vec3s[i] = pos;
            
        }
    }
    public void DrawPath()
    {
        //在scene视图中绘制出路径与线
        //iTween.DrawLine(paths, Color.yellow);
        if (copyObj != null)
        {
            ffff(copyObj.transform, false);
        }
        else
        {
            ffff(transform, true);
        }

        if(vec3s.Length > 1)
        {
            iTween.DrawPath(vec3s, Color.red);
            Gizmos.color = Color.yellow;
            for (int i = 0; i < vec3s.Length; i++)
            {
                Gizmos.DrawWireSphere(vec3s[i], 10f);

            }
        }
       
    }
    public void OnCreatePath()
    {
        if (pointCount <= 0)
        {
            Debug.LogError("pointCount is 0");
            return;
        }
        pathListVec3 = new List<Vector3>();
        nodeFrame = new List<int>();
        idleList = new List<ActionData>();

        
        iTween.CreatePath(ref pathListVec3,ref nodeFrame, pointCount, paths);

        for (int i = 0; i < paths.Length; i++)
        {
            var child = paths[i];

            var idle = child.GetComponent<Idle>();

            if (idle != null)
            {
                idleList.Add(new ActionData { ActionFrame = nodeFrame[i], PlayIdleIndex = idle.IdleIndex });
            }

        }
    }
}