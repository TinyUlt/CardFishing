using UnityEngine;

using UnityEngine.UI;


class TinyTransform : MonoSingletion<TinyTransform>
{
    private Camera cmr;
    private float L;//场景距离L为原点
    private float OW;//游戏逻辑分辨率为
    private float OH;
    private float OL;//逻辑距离L为原点
    private float SceneW;//该原点的场景宽度是
    private float SceneH;//该原点的场景高度是
    private float ScreenW;//游戏当前分辨率为（UI坐标）
    private float ScreenH;
    private float UIH;
    private float UIW;

    //3维逻辑坐标转场景坐标
    public Vector3 GetLogicToScenePosition(Vector3 op)
    {
        var x = SceneW / OW * op.x - SceneW / 2;
        var y = SceneH / OH * op.y - SceneH / 2;
        var z = SceneH / OH * op.z;

        return new Vector3(x, y, z);
    }
    public Vector3 GetSceneToLogicPosition(Vector3 op)
    {
        var x = (op.x + SceneW / 2) * OW / SceneW;
        var y = (op.y + SceneH / 2) * OH / SceneH;
        var z = op.z * OH / SceneH;

        return new Vector3(x, y, z);
    }
    public Vector3 GetSceneToUIPosition(Vector3 op)
    {
        var p = GetSceneToLogicPosition(op);
        return GetLogicToUIPosition(p);
    }
    //逻辑坐标转场景屏幕坐标(z轴为0的场景坐标)
    public Vector3 GetLogicToScenePositionZequal0(Vector3 op)
    {
        var p = LogicThreeToTwoPosition(op);

        return GetLogicToScenePosition(p);
    }
    //逻辑坐标转UI坐标
    public Vector2 GetLogicToUIPosition(Vector3 op)
    {
        var p = LogicThreeToTwoPosition(op);
        var x = UIW * p.x / OW - UIW / 2;
        var y = UIH * p.y / OH - UIH / 2;
        return new Vector2(x, y);
    }
    //UI坐标转逻辑坐标
    public Vector2 GetUIToLogicPosition(Vector3 op)
    {
        var x = OW * op.x / (UIW + UIW / 2);
        var y = OH * op.y / (UIH + UIH / 2);

        return new Vector2(x, y);
    }
    //逻辑坐标转屏幕坐标
    public Vector2 GetLogicToScreenPosition(Vector3 op)
    {
        var p = GetLogicToScenePositionZequal0(op);
        var x = ScreenW * p.x / SceneW;
        var y = ScreenH * p.y / SceneH;
        p.x += ScreenW / 2;
        p.y += ScreenH / 2;
        return p;
    }
    //屏幕坐标转逻辑坐标
    public Vector2 GetScreenToLogicPosition(Vector3 op)
    {
        var x = OW * op.x / ScreenW;
        var y = OH * op.y / ScreenH;

        return new Vector2(x, y);
    }
    //增加深度
    public void AddDeep(Transform tra, Vector3 op, float deep)
    {
        var p1 = LogicThreeToTwoPosition(op);
        var p2 = LogicTwoToThreePosition(p1, deep + op.z);
        tra.position = GetLogicToScenePosition(p2);
        var s = TwoToThreeScale(op.z, deep);
        tra.localScale = Vector3.one * s;
    }
    //三维场景转二维场景
    public Vector3 SceneThreeToTwoPosition(Vector3 op)
    {
        var x = L * op.x / (L + op.z);
        var y = L * op.y / (L + op.z);
        var z = 0;

        return new Vector3(x, y, z);
    }
    //三维逻辑转二维逻辑
    public Vector3 LogicThreeToTwoPosition(Vector3 op)
    {
        var x = OW / 2 - (OW / 2 - op.x) * OL / (OL + op.z);
        var y = OH / 2 - (OH / 2 - op.y) * OL / (OL + op.z);
        var z = 0;

        return new Vector3(x, y, z);
    }
    //三维转二维缩放
    public float ThreeToTwoScale(float z)
    {
        return OL / (z + OL);
    }
    //二维逻辑转三维逻辑
    public Vector3 LogicTwoToThreePosition(Vector3 p, float deep)
    {
        var z = deep;
        var x = (p.x - OW / 2) * (OL + z) / OL + OW / 2;
        var y = (p.y - OH / 2) * (OL + z) / OL + OH / 2;

        return new Vector3(x, y, z);
    }
    //2维转3维缩放
    public float TwoToThreeScale(float z, float deep)
    {
        return 1 + deep / (OL + z); //((deep + OL + z) / (OL + z));
    }
    //初始化
    public void Init(Camera c, float l, Vector2 design)
    {
        cmr = c;
        L = l;
        OW = design.x;
        OH = design.y;

        ScreenW = Screen.width;//游戏当前分辨率为
        ScreenH = Screen.height;

        SceneH = Mathf.Tan(cmr.fieldOfView / 2 * Mathf.PI / 180) * L * 2;
        SceneW = ScreenW / ScreenH * SceneH;
        OL = OH * L / SceneH;

        var canvas = GameObject.Find("Canvas");
        var canvesscaler = canvas.GetComponent("CanvasScaler") as CanvasScaler;
        if (canvesscaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
        {
            UIW = canvesscaler.referenceResolution.x;
            UIH = UIW * ScreenH / ScreenW;
        }
        else
        {
            UIW = ScreenW;
            UIH = ScreenH;
        }
    }
}

