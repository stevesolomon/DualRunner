using UnityEngine;
using System.Collections;

public class DestroyField : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.transform.gameObject.transform.parent) {
			Destroy(collider.transform.gameObject.transform.parent.gameObject);
			return;
		}

		Destroy(collider.transform.gameObject);
	}	
}
