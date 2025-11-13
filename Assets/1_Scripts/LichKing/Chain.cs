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

    void Awake()
    {
        transform.localScale = Vector3.zero;
    }
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
            isStay = true;
            StartCoroutine(Flooring.GetComponent<Floor>().Check());
            StartCoroutine(StretchRoutine());
        }
    }
    IEnumerator StretchRoutine()
    {
        // 체인 늘리기
        yield return new WaitForSeconds(0.2f);
        while (isStay)
        {
            Vector3 dir = target.transform.position - transform.position;
            float look = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, look + 85);

            float targetLength = 10f; // 원하는 길이
            transform.localScale = new Vector3(2, Mathf.MoveTowards(transform.localScale.y, targetLength, Time.deltaTime * 10f), 1);

            yield return null; // 매 프레임 갱신
        }
    }



    }
