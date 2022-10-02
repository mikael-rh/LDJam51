using UnityEngine;

public class UpgradeEnemy : MonoBehaviour {
	public GameObject[] upgrades = new GameObject[0];
	public float delay = 10;

	private EnemySpawner spawner;

	private void Start() {
		// TODO: Change if there will be more than one active spawner
		spawner = FindObjectOfType<EnemySpawner>();
		if (upgrades.Length == 0) Debug.LogError($"No upgrades provided. {name} will be destroyed in {delay} seconds!");

		FindObjectOfType<GlobalIntervalTimer>().PerformOnce(Upgrade);
	}

	private void Upgrade() {
		foreach (GameObject upgrade in upgrades) {
			spawner.SpawnEnemyAt(upgrade, position: transform.position, transform.rotation);
		}
		Destroy(gameObject);
	}
}
