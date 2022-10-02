using UnityEngine;

public class UpgradeEnemy : MonoBehaviour {
	[PrefabOnly]
	public GameObject[] upgrades = new GameObject[0];

	private EnemySpawner spawner;

	private void Start() {
		// TODO: Change if there will be more than one active spawner
		spawner = FindObjectOfType<EnemySpawner>();
		Debug.Assert(upgrades.Length > 0);

		FindObjectOfType<GlobalIntervalTimer>().PerformOnce(Upgrade);
	}

	private void Upgrade() {
		foreach (GameObject upgrade in upgrades) {
			spawner.SpawnEnemyAt(upgrade, position: transform.position, transform.rotation);
		}
		Destroy(gameObject);
	}
}
