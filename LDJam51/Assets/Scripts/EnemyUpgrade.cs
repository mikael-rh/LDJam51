using UnityEngine;
using System.Timers;

public class EnemyUpgrade : MonoBehaviour {
	Timer timer;
	public GameObject[] upgrades = new GameObject[0];

	private void Start() {
		timer = new Timer(10);
		timer.Elapsed += Timer_Elapsed;
		timer.Start();
	}

	private void Timer_Elapsed(object sender, ElapsedEventArgs e) {
		foreach (GameObject upgrade in upgrades) {
			Instantiate(upgrade, transform.position, transform.rotation);
		}
		Destroy(gameObject);
	}
}
