using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    public Transform Target;
    public float yValue;
    public float cameraSpeed;
    void Update()
    {
        transform.position = new Vector3(transform.position.x+(Target.position.x-transform.position.x)*Time.deltaTime/1.2f*cameraSpeed,
                                        transform.position.y+((Target.position.y+yValue)-transform.position.y)*Time.deltaTime/1.2f*cameraSpeed,
                                        -10);
        
    }
}
