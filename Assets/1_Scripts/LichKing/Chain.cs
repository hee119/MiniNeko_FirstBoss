using System;
using System.Collections;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public GameObject target;
    public float stretchSpeed = 5f;
    public GameObject Flooring;
    public bool isStay;
    public bool isTrigger;
    public GameObject[] portal;
    private SpriteRenderer sr;
    private Vector3 scale;


    void Awake()
    {

        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GetComponent<BoxCollider2D>().offset.x != 0f || GetComponent<BoxCollider2D>().offset.x != 0f)
        {
            Vector2 a = GetComponent<BoxCollider2D>().offset;
            a.x = 0;
            a.y = 0;
            GetComponent<BoxCollider2D>().offset = a;
        }
        if (scale.x > 0)
        {
            foreach (var h in portal)
            {
                h.transform.localScale = new Vector2(0.1f, 0.1f);
            }
        }
        else
        {
            foreach (var h in portal)
            {
                h.transform.localScale = new Vector2(0, 0);
            }
        }
        
            if (isStay == false || isTrigger == false)
            {
                float a = Mathf.Lerp(transform.localScale.y, 0, 2);
                transform.localScale = new Vector3(a, a, a);
            }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = true;
            Debug.Log(Flooring.name);
            StartCoroutine(StretchRoutine());
        }

        IEnumerator StretchRoutine()
        {
            float t = 0;
            while (isTrigger)
            {
                Vector3 dir = target.transform.position - transform.position;
                float Look = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, Look + 85);
                float a = Mathf.Lerp(transform.localScale.y, t * 2, t / 2);
                transform.localScale = new Vector3(2, a, 1);
                t += Time.deltaTime;
                yield return null;
            }
        }

        }

    }
