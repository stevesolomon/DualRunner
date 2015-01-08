using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelGenerator : MonoBehaviour {
	
	public RoomManager roomManager;

    public int currentDifficulty = 5;

    public int difficultyFactor = 5;

    private int difficultyQuota;

    public int currRoom = -1;

    public int roomLength = 12;

    public int roomYPosition = -7;

    public int initialXPosition = 0;

    private int nextBlockPositionX;
    private int nextBlockPositionY;

    private bool nextRoomDifficultyZero = false;

    public GameObject[] basicBlockPrefabs;

	void Awake() {
        nextBlockPositionX = initialXPosition;
        nextBlockPositionY = roomYPosition;
	}

    void Start()
    {
        if (roomManager == null)
        {
            roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();
        }

        difficultyQuota = currentDifficulty;
    }
	
	public void GenerateNextRoom() {

        var selectedDifficulty = GetNextDifficulty();
        var roomRef = roomManager.GetRoomWithDifficulty(selectedDifficulty);
        var actualRoomDifficulty = roomRef.GetComponent<RoomInfo>().difficulty;

        UpdateDifficultyQuota(actualRoomDifficulty);

        InstantiateNewRoom(roomRef, true);			
	}

    private void UpdateDifficultyQuota(int lastRoomDifficulty)
    {
        difficultyQuota -= lastRoomDifficulty;

        if (difficultyQuota <= 0)
        {
            currentDifficulty += difficultyFactor;
            difficultyQuota = currentDifficulty;
            nextRoomDifficultyZero = true;
        }
    }

    private int GetNextDifficulty()
    {
        //When we generate the next room we have to take into account our current difficulty, and the remaining quota that we have.
        //Ideally, we want to generate a room no higher than the remaining difficulty quota, but we'll introduce a bit of randomness
        //there to keep things interesting! If the quota is less than or equal to zero, then we'll:
        // (a) Insert a difficulty 0 room
        // (b) Increase the difficulty by the difficulty factor.
        // (c) Reset the quota.
        if (nextRoomDifficultyZero) 
        {
            nextRoomDifficultyZero = false;
            return 0;
        }

        var difficultyMin = currentDifficulty / difficultyFactor;
        if (difficultyMin == 0) { difficultyMin = 1; }

        var quotaRange = UnityEngine.Random.Range(difficultyQuota - (difficultyFactor / 2), difficultyQuota + (difficultyFactor / 2));
        var difficultyMax = Math.Max(quotaRange, difficultyMin);

        var selectedDifficulty = UnityEngine.Random.Range(difficultyMin, difficultyMax);

        return selectedDifficulty;
    }

    public void GenerateStartingRooms()
    {
        var roomRef = roomManager.GetRoomWithDifficulty(0);

        InstantiateNewRoom(roomRef, false);
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
        var randomSpacingIndex = Mathf.FloorToInt(UnityEngine.Random.Range(0.0f, basicBlockPrefabs.Length + 1));

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
