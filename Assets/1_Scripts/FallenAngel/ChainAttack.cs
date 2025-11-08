using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChainAttack : MonoBehaviour
{
    private GameObject player;
    public int isFlip = 1;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Start()
    {
        Appear();
    }
    void Appear()
    {
        Vector3 targetPos = player.transform.position - new Vector3(13 * isFlip, 13, 0);

        DOTween.To(
                () => transform.position,         // getter
                x => transform.position = x,      // setter
                targetPos,                        // target
                0.5f                              // duration
        );
    }
    public void DisAppear()
    {
        Vector3 targetPos = player.transform.position - new Vector3(24 * isFlip, 24, 0);

        DOTween.To(
                () => transform.position,         // getter
                x => transform.position = x,      // setter
                targetPos,                        // target
                0.5f                              // duration
        );
    }
}
