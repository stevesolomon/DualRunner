using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {
	
	private List<GameObject> rooms;

	public TextAsset[] roomDefinitions;
	
	private Dictionary<int, List<GameObject>> difficultyMap;

	private int maxDifficulty;

	private RoomLoader roomLoader;
	
	void Awake() {
		difficultyMap = new Dictionary<int, List<GameObject>>(8);
		roomLoader = new RoomLoader();
		rooms = new List<GameObject>(roomDefinitions.Length);

		foreach (var asset in roomDefinitions) {
			var room = roomLoader.LoadRoom(asset.text);
			var difficulty = room.GetComponent<RoomInfo>().difficulty;

			if (!difficultyMap.ContainsKey(difficulty)) {
				difficultyMap.Add(difficulty, new List<GameObject>(8));
			}

			difficultyMap[difficulty].Add(room);
		}
	}
	
	public void GenerateNextRoom() {
		int difficultyIndex = Mathf.FloorToInt(Random.Range (0, maxDifficulty));
		int pieceIndex = Mathf.FloorToInt(Random.Range (0, difficultyMap[difficultyIndex].Count));

		var newRoom = Instantiate (difficultyMap[difficultyIndex][pieceIndex], 
		             			   new Vector3(this.transform.position.x, this.transform.position.y, 0f),
		                           Quaternion.identity) as GameObject;

		newRoom.SetActive(true);
	}
}
