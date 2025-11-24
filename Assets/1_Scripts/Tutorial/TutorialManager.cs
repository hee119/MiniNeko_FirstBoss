using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Animator TutoUiAnim;
    public GameObject Stone;
    public CameraScripts CamSc;
    public void SetUiLevel(int a)
    {
        TutoUiAnim.SetInteger("Tutorial Level",a);
        if(a == 2)
            DropStone();
    }
    public void DropStone()
    {
        Stone.SetActive(true);
        CamSc.CameraShake(100f);
    }
    void Start()
    {
        TutoUiAnim = gameObject.GetComponent<Animator>();
        TutoUiAnim.SetInteger("Tutorial Level",1);
    }
}
