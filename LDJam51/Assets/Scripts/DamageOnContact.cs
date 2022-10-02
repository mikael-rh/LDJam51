using UnityEngine;

public class DamageOnContact : MonoBehaviour {
	public int damage;
	public float cooldown;
	public bool includeTriggers;

	private readonly Timer cooldownTimer = new();

	private void TryDamage(GameObject other) {
		PlayerState playerState = other.GetComponent<PlayerState>();
		if (cooldownTimer.Done && playerState != null) {
			cooldownTimer.Start(cooldown);
			playerState.ApplyDamage(damage);
		}
	}

	private void OnCollisionEnter(Collision collision) {
		TryDamage(collision.gameObject);
	}

	private void OnCollisionStay(Collision collision) {
		TryDamage(collision.gameObject);
	}

	private void OnTriggerEnter(Collider other) {
		if (includeTriggers) TryDamage(other.gameObject);
	}

	private void OnTriggerStay(Collider other) {
		if (includeTriggers) TryDamage(other.gameObject);
	}
}
