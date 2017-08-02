using UnityEngine;
using Google.Protobuf;
using System.IO;
using GtMsg;
#if UNITY_EDITOR 
using UnityEditor;
#endif
public class StoreProduct : MonoBehaviour {

    public Transform PathGroup;
    public GameObject MainEnter;
    public GameObject AddFishEnter;
    public bool EnableAddFish;
    //public GameServer gameServer;
    private void Start()
    {

    }
    [ContextMenu("Do")]
    public void Do()
    {
        var originPathList = PathGroup.GetComponentsInChildren<CreatePath>();

        ProductItemGroup productItemGroup = new ProductItemGroup();
        productItemGroup.MainEnter = MainEnter.name;
        productItemGroup.AddFishEnter = AddFishEnter.name;
        productItemGroup.EnableAddFish = EnableAddFish;
        foreach (var originItem in originPathList)
        {
            var productItem = new ProductItem();
            productItem.ContentName = originItem.gameObject.name;
            productItem.Time = 1;

            productItemGroup.ItemList.Add(productItem);
        }

        var ProductConfList = transform.GetComponentsInChildren<ProductConf>();
        foreach (var ProductConfChild in ProductConfList)
        {
            ProductConfChild.productItem = new ProductItem();
            ProductConfChild.productItem.ContentName = ProductConfChild.gameObject.name;
            //ProductConfChild.productItem.IsSync = ProductConfChild.IsSync;
            ProductConfChild.productItem.Time = ProductConfChild.CircleTime;
            productItemGroup.ItemList.Add(ProductConfChild.productItem);
        }
            

        foreach (var ProductConfChild in ProductConfList)
        {
            var ConfigItemList = ProductConfChild.transform.GetComponentsInChildren<ProductConfItem>();
            foreach (var Item in ConfigItemList)
            {
                ProductItemContent productItemContent = new ProductItemContent();

                productItemContent.IsYuZhen = Item.IsYuZhen;
                productItemContent.Frame = Item.Frame;
                productItemContent.CombinePaths = Item.CombinePaths;
                productItemContent.EnterMessage = Item.EnterMessage;
                productItemContent.LeaveMessage = Item.LeaveMessage;
                productItemContent.OnlyOne = Item.OnlyOne;
                productItemContent.WaitUntilDone = Item.WaitUntilDone;
                productItemContent.FastenOldFish = Item.FastenOldFish;
                productItemContent.FastenIn = Item.FastenIn;
                productItemContent.Toward = (int)Item.Toward;
                productItemContent.Offset = new Vec3() { X = Item.offset.x, Y = Item.offset.y, Z = Item.offset.z };
                productItemContent.FoldX = Item.foldX;
                productItemContent.FoldY = Item.foldY;
                productItemContent.FoldZ = Item.foldZ;
                productItemContent.SpeedScale = Item.speedScale;
                productItemContent.Deep = (int)Item.deep;
                productItemContent.RootMessage = Item.RootMessage;
                foreach(var messge in Item.TimeMessage)
                {
                    productItemContent.MessageList.Add(new GtMsg.MessageTimer { PassFrame = messge.passFrame, Message = messge.message });
                }
                

                string fishName = "";
                for(int i = 0; i < Item.Fishs.Length; i++)
                {
                    if (Item.Fishs[i].Fish != null)
                    {
                        if (Item.Fishs[i].IsRed)
                        {
                            fishName += "!";
                        }
                        fishName += Item.Fishs[i].Fish.name;

                        if (Item.Fishs[i+1].Fish == null)
                        {
                            break;
                        }
                        fishName += " ";
                    }
                }
                productItemContent.FishType = fishName;
                

                foreach (var path in Item.Paths)
                {
                    
                    if(path.Path != null)
                    {
                        var pathName = path.Path.name;

                        string[] names = new string[0];
                        ProductConf[] pconf = path.Path.GetComponentsInChildren<ProductConf>();
                        if (pconf.Length > 0)
                        {
                            names = new string[pconf.Length];
                            for (int i = 0; i < pconf.Length; i++)
                            {
                                names[i] = pconf[i].gameObject.name;
                            }

                        }
                        else
                        {
                            CreatePath[] cp = path.Path.GetComponentsInChildren<CreatePath>();
                            if (cp.Length > 0)
                            {
                                names = new string[cp.Length];
                                for (int i = 0; i < cp.Length; i++)
                                {
                                    names[i] = cp[i].gameObject.name;
                                }
                            }else
                            {
                                Debug.LogError("找不到"+ pathName+"的子节点");
                            }
                        }

                        foreach (var c in names)
                        {
                            pathName = c;
                            int index = 0;
                            foreach (var iii in productItemGroup.ItemList)
                            {
                                if (pathName == iii.ContentName)
                                {
                                    productItemContent.ProductItemIndex.Add(index);
                                    break;
                                }
                                index++;
                            }
                            //if (index == productItemGroup.ItemList.Count)
                            //{
                            //    var productItem = new ProductItem();
                            //    productItem.ContentName = pathName;
                            //    productItem.Time = 1;

                            //    productItemGroup.ItemList.Add(productItem);

                            //    productItemContent.ProductItemIndex.Add(index);
                            //}
                        }

                    }
                }

                ProductConfChild.productItem.ContentItems.Add(productItemContent);
            }
        }

        using (var output = File.Create(Application.dataPath + "/Resources/File/" + gameObject.name + ".bytes"))
        {
            productItemGroup.WriteTo(output);
        }
    //    using (var output = File.Create("E:/server/tt/运行/debug/unicode/3DfishServer/" + gameObject.name + ".bytes"))
        {
         //   productItemGroup.WriteTo(output);
        }

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
        Debug.Log("store product done");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
