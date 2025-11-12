using System.Collections;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private float t;
    public GameObject target;
    public Chain[] chain;

    void Start()
    {
        transform.position = new Vector2(target.transform.position.x, transform.position.y);
    }

    void Update()
    {
        foreach (Chain _chain in chain)
        {
            if (_chain.isTrigger)
            {
                transform.localScale = new Vector3(20, 1, 1f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Chain _chain in chain)
            {
                if (_chain.isTrigger)
                _chain.isStay = true;
            }
            target.GetComponent<Rigidbody2D>().velocity *= 0.5f;
            t += Time.deltaTime;
            if (t >= 4)
            {
                transform.localScale = new Vector3(0, 0, 0);
                t = 0;
                target.transform.position = transform.position;
                StartCoroutine( ShrinkChain());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D a)
    {
        if (a.CompareTag("Player"))
        {
            transform.localScale = Vector3.zero;
            StartCoroutine( ShrinkChain());
        }
    }

    IEnumerator ShrinkChain()
    {

        float speed = 1f; // 스케일이 줄어드는 속도
        foreach (Chain _chain in chain)
        {
            while (_chain.transform.localScale.y > 0.01f) // 0에 가까워지면 멈춤
            {
                // 현재 y값을 조금씩 줄이기
                float newY = Mathf.MoveTowards(_chain.transform.localScale.y, 0f, speed * Time.deltaTime);
                _chain.transform.localScale =
                    new Vector3(_chain.transform.localScale.x, newY, _chain.transform.localScale.z);

                yield return null; // 다음 프레임까지 대기
            }

            // 완전히 0으로 맞추기
            _chain.transform.localScale = new Vector3(_chain.transform.localScale.x, 0f, _chain.transform.localScale.z);
        }
    }
}
