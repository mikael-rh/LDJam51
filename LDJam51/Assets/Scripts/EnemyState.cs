using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    private int health;

    [SerializeField]
    private ParticleSystem particles;

    [SerializeField]
    private OnEnemyDeath onDeath;

    public void Start()
    {
        health = maxHealth;
    }

    public void ApplyDamage(int damage, ContactPoint contactPoint)
    {
        if (health <= 0)
        {
            onDeath.Trigger();
            health = maxHealth;
        }
        health -= damage;

        particles.transform.position = contactPoint.point;
        particles.transform.rotation *= Quaternion.FromToRotation(particles.transform.forward, contactPoint.normal);
        particles.Play();

    }
}
