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
                transform.localScale = new Vector3(3f, 0.1499997f, 1f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Chain _chain in chain)
            {
                _chain.isStay = true;
            }
            target.GetComponent<Rigidbody2D>().velocity *= 0.5f;
            t += Time.deltaTime;
            if (t >= 2)
            {
                t = 0;
                target.transform.position = transform.position;
                foreach (Chain _chain in chain)
                {
                    if (_chain.isStay)
                    {
                        _chain.isStay = false;
                        _chain.isTrigger = false;
                    }
                }
                transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D a)
    {
        if (a.CompareTag("Player"))
        {
            transform.localScale = Vector3.zero;
            foreach (Chain _chain in chain)
            {
                _chain.isStay = false;
                _chain.isTrigger = false;
            }
        }
    }
}
