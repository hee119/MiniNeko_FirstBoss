using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int AttackDamage = 20;
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log($"i attacked {col.transform.name}");
        if(col.CompareTag("Boss")||col.CompareTag("DestoryableStructer")){
            EnemyHealthScript? healthScript = col.gameObject.transform.GetComponent<EnemyHealthScript>();
            if(healthScript != null){
                healthScript.EnemyDamage(AttackDamage);
            }
        } 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
