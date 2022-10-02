using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraAnimation : MonoBehaviour {
	public float rotationSpeed;

	private void Update() {
		transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + rotationSpeed * Time.deltaTime, transform.eulerAngles.z);
	}
}
