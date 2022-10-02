using UnityEngine;

/// <summary>
/// Ensures that all subscribed actions are performed simultaneously at a fixed interval
/// </summary>
public class GameInterval : MonoBehaviour {
	public float interval = 10;

	private System.Action PerformManyClock, PerformOnceClock;

	private int prevIntervalI;

	private int IntervalI => Mathf.FloorToInt(Time.timeSinceLevelLoad / interval);

	public float Elapsed => Time.timeSinceLevelLoad - IntervalI * interval;

	public float Left => interval - Elapsed;

	public float Progress => Mathf.Clamp01(Elapsed / interval);

	private void Update() {
		int intervalI = IntervalI;
		if (intervalI > prevIntervalI) {
			prevIntervalI = intervalI;
			PerformManyClock?.Invoke();
			PerformOnceClock?.Invoke();
			PerformOnceClock = null;
		}
	}

	private void OnDestroy() {
		PerformManyClock = null;
		PerformOnceClock = null;
	}

	public void PerformOnce(System.Action action) {
		PerformOnceClock += action;
	}

	public void PerformMany(System.Action action) {
		PerformManyClock += action;
	}

	public void StopPerforming(System.Action action) {
		PerformOnceClock -= action;
		PerformManyClock -= action;
	}
}
