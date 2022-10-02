using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour {
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

	public void SpawnEnemiesAtRandom(GameObject enemyPrefab, int count) {
		Vector3 min = levelBounds.min;
		Vector3 max = levelBounds.max;
		int spawned = 0;
		while (spawned < count) {
			Vector3 p = new Vector3(
				Random.Range(min.x, max.x),
				Random.Range(min.y, max.y),
				Random.Range(min.z, max.z)
			);

			if (Vector3.Distance(p, enemyTarget.position) >= minDistanceFromTarget
				&& NavMesh.SamplePosition(p, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)) {
				// TODO: Enemy Pooling?
				NavMeshAgent agent = enemyPrefab.GetComponent<NavMeshAgent>();
				SpawnEnemyAt(enemyPrefab, hit.position + Vector3.up * agent.baseOffset);
				spawned++;
			}
		}

		Debug.Log($"Spawned {count} {enemyPrefab.name}");
	}

	public void SpawnEnemyAt(GameObject enemyPrefab, Vector3 position, Quaternion? rotation = null) {
		GameObject instance = Instantiate(enemyPrefab, position, rotation ?? Quaternion.identity);
		instance.GetComponent<Enemy>().target = enemyTarget;
	}

	private void OnDrawGizmosSelected() {
		Gizmos.DrawWireCube(levelBounds.center, levelBounds.size);
	}
}