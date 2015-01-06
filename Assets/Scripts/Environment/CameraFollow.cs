using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject objectToFollow;

	public float xOffset = 5f;

	void Update () {
	    if (objectToFollow == null) return;

	    var newPos = new Vector3(objectToFollow.transform.position.x + xOffset,
	                             this.transform.position.y,
	                             this.transform.position.z);

	    transform.position = newPos;  
	}

    private float RoundToNearestPixel(float unityUnits, Camera viewingCamera)
    {
        float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2)) * unityUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2));
        return adjustedUnityUnits;
    }

}
