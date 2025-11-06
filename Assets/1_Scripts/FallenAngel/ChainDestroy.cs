using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainDestroy : MonoBehaviour
{
    private bool isHit = false;
    private GameObject Chain;
    private void Awake()
    {
        Chain = transform.parent.gameObject;
    }
    private void Start()
    {
        StartCoroutine(Destroy());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isHit = true;
            collision.gameObject.GetComponent<PlayerMove>().isStop = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMove>().isStop = false;
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        if (isHit)
        {
            Destroy(Chain, 1.5f);
            yield return new WaitForSeconds(1.5f);
        }
        else
        {
            Destroy(Chain);
        }
    }
}
