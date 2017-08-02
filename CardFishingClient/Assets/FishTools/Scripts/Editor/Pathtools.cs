using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Pathtools : Editor
{

    [MenuItem("PathTools/Rename/Add Number")]
    static void Rename_Number()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
       
        foreach(var selectedObject in selectedObjects)
        {
            var parentName = selectedObject.name;
            int i = 1;
            foreach (Transform child in selectedObject.transform)
            {
                child.gameObject.name = parentName + "_" + i;
                i++;
            }
        }
    }
    [MenuItem("PathTools/Rename/Add Letter")]
    static void Letter_Rename()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (var selectedObject in selectedObjects)
        {
            var parentName = selectedObject.name;
            char i = 'A';
            foreach (Transform child in selectedObject.transform)
            {
                child.gameObject.name = parentName + "_" + i;
                i++;
            }
        }
    }
    [MenuItem("PathTools/SetSpeedScale")]
    static void SetSpeedScale()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (var selectedObject in selectedObjects)
        {
            //var parentName = selectedObject.name;
            string[] s = selectedObject.name.Split(new char[] { '_' });
            float speedscale = float.Parse(s[1]);
            CreatePath[] createpathlist = selectedObject.GetComponentsInChildren<CreatePath>();
            foreach(var c in createpathlist)
            {
                c.speedScale = speedscale;
            }

        }
    }
    [MenuItem("PathTools/CleanChildren")]
    static void CleanChildren()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (var selectedObject in selectedObjects)
        {
            GameObject[] gos = new GameObject[selectedObject.transform.childCount];
            for (int i = 0; i < selectedObject.transform.childCount; i++)
            {
                gos[i] = selectedObject.transform.GetChild(i).gameObject;
               
            }
            foreach(var go in gos)
            {
                DestroyImmediate(go);
            }
        }
    }

    [MenuItem("PathTools/CreateAllPath")]
    static void CreateAllPath()
    {
        string[] flods = { "fff", "fft", "ftt", "ftf", "tff", "ttf", "tft", "ttt" };
        string[] offsets = { "fff", "fft", "ftt", "ftf", "tff", "ttf", "tft", "ttt" };
        CreatePath(flods, offsets);
    }
    [MenuItem("PathTools/CreateFaceIdlePath")]
    static void CreateIdlePath()
    {
        string[] flods =  { "fff", "tff", "ttf", "ftf" };
        string[] offsets = { "fff", "fft", "ftt", "ftf", "tff", "ttf", "tft", "ttt" };
        CreatePath(flods, offsets);
    }
    [MenuItem("PathTools/Row_Group")]
    static void Row_Group()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (var selectedObject in selectedObjects)
        {
            string[] s = selectedObject.name.Split(new char[] { '_' });
            if (s[0] == "Row")
            {
                Row(selectedObject);
            }
            else if (s[0] == "Group")
            {
                Group(selectedObject);
            }
            else
            {
                continue;
            }
        }
    }
    [MenuItem("PathTools/CircleLine")]
    static void CircleLine()
    {
        
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (var selectedObject in selectedObjects)
        {
            string[] s = selectedObject.name.Split(new char[] { '_' });
            if (s[0] != "CircleLine")
            {
                continue;
            }
            int count = int.Parse(s[1]);
            float radius = float.Parse(s[2]);
            float speedScale = float.Parse(s[3]);

            float hudu = 2 * Mathf.PI / count;
            for (var i = 0; i < count; i++)
            {
                GameObject go = new GameObject(selectedObject.name+ "_" + (i+1));

                go.transform.parent = selectedObject.transform;

                var createpathteminal = go.AddComponent<CreatePath>();
                SrouceObject script = selectedObject.GetComponent<SrouceObject>();
                foreach (var obj in script.objs)
                {
                    CreatePath createpathsource = obj.gameObject.GetComponent<CreatePath>();
                    createpathteminal.copyObj = createpathsource.gameObject;

                    createpathteminal.offset.y = radius * Mathf.Cos(i * hudu);

                    createpathteminal.offset.z = radius * Mathf.Sin(i * hudu);
                }
          }
        }
    }





    [MenuItem("PathTools/CreateZhuzi")]
    static void CreateZhuzi()
    {

        GameObject selectedObject = Selection.gameObjects[0];

        int beginX = -100;
        int endX = 1060;
        int lenth = 200;
        int degree = 0;
        while (beginX <= endX)
        {
            GameObject g = new GameObject();
            g.name = "obj";
            g.transform.SetParent(selectedObject.transform);
            
            var d = 2 * Mathf.PI * degree / 360;
            var y = Mathf.Cos(d) * lenth + 320;
            var z = Mathf.Sin(d) * lenth;
            g.transform.position = new Vector3(beginX, y, z);
            degree += 20;
            beginX += 10;
        }
       

    }
    [MenuItem("PathTools/CreateXuanwo")]
    static void CreateXuanwo()
    {

        GameObject selectedObject = Selection.gameObjects[0];

        int beginX = -100;
        int lenth = 500;
        int degree = 0;
        int degreeIncrease = 20;
        while (lenth >=0)
        {
            GameObject g = new GameObject();
            g.name = "obj";
            g.transform.SetParent(selectedObject.transform);

            var d = 2 * Mathf.PI * degree / 360;
            var y = Mathf.Cos(d) * lenth + 320;
            var z = Mathf.Sin(d) * lenth;
            g.transform.position = new Vector3(beginX, y, z);
            degree += degreeIncrease;
            degreeIncrease += 1;
            beginX += 10;
            lenth -= 8;
        }


    }
    [MenuItem("PathTools/CreateShuanCenXuanwo")]
    static void CreateShuanCenXuanwo()
    {

        GameObject selectedObject = Selection.gameObjects[0];

        // int beginX = -100;
        int beginY = 100;
        int lenth = 500;
        int degree = 0;
        int degreeIncrease = 20;
        while (lenth >= -415)
        {
            GameObject g = new GameObject();
            g.name = "obj";
            g.transform.SetParent(selectedObject.transform);

            var d = 2 * Mathf.PI * degree / 360;
            var x = Mathf.Cos(d) * Mathf.Abs( lenth) + 480;
            var z = Mathf.Sin(d) * Mathf.Abs(lenth);
            g.transform.position = new Vector3(x, beginY, z);
            degree += degreeIncrease;
            if (lenth > 0)
            {
                degreeIncrease += 2;
            }else
            {
                degreeIncrease -= 2;

            }

            //beginX += 10;
            beginY -= 5;
            lenth -= 15;
        }


    }

    static void Row(GameObject selectedObject)
    {
        //GameObject[] selectedObjects = Selection.gameObjects;

        //foreach (var selectedObject in selectedObjects)
        {
            string[] s = selectedObject.name.Split(new char[] { '_' });
           
            int  count = int.Parse(s[1]);
            int space = int.Parse(s[2]);

            SrouceObject script = selectedObject.GetComponent<SrouceObject>();

            foreach (var obj in script.objs)
            {
                CreatePath[] createpathsources = obj.gameObject.GetComponentsInChildren<CreatePath>();
                foreach (var createpathsource in createpathsources)
                {
                    GameObject go = new GameObject(selectedObject.name + "_" +  createpathsource.gameObject.name);
                    go.AddComponent<ProductConf>();
                    go.transform.parent = selectedObject.transform;
                    for(int i = 0; i < count; i++)
                    {
                        GameObject go2 = new GameObject(createpathsource.gameObject.name);
                        go2.transform.parent = go.transform;

                        ProductConfItem item = go2.AddComponent<ProductConfItem>();
                        item.Frame = i == count-1 ? 0 : space;
                        item.Paths = new PathStruct[1];
                        item.Paths[0] = new PathStruct();
                        item.Paths[0].Path = createpathsource.gameObject;
                    }
                }
            }
        }
    }
    //[MenuItem("PathTools/Group")]
    static void Group(GameObject selectedObject)
    {
        //GameObject[] selectedObjects = Selection.gameObjects;

        //foreach (var selectedObject in selectedObjects)
        {
            string[] s = selectedObject.name.Split(new char[] { '_' });
           
            int count = int.Parse(s[1]);
            int space = int.Parse(s[2]);
            SrouceObject script = selectedObject.GetComponent<SrouceObject>();

            foreach (var obj in script.objs)
            {
                GroupSigned[] groups = obj.gameObject.GetComponentsInChildren<GroupSigned>();
                foreach (var group in groups)
                {
                    GameObject go = new GameObject(selectedObject.name + "_" + group.gameObject.name);
                    go.transform.parent = selectedObject.transform;
                    go.AddComponent<ProductConf>();

                    CreatePath[] createpathsources = group.gameObject.GetComponentsInChildren<CreatePath>();
                    RandomPaths(createpathsources);
                    for (int i = 0; i < count; i++)
                    {
                        GameObject go2 = new GameObject(createpathsources[i].gameObject.name);
                        go2.transform.parent = go.transform;

                        ProductConfItem item = go2.AddComponent<ProductConfItem>();
                        item.Frame = i == count - 1 ? 0 : space;
                        item.Paths = new PathStruct[1];
                        item.Paths[0] = new PathStruct();
                        item.Paths[0].Path = createpathsources[i].gameObject;
                    }
                }
            }
        }
    }

    static void RandomPaths(CreatePath[] a)
    {
        int index, i;
        CreatePath tmp;
        int n = a.Length;
        for (i = 0; i < n; i++)
        {
            index =Random.Range(0, n-i)  + i;
            if (index != i)
            {
                tmp = a[i];
                a[i] = a[index];
                a[index] = tmp;
            }
        }
    }
    static void CreatePath(string [] flods, string[] offsets)
    {
        
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (var selectedObject in selectedObjects)
        {
            string[] s = selectedObject.name.Split(new char[] { '_' });
            if (s[0] != "Ex")
            {
                continue;
            }
            float speedscale = float.Parse(s[1]);
            float offset = float.Parse(s[2]);
            SrouceObject script = selectedObject.GetComponent<SrouceObject>();

            foreach(var obj in script.objs)
            {
                CreatePath[] createpathsources = obj.gameObject.GetComponentsInChildren<CreatePath>();
                foreach (var createpathsource in createpathsources)
                {
                    GameObject go = new GameObject(createpathsource.gameObject.name + "_" + selectedObject.name);
                    go.transform.parent = selectedObject.transform;

                    for (int i = 0; i < flods.Length; i++)
                    {
                        GameObject go2 = new GameObject(go.name + "_" + flods[i]);
                        go2.transform.parent = go.transform;
                        go2.AddComponent<GroupSigned>();
                        //                     CreatePath createpathterminal = go2.AddComponent<CreatePath>();
                        //                     createpathterminal.copyObj = createpathsource.gameObject;
                        //                     createpathterminal.foldX = flods[i][0] == 't';
                        //                     createpathterminal.foldY = flods[i][1] == 't';
                        //                     createpathterminal.foldZ = flods[i][2] == 't';

                        for (int j = 0; j < offsets.Length; j++)
                        {
                            GameObject go3 = new GameObject(go2.name + "_" + offsets[j]);
                            go3.transform.parent = go2.transform;

                            CreatePath createpathterminal = go3.AddComponent<CreatePath>();
                            createpathterminal.copyObj = createpathsource.gameObject;

                            createpathterminal.foldX = flods[i][0] == 't';
                            createpathterminal.foldY = flods[i][1] == 't';
                            createpathterminal.foldZ = flods[i][2] == 't';
                            createpathterminal.offset.x = offsets[j][0] == 't' ? offset : -offset;
                            createpathterminal.offset.y = offsets[j][1] == 't' ? offset : -offset;
                            createpathterminal.offset.z = offsets[j][2] == 't' ? offset : -offset;
                            createpathterminal.speedScale = speedscale;
                        }
                    }


                }
            }
           
        }
    }
}
