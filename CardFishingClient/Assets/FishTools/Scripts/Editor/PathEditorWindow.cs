using GtMsg;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PathEditorWindow : EditorWindow
{

    private Rect mPropertyToolbarRect;
    private Rect mPropertyBoxRect;
    private Rect mGraphRect;

    private int mBehaviorToolbarSelection = 1;

    private float mPrevScreenWidth = -1f;

    private float mPrevScreenHeight = -1f;

    PathGroupClient PGClient;
   
    Vector2 mScrollPosition;

    SinglePathInspector mSinglePathInspector = new SinglePathInspector();

    SinglePathEditorMgr mSinglePathMgr;
    SinglePathEditorData mSelectPath;

    private string[] mBehaviorToolbarStrings = new string[]
        {
            "原始路径",
            "组合路径",
            "设置面板",
            "属性面板"
        };

    static PathEditorWindow m_window;
    [MenuItem("Window/路径创建")]
    static void Init()
    {
        m_window = (PathEditorWindow)EditorWindow.GetWindow(typeof(PathEditorWindow), false, "路径创建", false);
        m_window.Show();
    }

    public static GUIStyle PropertyBoxGUIStyle
    {
        get
        {
            if (propertyBoxGUIStyle == null)
            {
                InitPropertyBoxGUIStyle();
            }
            return propertyBoxGUIStyle;
        }
    }
    private static void InitPropertyBoxGUIStyle()
    {
        propertyBoxGUIStyle = new GUIStyle();
        propertyBoxGUIStyle.padding = new RectOffset(2, 2, 0, 0);
    }
    private static GUIStyle propertyBoxGUIStyle = null;

    private void SetupSizes()
    {
        float width = base.position.width;
        float num = base.position.height + 22f;
        if (this.mPrevScreenWidth == width && this.mPrevScreenHeight == num )
        {
            return;
        }
        this.mPropertyToolbarRect = new Rect(0f, 0f, 300f, 18f);
        this.mPropertyBoxRect = new Rect(0f, this.mPropertyToolbarRect.height, 300f, num - this.mPropertyToolbarRect.height - 21f);
        this.mGraphRect = new Rect(300f, 18f, width - 300f - 15f, num - 36f - 21f - 15f);
        this.mPrevScreenWidth = width;
        this.mPrevScreenHeight = num;
    }

    void OnDestroy()
    {
        if (mSinglePathInspector != null)
        {
            mSinglePathInspector.OnDestroy();
            //mSinglePathInspector.DrawPathInScene();
        }
    }

    public void OnGUI()
    {
        SetupSizes();
        DrawPropertiesBox();
        DrawGraphArea();

        
    }

    void OnInspectorUpdate()
    {
        this.Repaint();
    }

    private void DrawPropertiesBox()
    {
        GUILayout.BeginArea(this.mPropertyToolbarRect, EditorStyles.toolbar);
        int num = this.mBehaviorToolbarSelection;
        this.mBehaviorToolbarSelection = GUILayout.Toolbar(this.mBehaviorToolbarSelection, this.mBehaviorToolbarStrings, EditorStyles.toolbarButton, new GUILayoutOption[0]);
        GUILayout.EndArea();
        GUILayout.BeginArea(this.mPropertyBoxRect, PropertyBoxGUIStyle);
        if (this.mBehaviorToolbarSelection == 0)
        {
            DrawSinglePathList();
        }
        else if (this.mBehaviorToolbarSelection == 1)
        {

        }
        else if (this.mBehaviorToolbarSelection == 2)
        {

        }
        else if (this.mBehaviorToolbarSelection == 3)
        {

        }
        GUILayout.EndArea();
    }

    private void DrawGraphArea()
    {
        GUILayout.BeginArea(this.mGraphRect);

        if (this.mBehaviorToolbarSelection == 0)
        {
            DrawSinglePathInspector();
        }
        GUILayout.EndArea();
    }

    void DrawSinglePathList()
    {
        if(mSinglePathMgr == null)
        {
            Object o = AssetDatabase.LoadAssetAtPath("Assets/FishTools/SinglePathConfig.asset", typeof(SinglePathEditorMgr));
            if(o != null)
            {
                mSinglePathMgr = o as SinglePathEditorMgr;
            }
            else
            {
                mSinglePathMgr = ScriptableObject.CreateInstance<SinglePathEditorMgr>();
            }
        }
        GUILayout.Space(2);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("创建新路径", EditorStyles.toolbarButton))
        {
            SinglePathEditorData path = new SinglePathEditorData();
            if(mSinglePathMgr.Paths == null)
            {
                mSinglePathMgr.Paths = new List<SinglePathEditorData>();
            }
            mSinglePathMgr.Paths.Add(path);
            OnClickOneSinglePath(path);
        }
        if (GUILayout.Button("保存配置", EditorStyles.toolbarButton))
        {
            EditorUtility.SetDirty(mSinglePathMgr);
            AssetDatabase.CreateAsset(mSinglePathMgr, "Assets/FishTools/SinglePathConfig.asset");
            AssetDatabase.SaveAssets();
        }
        if (GUILayout.Button("生成路径", EditorStyles.toolbarButton))
        {
            
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(2);
        this.mScrollPosition = GUILayout.BeginScrollView(this.mScrollPosition, new GUILayoutOption[0]);
        for (int i = 0; mSinglePathMgr.Paths != null && i < mSinglePathMgr.Paths.Count; i++)
        {
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            GUILayout.Space((float)(EditorGUI.indentLevel * 16));

            SinglePathEditorData path = mSinglePathMgr.Paths[i];
            if (GUILayout.Button(path.pathName, EditorStyles.toolbarButton, new GUILayoutOption[]
            {
                            GUILayout.MaxWidth((float)(300 - EditorGUI.indentLevel * 16 - 24 - 30))
            }))
            {
                OnClickOneSinglePath(path);
            }
            if (GUILayout.Button("-", EditorStyles.toolbarButton, new GUILayoutOption[]
            {
                            GUILayout.MaxWidth(30)
            }))
            {
                if (EditorUtility.DisplayDialog("删除路径提示", "路径删除不可撤销，是否确定删除", "删除", "取消")) //显示对话框  
                {
                    mSinglePathMgr.Paths.RemoveAt(i);
                }
                
            }
            GUILayout.Space(3f);
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();

        
    }

    void DrawSinglePathInspector()
    {
        if(mSinglePathInspector != null)
        {
            mSinglePathInspector.DrawSinglePathInspector(mSelectPath);
            //mSinglePathInspector.DrawPathInScene();
        }
            

    }



    void OnClickOneSinglePath(SinglePathEditorData path)
    {
        mSinglePathInspector.OnSelectOnePath(path);
        mSelectPath = path;

        SceneView.RepaintAll();
    }
}
