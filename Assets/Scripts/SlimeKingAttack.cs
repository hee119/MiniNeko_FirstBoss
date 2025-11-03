using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeKingAttack : MonoBehaviour
{
    public GameObject Target;
    public GameObject Wave;
    float atkbftime = -5.0f;
    Vector2 atksee;
    void Update()
    {
        if(Time.time-atkbftime > 5f)    
        {
            atkbftime = Time.time;
            
            if (transform.transform.position.x > Target.transform.position.x)
                atksee = Vector2.left * 200f * Math.Abs(transform.transform.position.x-Target.transform.position.x)/8 + Vector2.up * 1000f;
            if (transform.transform.position.x < Target.transform.position.x)
                atksee = Vector2.right * 200f * Math.Abs(transform.transform.position.x - Target.transform.position.x) / 8 + Vector2.up * 1000f;
            GameObject Wave1 = Instantiate(Wave);
            Wave1.transform.position = transform.position;
            Wave1.transform.Translate(Vector2.down * 2f);
            Destroy(Wave1, 0.8f);
            gameObject.GetComponent<Rigidbody2D>().AddForce(atksee);
            
        }
    }
}
