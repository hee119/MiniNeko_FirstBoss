using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{  
    public int startHP;
    public int playerHP;
    public RectTransform HPBar;
    public TextMeshProUGUI HealthText;
    public GameObject DiedUI;
    float nuckback = 0f;
    Vector2 lF;
    float mvs;
    void Start()
    {
        playerHP = startHP;
        mvs = gameObject.GetComponent<PlayerMove>().moveSpeed;
    }
    public void Damage(int num){
        playerHP -= num;
    }
    // Update is called once per frame
    
    void Update()
    {
        HealthText.text = $"{playerHP}/{startHP}";
        float HPBX = HPBar.localScale.x;
        HPBar.localScale = new Vector3(HPBX + ((float)playerHP/(float)startHP - HPBX)*Time.deltaTime*10,1f,1f);
        if (transform.position.y < -50)
        {
            playerHP -= 10;
            transform.position = new Vector2(0, 10);
        }
        if (transform.position.y < -50)
        {
            playerHP -= 10;
            transform.position = new Vector2(0, 10);
        }
        if (nuckback > 1f)
        {
            gameObject.GetComponent<PlayerMove>().moveSpeed = 0f;
        }
        else
        {
            gameObject.GetComponent<PlayerMove>().moveSpeed = mvs;
        }

        gameObject.GetComponent<Transform>().Translate(lF * -nuckback * Time.deltaTime);
        nuckback *= 0.995f;
        if (playerHP <= 0)
            DiedUI.SetActive(true);
    }
}
