using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using GtMsg;
using Google.Protobuf;
using Google.Protobuf.Collections;
public class GameData : MonoSingletion<GameData>
{
    //鱼路径逻辑数据文件名
    public string PathLogicFileName = "";

    public ProductItemGroup GetProductItemGroup()
    {
        if (ProductItemStore == null)
        {
            ProductItemStore = GetPathLogic();
        }
        return ProductItemStore;
    }
    //鱼路径逻辑数据
    private ProductItemGroup ProductItemStore;


    public bool InitGameData()
    {
        if (ProductItemStore == null)
        {
            ProductItemStore = GetPathLogic();
        }
        return true;
    }
    //初始化鱼路径逻辑
    private ProductItemGroup GetPathLogic()
    {
        TextAsset t = Resources.Load("File/" + PathLogicFileName) as TextAsset;

        if (t != null)
        {
            ProductItemStore = ProductItemGroup.Parser.ParseFrom(t.bytes);

            foreach (var Item in ProductItemStore.ItemList)
            {
                foreach (var content in Item.ContentItems)
                {
                    foreach (var index in content.ProductItemIndex)
                    {
                        
                        content.Items.Add(ProductItemStore.ItemList[index]);
                      
                    }
                }
            }

            return ProductItemStore;
        }

        return null;
    }
}
