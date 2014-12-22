using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

[RequireComponent(typeof(RunnerController))]
public class PlayerControlManager : MonoBehaviour
{
	public string jumpInputName;
	public Camera controllingCamera;

	private RunnerController character;
	private bool jumpPressed;
	private bool jumpExtending;

	private void Awake()
	{
	    character = GetComponent<RunnerController>();
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
			if (Input.GetTouch(i).phase == TouchPhase.Began &&
			    controllingCamera.pixelRect.Contains(Input.GetTouch(i).position)) {
				return true;
			}
		}

		return false;
	}

	private bool CheckTouchExtendJump() {
		for (var i = 0; i < Input.touchCount; i++) {
			if ((Input.GetTouch(i).phase == TouchPhase.Began || 
			     Input.GetTouch(i).phase == TouchPhase.Stationary || 
			     Input.GetTouch(i).phase == TouchPhase.Moved) && 
			    controllingCamera.pixelRect.Contains(Input.GetTouch(i).position)) {
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
