using UnityEngine;

public class WaveSpawner : MonoBehaviour {
	[PrefabOnly]
	public GameObject[] prefabs;

	public int spawnAmount = 1;
	public EnemySpawner spawner;
	public bool spawnOnStart;

	private void Start() {
		FindObjectOfType<GlobalIntervalTimer>().PerformMany(Spawn);
		if (spawnOnStart) Spawn();
	}

	private void Spawn() {
		for (int i = 0; i < spawnAmount; i++) {
			GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
			bool success = spawner.SpawnEnemyAtRandom(prefab);
			if (!success) throw new System.InvalidOperationException($"Failed to spawn {prefab.name}");
		}
	}
}
