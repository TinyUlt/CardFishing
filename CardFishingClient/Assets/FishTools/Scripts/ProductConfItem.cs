using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using System.IO;
using GtMsg;
public enum FishTowards
{
    Free = 0,
    Back = 1,
    Left = 2,
    Forward = 3,
    Right = 4,
    Up = 5,
    Down = 6
}
public enum PathDeep
{
    Deep0 = 0,
    Deep1 ,
    Deep2 ,
    Deep3 ,
    Deep4 ,
    Deep5 ,
    Deep6 ,
    Deep7 ,
    Deep8 ,
    Deep9 ,
}
[System.Serializable]
public class FishStruct
{
    public bool IsRed;
    public GameObject Fish;
}

[System.Serializable]
public class PathStruct
{
    public GameObject Path;
}
[System.Serializable]
public class MessageTimer
{
    public int passFrame;
    public string message;
}
public class ProductConfItem : MonoBehaviour {

    public bool IsYuZhen = false;
    public bool FastenOldFish = false;
    public bool FastenIn = false;
    public PathDeep deep = PathDeep.Deep0;
    public FishTowards Toward = FishTowards.Free;//0（自由方向），1（自己），2（左边），3（对面），4（右边），5（上面），6（下面）
    public FishStruct[] Fishs;

    public Vector3 offset;
    public bool foldX;
    public bool foldY;
    public bool foldZ;
    public float speedScale = 1f;

    public bool CombinePaths;
    public PathStruct[] Paths;

    public string EnterMessage = "";
    public string LeaveMessage = "";
    public MessageTimer[] TimeMessage;

    public bool OnlyOne;
    public bool WaitUntilDone = false;
    public int Frame;

    public string RootMessage="";
    public void Init()
    {

    }

    public void OnDrawGizmos()
    {
        if(Fishs == null)
        {
            Fishs = new FishStruct[1];
            Fishs[0] = new FishStruct();
        }
        if (Paths == null)
        {
            Paths = new PathStruct[1];
            Paths[0] = new PathStruct();
        }
        if (Paths.Length > 0)
        {
            string pathName = "";
            for (int i = 0; i < Paths.Length; i++)
            {
                if (Paths[i].Path != null)
                {
                    pathName += Paths[i].Path.name;
                    if (i != Paths.Length - 1)
                    {
                        pathName += " ";
                    }
                }
               
            }
            if(pathName == "")
            {
                pathName = "Empty";
            }
            gameObject.name = pathName;
        }

        if (Fishs.Length == 0 || Fishs[Fishs.Length - 1].Fish != null)
        {
            var temp = new FishStruct[Fishs.Length + 1];
            temp[Fishs.Length] = new FishStruct();

            for (int i = 0; i < Fishs.Length; i++)
            {
                temp[i] = Fishs[i];
            }
            Fishs = temp;
        }
 
        if (Paths.Length == 0 || Paths[Paths.Length-1].Path != null)
        {
            var temp = new PathStruct[Paths.Length + 1];
            temp[Paths.Length] = new PathStruct();

            for(int i = 0; i < Paths.Length; i++)
            {
                temp[i] = Paths[i];
            }
            Paths = temp;
        }
       
    }

   

}




