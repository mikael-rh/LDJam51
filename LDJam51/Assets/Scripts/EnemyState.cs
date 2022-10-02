using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    public int health;

    public void Start()
    {
        health = maxHealth;
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
    }
}
