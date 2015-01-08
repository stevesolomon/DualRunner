using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class RoomManager : MonoBehaviour {
	
	public TextAsset[] roomDefinitions;

    public int pixelsPerUnit;
	
	private Dictionary<int, List<GameObject>> difficultyMap;
	
	public List<int> Difficulties { get; protected set; }

    public GameManagement gameManagement;
	
	private RoomLoader roomLoader;

	void Awake () {
		difficultyMap = new Dictionary<int, List<GameObject>>(16);
		Difficulties = new List<int>(16);
		roomLoader = new RoomLoader(pixelsPerUnit);

        foreach (var asset in roomDefinitions)
        {
            var room = roomLoader.LoadRoom(asset.text, gameManagement);
            var difficulty = room.GetComponent<RoomInfo>().difficulty;

            if (!difficultyMap.ContainsKey(difficulty))
            {
                difficultyMap.Add(difficulty, new List<GameObject>(8));
                Difficulties.Add(difficulty);
            }

            difficultyMap[difficulty].Add(room);
        }

        Difficulties.Sort(); //Sort the difficulties for ordered indexing later.

        Debug.Log(this.ToString());
	}
    
	public GameObject GetRandomRoom() {
		int difficultyIndex = Mathf.FloorToInt(Random.Range (0, Difficulties.Count));
		int difficulty = Difficulties[difficultyIndex];
		int pieceIndex = Mathf.FloorToInt(Random.Range (0, difficultyMap[difficulty].Count));

		return difficultyMap[difficulty][pieceIndex];
	}

	public GameObject GetRoomWithDifficulty(int difficulty) {

        if (!difficultyMap.ContainsKey(difficulty))
        {            
            difficulty = GetNextClosestDifficulty(difficulty);
        }

		int pieceIndex = Mathf.FloorToInt(Random.Range (0, difficultyMap[difficulty].Count));
		return difficultyMap[difficulty][pieceIndex];
	}

    private int GetNextClosestDifficulty(int difficulty)
    {
        int closestSoFar = int.MaxValue;
        int closestDifficulty = Difficulties[0];

        //If we don't have this difficulty as a key, then just choose the next closest from our list of difficulties.
        //Yeah, we're scanning through a list every time right now but the list of difficulties will always be short.
        // TODO: Make something better than this :)
        foreach (var currDifficulty in Difficulties)
        {
            //Ignore difficulty 0 in our calculations.
            if (currDifficulty == 0) continue;

            var difference = Mathf.Abs(difficulty - currDifficulty);

            if (difference < closestSoFar) {
                closestSoFar = difference;
                closestDifficulty = currDifficulty;
            }
        }

        return closestDifficulty;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var difficulty in Difficulties)
        {
            var rooms = difficultyMap[difficulty];

            sb.AppendLine(string.Format("Number of rooms with difficulty {0}: {1}", difficulty, rooms.Count));
        }

        return sb.ToString();
    }
}
