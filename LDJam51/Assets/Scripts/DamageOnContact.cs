using UnityEngine;

public class DamageOnContact : MonoBehaviour {
	public string targetTag = "Player";
	public int damage;
	public float cooldown;

	private readonly Timer cooldownTimer = new();

	private void TryDamage(GameObject other) {
		PlayerState playerState = other.GetComponent<PlayerState>();
		if (cooldownTimer.Done && other.CompareTag(targetTag) && playerState != null) {
			cooldownTimer.Reset(cooldown);
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
		TryDamage(other.gameObject);
	}

	private void OnTriggerStay(Collider other) {
		TryDamage(other.gameObject);
	}
}
