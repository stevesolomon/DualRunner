using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject objectToFollow;

	public float xOffset = 5f;

    private Camera camera;

    void Start()
    {
        camera = this.GetComponent<Camera>();
    }

	void Update () {
	    if (objectToFollow == null) return;

	    var newPos = new Vector3(objectToFollow.transform.position.x + xOffset,
	                             this.transform.position.y,
	                             this.transform.position.z);
	   // var roundPos = new Vector3(RoundToNearestPixel(newPos.x, camera), RoundToNearestPixel(newPos.y, camera), newPos.z);

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
