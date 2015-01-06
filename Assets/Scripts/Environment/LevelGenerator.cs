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

    private int nextBlockPositionX;
    private int nextBlockPositionY;

    public GameObject[] basicBlockPrefabs;

	void Start() {
        if (roomManager == null)
        {
            roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();
        }

        nextBlockPositionX = initialXPosition;
        nextBlockPositionY = roomYPosition;
	}
	
	public void GenerateNextRoom() {        
        var roomRef = roomManager.GetRandomRoom();

        InstantiateNewRoom(roomRef, true);			
	}

    public void GenerateStartingRooms()
    {
        var roomRef = roomManager.GetRoomWithDifficulty(0);

        InstantiateNewRoom(roomRef, false);
        InstantiateNewRoom(roomRef, false);
    }

    private Vector3 GetNextRoomPosition()
    {
        int xPosition = nextBlockPositionX;
        int yPosition = nextBlockPositionY;

        nextBlockPositionX += roomLength;
        
        return new Vector3((float) xPosition, (float) yPosition, 0f);
    }

    private void InstantiateNewRoom(GameObject roomReference, bool usePaddingBlocks)
    {
        //Pick a random number of extra ground blocks to add as random spacing
        var randomSpacingIndex = Mathf.FloorToInt(Random.Range(0.0f, basicBlockPrefabs.Length + 1));

        var roomStartingPos = GetNextRoomPosition();
        var roomPaddedPos = roomStartingPos;

        if (usePaddingBlocks && randomSpacingIndex != basicBlockPrefabs.Length)
        {
            var paddingAmount = randomSpacingIndex + 1;
            roomPaddedPos = new Vector3(roomPaddedPos.x + paddingAmount, roomPaddedPos.y, roomPaddedPos.z);
            Instantiate(basicBlockPrefabs[randomSpacingIndex], roomStartingPos, Quaternion.identity);
            nextBlockPositionX += paddingAmount;
        }

        var newRoom = Instantiate(roomReference, roomPaddedPos, Quaternion.identity) as GameObject;	

        newRoom.transform.FindChild("EnterRoomTrigger").GetComponent<EnterRoom>().levelGenerator = this;
        newRoom.SetActive(true);

        currRoom++;
    }
}
