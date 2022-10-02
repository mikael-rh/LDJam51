using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {
	public float spawnForce;
	public float wakeupTime = 1;
	public Transform target;

	private Rigidbody rb;
	private NavMeshAgent agent;

	public bool FollowTarget {
		get => agent != null && agent.isActiveAndEnabled;
		set {
			if (agent != null) {
				agent.enabled = value;
				if (!value) rb.velocity = Vector3.zero;
			}
		}
	}

	private void Start() {
		rb = GetComponent<Rigidbody>();
		agent = GetComponent<NavMeshAgent>();

		if (spawnForce > 0) {
			rb.AddForce(transform.forward * spawnForce, ForceMode.VelocityChange);
		}

		Timer.Schedule(() => FollowTarget = true, Mathf.RoundToInt(wakeupTime * 1000));
	}

    //private void OnEnable()
    //{
    //    if (FollowTarget && target != null)
    //    {
	   //     agent.SetDestination(target.position);
    //        rb.velocity = agent.velocity;
    //    }
    //}

	private void Update() {
		if (FollowTarget && target != null) {
			agent.SetDestination(target.position);
			rb.velocity = agent.velocity;
		}
	}

    private void OnDrawGizmosSelected() {
		if (FollowTarget && agent != null && target != null) {
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(transform.position, agent.destination);
		}
	}
}
