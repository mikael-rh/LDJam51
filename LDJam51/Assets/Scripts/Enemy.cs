using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {
	public float spawnForce;
	public float wakeupTime = 1;
	public Transform target;

	private Rigidbody rb;
	private NavMeshAgent agent;

	private Timer wakeupTimer;
	private bool IsAwake => wakeupTimer != null && wakeupTimer.Done;

	private void Start() {
		rb = GetComponent<Rigidbody>();
		agent = GetComponent<NavMeshAgent>();

		if (spawnForce > 0) {
			rb.AddForce(transform.forward * spawnForce, ForceMode.VelocityChange);
		}

		wakeupTimer = new (wakeupTime);
	}

	private void Update() {
		if (IsAwake && target != null) {
			agent.SetDestination(target.position);
			rb.velocity = agent.velocity;
		}
	}

	private void OnDrawGizmosSelected() {
		if (IsAwake && agent != null && target != null) {
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(transform.position, agent.destination);
		}
	}
}
