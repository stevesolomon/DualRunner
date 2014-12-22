﻿using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

[RequireComponent(typeof(RunnerController))]
public class PlayerControlManager : MonoBehaviour
{
	public string jumpInputName;
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

			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
				jumpPressed = true;
			}
		}

		jumpExtending = Input.GetButton(jumpInputName);

		if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Stationary ||
		    Input.GetTouch(0).phase == TouchPhase.Moved)) {
			jumpExtending = true;
		}
	}

	private void FixedUpdate()
	{
		character.Move(1, jumpPressed, jumpExtending);
		jumpPressed = false;
	}
}
