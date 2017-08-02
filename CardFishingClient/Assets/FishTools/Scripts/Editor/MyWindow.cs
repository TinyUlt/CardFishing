using UnityEngine;
using System.Collections;
using UnityEditor;//注意要引用
public class MyWindow : EditorWindow
{
    //     string move;
    //     [MenuItem("Window/MyWindow")]//在unity菜单Window下有MyWindow选项
    //     static void Init()
    //     {
    //         MyWindow myWindow = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow), false, "MyWindow", false);
    //         myWindow.Show(true);
    //     }
    //     void OnGUI()
    //     {
    //         move = EditorWindow.mouseOverWindow ? EditorWindow.mouseOverWindow.ToString() : "Nothing";
    //         EditorGUILayout.LabelField(move);
    //     }
    //     void OnInspectorUpdate()
    //     {
    //         if (EditorWindow.mouseOverWindow)
    //             EditorWindow.mouseOverWindow.Focus();//就是当鼠标移到那个窗口，这个窗口就自动聚焦
    //         this.Repaint();//重画MyWindow窗口，更新Label
    //     }


    //     int i = 0;
    //     [MenuItem("Window/MyWindow")]//在unity菜单Window下有MyWindow选项
    //     static void Init()
    //     {
    //         MyWindow myWindow = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow), false, "MyWindow", false);
    //         myWindow.autoRepaintOnSceneChange = true;
    //         myWindow.Show(true);
    //     }
    //     void OnGUI()
    //     {
    //         i++;
    //         EditorGUILayout.LabelField(i.ToString());
    //     }

    //     [MenuItem("Window/MyWindow")]//在unity菜单Window下有MyWindow选项
    //     static void Init()
    //     {
    //         MyWindow myWindow = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow), false, "MyWindow", false);
    //         myWindow.autoRepaintOnSceneChange = true;
    //         myWindow.Show(true);
    //     }
    //     void OnGUI()
    //     {
    //         maximized = EditorGUILayout.ToggleLeft("Max", maximized);
    //     }


    //    static MyWindow myWindow;
    //    [MenuItem("Window/MyWindow")]//在unity菜单Window下有MyWindow选项
    //    static void Init()
    //    {
    //        myWindow = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow), false, "MyWindow", false);
    //        myWindow.Show(true);
    //    }
    //    void OnEnable()
    //    {
    //
    //    }
    //    void OnGUI()
    //    {
    //        wantsMouseMove = EditorGUILayout.Toggle("receive mouseMove:", wantsMouseMove);//是否启用接收鼠标移动事件监听
    //		EditorGUILayout.LabelField("Mouse Position:"+Event.current.type.ToString(),  Event.current.mousePosition.ToString());
    //        if (Event.current.type == EventType.mouseMove && wantsMouseMove)//如果是鼠标移动的事件，就重画窗口
    //        {                                              ///因为上面注意那里有讲到：他不会自动调用repaint()方法
    //            Repaint();
    //        }
    //    }



    //    static MyWindow myWindow;
    //    public Rect windowRect = new Rect(0, 0, 200, 200);//子窗口的大小和位置
    //    [MenuItem("Window/MyWindow")]//在unity菜单Window下有MyWindow选项
    //    static void Init()
    //    {
    //        myWindow = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow), false, "MyWindow", false);
    //        myWindow.Show(true);
    //    }
    //    void OnEnable()
    //    {
    //
    //    }
    //    void OnGUI()
    //    {
    //        BeginWindows();//标记开始区域所有弹出式窗口
    //        windowRect = GUILayout.Window(1, windowRect, DoWindow, "子窗口");//创建内联窗口,参数分别为id,大小位置，创建子窗口的组件的函数，标题
    //        EndWindows();//标记结束
    //    }
    //    void DoWindow(int unusedWindowID)
    //    {
    //        GUILayout.Button("按钮");//创建button
    //        GUI.DragWindow();//画出子窗口
    //    }

    //	static MyWindow myWindow;
    //	[MenuItem("Window/MyWindow")]//在unity菜单Window下有MyWindow选项
    //	static void Init()
    //	{
    //		myWindow = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow), false, "MyWindow", false);
    //		myWindow.Show();
    //	}
    //	void OnEnable()
    //	{
    //
    //	}
    //	void OnGUI()
    //	{
    //		if (GUILayout.Button("关闭窗口"))
    //		{
    //			myWindow.Close();
    //		}
    //	}

    //	static MyWindow myWindow;
    //	[MenuItem("Window/MyWindow")]//在unity菜单Window下有MyWindow选项
    //	static void Init()
    //	{
    //		myWindow = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow), false, "MyWindow", false);
    //		myWindow.Show();
    //	}
    //	void OnEnable()
    //	{
    //
    //	}
    //	void OnGUI()
    //	{
    //		EditorGUILayout.LabelField("聚焦窗体名字："+EditorWindow.focusedWindow.ToString());
    //	}
    //	[MenuItem("Custom Editor/Focus Window")]
    //	static void FocusWindow()
    //	{
    //		myWindow.Focus();
    //	} 

    // 	static MyWindow myWindow;
    // 	string m_notification = "我是消息内容";
    // 	[MenuItem("Window/MyWindow")]//在unity菜单Window下有MyWindow选项
    // 	static void Init()
    // 	{
    // 		myWindow = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow), false, "MyWindow", false);
    // 		myWindow.Show();
    // 	}
    // 	void OnEnable()
    // 	{
    // 
    // 	}
    // 	void OnGUI()
    // 	{
    // 		m_notification = EditorGUILayout.TextField(m_notification);
    // 		if (GUILayout.Button("显示消息"))
    // 		{
    // 			myWindow.ShowNotification(new GUIContent(m_notification));
    // 		}
    // 		if (GUILayout.Button("不显示消息"))
    // 		{
    // 			myWindow.RemoveNotification();
    // 		}
    // 	}

    //     static MyWindow myWindow;
    //     [MenuItem("Window/MyWindow")]//在unity菜单Window下有MyWindow选项
    //     static void Init()
    //     {
    //         myWindow = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow), false, "MyWindow", false);
    //         myWindow.Show();
    //     }
    //     void OnEnable()
    //     {
    // 
    //     }
    //     void OnGUI()
    //     {
    //         if (EditorWindow.focusedWindow.ToString().Trim() == "(UnityEditor.SceneHierarchyWindow)")
    //         {
    //             EditorWindow.focusedWindow.SendEvent(EditorGUIUtility.CommandEvent("Paste"));//传递粘贴的事件
    //         }
    //     }

    //     static EditorWindow myWindow;
    //     [MenuItem("Window/MyWindow")]
    //     static void Init()
    //     {
    //         myWindow = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow), false, "MyWindow", false);
    //         myWindow.Show();
    //     }
    //     void OnHierarchyChange()
    //     {
    //         Debug.Log("Sync");
    //     }

    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/My Window")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        MyWindow window = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
    }
}