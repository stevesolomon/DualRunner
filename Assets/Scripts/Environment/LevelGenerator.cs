using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

	public GameObject[] pieces;

	public void GenerateNextRoom() {
		int pieceIndex = Mathf.FloorToInt(Random.Range(0, pieces.Length));
		Instantiate (pieces[pieceIndex], new Vector3(this.transform.position.x, this.transform.position.y, 0f),
		             Quaternion.identity);
	}
}
