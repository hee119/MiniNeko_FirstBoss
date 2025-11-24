using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTutorialUi : MonoBehaviour
{
    public int Level;
    public TutorialManager tutomana;
    void OnTriggerEnter2D(Collider2D collision)
    {
        tutomana.SetUiLevel(Level);
        Destroy(gameObject);
    }
}
