using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//展示选中的那条路径
public class ShowPathSelected : MonoBehaviour {

    public GameObject NowSelectedObj;
    CreatePath[] m_cachePaths;
	// Use this for initialization
	void Start () {
		
	}
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (NowSelectedObj != UnityEditor.Selection.activeGameObject)
        {
            NowSelectedObj = UnityEditor.Selection.activeGameObject;
            CheckPathNeedShow();
        }
        if (m_cachePaths != null && m_cachePaths.Length > 0)
        {
            for(int i = 0; i < m_cachePaths.Length; i++)
            {
                if(m_cachePaths[i] != null)
                {
                    m_cachePaths[i].DrawPath();
                }
            }
        }
    }
#endif

    void CheckPathNeedShow()
    {
        if (NowSelectedObj == null)
        {
            m_cachePaths = null;
            return;
        }
        m_cachePaths = NowSelectedObj.transform.GetComponentsInChildren<CreatePath>();
        if(m_cachePaths == null || m_cachePaths.Length == 0)
        {
            CreatePath parentPath = NowSelectedObj.transform.GetComponentInParent<CreatePath>();
            if(parentPath != null)
            {
                m_cachePaths = new CreatePath[1] { parentPath };
            }
            
        }
    }
}
