using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform[] icons = GetComponentsInChildren<Transform>();
        Debug.Log($"SCALE:{GetComponent<RectTransform>().sizeDelta.x}");
        float x = 0f;
        foreach(Transform child in transform){
            child.GetComponent<RectTransform>().localPosition = new Vector3(x-(GetComponent<RectTransform>().sizeDelta.x/2f),0,0);
            x+=100f;
        }
    }
}
