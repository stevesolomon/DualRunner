using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {
	
	public GameObject[] rooms;
	
	private Dictionary<int, List<GameObject>> difficultyMap;
	
	void Awake() {
		difficultyMap = new Dictionary<int, List<GameObject>>(8);
		
		//Loop through our rooms and build up our difficulty map.
		foreach (var room in rooms) {
			var roomInfo = room.GetComponent<RoomInfo>();
			
			if (!difficultyMap.ContainsKey(roomInfo.difficulty)) {
				difficultyMap.Add(roomInfo.difficulty, new List<GameObject>(8));
			} 
			
			difficultyMap[roomInfo.difficulty].Add(room);
		}
	}
	
	public void GenerateNextRoom() {
		int pieceIndex = Mathf.FloorToInt(Random.Range(0, rooms.Length));
		Instantiate (rooms[pieceIndex], new Vector3(this.transform.position.x, this.transform.position.y, 0f),
		             Quaternion.identity);
	}
}
