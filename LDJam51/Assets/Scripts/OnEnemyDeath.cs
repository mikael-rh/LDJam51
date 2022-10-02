using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OnEnemyDeath : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    private RigidbodyConstraints rbConstraints;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private float deathTime;
    private float timeDead;

    private bool isTriggered = false;

    private void Start()
    {
        rbConstraints = rb.constraints;
    }

    public void Trigger()
    {
        if (isTriggered) return;

        isTriggered = true;
        rb.constraints = RigidbodyConstraints.None;
        agent.enabled = false;
        enemy.enabled = false;

        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        while(true)
        {
            if (timeDead >= deathTime)
            {
                break;
            }
            timeDead += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        rb.constraints = rbConstraints;
        agent.enabled = true;
        isTriggered = false;
        gameObject.SetActive(false);
    }

}
