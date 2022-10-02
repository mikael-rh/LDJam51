using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour {
	private int MAX_ATTEMPTS = 100;

	[SerializeField]
	private float minDistanceFromTarget;

	[SerializeField]
	Bounds levelBounds; // level bounds in worldspace 

	public bool SpawnEnemyAtRandom(GameObject enemyInstance, Transform target) {
		Vector3 min = levelBounds.min;
		Vector3 max = levelBounds.max;

		for (int i = 0; i < MAX_ATTEMPTS; i++) {
			Vector3 p = new Vector3(
				Random.Range(min.x, max.x),
				Random.Range(min.y, max.y),
				Random.Range(min.z, max.z)
			);

			if (NavMesh.SamplePosition(p, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)
				&& Vector3.Distance(p, target.position) >= minDistanceFromTarget) {
                var enemy = enemyInstance.GetComponent<Enemy>();
                enemy.target = target;
                enemyInstance.SetActive(true);
                NavMeshAgent agent = enemyInstance.GetComponent<NavMeshAgent>();
                enemyInstance.transform.position = hit.position + Vector3.up * agent.baseOffset;
				return true;
			}
		}

		return false;
	}

	private void OnDrawGizmosSelected() {
		Gizmos.DrawWireCube(levelBounds.center, levelBounds.size);
	}
}