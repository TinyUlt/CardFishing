using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GtMsg;

[ExecuteInEditMode]
public class ProductConf : MonoBehaviour
{
    //public bool IsSync = false;
    public int CircleTime = 1;

    //public string EnterMessage="";
    //public string LeaveMessage="";


    public ProductItem productItem;

    StoreProduct storeProduct = null;
    // Use this for initialization
    //     private void OnEnable()
    //     {
    //         UnityEditor.EditorApplication.update += OnUpdate;
    //     }
    // 
    //     private void OnDisable()
    //     {
    //         UnityEditor.EditorApplication.update -= OnUpdate;
    //     }

    // Update is called once per frame
    void OnUpdate()
    {
        Debug.Log("log ");
    }
    void FixedUpdate()
    {
        //Debug.Log("log ");
    }
    public void OnDrawGizmos()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<ProductConfItem>() == null)
            {
                child.gameObject.AddComponent<ProductConfItem>();
            }
        }

        //         if (gameObject == UnityEditor.Selection.activeGameObject)
        //             Debug.Log("log "    
    }
    void GetStoreProduct()
    {
        if(storeProduct == null)
        {
            Transform parent = transform.parent;
            Transform terminal = transform;
            while (parent != null)
            {
                terminal = parent;
                parent = terminal.parent;
            }
            storeProduct = terminal.GetComponent<StoreProduct>();

        }
        
    }
    [ContextMenu("BuildAndRun")]
    void BuildAndRun()
    {
        GetStoreProduct();
        storeProduct.Do();
        Run();
    }
    [ContextMenu("run")]
    void Run()
    {
        GetStoreProduct();

        //storeProduct.gameServer.Run(gameObject.name);

    }
    [ContextMenu("stop")]
    void Stop()
    {
        GetStoreProduct();

        //storeProduct.gameServer.Stop();
    }
    [ContextMenu("resume")]
    void Resume()
    {
        GetStoreProduct();
        //storeProduct.gameServer.Resume();
    }
    [ContextMenu("end")]
    void End()
    {
        GetStoreProduct();
       // storeProduct.gameServer.End();
    }
}

