using UnityEngine;
using System.Collections;
using GtMsg;

public class CreateAIBulletPathGroup : MonoBehaviour {

	[HideInInspector]
	public AIBulletPathGroup Group;

	// Use this for initialization
	void Start () {

	
	}
	// Update is called once per frame
	void Update () {
	
	}
	public void OnCreateGroup(){

		Debug.Log ("Create Group");

		Group = new AIBulletPathGroup ();

		//Group.GroupName = gameObject.name;

		foreach (var createPath in GetComponentsInChildren<CreatePath> ()) {

			AIBulletPath aiBulletPath = new AIBulletPath ();

			//aiBulletPath.PathName = createPath.name;

			createPath.OnCreatePath ();

			foreach (var vec3 in createPath.pathListVec3) {

				aiBulletPath.PathPointList.Add (new Vec2 (){ X = vec3.x, Y = vec3.y });
			}

			Group.PathList.Add (aiBulletPath);
		}

//		Group.GroupName = Name;
//		Group.PathList.AddaiBulletPath;
	}
}
