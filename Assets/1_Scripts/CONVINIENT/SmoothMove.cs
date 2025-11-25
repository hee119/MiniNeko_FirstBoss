using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SmoothMove{
    public static void slight(Transform t, float x, float y, float z)
    {
        t.position = new Vector3(x+(x-t.position.x)/10f*Time.deltaTime,y+(y-t.position.y)/10f*Time.deltaTime,z);
    }

    public static void slight(Transform t1, Transform t2)
    {
        t1.position = new Vector3(t2.position.x+(t2.position.x-t1.position.x)/10f*Time.deltaTime,t2.position.y+(t2.position.y-t1.position.y)/10f*Time.deltaTime,t2.position.z);
    }
}
