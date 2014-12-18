using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {

		//TODO: Clean this up - this component should only be responsible for signaling this event :)
		if (collider.gameObject.CompareTag("Player")) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
