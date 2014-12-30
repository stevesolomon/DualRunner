using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

[RequireComponent(typeof(RunnerController))]
public class PlayerControlManager : MonoBehaviour
{
	public string jumpInputName;
	public CameraTouchRegion cameraTouchRegion;

	private RunnerController character;
	private bool jumpPressed;
	private bool jumpExtending;

	private void Awake()
	{
	    character = GetComponent<RunnerController>();
        cameraTouchRegion = GetComponent<CameraTouchRegion>();
	}

	private void Update()
	{
		if (!jumpPressed) 
		{
			jumpPressed = Input.GetButtonDown(jumpInputName);
			jumpPressed |= CheckTouchJump();
		}

		jumpExtending = Input.GetButton(jumpInputName);
		jumpExtending |= CheckTouchExtendJump();
	}

	//Check all the touches and grab the first one beginning in the range of this camera.
	private bool CheckTouchJump() {
		for (var i = 0; i < Input.touchCount; i++) {
            var touch = Input.GetTouch(i);
            var realPosition = new Vector2(touch.position.x, Screen.height - touch.position.y);
			if (touch.phase == TouchPhase.Began && cameraTouchRegion.TouchRect.Contains(realPosition)) {
				return true;
			}
		}

		return false;
	}

	private bool CheckTouchExtendJump() {
		for (var i = 0; i < Input.touchCount; i++) {
            var touch = Input.GetTouch(i);
            var realPosition = new Vector2(touch.position.x, Screen.height - touch.position.y);
            if ((touch.phase == TouchPhase.Began ||
                 touch.phase == TouchPhase.Stationary ||
                 touch.phase == TouchPhase.Moved) && 
			    cameraTouchRegion.TouchRect.Contains(realPosition)) {
				return true;
			}
		}

		return false;
	}

	private void FixedUpdate()
	{
		character.Move(1, jumpPressed, jumpExtending);
		jumpPressed = false;
	}
}
