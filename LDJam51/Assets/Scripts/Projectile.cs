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

    [SerializeField]
    private Stick sticker;

    [SerializeField]
    private float damageThreshold;

    public void Launch(Vector3 position, Quaternion initialOrientation, Vector3 direction, float speed)
    {
        if (sticker != null) {
            sticker.Unstuck();
        }

        rb.velocity = new Vector3();
        transform.position = position;
        transform.rotation = initialOrientation;

        rb.AddForce(direction * speed, ForceMode.VelocityChange);
        rb.angularVelocity = initialOrientation * new Vector3(-30, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.sqrMagnitude < damageThreshold) return;

        EnemyState enemyState;
        if (collision.gameObject.TryGetComponent(out enemyState))
        {
            enemyState.ApplyDamage(stats.damage);
        }
    }
}
