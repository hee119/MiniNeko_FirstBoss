using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{   
    public string CameraMode;
    public Transform Target;
    public float yValue;
    public float cameraSpeed;
    public float ylimit = -5f;
    public float Size = 14f;
    float ShakeForce = 0f;
    void Update()
    {
        if(Target.transform.position.y > ylimit+yValue)
            transform.position = new Vector3(transform.position.x+(Target.position.x-transform.position.x)*Time.deltaTime/1.2f*cameraSpeed,
                                        transform.position.y+((Target.position.y+yValue)-transform.position.y)*Time.deltaTime/1.2f*cameraSpeed,
                                        -10);
        else
            transform.position = new Vector3(transform.position.x+(Target.position.x-transform.position.x)*Time.deltaTime/1.2f*cameraSpeed,
                                        transform.position.y+(ylimit+yValue-transform.position.y)*Time.deltaTime/1.2f*cameraSpeed,
                                        -10);
        if(ShakeForce >= 0.01f)
        {
            float xdif = UnityEngine.Random.Range(-ShakeForce,ShakeForce);
            float ydif = UnityEngine.Random.Range(-ShakeForce,ShakeForce);
            transform.position = new Vector3(transform.position.x+(xdif*Time.deltaTime),transform.position.y+(ydif*Time.deltaTime),transform.position.z);
            ShakeForce -= ShakeForce*5f*Time.deltaTime;
        }
        GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize+((Size-GetComponent<Camera>().orthographicSize)*Time.deltaTime*3f);
    }
    public void CameraShake(float a)
    {
        Debug.Log("SHAKE");
        ShakeForce = a;
    }
}
