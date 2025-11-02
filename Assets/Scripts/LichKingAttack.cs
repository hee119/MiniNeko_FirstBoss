using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LichKingAttack : MonoBehaviour
{
    public GameObject Target;
    bool isAttacking = false;
    int PasePettenOneDamage = 40;
    private Vector3 flipx;
    Rigidbody2D rb;
    public Transform Sword;
    public int speed;
    
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
            int pettenSycle = Random.Range(0, 3);
            switch (pettenSycle)
            {
                case 0:
                    StartCoroutine(Coroutine_SwingAttack());
                    break;
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
            }
        }
    }

    IEnumerator Coroutine_SwingAttack()
    {
        isAttacking = true;
        int lastRan = -1; // 처음엔 아무 패턴도 없다고 표시
        int ran = Random.Range(0, 3);
        Quaternion startRot = Sword.localRotation;
        Quaternion targetRot = Quaternion.Euler(0, 0, 90f);

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
                    Sword.transform.GetChild(0).gameObject.SetActive(true);
                    Sword.transform.GetChild(1).gameObject.SetActive(false);
                    Sword.transform.GetChild(2).gameObject.SetActive(false);
                    
                    while (Quaternion.Angle(Sword.localRotation, targetRot) > 0.1f)
                    {
                        Sword.localRotation = Quaternion.RotateTowards(
                            Sword.localRotation,
                            targetRot,
                            speed * Time.deltaTime
                        );
                        yield return null;
                    }

                    yield return new WaitForSeconds(1f);

                    while (Quaternion.Angle(Sword.localRotation, startRot) > 0.1f)
                    {
                        Sword.localRotation = Quaternion.RotateTowards(
                            Sword.localRotation,
                            startRot,
                            speed * Time.deltaTime
                        );
                        yield return null;
                    }
                    break;

                case 1:
                    Sword.transform.GetChild(0).gameObject.SetActive(false);
                    Sword.transform.GetChild(1).gameObject.SetActive(true);
                    Sword.transform.GetChild(2).gameObject.SetActive(false);
                    float rotated = 0f;

                    while (rotated < 360f)
                    {
                        float delta = speed * Time.deltaTime;
                        rotated += delta;

                        transform.Rotate(0f, delta, 0f); // Y축 기준 회전
                        yield return null;
                    }
                    break;

                case 2:
                    Debug.Log(2);
                    Sword.transform.GetChild(0).gameObject.SetActive(false);
                    Sword.transform.GetChild(1).gameObject.SetActive(false);
                    Sword.transform.GetChild(2).gameObject.SetActive(true);
                    if (transform.transform.position.x > Target.transform.position.x)
                    {
                        rb.AddForce(Vector2.left * 200f, ForceMode2D.Impulse);
                    }
                    else
                    {
                        rb.AddForce(Vector2.right * 200f, ForceMode2D.Impulse);
                    }

                    while (Quaternion.Angle(Sword.localRotation, targetRot) > 0.1f)
                    {
                        Sword.localRotation = Quaternion.RotateTowards(
                            Sword.localRotation,
                            targetRot,
                            speed * Time.deltaTime
                        );
                        yield return null;
                    }

                    yield return new WaitForSeconds(1f);

                    while (Quaternion.Angle(Sword.localRotation, startRot) > 0.1f)
                    {
                        Sword.localRotation = Quaternion.RotateTowards(
                            Sword.localRotation,
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
