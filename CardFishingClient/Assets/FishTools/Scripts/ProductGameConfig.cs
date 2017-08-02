using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum FishTypeDefine
{
    xiaohuangyu,
    cheqiyu,
    jinqiangyu_blue,
    jinqiangyu_red,
    jinqiangyu_gold,
    gaiputi,
    tianshiyu,
    fangyu,
    douyu_green,
    douyu_red,
    shiziyu,
    xiaochouyu,
    haigui_red,
    haigui_ice,
    haigui_gold,
    hetun_green,
    hetun_blue,
    dinianyu,
    dianmanyu,
    caoyu,
    jianyu,
    bianfuyu,
    bianfuyu_gold,
    denglongyu,
    shayu,
    sitoukuangsha,
    swk,
    jgb,
    jgq,
    hy1,
    hy2,
    hy3,
    mry,
    hy,
    dwx,
    DragonBoss2,
    eyu,
    zy,    
    mgy,
    pxboss,

}
public class ProductGameConfig : MonoBehaviour {

    [System.Serializable]
    public class ActionData
    {
        public string ActionName;
        public int ActionFrame;
        public bool IsIdle;
    }
    public enum Importance
    {
        low, 
        normal,
        high
    }
    public enum DieEffect
    {
        BubleEffect_Small_0,
        BubleEffect_Small_1,
        BubleEffect_Small_2,
        BubleEffect_Small_3,
        effect_meirenyu,
    }
    static public string[] DieEffectString = {
        "BubleEffect_Small_0",
        "BubleEffect_Small_1",
        "BubleEffect_Small_2",
        "BubleEffect_Small_3",
        "effect_meirenyu"};

    public FishTypeDefine FishKind;
    public string FishName = "";
    public Importance FishImportance = Importance.low;

    public int[] FishScore;
    public DieEffect[] FishDieEffect;
    public ActionData[] FishActionDataList;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
