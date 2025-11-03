using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenAngelAttack : MonoBehaviour
{
    private GameObject player;
    private float time;

    public GameObject chain;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        time = Time.time;
    }

    void chainAttack()
    {
        var chainLeft = Instantiate(chain);
        var chainRight = Instantiate(chain);
        chainRight.GetComponent<SpriteRenderer>().flipX = true;
    }
}
