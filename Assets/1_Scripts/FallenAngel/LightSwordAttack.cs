using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightSwordAttack : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        sr.color = new Color(1,1,1,0);
        sr.sortingOrder = -1;

        Vector3 curPos = transform.parent.position;
        Vector3 startPos = transform.parent.position + Vector3.right * 2f; 

        transform.position = startPos;

        StartCoroutine(Attack());
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(curPos, 1f));
        seq.Join(sr.DOFade(1f, 1f));
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        sr.sortingOrder = 5;
        yield return new WaitForSeconds(0.2f);
        rb.AddForce(Vector2.left*4444);
    }
    
}