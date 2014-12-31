using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {
	
	public TextAsset[] roomDefinitions;

    public int pixelsPerUnit;
	
	private Dictionary<int, List<GameObject>> difficultyMap;
	
	public List<int> Difficulties { get; protected set; }
	
	private RoomLoader roomLoader;

	void Awake () {
		difficultyMap = new Dictionary<int, List<GameObject>>(16);
		Difficulties = new List<int>(16);
		roomLoader = new RoomLoader(pixelsPerUnit);
		
		foreach (var asset in roomDefinitions) {
			var room = roomLoader.LoadRoom(asset.text);
			var difficulty = room.GetComponent<RoomInfo>().difficulty;
			
			if (!difficultyMap.ContainsKey(difficulty)) {
				difficultyMap.Add(difficulty, new List<GameObject>(8));
				Difficulties.Add(difficulty);
			}
			
			difficultyMap[difficulty].Add(room);
		}
		
		Difficulties.Sort(); //Sort the difficulties for ordered indexing later.
	}

	public GameObject GetRandomRoom() {
		int difficultyIndex = Mathf.FloorToInt(Random.Range (0, Difficulties.Count));
		int difficulty = Difficulties[difficultyIndex];
		int pieceIndex = Mathf.FloorToInt(Random.Range (0, difficultyMap[difficulty].Count));

		return difficultyMap[difficulty][pieceIndex];
	}

	public GameObject GetRoomWithDifficulty(int difficulty) {
		if (difficultyMap.ContainsKey(difficulty)) {
			int pieceIndex = Mathf.FloorToInt(Random.Range (0, difficultyMap[difficulty].Count));
			return difficultyMap[difficulty][pieceIndex];
		}

		return null;
	}
}
