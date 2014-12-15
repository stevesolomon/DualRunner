using UnityEngine;
using System.Collections;

public class EnterRoom : MonoBehaviour {

	public LevelGenerator levelGenerator;

	void Awake() {
		levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		levelGenerator.GenerateNextRoom();
		Debug.Log("Triggered!");
	}
}
