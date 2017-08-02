using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TestCreatePathEditorData : MonoBehaviour {
    [ContextMenu("Do")]
	void CreatePathEditorData()
    {
        var children = GetComponentsInChildren<CreatePath>();
        SinglePathEditorMgr mSinglePathMgr = new SinglePathEditorMgr();
        mSinglePathMgr.Paths = new List<SinglePathEditorData>();
        foreach (var createPath in children)
        {
            SinglePathEditorData data = new SinglePathEditorData();
            data.pathName = createPath.gameObject.name;

            data.offset = createPath.offset;
            data.foldX = createPath.foldX;
            data.foldY = createPath.foldY;
            data.foldZ = createPath.foldZ;
            data.speedScale = createPath.speedScale;
            data.pointCount = createPath.pointCount;

            if (createPath.copyObj != null)
            {
                data.copySource = createPath.copyObj.name;
            }
            else
            {

                for(int i = 0; i < createPath.transform.childCount; i++)
                {
                    data.pathPoints.Add(createPath.transform.GetChild(i).position);
                }
            }
            mSinglePathMgr.Paths.Add(data);
        }
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(mSinglePathMgr, "Assets/FishTools/SinglePathConfig.asset");
        AssetDatabase.SaveAssets();
#endif
    }
}
