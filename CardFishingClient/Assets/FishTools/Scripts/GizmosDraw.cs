using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosDraw : MonoBehaviour {

    public Action CallGizmosDraw;

    private void OnDrawGizmos()
    {
        if(CallGizmosDraw != null)
        {
            CallGizmosDraw();
        }
    }
}
