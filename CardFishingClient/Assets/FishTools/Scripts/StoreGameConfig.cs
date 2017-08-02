using UnityEngine;
using System.IO;

using GtMsg;
using Google.Protobuf;
#if UNITY_EDITOR 
using UnityEditor;
#endif
public class StoreGameConfig : MonoBehaviour {

    [ContextMenu("Do")]
    void Do()
    {
        var gameConfig = new GameConfig();
        ProductGameConfig[] children = transform.GetComponentsInChildren<ProductGameConfig>();
        for(int i = 0; i < children.Length; i++)
        {
            gameConfig.FishConfigs.Add(new FishData ());
        }
        

        foreach (ProductGameConfig child in children)
        {
            int index = (int)child.FishKind;
            gameConfig.FishConfigs[index].FishName = child.FishName;
            gameConfig.FishConfigs[index].FishModel = child.gameObject.name;
            gameConfig.FishConfigs[index].FishImportance =(int) child.FishImportance;
            foreach(var score in child.FishScore)
            {
                gameConfig.FishConfigs[index].FishScore.Add(score);
            }
            foreach(var effect in child.FishDieEffect)
            {
                gameConfig.FishConfigs[index].FishDieEffect.Add(ProductGameConfig.DieEffectString[(int) effect]);
            }
            foreach(var action in child.FishActionDataList)
            {
                gameConfig.FishConfigs[index].FishActionDataList.Add(new FishActionData { ActionName = action.ActionName, ActionFrame = action.ActionFrame, IsIdle = action.IsIdle });
            }
        }

        //普通子弹 0
        //gameConfig.BulletConfigs.Add(new BulletData { ConfigID = 0, Speed = 10, Radius = 10.0f, Model = "zidan_0", YuwangModel = "yuwang_0", TurretModel = "pao_1", Multiple = 1 });
        //gameConfig.BulletConfigs.Add(new BulletData { ConfigID = 1, Speed = 10, Radius = 10.0f, Model = "zidan_1", YuwangModel = "yuwang_1", TurretModel = "pao_2", Multiple = 2 });
        //gameConfig.BulletConfigs.Add(new BulletData { ConfigID = 2, Speed = 10, Radius = 10.0f, Model = "zidan_2", YuwangModel = "yuwang_2", TurretModel = "pao_3", Multiple = 3 });
        //gameConfig.BulletConfigs.Add(new BulletData { ConfigID = 3, Speed = 10, Radius = 10.0f, Model = "zidan_3", YuwangModel = "yuwang_3", TurretModel = "pao_4", Multiple = 4 });
        //gameConfig.BulletConfigs.Add(new BulletData { ConfigID = 4, Speed = 10, Radius = 10.0f, Model = "zidan_4", YuwangModel = "yuwang_4", TurretModel = "pao_5", Multiple = 5 });

        //技能子弹 暂时用不上
        //gameConfig.InstantConfigs.Add(new InstantData{Type = 0,Speed = 10, Multiple = 6000, Model = "zidan_0", Effect = "Skill_0", Discribe = ""});
        //gameConfig.InstantConfigs.Add(new InstantData { ConfigID = 0, Speed = 10, Multiple = 6000, Model = "zidanSkill_1", Effect = "Skill_1", Discribe = "" });
        //gameConfig.InstantConfigs.Add(new InstantData { ConfigID = 1, Speed = 10, Multiple = 7000, Model = "zidanSkill_2", Effect = "Skill_2", Discribe = "" });
        //gameConfig.InstantConfigs.Add(new InstantData { ConfigID = 2, Speed = 15, Multiple = 8000, Model = "zidanSkill_3", Effect = "Skill_3", Discribe = "" });
        //gameConfig.InstantConfigs.Add(new InstantData { ConfigID = 3, Speed = 10, Multiple = 9000, Model = "zidanSkill_4", Effect = "Skill_4", Discribe = "" });

        //道具 1  Price 道具(价格)
        gameConfig.PropConfigs.Add(new PropData { ConfigID = 0, Price = 1000 }); //冰冻 
        gameConfig.PropConfigs.Add(new PropData { ConfigID = 1, Price = 1000 }); //锁定
        gameConfig.PropConfigs.Add(new PropData { ConfigID = 2, Price = 1000 }); //加速

        //技能 2
        //gameConfig.MagicConfigs.Add(new MagicData { ConfigID = 3, Multiple = 2000 }); //激光

        
        gameConfig.GunPos.Add(new Vec2 { X = 260, Y = 50 });
        gameConfig.GunPos.Add(new Vec2 { X = 700, Y = 50 });
        gameConfig.GunPos.Add(new Vec2 { X = 700, Y = 590 });
        gameConfig.GunPos.Add(new Vec2 { X = 260, Y = 590 });
       

        //gameConfig.TurretPos.Add (new Vec2 {X = 120, Y = 640});
        //gameConfig.TurretPos.Add (new Vec2 {X = 840, Y = 640});
        gameConfig.TurretPos.Add(new Vec2 { X = 260, Y = 0 });
        gameConfig.TurretPos.Add(new Vec2 { X = 700, Y = 0 });
        gameConfig.TurretPos.Add(new Vec2 { X = 700, Y = 640 });
        gameConfig.TurretPos.Add(new Vec2 { X = 260, Y = 640 });

        //添加倍数逻辑
        {
            //List<BulletData> list = new List<BulletData>();
            //float originCount = gameConfig.BulletConfigs.Count;

            //int[] muls = new int[] { 100, 200, 300, 400, 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 4500, 5000, 5500, 6000, 6500, 7000, 7500, 8000 };
            //float mulCount = muls.Length;


            //var step = originCount / mulCount;

            //for (int j = 0; j < mulCount; j++)
            //{

            //    int s = (int)(step * (float)j);

            //    list.Add(new BulletData(gameConfig.BulletConfigs[s]));
            //    list[j].ConfigID = j;
            //    list[j].Multiple = muls[j];

            //    Debug.Log(list[j]);
            //}
            //gameConfig.BulletConfigs.Clear();

            //for (int j = 0; j < mulCount; j++)
            //{

            //    gameConfig.BulletConfigs.Add(list[j]);
            //}

        }

        //技能子弹配置
        //{
        //    int[] muls = new int[] { 6000, 7000, 8000, 9000 };

        //    for (int j = 0; j < muls.Length; j++)
        //    {

        //        if (j < gameConfig.InstantConfigs.Count)
        //        {

        //            gameConfig.InstantConfigs[j].Multiple = muls[j];
        //        }
        //    }
        //}

        var fileName = "GameConfig";

        using (var output = File.Create(Application.dataPath + "/Resources/File/" + fileName + ".bytes"))
        {
            gameConfig.WriteTo(output);
        }
        CreateBulletConfig();
    }
    void CreateBulletConfig()
    {

        BulletConfig bulletConfig = new BulletConfig();

        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 0, TurretModel = "Pao_0_0", ConfigID = 0, BulletModel = "FX_Pao_zidan_0_0", NetModel = "FX_Pao_zidan_bd_0_0", Multiple = 1, Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_0_0", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1" });
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 0, TurretModel = "Pao_0_0", ConfigID = 1, BulletModel = "FX_Pao_zidan_0_0", NetModel = "FX_Pao_zidan_bd_0_0", Multiple = 5, Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_0_0", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1" });
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 0, TurretModel = "Pao_0_1", ConfigID = 2, BulletModel = "FX_Pao_zidan_0_1", NetModel = "FX_Pao_zidan_bd_0_1", Multiple = 10, Speed = 10, Radius = 10.0f, SoundIndex = 1, Paokou = "FX_Pao_qiangkou_0_1", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1" });
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 0, TurretModel = "Pao_0_1", ConfigID = 3, BulletModel = "FX_Pao_zidan_0_1", NetModel = "FX_Pao_zidan_bd_0_1", Multiple = 20, Speed = 10, Radius = 10.0f, SoundIndex = 1, Paokou = "FX_Pao_qiangkou_0_1", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1" });
                                                                                                                                                                                                                                                                                       
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 1, TurretModel= "Pao_1_0", ConfigID = 0, BulletModel = "FX_Pao_zidan_1_0", NetModel = "FX_Pao_zidan_bd_1_0", Multiple = 1,  Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_1_0", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1" });
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 1, TurretModel= "Pao_1_0", ConfigID = 1, BulletModel = "FX_Pao_zidan_1_0", NetModel = "FX_Pao_zidan_bd_1_0", Multiple = 5,  Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_1_0", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1"});
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 1, TurretModel= "Pao_1_0", ConfigID = 2, BulletModel = "FX_Pao_zidan_1_0", NetModel = "FX_Pao_zidan_bd_1_0", Multiple = 10,  Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_1_0", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1"});
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 1, TurretModel= "Pao_1_1", ConfigID = 3, BulletModel = "FX_Pao_zidan_1_1", NetModel = "FX_Pao_zidan_bd_1_1", Multiple = 20,  Speed = 10, Radius = 10.0f, SoundIndex = 1, Paokou = "FX_Pao_qiangkou_1_1", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1"});
		bulletConfig.BulletList.Add(new BulletExtern { TurretID = 1, TurretModel= "Pao_1_1", ConfigID = 4, BulletModel = "FX_Pao_zidan_1_1", NetModel = "FX_Pao_zidan_bd_1_1", Multiple = 30, Speed = 10, Radius = 10.0f, SoundIndex = 1, Paokou = "FX_Pao_qiangkou_1_1", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1" });
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 1, TurretModel= "Pao_1_1", ConfigID = 5, BulletModel = "FX_Pao_zidan_1_1", NetModel = "FX_Pao_zidan_bd_1_1", Multiple = 40, Speed = 10, Radius = 10.0f, SoundIndex = 1, Paokou = "FX_Pao_qiangkou_1_1", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1" });
                                                                                                                                                                                                                                                                                       
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 2, TurretModel= "Pao_2_0", ConfigID = 0, BulletModel = "FX_Pao_zidan_2_0", NetModel = "FX_Pao_zidan_bd_2_0", Multiple = 1, Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_2_0", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1" });
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 2, TurretModel= "Pao_2_0", ConfigID = 1, BulletModel = "FX_Pao_zidan_2_0", NetModel = "FX_Pao_zidan_bd_2_0", Multiple = 5, Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_2_0", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1"});
		bulletConfig.BulletList.Add(new BulletExtern { TurretID = 2, TurretModel= "Pao_2_0", ConfigID = 2, BulletModel = "FX_Pao_zidan_2_0", NetModel = "FX_Pao_zidan_bd_2_0", Multiple = 10, Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_2_0", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1"});
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 2, TurretModel= "Pao_2_0", ConfigID = 3, BulletModel = "FX_Pao_zidan_2_0", NetModel = "FX_Pao_zidan_bd_2_0", Multiple = 20, Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_2_0", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1"});
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 2, TurretModel= "Pao_2_1", ConfigID = 4, BulletModel = "FX_Pao_zidan_2_1", NetModel = "FX_Pao_zidan_bd_2_1", Multiple = 30, Speed = 10, Radius = 10.0f, SoundIndex = 1, Paokou = "FX_Pao_qiangkou_2_1", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1"});
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 2, TurretModel= "Pao_2_1", ConfigID = 5, BulletModel = "FX_Pao_zidan_2_1", NetModel = "FX_Pao_zidan_bd_2_1", Multiple = 40, Speed = 10, Radius = 10.0f, SoundIndex = 1, Paokou = "FX_Pao_qiangkou_2_1", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1" });
		bulletConfig.BulletList.Add(new BulletExtern { TurretID = 2, TurretModel= "Pao_2_1", ConfigID = 6, BulletModel = "FX_Pao_zidan_2_1", NetModel = "FX_Pao_zidan_bd_2_1", Multiple = 50, Speed = 10, Radius = 10.0f, SoundIndex = 1, Paokou = "FX_Pao_qiangkou_2_1", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1" });
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 2, TurretModel= "Pao_2_1", ConfigID = 7, BulletModel = "FX_Pao_zidan_2_1", NetModel = "FX_Pao_zidan_bd_2_1", Multiple = 60, Speed = 10, Radius = 10.0f, SoundIndex = 1, Paokou = "FX_Pao_qiangkou_2_1", SkillModel = "FX_Pao_Skill01_0_1", Skillbg = "FX_UI_Pao_pingmu_0_1" });
                                                                                                                                                                                                                                                                         
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 3, TurretModel = "Pao_3_0", ConfigID = 0, BulletModel = "FX_Pao_zidan_3_0", NetModel = "FX_Pao_zidan_bd_3_0", Multiple = 1, Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_3_0",SkillModel = "FX_Pao_Skill01_3_1", Skillbg = "FX_UI_Pao_pingmu_3_1"});
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 3, TurretModel = "Pao_3_0", ConfigID = 1, BulletModel = "FX_Pao_zidan_3_0", NetModel = "FX_Pao_zidan_bd_3_0", Multiple = 5, Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_3_0",SkillModel = "FX_Pao_Skill01_3_1", Skillbg = "FX_UI_Pao_pingmu_3_1"});
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 3, TurretModel = "Pao_3_0", ConfigID = 2, BulletModel = "FX_Pao_zidan_3_0", NetModel = "FX_Pao_zidan_bd_3_0", Multiple = 10, Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_3_0",SkillModel = "FX_Pao_Skill01_3_1", Skillbg = "FX_UI_Pao_pingmu_3_1"});
		bulletConfig.BulletList.Add(new BulletExtern { TurretID = 3, TurretModel = "Pao_3_0", ConfigID = 3, BulletModel = "FX_Pao_zidan_3_0", NetModel = "FX_Pao_zidan_bd_3_0", Multiple = 20, Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_3_0",SkillModel = "FX_Pao_Skill01_3_1", Skillbg = "FX_UI_Pao_pingmu_3_1"});
		bulletConfig.BulletList.Add(new BulletExtern { TurretID = 3, TurretModel = "Pao_3_0", ConfigID = 4, BulletModel = "FX_Pao_zidan_3_0", NetModel = "FX_Pao_zidan_bd_3_0", Multiple = 30, Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_3_0",SkillModel = "FX_Pao_Skill01_3_1", Skillbg = "FX_UI_Pao_pingmu_3_1"});
		bulletConfig.BulletList.Add(new BulletExtern { TurretID = 3, TurretModel = "Pao_3_0", ConfigID = 5, BulletModel = "FX_Pao_zidan_3_0", NetModel = "FX_Pao_zidan_bd_3_0", Multiple = 40, Speed = 10, Radius = 10.0f, SoundIndex = 0, Paokou = "FX_Pao_qiangkou_3_0",SkillModel = "FX_Pao_Skill01_3_1", Skillbg = "FX_UI_Pao_pingmu_3_1"});
		bulletConfig.BulletList.Add(new BulletExtern { TurretID = 3, TurretModel = "Pao_3_1", ConfigID = 6, BulletModel = "FX_Pao_zidan_3_1", NetModel = "FX_Pao_zidan_bd_3_1", Multiple = 50, Speed = 10, Radius = 10.0f, SoundIndex = 1, Paokou = "FX_Pao_qiangkou_3_1",SkillModel = "FX_Pao_Skill01_3_1", Skillbg = "FX_UI_Pao_pingmu_3_1" });
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 3, TurretModel = "Pao_3_1", ConfigID = 7, BulletModel = "FX_Pao_zidan_3_1", NetModel = "FX_Pao_zidan_bd_3_1", Multiple = 60, Speed = 10, Radius = 10.0f, SoundIndex = 1, Paokou = "FX_Pao_qiangkou_3_1",SkillModel = "FX_Pao_Skill01_3_1", Skillbg = "FX_UI_Pao_pingmu_3_1" });
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 3, TurretModel = "Pao_3_1", ConfigID = 8, BulletModel = "FX_Pao_zidan_3_1", NetModel = "FX_Pao_zidan_bd_3_1", Multiple = 70, Speed = 10, Radius = 10.0f, SoundIndex = 1, Paokou = "FX_Pao_qiangkou_3_1",SkillModel = "FX_Pao_Skill01_3_1", Skillbg = "FX_UI_Pao_pingmu_3_1" });
        bulletConfig.BulletList.Add(new BulletExtern { TurretID = 3, TurretModel = "Pao_3_1", ConfigID = 9, BulletModel = "FX_Pao_zidan_3_1", NetModel = "FX_Pao_zidan_bd_3_1", Multiple = 80, Speed = 10, Radius = 10.0f, SoundIndex = 1, Paokou = "FX_Pao_qiangkou_3_1",SkillModel = "FX_Pao_Skill01_3_1", Skillbg = "FX_UI_Pao_pingmu_3_1" });


      
        var fileName = "BulletConfig";

        using (var output = File.Create(Application.dataPath + "/Resources/File/" + fileName + ".bytes"))
        {
            bulletConfig.WriteTo(output);
        }

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
