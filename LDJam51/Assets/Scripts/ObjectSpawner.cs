using UnityEngine;

public class ObjectSpawner : MonoBehaviour {
	[SerializeField]
	Bounds levelBounds; // level bounds in worldspace 

	[PrefabOnly]
	public GameObject[] prefabs;
	public Quaternion minSpawnRotation, maxSpawnRotation;
	public int spawnAmount;

	private void Start() {
		for (int i = 0; i < spawnAmount; i++) {
			SpawnAtRandom(prefabs[Random.Range(0, prefabs.Length)]);
		}
	}

	public void SpawnAtRandom(GameObject prefab) {
		Vector3 min = levelBounds.min;
		Vector3 max = levelBounds.max;

		Vector3 p = new Vector3(
				Random.Range(min.x, max.x),
				Random.Range(min.y, max.y),
				Random.Range(min.z, max.z)
			);

		Quaternion rotation = Quaternion.Euler(
			Random.Range(minSpawnRotation.eulerAngles.x, maxSpawnRotation.eulerAngles.x),
			Random.Range(minSpawnRotation.eulerAngles.y, maxSpawnRotation.eulerAngles.y),
			Random.Range(minSpawnRotation.eulerAngles.z, maxSpawnRotation.eulerAngles.z)
		);

		Instantiate(prefab, p, rotation);
	}

	private void OnDrawGizmosSelected() {
		Gizmos.DrawWireCube(levelBounds.center, levelBounds.size);
	}
}