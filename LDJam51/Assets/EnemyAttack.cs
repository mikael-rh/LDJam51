using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour {
	public UnityEvent OnAttack = new UnityEvent();

	public Enemy enemy;
	public Animator animator;
	public string stateName = "attack";
	public float duration, wakeup, cooldown;

	public bool attackOnTrigger;

	private Timer durationTimer = new();
	private Timer wakeupTimer = new();
	private Timer cooldownTimer = new();

	private void Start() {
		Debug.Assert(enemy != null);
		Debug.Assert(animator != null);
	}

	public void Update() {
		if (durationTimer.PerformOnce()) {
			animator.SetBool(stateName, false);  // End attack
			wakeupTimer.Start(wakeup);
		}

		if (wakeupTimer.PerformOnce()) {
			enemy.FollowTarget = true;
		}
	}

	public bool Attack() {
		if (!(enemy.FollowTarget && cooldownTimer.Done && wakeupTimer.Done && durationTimer.Done)) return false;

		animator.SetBool(stateName, true);  // Start attack
		enemy.FollowTarget = false;
		durationTimer.Start(duration);
		cooldownTimer.Start(cooldown);

		OnAttack.Invoke();
		return true;
	}

	private void OnTriggerStay(Collider other) {
		if (attackOnTrigger && other.gameObject.CompareTag("Player")) {
			Debug.Log($"{enemy.name} is performing {gameObject.name}");
			Attack();
		}
	}
}
