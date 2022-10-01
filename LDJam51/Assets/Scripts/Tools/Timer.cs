using UnityEngine;


public class Timer {
	private float startTime, targetTime;

	public float Duration { get; set; }

	public virtual float CurrentTime => Time.timeSinceLevelLoad;

	public Timer() { }

	public Timer(float duration) {
		Reset(duration);
	}

	/// <summary>
	/// Enables and resets the timer progress
	/// </summary>
	/// <param name="duration">New duration (if null, duration remains unchanged)</param>
	public void Reset(float? duration = null) {
		if (duration != null) Duration = duration.Value;

		startTime = CurrentTime;
		targetTime = startTime + Duration;
		Enabled = true;
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
		if (!Done) return false;
		Reset();
		return true;
	}

	public bool Done => CurrentTime >= targetTime;
	public float Left => Mathf.Max(0, targetTime - CurrentTime);
	public float Elapsed => Mathf.Max(0, CurrentTime - startTime);

	public bool Enabled { get; set; } = true;
}
