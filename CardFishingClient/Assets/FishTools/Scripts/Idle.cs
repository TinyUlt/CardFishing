using UnityEngine;
using System.Collections;

public class Idle : MonoBehaviour {

	public int IdleIndex;

	// Use this for initialization
	void Start () {
	
	}
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.Label(transform.position,
            "待机: "+ IdleIndex.ToString());
    }
#endif
}
