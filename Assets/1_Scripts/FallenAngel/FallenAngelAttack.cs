using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UIElements;

public class FallenAngelAttack : MonoBehaviour
{
    private GameObject player;
    [SerializeField]

    public GameObject chain;
    public GameObject chainSkillRange;

    public GameObject sword;
    public GameObject swordGate;

    public GameObject light;
    public GameObject lightSkillRange;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        StartCoroutine(FirstPattern());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChainAttack();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(SwordAttack());
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(LightAttack());
        }
    }

    IEnumerator ChainAttack()
    {
        Vector3 coo = player.transform.position;
        var chainAttackRange = Instantiate(chainSkillRange, coo, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(chainAttackRange );
        yield return new WaitForSeconds(0.5f);
        var chainLeft = Instantiate(chain, (coo - new Vector3(24, 24, 0)), Quaternion.identity);
        var chainRight = Instantiate(chain, (coo + new Vector3(24, -24, 0)), Quaternion.identity);
        chainRight.GetComponent<Transform>().Rotate(0, 180, 0);
        chainLeft.GetComponent<ChainAttack>().isFlip = 1;
        chainRight.GetComponent<ChainAttack>().isFlip = -1;
        chainLeft.GetComponent <ChainAttack>().coo = coo;
        chainRight.GetComponent <ChainAttack>().coo = coo;
        yield return new WaitForSeconds(5f);
        Destroy(chainLeft );
        Destroy(chainRight );
    }

    IEnumerator SwordAttack()
    {
        var firstGate = Instantiate(swordGate, new Vector3(this.transform.position.x - 7, player.transform.position.y, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        var secondGate = Instantiate(swordGate, new Vector3(this.transform.position.x - 7, player.transform.position.y + 3, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        var thirdGate = Instantiate(swordGate, new Vector3(this.transform.position.x - 7, player.transform.position.y + 6, 0), Quaternion.identity);
    }
    IEnumerator LightAttack()
    {
        Vector3 coo = new Vector3(player.transform.position.x, 10, 0);
        var lightAttackRange = Instantiate(this.lightSkillRange,coo, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(lightAttackRange);
        coo = new Vector3(coo.x,coo.y, -0.1f);
        var lightAttack = Instantiate(this.light, coo, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(lightAttack);
    }
    IEnumerator FirstPattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            int value = Random.Range(0, 6);
            switch (value)
            {
                case 0:
                    StartCoroutine(SwordAttack());
                    yield return new WaitForSeconds(2f);
                    break;
                case 1:
                    StartCoroutine(ChainAttack());
                    StartCoroutine(SwordAttack());
                    yield return new WaitForSeconds(2f);
                    break;
                case 2:
                    StartCoroutine (SwordAttack());
                    yield return new WaitForSeconds (2f);
                    StartCoroutine(SwordAttack());
                    yield return new WaitForSeconds(2f);
                    break;
                case 3:
                    StartCoroutine(ChainAttack());
                    yield return new WaitForSeconds(2f);
                    StartCoroutine(LightAttack());
                    break;
                case 4:
                    StartCoroutine(LightAttack());
                    yield return new WaitForSeconds(1f);
                    StartCoroutine (LightAttack());
                    yield return new WaitForSeconds(1f);
                    StartCoroutine(LightAttack());
                    yield return new WaitForSeconds(2f);
                    break;
                case 5:
                    StartCoroutine(SwordAttack());
                    yield return new WaitForSeconds(2f);
                    StartCoroutine(ChainAttack());
                    StartCoroutine (LightAttack());
                    break;
                default:
                    Debug.LogWarning("Error?!?!?");
                    break;
            }

        }
    }
    //IEnumerator SecondPattern()
    //{

    //}
}
