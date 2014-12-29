using UnityEngine;

public class EnterRoom : MonoBehaviour {

	public LevelGenerator levelGenerator;

	void OnTriggerEnter2D(Collider2D collider) {
		levelGenerator.GenerateNextRoom();
	}
}
