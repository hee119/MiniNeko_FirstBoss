using System.Collections;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private float t;
    public GameObject target;
    public Chain[] chain;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Chain _chain in chain)
            {
                if (_chain.isTrigger)
                _chain.isStay = true;
            }
            target.GetComponent<PlayerMove>().moveSpeed *= 0.5f;
            target.GetComponent<PlayerMove>().JumpPower = 0;
            target.GetComponent<PlayerMove>().DashForce = 0;
            t += Time.deltaTime;
            if (t >= 4)
            {
                transform.localScale = new Vector3(0, 0, 0);
                t = 0;
                target.transform.position = transform.position;
                StartCoroutine( ShrinkChain());
                target.GetComponent<PlayerMove>().moveSpeed = 7f;
                target.GetComponent<PlayerMove>().JumpPower = 5;
                target.GetComponent<PlayerMove>().DashForce = 5;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D a)
    {
        if (a.CompareTag("Player"))
        {
            transform.localScale = Vector3.zero;
            StartCoroutine( ShrinkChain());
            target.GetComponent<PlayerMove>().moveSpeed = 7f;
            target.GetComponent<PlayerMove>().JumpPower = 5;
            target.GetComponent<PlayerMove>().DashForce = 5;
        }
    }

    public IEnumerator LocalScale()
    {
        transform.position = new Vector2(target.transform.position.x, transform.position.y);
        transform.localScale = new Vector3(20, 1, 1);
        yield return null;
    }
    IEnumerator ShrinkChain()
    {

        foreach (Chain _chain in chain)
        {
            while (_chain.transform.localScale.y > 0.01f) // 0에 가까워지면 멈춤
            {
                // 현재 y값을 조금씩 줄이기
                float newY = Mathf.MoveTowards(_chain.transform.localScale.y, 0f,  2 * Time.deltaTime);
                _chain.transform.localScale =
                    new Vector3(_chain.transform.localScale.x, newY, _chain.transform.localScale.z);

                yield return null; // 다음 프레임까지 대기
            }

            // 완전히 0으로 맞추기
            _chain.transform.localScale = new Vector3(_chain.transform.localScale.x, 0f, _chain.transform.localScale.z);
        }
    }
}
