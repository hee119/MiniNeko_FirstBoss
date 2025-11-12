using System;
using System.Collections;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public GameObject target;
    public GameObject Flooring;
    public bool isStay;
    public bool isTrigger;
    public GameObject[] portal;
    private Vector3 scale;

    private void Update()
    {
        if (GetComponent<BoxCollider2D>().offset.x != 0f || GetComponent<BoxCollider2D>().offset.y != 0f)
        {
            Vector2 a = GetComponent<BoxCollider2D>().offset;
            a.x = 0;
            a.y = 0;
            GetComponent<BoxCollider2D>().offset = a;
        }
        if (scale.x > 0)
        {
            foreach (GameObject h in portal)
            {
                h.transform.localScale = new Vector2(0.1f, 0.1f);
            }
        }
        else
        {
            foreach (GameObject h in portal)
            {
                h.transform.localScale = new Vector2(0, 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = true;
            StartCoroutine(StretchRoutine());
        }
        }
    IEnumerator StretchRoutine()
    {
        Vector3 dir;
        float t = 0;
        float Look;
        float a;
        yield return new WaitForSeconds(0.1f);
        while (isStay)
        {
            dir = target.transform.position - transform.position;
            Look = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, Look + 85);
            a = Mathf.Lerp(transform.localScale.y, dir.magnitude, t);
            transform.localScale = new Vector3(2, a, 1);
            t += Time.deltaTime;
            yield return null;
        }
    }

    }
