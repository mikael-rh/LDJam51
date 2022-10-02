using UnityEngine;

public class WaveSpawner : MonoBehaviour {
	[PrefabOnly]
	public GameObject sampleEnemy; // TODO: Allow for different enemies

	public int spawnAmount = 1;
	public EnemySpawner spawner;
	public bool spawnOnStart;

	private void Start() {
		FindObjectOfType<GlobalIntervalTimer>().PerformMany(Spawn);
		if (spawnOnStart) Spawn();
	}

	private void Spawn() {
		spawner.SpawnEnemiesAtRandom(sampleEnemy, spawnAmount);
	}
}
