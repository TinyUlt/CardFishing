using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using GtMsg;


#if UNITY_EDITOR  
using UnityEditor;
#endif 
public class StorePath : MonoBehaviour
{

    public bool IsAutoOpen = false;
    public GameObject mG;
    void Start()
    {

    }
    [ContextMenu("Do")]
    void Do()
    {
        if(mG == null)
        {
            Debug.LogError("mG not ref ");
            return;
        }

        if (IsAutoOpen)
        {
            foreach (var child in transform)
            {

                (child as Transform).gameObject.SetActive(true);
            }
        }
        var children = GetComponentsInChildren<CreatePath>();

        PathGroupClient pgClient = new PathGroupClient();
        PathGroupServer pgServer = new PathGroupServer();

        //mG.transform.parent = transform;

        mG.transform.position = new Vector3(0, 0, 0);

        foreach (var createPath in children)
        {



            SinglePathClient spClient = new SinglePathClient();
            SinglePathServer spServer = new SinglePathServer();

            spClient.PathName = createPath.gameObject.name;
            spServer.PathName = createPath.gameObject.name;

            //spServer.Offset = new Vec3();
            //spServer.Rotation = new Vec3();
           

            spClient.Offset = new Vec3() { X = createPath.offset.x, Y = createPath.offset.y, Z = createPath.offset.z };
            spClient.FoldX = createPath.foldX;
            spClient.FoldY = createPath.foldY;
            spClient.FoldZ = createPath.foldZ;
            spClient.SpeedScale = createPath.speedScale;


            spServer.Offset = new Vec3() { X = createPath.offset.x, Y = createPath.offset.y, Z = createPath.offset.z };
            spServer.FoldX = createPath.foldX;
            spServer.FoldY = createPath.foldY;
            spServer.FoldZ = createPath.foldZ;
            spServer.SpeedScale = createPath.speedScale;

            if (createPath.copyObj != null)
            {
                spClient.CopyPathName = createPath.copyObj.name;
                spServer.CopyPathName = createPath.copyObj.name;
            }else
            {
                createPath.OnCreatePath();

                foreach (var i in createPath.idleList)
                {

                    spClient.Actions.Add(i);
                    spServer.Actions.Add(i);
                }
                //spServer.Idles = createPath.Idles;
                Vector3 oldP = Vector3.zero;

                Quaternion oldRotation = new Quaternion();
                foreach (var vec3 in createPath.pathListVec3)
                {

                    PathPointClient vClient = new PathPointClient();
                    PathPointServer vServer = new PathPointServer();

                    vClient.Point = new Vec3() { X = vec3.x, Y = vec3.y, Z = vec3.z };
                    vServer.Point = vClient.Point;

                    mG.transform.position = vec3;

                    Quaternion rotation;

                    if (vec3 == oldP)
                    {

                        rotation = oldRotation;

                    }
                    else
                    {

                        rotation = Quaternion.LookRotation(vec3 - oldP);
                    }

                    mG.transform.localRotation = rotation;

                    var xV = mG.transform.right;
                    var yV = mG.transform.up;
                    var zV = mG.transform.forward;
                    vServer.NormalizedX = new Vec3() { X = xV.x, Y = xV.y, Z = xV.z };
                    vServer.NormalizedY = new Vec3() { X = yV.x, Y = yV.y, Z = yV.z };
                    vServer.NormalizedZ = new Vec3() { X = zV.x, Y = zV.y, Z = zV.z };

                    spClient.PathPointList.Add(vClient);
                    spServer.PathPointList.Add(vServer);

                    oldP = vec3;

                    oldRotation = rotation;
                }
            }
           

            pgClient.PathList.Add(spClient);
            pgServer.PathList.Add(spServer);


        }
        using (var output = File.Create(Application.dataPath + "/Resources/File/OriginPathFile_Server.bytes"))
        {
            pgServer.WriteTo(output);
        }
        using (var output = File.Create(Application.dataPath + "/Resources/File/OriginPathFile_Client.bytes"))
        {
            pgClient.WriteTo(output);
        }
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
        Debug.Log("store path done");
    }
}
