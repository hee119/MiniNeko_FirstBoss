using System.Collections;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private float t;
    public GameObject target;
    public Chain[] chain;
    private SpriteRenderer sr;
    private Collider2D cor;
    bool isCheck;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        cor = GetComponent<Collider2D>();
        sr.enabled = false;
        cor.enabled = false;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target.GetComponent<PlayerMove>().moveSpeed *= 0.5f;
            target.GetComponent<PlayerMove>().JumpPower = 0;
            target.GetComponent<PlayerMove>().DashForce = 0;
            t += Time.deltaTime;
            if (t >= 3)
            {
                target.transform.position = transform.position;
                target.GetComponent<PlayerMove>().moveSpeed = 7f;
                target.GetComponent<PlayerMove>().JumpPower = 5;
                target.GetComponent<PlayerMove>().DashForce = 5;
                StartCoroutine( ShrinkChain());
            }
        }
    }
    public IEnumerator LocalScale()
    {
        if (isCheck)
        {
            transform.position = new Vector2(target.transform.position.x, transform.position.y);
            sr.enabled = true;
            cor.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
    }
    private void OnTriggerExit2D(Collider2D a)
    {
        if (a.CompareTag("Player"))
        {
            target.GetComponent<PlayerMove>().moveSpeed = 7f;
            target.GetComponent<PlayerMove>().JumpPower = 5;
            target.GetComponent<PlayerMove>().DashForce = 5;
        }
        StartCoroutine( ShrinkChain());
    }

    
    IEnumerator ShrinkChain()
    {
        isCheck = false;
        cor.enabled = false;
        sr.enabled = false;
        t = 0;

        // 모든 체인의 isStay를 false
        foreach (Chain _chain in chain)
        {
            if (_chain != null)
                _chain.isStay = false;
                _chain.isTrigger = false;

        }

        bool anyChainLeft = true;

        // 최소 하나라도 줄일 체인이 있을 때 반복
        while (anyChainLeft)
        {
            anyChainLeft = false;

            foreach (Chain _chain in chain)
            {
                if (_chain == null) 
                    continue;

                if (_chain.transform.localScale.y > 0.01f)
                {
                    // y값을 조금씩 줄이기
                    float newY = Mathf.MoveTowards(_chain.transform.localScale.y, 0f, 2 * Time.deltaTime);
                    _chain.transform.localScale = new Vector3(_chain.transform.localScale.x, newY, _chain.transform.localScale.z);

                    anyChainLeft = true; // 아직 줄일 체인이 있음
                }
            }

            yield return null; // 다음 프레임까지 대기
        }

        // 모든 체인 완전히 0으로 맞춤
        foreach (Chain _chain in chain)
        {
            if (_chain != null)
                _chain.transform.localScale = new Vector3(_chain.transform.localScale.x, 0f, _chain.transform.localScale.z);
        }
    }

    public IEnumerator Check()
    {
        if(isCheck)
            yield break;
        
        foreach (Chain _chain in chain)
        {
            if (!_chain.isTrigger)
                continue;
            isCheck = true;
        }

        yield return LocalScale();
    }

}