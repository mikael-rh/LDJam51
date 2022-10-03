using UnityEngine;

public class ConfigureCursor : MonoBehaviour {
	public CursorLockMode initLockMode = CursorLockMode.None;

	private void Start() {
		Cursor.lockState = initLockMode;
	}

	public void FreeCursor() {
		Cursor.lockState = CursorLockMode.None;
	}
}
