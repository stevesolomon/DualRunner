using UnityEngine;

public class RunnerController : MonoBehaviour
{
	[SerializeField] private float maxSpeed = 10f; 
	[SerializeField] private float jumpForce = 400f; 
	[SerializeField] private float addedJumpForce = 80f;
	[SerializeField] private LayerMask whatIsGround; //A mask determining what is ground to the runner

	private Transform groundCheck; 
	private float groundedHeightTest = 4f; 
	private bool grounded = false; 
	private bool jumping = false;

	public float minTimeBetweenJumps = 0.25f;
	public float maxJumpExtendTime = 1f;

	private float timeSinceLastJump = 0.0f;
	private float timeExtendingJump = 0.0f;
	private bool canExtendJump = false;

	private void Awake()
	{
	    groundCheck = transform.Find("GroundCheck");
		timeSinceLastJump = minTimeBetweenJumps + 1f;
	}

	private void FixedUpdate()
	{
		grounded = Physics2D.OverlapArea(new Vector2(groundCheck.position.x - 16, groundCheck.position.y),
		                                 new Vector2(groundCheck.position.x + 16, groundCheck.position.y - groundedHeightTest),
		                                 whatIsGround);

		timeSinceLastJump += Time.deltaTime;
	}


	public void Move(float move, bool jumpPressed, bool jumpExtending)
	{
		bool canJump = false; 

		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

		if (timeSinceLastJump > minTimeBetweenJumps && grounded) {
			jumping = false;
			canJump = true;
		}

		//First check if we are able to jump or not.
	    if (canJump && jumpPressed) {
	        grounded = false;
	        rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			timeSinceLastJump = 0f;
			timeExtendingJump = 0f;
			canExtendJump = true;
			jumping = true;

			return; //We're done here
	    }

		//If we are currently jumping we might be able to extend the jump if the jump button is still pressed!
		//The following criteria have to be satisfied: 
		// (1) We are currently jumping.
		// (2) The jump button is pressed
		// (3) We can extend the jump (the moment the jump button is let go of we can no longer extend the current jump)
		// (4) We have not been extending the jump for too long
		// (5) We are not moving downwards (more of a sanity check than anything)
		if (jumping && jumpExtending && canExtendJump && 
		    timeExtendingJump < maxJumpExtendTime && rigidbody2D.velocity.y > 0f) {
			timeExtendingJump += Time.deltaTime;
			var normalizedJumpForce = addedJumpForce * Mathf.Lerp(1, 0, timeExtendingJump / maxJumpExtendTime);
			rigidbody2D.AddForce(new Vector2(0f, normalizedJumpForce));
		} else { //We're not extending the jump, so block it from happening for the rest of this jump.
			canExtendJump = false;
		}
	}
}
