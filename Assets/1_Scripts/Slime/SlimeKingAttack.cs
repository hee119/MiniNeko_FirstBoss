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
    public GameObject Missile;
    float atkbftime = -5.0f;
    float mscooltime = 0.0f;
    Vector2 atksee;
    object enemyhealthsc;
    IEnumerator coroutine;
    IEnumerator SummonMissile(int missile_num)
    {
        Debug.Log("missile Launched");
        for(int i = 0; i < missile_num; i++){
            yield return new WaitForSeconds(0.1f);
            GameObject CopyMissile = Instantiate(Missile);
            CopyMissile.transform.position = transform.position;
            CopyMissile.GetComponent<MissileScript>().Target = Target;
        }
    }
    void Start()
    {
        mscooltime = Time.time;
    }
    void Update()
    {
        //1 phase pattern
        if (Time.time - atkbftime > 5f)
        {
            atkbftime = Time.time;

            if (transform.transform.position.x > Target.transform.position.x)
                atksee = Vector2.left * 200f * Math.Abs(transform.transform.position.x - Target.transform.position.x) / 8 + Vector2.up * 1000f;
            if (transform.transform.position.x < Target.transform.position.x)
                atksee = Vector2.right * 200f * Math.Abs(transform.transform.position.x - Target.transform.position.x) / 8 + Vector2.up * 1000f;
            GameObject Wave1 = Instantiate(Wave);
            Wave1.transform.position = transform.position;
            Wave1.transform.Translate(Vector2.down * 2f);
            Destroy(Wave1, 0.8f);
            gameObject.GetComponent<Rigidbody2D>().AddForce(atksee);
        }
        //2 phase
        if (gameObject.GetComponent<EnemyHealthScript>().Health < gameObject.GetComponent<EnemyHealthScript>().StartHealth / 2)
        {
            Debug.Log("HPHALF");
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255,0,0);
            Debug.Log("COLORCHANGE");
            if (Time.time - mscooltime > 5f)
            {
                mscooltime = Time.time;
                coroutine = SummonMissile(4);
                StartCoroutine(coroutine);
            }
        }
    }
}
