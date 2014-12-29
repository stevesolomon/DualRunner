using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject objectToFollow;

	public float xOffset = 5f;

    void Start()
    {
        Screen.SetResolution(576, 800, false);
    }

	void Update () {
		if (objectToFollow != null) {
			this.transform.position = new Vector3(objectToFollow.transform.position.x + xOffset, 
			                                      this.transform.position.y, 
			                                      this.transform.position.z);
		}
	}
}
