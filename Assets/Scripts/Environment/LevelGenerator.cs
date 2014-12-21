using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {
	
	private List<GameObject> rooms;

	public TextAsset[] roomDefinitions;
	
	private Dictionary<int, List<GameObject>> difficultyMap;

	private List<int> difficulties;

	private RoomLoader roomLoader;
	
	void Awake() {
		difficultyMap = new Dictionary<int, List<GameObject>>(16);
		difficulties = new List<int>(16);
		roomLoader = new RoomLoader(Constants.PIXELS_PER_UNIT);
		rooms = new List<GameObject>(roomDefinitions.Length);

		foreach (var asset in roomDefinitions) {
			var room = roomLoader.LoadRoom(asset.text);
			var difficulty = room.GetComponent<RoomInfo>().difficulty;

			if (!difficultyMap.ContainsKey(difficulty)) {
				difficultyMap.Add(difficulty, new List<GameObject>(8));
				difficulties.Add(difficulty);
			}

			difficultyMap[difficulty].Add(room);
		}

		difficulties.Sort(); //Sort the difficulties for ordered indexing later.
	}

	void Start() {
		var cameraFollow = transform.parent.GetComponent<CameraFollow>();
		transform.position = new Vector3(transform.position.x - cameraFollow.xOffset,
		                                 transform.position.y, 0f);
	}
	
	public void GenerateNextRoom() {
		int difficultyIndex = Mathf.FloorToInt(Random.Range (0, difficulties.Count));
		int difficulty = difficulties[difficultyIndex];
		int pieceIndex = Mathf.FloorToInt(Random.Range (0, difficultyMap[difficulty].Count));

		var newRoom = Instantiate (difficultyMap[difficulty][pieceIndex], 
		             			   new Vector3(this.transform.position.x, this.transform.position.y, 0f),
		                           Quaternion.identity) as GameObject;

		newRoom.transform.FindChild("EnterRoomTrigger").GetComponent<EnterRoom>().levelGenerator = this;
		newRoom.SetActive(true);
	}
}
