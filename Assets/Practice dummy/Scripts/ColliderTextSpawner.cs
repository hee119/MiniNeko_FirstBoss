using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTextSpawner : MonoBehaviour
{
    /// <summary>
    /// Name of the tag that will trigger the Text spawn
    /// </summary>
    public string TriggerTagName;
    public string TextToDisplay;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == TriggerTagName)
        {
            TextSpawner.SpawnText(TextToDisplay, transform.position);
        }
    }
}
