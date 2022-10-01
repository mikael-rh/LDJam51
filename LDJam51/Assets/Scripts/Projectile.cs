using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private ProjectileStats stats;

    private void OnCollisionEnter(Collision collision)
    {
        EnemyState enemyState;
        if (collision.gameObject.TryGetComponent(out enemyState))
        {
            enemyState.ApplyDamage(stats.damage);
        }
    }
}
