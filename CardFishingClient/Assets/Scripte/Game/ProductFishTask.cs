using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using GtMsg;
using Google.Protobuf;
using Google.Protobuf.Collections;
public class ProductFishTask : MonoBehaviour {

    int IsYuZhen;
    ProductItemGroup ProductItemStore;
    string PathEnter;
    string AddFishEnter;
    ProductItem AddFishProductItem;
    public void StartThread()
    {
        ProductItemStore = GameData.Instance.GetProductItemGroup();
        PathEnter = ProductItemStore.MainEnter;
        AddFishEnter = ProductItemStore.AddFishEnter;
        Thread thread = new Thread(CreateFramePathLogic);

        thread.Start();
    }

    //产鱼线程
    void CreateFramePathLogic()
    {
        ProductItem productItem = null;

        int breaktime = 0;
        IsYuZhen = 0;
        foreach (var item in ProductItemStore.ItemList)
        {

            if (item.ContentName == PathEnter)
            {

                productItem = item;
                breaktime++;
            }
            if (item.ContentName == AddFishEnter)
            {

                AddFishProductItem = item;
                breaktime++;
            }
            if (breaktime >= 2)
            {
                break;
            }
        }



        if (productItem == null)
        {

            return;
        }

        int frame = 0;

        ran = new System.Random((int)(System.DateTime.Now.Millisecond));
        while (m_stop == false)
        {

            FramePathCircle(true, productItem, ref frame, gameConfig.FishConfigs[0].FishModel, "", "", null, "", false, false, false, false, 0, new Vec3(), false, false, false, 1, 0);
        }
        Debug.Log("CreateFramePathLogic end");

    }
    // Update is called once per frame

}
