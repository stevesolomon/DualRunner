using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public float xOffset = 0f;

    public GameObject follow;
    	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(follow.transform.position.x + xOffset, this.transform.position.y);	
	}
}
