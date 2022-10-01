using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    private int health;


    public void ApplyDamage(int damage)
    {
        health -= damage;
    }
}
