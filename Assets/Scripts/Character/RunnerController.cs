using UnityEngine;

public class RunnerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f; 
    [SerializeField] private float jumpForce = 400f; 
    [SerializeField] private float addedJumpForce = 80f;
    [SerializeField] private LayerMask whatIsGround; //A mask determining what is ground to the runner

    private Transform groundCheck; 
    public float groundedHeightTest = 0.08f;    

    private bool jumping = false;

    public float minTimeBetweenJumps;
    public float maxJumpExtendTime;

    private float timeSinceLastJump = 0.0f;
    private float timeExtendingJump = 0.0f;
    private bool canExtendJump = false;

    private float timeUntilGroundTest = 0.0f;

    private MessageBus messageBus;
    private Animator animator;
    private Rigidbody2D rigidbody;

    public bool IsGrounded
    {
        get;
        protected set;
    } 

    private void Awake()
    {
        groundCheck = transform.Find("GroundCheck");
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        timeSinceLastJump = minTimeBetweenJumps + 1f;
    }

    private void Start()
    {
        messageBus = MessageBus.Instance;
    }

    private void FixedUpdate()
    {
        IsGrounded = false;
        timeUntilGroundTest -= Time.deltaTime;

        if (timeUntilGroundTest <= 0f)
        {
            IsGrounded = Physics2D.OverlapArea(new Vector2(groundCheck.position.x - 0.5f, groundCheck.position.y),
                                             new Vector2(groundCheck.position.x + 0.5f, groundCheck.position.y - groundedHeightTest),
                                             whatIsGround);
        }

        animator.SetBool("isGrounded", IsGrounded);
        animator.SetFloat("yVelocity", rigidbody.velocity.y);
        
        timeSinceLastJump += Time.deltaTime;
    }


    public void Move(float move, bool jumpPressed, bool jumpExtending)
    {
        var canJump = false; 

        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (IsGrounded) 
        {
            jumping = false;

            if (timeSinceLastJump > minTimeBetweenJumps)
                canJump = true;
        }

        //First check if we are able to jump or not.
        if (canJump && jumpPressed) 
        {
            IsGrounded = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            timeSinceLastJump = 0f;
            timeExtendingJump = 0f;
            canExtendJump = true;
            jumping = true;
            timeUntilGroundTest = 0.1f;

            messageBus.SendMessage(new PlayerJumpedMessage());

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
            timeExtendingJump < maxJumpExtendTime && GetComponent<Rigidbody2D>().velocity.y > 0f) 
        {
            timeExtendingJump += Time.deltaTime;
            var normalizedJumpForce = addedJumpForce * Mathf.Lerp(1, 0, timeExtendingJump / maxJumpExtendTime);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, normalizedJumpForce));
        }
        else  //We're not extending the jump, so block it from happening for the rest of this jump.
        {
            canExtendJump = false;
        }
    }
}
