using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {
	
	public RoomManager roomManager;

    public int currentDifficulty = 1;

    public int currRoom = -1;

    public int roomLength = 12;

    public int roomYPosition = -7;

    public int initialXPosition = 0;

	void Start() {
        if (roomManager == null)
        {
            roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();
        }
	}
	
	public void GenerateNextRoom() {        
        var roomRef = roomManager.GetRandomRoom();

        InstantiateNewRoom(roomRef);			
	}

    public void GenerateStartingRooms()
    {
        var roomRef = roomManager.GetRoomWithDifficulty(0);

        InstantiateNewRoom(roomRef);
        InstantiateNewRoom(roomRef);
    }

    private Vector2 GetNextRoomPosition()
    {
        int xPosition = initialXPosition + (currRoom * roomLength);
        int yPosition = roomYPosition;

        return new Vector2((float) xPosition, (float) yPosition);
    }

    private void InstantiateNewRoom(GameObject roomReference)
    {
        var roomPos = GetNextRoomPosition();
        var newRoom = Instantiate(roomReference, new Vector3(roomPos.x, roomPos.y, 0f), Quaternion.identity) as GameObject;	

        newRoom.transform.FindChild("EnterRoomTrigger").GetComponent<EnterRoom>().levelGenerator = this;
        newRoom.SetActive(true);

        currRoom++;
    }
}
