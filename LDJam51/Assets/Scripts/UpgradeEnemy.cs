using UnityEngine;

public class UpgradeEnemy : MonoBehaviour {
	[PrefabOnly]
	public EnemyPoolConfig[] upgrades;

	private WaveSpawner spawner;

	private void Start() {
		// TODO: Change if there will be more than one active spawner
		spawner = FindObjectOfType<WaveSpawner>();
		Debug.Assert(upgrades.Length > 0);

		FindObjectOfType<GlobalIntervalTimer>().PerformOnce(Upgrade);
	}

	private void Upgrade() {
		foreach (EnemyPoolConfig upgrade in upgrades) {
			spawner.ManualSpawn(upgrade, transform.position, transform.rotation);
		}
        gameObject.SetActive(false);
	}
}
