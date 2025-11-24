using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardScript : MonoBehaviour
{
    public KeyCode Key;
    public Sprite changeSprite;
    public bool wasChanged = false;
    // Start is called before the first frame update
    public static int changedKeys = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Key))
        {   
            changedKeys += 1 - (wasChanged?1:0);
            wasChanged = true;
            gameObject.GetComponent<Image>().sprite = changeSprite;
            return;
        } 
        Debug.Log(changedKeys);
    }
}
