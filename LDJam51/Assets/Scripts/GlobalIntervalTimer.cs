using UnityEngine;

public class GlobalIntervalTimer : MonoBehaviour {
	public float interval = 10;

	private static System.Action GlobalClock;

	private int prevIntervalI;

	private int IntervalI => Mathf.FloorToInt(Time.timeSinceLevelLoad / interval);

	public float Elapsed => Time.timeSinceLevelLoad - IntervalI * interval;

	public float Left => interval - Elapsed;

	public float Progress => Mathf.Clamp01(Elapsed / interval);

	private void Update() {
		int intervalI = IntervalI;
		if (intervalI > prevIntervalI) {
			prevIntervalI = intervalI;
			GlobalClock?.Invoke();
		}
	}

	public void PerformOnce(System.Action action) {
		void SingleCall() {
			StopPerforming(SingleCall);
			action();
		}

		GlobalClock += SingleCall;
	}

	public void PerformMany(System.Action action) {
		GlobalClock += action;
	}

	public void StopPerforming(System.Action action) {
		GlobalClock -= action;
	}
}
