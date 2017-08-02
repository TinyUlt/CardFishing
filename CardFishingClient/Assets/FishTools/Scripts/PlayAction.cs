using UnityEngine;
using System.Collections;

public class PlayAction : MonoBehaviour {

	// Use this for initialization
	public string ActionName;
	public int Duration;
	public float ActionRandom = 1.0f;
	void Start () {
	
	}
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.Label(transform.position + Vector3.up*4,
            "动作: " + ActionName + " 时间： "+Duration.ToString());
    }
#endif
}
