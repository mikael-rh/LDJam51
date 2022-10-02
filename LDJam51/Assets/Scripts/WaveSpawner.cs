using UnityEngine;

public class WaveSpawner : MonoBehaviour {
	[PrefabOnly]
	public GameObject sampleEnemy; // TODO: Allow for different enemies

	private Timer spawnTimer;
	public float spawnInterval = 10;
	public int spawnAmount = 1;
	public EnemySpawner spawner;

	private void Start() {
		spawnTimer = new Timer(spawnInterval);
		spawnTimer.ForceDone();  // Start spawning immediately
	}

	private void Update() {
		if (spawnTimer.PerformMany()) {
			spawner.SpawnEnemiesAtRandom(sampleEnemy, spawnAmount);
		}
	}
}
