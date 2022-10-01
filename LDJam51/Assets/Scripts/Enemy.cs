using UnityEngine;
using UnityEngine.AI;

// [RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {
	public float spawnForce;
	public float wakeupTime = 1;

	private Rigidbody rb;
	private NavMeshAgent agent;

	private Timer wakeupTimer;

	private void Start() {
		rb = GetComponent<Rigidbody>();
		agent = GetComponent<NavMeshAgent>();

		if (spawnForce > 0) {
			//rb.AddForce(transform.forward * spawnForce, ForceMode.VelocityChange);
		}

		wakeupTimer = new(wakeupTime);
	}

	private void Update() {
		if (wakeupTimer.PerformOnce()) {

			// Placeholder destination
			agent.SetDestination(transform.position + new Vector3(Random.value, 0, Random.value) * 10);
		}
		rb.velocity = agent.velocity;
	}

	private void OnDrawGizmos() {
		if (agent != null) {
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(transform.position, agent.destination);
		}
	}
}
