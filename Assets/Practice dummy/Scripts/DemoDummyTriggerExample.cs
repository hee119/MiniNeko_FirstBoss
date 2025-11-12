using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoDummyTriggerExample : MonoBehaviour, IDamagable
{
    [SerializeField] PracticeDummy dummy;

    public void Damage(int damage, string name, Vector3 position) => dummy.Damage(damage, position, " - " + name);
}

internal interface IDamagable
{
    void Damage(int damage, string name, Vector3 position);
}