using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int swordDamage = 15;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("!@#@$@#^#^@#^!#^!#$");
            collision.gameObject.GetComponent<PlayerHealth>().Damage(swordDamage);
        }
    }
}
