using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using GtMsg;

#if UNITY_EDITOR  
using UnityEditor;
#endif 
public class CreateAIBulletPathSets : MonoBehaviour {

	// Use this for initialization
	public AIBulletPathSets aiBulletPathSets;
	void Start () {
	
		OnCreateSets ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCreateSets(){


		aiBulletPathSets = new AIBulletPathSets ();
		
		foreach (var createGroup in GetComponentsInChildren<CreateAIBulletPathGroup> ()) {
			
			createGroup.OnCreateGroup ();

			aiBulletPathSets.GroupList.Add (createGroup.Group);
		}

		using (var output = File.Create(Application.dataPath+"/Resources/File/AIBulletPathSets.bytes"))
		{
			aiBulletPathSets.WriteTo(output);
		}
	}

	void OnDestroy(){
		#if UNITY_EDITOR 
		AssetDatabase.Refresh ();
		#endif 
	}
}
