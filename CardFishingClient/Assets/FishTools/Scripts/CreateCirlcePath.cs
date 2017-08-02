using UnityEngine;
using System.Collections;

public class CreateCirlcePath : MonoBehaviour {

	// Use this for initialization

	public int count ;
	public int length ;
	public int time;
	void Start () {
	
		Init ();
	}
	public void Init(){

		float hudu = 2 * Mathf.PI / count;

		int TurnCount = 2;

		for (var i = 0; i < 15; i++) {


			GameObject obj = new GameObject ("Circle_R" + length + "_C" + count + "_"+"T"+time+"_" + i);

			obj.transform.parent = transform;

			var pointScript = obj.AddComponent<CreatePath> ();

			pointScript.paths = new Transform[TurnCount];

			Vector3 v = new Vector3 ();

			for (int j = 0; j < TurnCount; j++) {

				GameObject child = new GameObject ("Child");

				child.transform.parent = obj.transform;

				pointScript.paths [j] = child.transform;

				v.y = length * Mathf.Cos (i * hudu);

				v.z = length * Mathf.Sin (i * hudu);

				
			}

			pointScript.paths [0].position =new Vector3( 0-200 +v.z / 2, v.y, v.z);

			pointScript.paths [1].position = new Vector3(960+200 - v.z / 2,v.y, v.z);

			pointScript.pointCount = time;

			obj.transform.position = new Vector3 (0, 320, 0);
		}


	}
	// Update is called once per frame
	void Update () {
	
	}
}
