using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CreateIdleRandomPath : MonoBehaviour {

	public int width;
	public int height;
	public int PathCount;
	public int TurnCount ;
	public int PointCount;
	public int ZDistance;
	public string Name;
	public int GroupMax;
	public int GroupMin;
	public int GroupMagin;
	public int IdleTime;

	public void Init(){

		if (TurnCount < 5) {
		
			TurnCount = 5;
		}
		var mid = TurnCount / 2;
		var pre = mid - 1;
		var nex = mid + 1;

		List<Transform> list = new List<Transform> ();

		foreach (Transform item in transform) {

			list.Add (item);

		}
		foreach (Transform item in list) {

			item.parent = null;

			Destroy(item.gameObject);
		}

		for (int k = 0; k < PathCount; k++) {

			string AName = Name;

			AName +=  "_" + k;

			var Paths = new Vector3[TurnCount];

			int t = 0;

			Paths [t] = createOutTrunPoint ();
			t++;

			for (; t < TurnCount - 1; t++) {

				Paths[t] = createInTurnPoint ();
			}
			Paths[t] = createOutTrunPoint ();

			int count = Random.Range (GroupMin, GroupMax + 1);
			for (int p = 0; p < count; p++) {

				string BName = AName;

				if (GroupMin != 1) {

					BName = AName + "_" + p;
				}
				GameObject pointObj = new GameObject (BName);

				pointObj.transform.parent = transform;

				var pointScript = pointObj.AddComponent<CreatePath> ();


				pointScript.paths = new Transform[TurnCount];

				for (int i = 0; i < TurnCount; i++) {

					GameObject child = new GameObject ("Child");

					child.transform.parent = pointObj.transform;

					pointScript.paths [i] = child.transform;
				}

				for(int j = 0; j < TurnCount; j ++){

					var scale = (1 - Paths [j].z / 500);
					var magin = GroupMagin / 2 * scale;
					var x = Paths [j].x + Random.Range (-magin, magin);
					var y = Paths [j].y + Random.Range (-magin, magin);
					var z = Paths [j].z + Random.Range (-magin , magin);
					pointScript.paths [j].position = new Vector3(x,y,z);
				}
				pointScript.paths [mid].gameObject.AddComponent<Idle> ().IdleIndex = IdleTime;

				var wp = pointScript.paths [mid].position;
				pointScript.paths [pre].position = new Vector3(wp.x, wp.y, wp.z + 100);
				pointScript.paths [nex].position = new Vector3(wp.x, wp.y, wp.z - 100);
				pointScript.pointCount = PointCount;
			}
		}
	}
	void Start () {

		Init ();
	}

	Vector3 createOutTrunPoint(){
		int x = 0;
		int y = 0;
		int offset = 100;
		int r = Random.Range (0, 4);

		switch (r) {
		case 0:
			{
				x = -offset;
				y = Random.Range (-offset, height + offset) ;
				break;
			}
		case 1:
			{
				x = width + offset;
				y = Random.Range (-offset, height + offset) ;
				break;
			}
		case 2:
			{
				y = -offset;
				x = Random.Range (-offset, width + offset) ;
				break;
			}
		case 3:
			{
				y = height + offset;
				x = Random.Range (-offset, width + offset) ;
				break;
			}
		}

		return new Vector3 (x, y, Random.Range(-ZDistance, ZDistance));
	}
	Vector3 createInTurnPoint(){
		var magin = 100;

		return new Vector3 (Random.Range (magin, width - magin), Random.Range (magin, height - magin), Random.Range (-ZDistance, ZDistance));
	}
}
