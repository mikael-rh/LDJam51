using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnPlayerDeath : MonoBehaviour {
	public UnityEvent OnTrigger = new UnityEvent();

	[SerializeField]
	private Behaviour[] disabledBehaviours;
	[SerializeField]
	private GameObject[] disabledObjects;

	[SerializeField]
	private GameObject deathHud;

	private bool hasTriggered = false;

	public void Trigger() {
		if (hasTriggered) return;

		foreach (Behaviour behaviour in disabledBehaviours) {
			behaviour.enabled = false;
		}

		foreach (GameObject obj in disabledObjects) {
			obj.SetActive(false);
		}

		deathHud.SetActive(true);

		hasTriggered = true;
		OnTrigger.Invoke();
	}
}
