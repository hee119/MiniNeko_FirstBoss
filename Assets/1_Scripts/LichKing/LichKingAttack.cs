using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LichKingAttack : MonoBehaviour
{
    public GameObject Target;
    bool isAttacking = false;
    int PasePettenOneDamage = 40;
    private Vector3 flipx;
    Rigidbody2D rb;
    public Transform sword;
    public int speed;
    bool page2 = false;
    
    void Awake()
    {
        flipx = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (transform.transform.position.x > Target.transform.position.x)
        {
            flipx.x = Mathf.Abs(flipx.x); 
            transform.localScale = flipx;
            rb.linearVelocity = Vector2.left * 0.7f * Math.Abs(transform.transform.position.x - Target.transform.position.x);
        }
        else
        {
            flipx.x = -Mathf.Abs(flipx.x);
            transform.localScale = flipx;
            rb.linearVelocity = Vector2.right * 0.7f * Math.Abs(transform.transform.position.x - Target.transform.position.x);
        }
        if (!isAttacking)
        {
            int pettenSycle = Random.Range(0, 4);
            switch (pettenSycle)
            {
                case 0:
                    StartCoroutine(Coroutine_SwingAttack());
                    break;
                case 1:

                    break;
                
            }

            if (page2)
            {
                switch (pettenSycle)
                {
                    case 2:

                        break;
                    case 3:

                        break;
                }
            }
        }
    }

    IEnumerator Coroutine_SwingAttack()
    {
        isAttacking = true;
        int lastRan = -1; // 처음엔 아무 패턴도 없다고 표시
        int ran = Random.Range(0, 3);
        Quaternion startRot = sword.localRotation;
        Quaternion targetRot = Quaternion.Euler(0, 0, 90f);
        if (page2)
        {
            transform.GetChild(0).GetComponent<AttackEffectScript>().Damage += 30;
            transform.GetChild(1).GetComponent<AttackEffectScript>().Damage += 30;
            transform.GetChild(2).GetComponent<AttackEffectScript>().Damage += 30;
        }

        for (int i = 0; i < 3; i++)
        {

            // 바로 여기서 이전 값과 다를 때까지 다시 뽑기
            while (ran == lastRan)
            {
                ran = Random.Range(0, 3);
            }
            lastRan = ran; // 다음 회차를 위해 저장

            switch (ran)
            {
                case 0:
                    sword.transform.GetChild(0).gameObject.SetActive(true);
                    sword.transform.GetChild(1).gameObject.SetActive(false);
                    sword.transform.GetChild(2).gameObject.SetActive(false);
                    
                    while (Quaternion.Angle(sword.localRotation, targetRot) > 0.1f)
                    {
                        sword.localRotation = Quaternion.RotateTowards(
                            sword.localRotation,
                            targetRot,
                            speed * Time.deltaTime
                        );
                        yield return null;
                    }

                    yield return new WaitForSeconds(1f);

                    while (Quaternion.Angle(sword.localRotation, startRot) > 0.1f)
                    {
                        sword.localRotation = Quaternion.RotateTowards(
                            sword.localRotation,
                            startRot,
                            speed * Time.deltaTime
                        );
                        yield return null;
                    }
                    break;

                case 1:
                    sword.transform.GetChild(0).gameObject.SetActive(false);
                    sword.transform.GetChild(1).gameObject.SetActive(true);
                    sword.transform.GetChild(2).gameObject.SetActive(false);
                    float rotated = 0f;

                    while (rotated < 360f)
                    {
                        float delta = speed * Time.deltaTime;
                        rotated += delta;

                        transform.Rotate(0f, delta, 0f); // Y축 기준 회전
                        yield return null;
                    }
                    sword.transform.GetChild(0).gameObject.SetActive(true);
                    sword.transform.GetChild(1).gameObject.SetActive(false);
                    sword.transform.GetChild(2).gameObject.SetActive(false);
                    break;

                case 2:
                    sword.transform.GetChild(0).gameObject.SetActive(false);
                    sword.transform.GetChild(1).gameObject.SetActive(false);
                    sword.transform.GetChild(2).gameObject.SetActive(true);
                    if (transform.transform.position.x > Target.transform.position.x)
                    {
                        rb.AddForce(Vector2.left * 500);
                    }
                    else
                    {
                        rb.AddForce(Vector2.right * 500);
                    }

                    while (Quaternion.Angle(sword.localRotation, targetRot) > 0.1f)
                    {
                        sword.localRotation = Quaternion.RotateTowards(
                            sword.localRotation,
                            targetRot,
                            speed * Time.deltaTime
                        );
                        yield return null;
                    }

                    yield return new WaitForSeconds(1f);

                    while (Quaternion.Angle(sword.localRotation, startRot) > 0.1f)
                    {
                        sword.localRotation = Quaternion.RotateTowards(
                            sword.localRotation,
                            startRot,
                            speed * Time.deltaTime
                        );
                        yield return null;
                    }
                    break;
            }

            yield return new WaitForSeconds(2f);
        }
        isAttacking = false;
    }

}
