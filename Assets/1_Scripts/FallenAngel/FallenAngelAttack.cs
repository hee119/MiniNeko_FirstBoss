using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenAngelAttack : MonoBehaviour
{
    private GameObject player;
    [SerializeField]

    public GameObject chain;
    public GameObject ChainSkillRange;

    public GameObject sword;
    public GameObject swordGate;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        ChainAttack();
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
    }

    void ChainAttack()
    {
        var chainLeft = Instantiate(chain, (player.transform.position - new Vector3(24, 24, 0)), Quaternion.identity);
        var chainRight = Instantiate(chain, (player.transform.position + new Vector3(24, -24, 0)), Quaternion.identity);
        chainRight.GetComponent<Transform>().Rotate(0, 180, 0);
        chainLeft.GetComponent<ChainAttack>().isFlip = 1;
        chainRight.GetComponent<ChainAttack>().isFlip = -1;
    }

    IEnumerator SwordAttack()
    {
        var firstGate = Instantiate(swordGate, new Vector3(this.transform.position.x - 7, player.transform.position.y, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        var secondGate = Instantiate(swordGate, new Vector3(this.transform.position.x - 7, player.transform.position.y + 3, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        var thirdGate = Instantiate(swordGate, new Vector3(this.transform.position.x - 7, player.transform.position.y + 6, 0), Quaternion.identity);
    }
}
