using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private ProjectileStats stats;

    [SerializeField]
    private Rigidbody rb;

    public void Launch(Vector3 position, Vector3 direction, float speed)
    {
        rb.velocity = new Vector3();
        transform.position = position;

        rb.AddForce(direction * speed, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyState enemyState;
        if (collision.gameObject.TryGetComponent(out enemyState))
        {
            enemyState.ApplyDamage(stats.damage);
        }
    }
}
