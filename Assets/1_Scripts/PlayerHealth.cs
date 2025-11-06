using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class PlayerHealth : MonoBehaviour
{  
    public int maxHp;
    private float _curHp;
    public Image HpBar;
    public TextMeshProUGUI HealthText;
    public GameObject DiedUI;
    float nuckback = 0f;
    Vector2 lF;
    float mvs;
    void Start()
    {
        _curHp = maxHp;
        mvs = gameObject.GetComponent<PlayerMove>().moveSpeed;
        HpBar.fillAmount = 0.5f;
    }
    public float CurHp
    {
        get { return _curHp;}
        set
        {
            float target = value;
            Debug.Log(target);
            if (target >= maxHp)
                _curHp = maxHp;
            else if (target <= 0)    
            {
                _curHp = 0;
                DiedUI.SetActive(true);
            }

            DOTween.To(() => _curHp, x => _curHp = x, target, 0.5f).SetEase(Ease.OutQuad);
        }   
    }
    public void Damage(int num){
        CurHp -= num;
    }
    void Update()
    {
        HpBar.fillAmount = CurHp / maxHp;
        HealthText.text = $"{Math.Round(CurHp)}/{maxHp}";
        if (transform.position.y < -50)
        {
            CurHp -= 10;
            transform.position = new Vector2(0, 10);
        }
        if (transform.position.y < -50)
        {
            CurHp -= 10;
            transform.position = new Vector2(0, 10);
        }
        knockback();
    }
    void knockback()
    {
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
    }
}