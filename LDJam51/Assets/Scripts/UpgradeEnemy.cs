using UnityEngine;

public class UpgradeEnemy : MonoBehaviour {
	public GameObject[] upgrades = new GameObject[0];
	public float delay = 10;

	private Timer upgradeTimer;

	private void Start() {
		upgradeTimer = new(delay);

		if (upgrades.Length == 0) Debug.LogError($"No upgrades provided. {name} will be destroyed in {delay} seconds!");
	}

	private void Update() {
		if (upgradeTimer.PerformOnce()) {
			PerformUpgrade();
		}
	}

	private void PerformUpgrade() {
		Debug.Log("Upgrading " + name);

		foreach (GameObject upgrade in upgrades) {
			Instantiate(upgrade, transform.position, transform.rotation);
		}
		Destroy(gameObject);
	}
}
