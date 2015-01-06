using UnityEngine;
using System.Collections;

public class CameraTouchRegion : MonoBehaviour 
{
    public Camera camera;

    public Rect TouchRect { get; private set; }

    public int order;
    public int totalRegions;

    // Use this for initialization
    void Start () 
    {
        if (camera == null)
        {
            camera = Camera.main;
        }

        var fullRect = new Rect(0, 0, Screen.width, Screen.height);

        var myHeight = fullRect.height / (float) totalRegions;
        var myWidth = fullRect.width;
        var myX = fullRect.x;
        var myY = fullRect.y + (myHeight * order);

        TouchRect = new Rect(myX, myY, myWidth, myHeight);
    }
}
