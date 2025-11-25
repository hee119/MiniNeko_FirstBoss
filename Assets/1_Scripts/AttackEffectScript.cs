using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackEffectScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int Damage;
    public string Type;
    public float invisibleTime = 0.03f;
    void OnTriggerStay2D(Collider2D collision)
    {
        if(Type == "Stay"){
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerHealth>().Damage(Damage,invisibleTime);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {   
        if(Type == "Enter" || Type == ""){
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerHealth>().Damage(Damage,invisibleTime);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
