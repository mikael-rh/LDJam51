using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour {
	private int MAX_ATTEMPTS = 100;

	public Transform enemyTarget;

	[SerializeField]
	private float minDistanceFromTarget;

	[SerializeField]
	Bounds levelBounds; // level bounds in worldspace

	//  TODO:  Use triangulation to spawn on mesh?
	// Requires balancing based on triangle area etc. Relatively cheap to compute, 
	// but requires a lot of boilerplate

	//private void Awake()
	//{
	//    //NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
	//}

	public bool SpawnEnemyAtRandom(GameObject enemyPrefab) {
		Vector3 min = levelBounds.min;
		Vector3 max = levelBounds.max;

		for (int i = 0; i < MAX_ATTEMPTS; i++) {
			Vector3 p = new Vector3(
				Random.Range(min.x, max.x),
				Random.Range(min.y, max.y),
				Random.Range(min.z, max.z)
			);

			if (NavMesh.SamplePosition(p, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)
				&& Vector3.Distance(p, enemyTarget.position) >= minDistanceFromTarget) {
				// TODO: Enemy Pooling?
				NavMeshAgent agent = enemyPrefab.GetComponent<NavMeshAgent>();
				SpawnEnemyAt(enemyPrefab, hit.position + Vector3.up * agent.baseOffset);

				return true;
			}
		}

		return false;
	}

	public void SpawnEnemyAt(GameObject enemyPrefab, Vector3 position, Quaternion? rotation = null) {
		GameObject instance = Instantiate(enemyPrefab, position, rotation ?? Quaternion.identity);
		instance.GetComponent<Enemy>().target = enemyTarget;
	}

	private void OnDrawGizmosSelected() {
		Gizmos.DrawWireCube(levelBounds.center, levelBounds.size);
	}
}