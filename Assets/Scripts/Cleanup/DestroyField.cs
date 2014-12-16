using UnityEngine;
using System.Collections;

public class DestroyField : MonoBehaviour {

	public bool testForHero = false;

	void OnTriggerEnter2D(Collider2D collider) {

		//Check if this was the player that fell - if so, reload the level.
		//TODO: Clean this up - this component should only be responsible for signaling this event :)
		if (testForHero && collider.gameObject.CompareTag("Player")) {
			Application.LoadLevel(Application.loadedLevel);
		}

		if (collider.transform.gameObject.transform.parent) {
			Destroy(collider.transform.gameObject.transform.parent.gameObject);
			return;
		}

		Destroy(collider.transform.gameObject);
	}	
}
