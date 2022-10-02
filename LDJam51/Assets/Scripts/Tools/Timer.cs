using UnityEngine;
using System.Threading.Tasks;

public class Timer {
	public Timer() { }

	public Timer(float duration) {
		Start(duration);
	}

	public bool Enabled { get; set; }

	public float StartTime { get; private set; }

	public float Duration { get; private set; }

	public float EndTime => StartTime + Duration;

	public static float CurrentTime => Time.timeSinceLevelLoad;

	public bool Done => CurrentTime >= EndTime;

	public float Left => Mathf.Max(0, EndTime - CurrentTime);

	public float Elapsed => Mathf.Max(0, CurrentTime - StartTime);

	/// <summary>
	/// Enables and resets the timer progress
	/// </summary>
	/// <param name="duration">New duration (if null, duration remains unchanged)</param>
	public void Start(float? duration = null) {
		if (duration != null) Duration = duration.Value;

		StartTime = CurrentTime;
		Enabled = true;
	}

	public void ForceDone() {
		if (!Done) StartTime -= Left;
	}

	/// <summary>
	/// Waits until the timer is done, then disables it
	/// </summary>
	/// <returns>True if the timer is done and enabled</returns>
	public bool PerformOnce() {
		if (!Enabled || !Done) return false;
		Enabled = false;
		return true;
	}

	/// <summary>
	/// Waits until the timer is done, then resets it
	/// </summary>
	/// <returns>True if the timer is done and enabled</returns>
	public bool PerformMany() {
		if (!Enabled || !Done) return false;
		Start();
		return true;
	}

	public static async void Schedule(System.Action action, int millisecondsDelay, System.Threading.CancellationToken? cancellationToken = null) {
		if (cancellationToken == null) {
			await Task.Delay(millisecondsDelay);
		}
		else {
			await Task.Delay(millisecondsDelay, cancellationToken.Value);
		}

		action();
	}
}
