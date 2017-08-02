using UnityEngine;

public class main : MonoBehaviour
{
    public Camera cmr;
    public Vector3 OriginPosition;
    public GameObject SceneObj;
    public RectTransform UIObj;
    //private float UIW;
    //3维逻辑坐标转场景坐标
    [ContextMenu("LogicToScenePosition")]
    void TestLogicToScenePosition()
    {
        var p = TinyTransform.Instance.GetLogicToScenePosition(OriginPosition);//GetLogicToScenePosition(OriginPosition);
        SceneObj.transform.position = p;
        SceneObj.transform.localScale = Vector3.one;
    }
    [ContextMenu("SceneThreeToTwoPosition")]
    void TestSceneThreeToTwoPosition()
    {
        var p = TinyTransform.Instance.SceneThreeToTwoPosition(SceneObj.transform.position);//GetLogicToScenePosition(OriginPosition);
        SceneObj.transform.position = p;
    }
    [ContextMenu("LogicToScenePosition z=0")]
    void TestLogicToScenePositionZequal0()
    {
        var p = TinyTransform.Instance.GetLogicToScenePositionZequal0(OriginPosition);
        var s = TinyTransform.Instance.ThreeToTwoScale(OriginPosition.z);
        SceneObj.transform.position = p;
        SceneObj.transform.localScale = Vector3.one * s;
    }
    [ContextMenu("AddDeep")]
    void TestAddDeep()
    {
        TinyTransform.Instance.AddDeep(SceneObj.transform, OriginPosition, 100);
    }
    [ContextMenu("LogicToUIPosition")]
    void TestLogicToUIPosition()
    {
        var p = TinyTransform.Instance.GetLogicToUIPosition(OriginPosition);
        UIObj.anchoredPosition = p;
    }
   
    // Use this for initialization
    void Start()
    {
        TinyTransform.Instance.Init(cmr, 100, new Vector2(960, 640));
    }
}
