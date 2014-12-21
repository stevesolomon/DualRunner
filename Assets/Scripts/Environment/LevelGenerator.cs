using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {
	
	public RoomManager roomManager;

	void Start() {
		var cameraFollow = transform.parent.GetComponent<CameraFollow>();
		transform.position = new Vector3(transform.position.x - cameraFollow.xOffset,
		                                 transform.position.y, 0f);
	}
	
	public void GenerateNextRoom() {
		if (roomManager == null) {
			roomManager = GameObject.Find ("RoomManager").GetComponent<RoomManager>();
		}

		var roomRef = roomManager.GetRandomRoom();
		var newRoom = Instantiate (roomRef, 
		             			   new Vector3(this.transform.position.x, this.transform.position.y, 0f),
		                           Quaternion.identity) as GameObject;

		newRoom.transform.FindChild("EnterRoomTrigger").GetComponent<EnterRoom>().levelGenerator = this;
		newRoom.SetActive(true);
	}
}
