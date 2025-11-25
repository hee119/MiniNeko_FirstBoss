using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int AttackDamage = 20;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Boss")||col.CompareTag("DestoryableStructer")||col.CompareTag("Enemy")){
            EnemyHealthScript healthScript = col.gameObject.transform.GetComponent<EnemyHealthScript>();
            healthScript.EnemyDamage(AttackDamage);
        } 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
