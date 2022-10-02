using UnityEngine;
using UnityEngine.Events;

public class TriggerAction : MonoBehaviour {
	public string triggerTag;
	public UnityEvent OnTrigger = new UnityEvent();

	private void OnTriggerEnter(Collider other) {
		if (triggerTag.Length == 0 || other.gameObject.CompareTag(triggerTag)) {
			OnTrigger.Invoke();
		}
	}
}
