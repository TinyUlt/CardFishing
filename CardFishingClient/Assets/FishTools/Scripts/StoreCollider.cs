using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using GtMsg;
using System;


#if UNITY_EDITOR
using UnityEditor;
#endif 
public class StoreCollider : MonoBehaviour
{
    void Start()
    { }

    [ContextMenu("Do")]
    void Do()
    {

        FishColliderGroup group = new FishColliderGroup();

        //遍历所以鱼
        for (int j = 0; j < transform.childCount; j++)
        {

            var fish = transform.GetChild(j);

            //一条鱼包含一个
            FishCollider fc = new FishCollider();

            fc.FishName = fish.gameObject.name;
            fc.ZBase = (int)(fish.localPosition.z);
            //遍历鱼中的球
            for (int i = 0; i < fish.childCount; i++)
            {

                var ct = fish.GetChild(i);

                //如果球的名字为C
                if (ct.gameObject.name == "C")
                {

                    //每个球都有一个
                    ColliderCircle cc = new ColliderCircle();

                    cc.Offset = new Vec3 { X = ct.localPosition.x / (6.0f / 640.0f), Y = ct.localPosition.y / (6.0f / 640.0f), Z = ct.localPosition.z / (6.0f / 640.0f) };
                    cc.Radius = ct.localScale.x / (6.0f / 640.0f) / 2;

                    fc.Colliders.Add(cc);
                }
            }
            Debug.Log(fc);
            group.ColliderList.Add(fc);
        }
        Debug.Log("Save File");

        var output = File.Create(Application.dataPath + "/Resources/File/Collider.bytes");
        group.WriteTo(output);
#if UNITY_EDITOR
        //AssetDatabase.Refresh();
#endif
        output.Dispose();
        output.Close();
    }
}
