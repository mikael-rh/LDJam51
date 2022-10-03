using UnityEngine;
using UnityEngine.Events;

public class KeyPressTrigger : MonoBehaviour {
	public KeyCode keyCode;
	public UnityEvent OnKeyUp = new UnityEvent();
	public bool triggerOnce;

	private void Update() {
		if (Input.GetKeyDown(keyCode)) {
			if (triggerOnce) enabled = false;
			OnKeyUp.Invoke();
		}
	}
}
