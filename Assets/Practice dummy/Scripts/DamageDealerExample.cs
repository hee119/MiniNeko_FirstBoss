using UnityEngine;

public class DamageDealerExample : MonoBehaviour
{
    public int DamageMin, DamageMax;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable damagable))
            damagable.Damage(RandomDamage, gameObject.name, transform.position);
    }

    int RandomDamage => Random.Range(DamageMin, DamageMax);
}
