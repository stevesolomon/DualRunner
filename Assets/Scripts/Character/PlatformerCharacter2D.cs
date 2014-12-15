using UnityEngine;

namespace UnitySampleAssets._2D
{

    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 10f; 
        [SerializeField] private float jumpForce = 400f; //Amount of force added when the player jumps.	
        [SerializeField] private LayerMask whatIsGround; //A mask determining what is ground to the character

        private Transform groundCheck; 
        private float groundedRadius = 4f; 
        private bool grounded = false; //Whether or not the player is grounded.

		public float minTimeBetweenJumps = 0.25f;

		private float timeSinceLastJump;

        private void Awake()
        {
            groundCheck = transform.Find("GroundCheck");
			timeSinceLastJump = minTimeBetweenJumps + 1f;
        }

        private void FixedUpdate()
        {
			grounded = Physics2D.OverlapArea(new Vector2(groundCheck.position.x - 16, groundCheck.position.y),
			                                 new Vector2(groundCheck.position.x + 16, groundCheck.position.y - groundedRadius),
			                                 whatIsGround);

			timeSinceLastJump += Time.deltaTime;
        }


        public void Move(float move, bool jump)
        {
			bool canJump = false; 

			if (timeSinceLastJump > minTimeBetweenJumps)
				canJump = true;

            rigidbody2D.velocity = new Vector2(move*maxSpeed, rigidbody2D.velocity.y);

            // If the player should jump...
            if (grounded && canJump && jump /*&& anim.GetBool("Ground")*/)
            {
                grounded = false;
                rigidbody2D.AddForce(new Vector2(0f, jumpForce));
				timeSinceLastJump = 0f;
            }
        }
    }
}