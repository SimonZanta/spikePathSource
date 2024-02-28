using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public float horizontalMove = 0;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private float input;

    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    public bool isTouchingFront;
    public Transform frontCheck;
    private bool wallsliding;
    public float wallSlidingSpeed;

    private bool walljumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    public bool canDoublejump;
    private int velocity;
    public int JumpCount;


    public Animator animator;
    private string currentState;

    private const string run = "run";
    private const string idle = "idle";
    private const string jump = "jump";
    private const string wallJump = "WallJump";
    private const string wallSlide = "wallSlide";
    private const string death = "death";


    private int extraJumps;
    public int extraJumpsValue;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        extraJumps = extraJumpsValue;
    }

    public void FixedUpdate()
    {
        // check ground and front
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        //basic movement
        input = Input.GetAxisRaw("Horizontal");
        horizontalMove = input * speed;

        //flip
        if (facingRight == false && input > 0)
        {
            Flip();
        }
        else if (facingRight == true && input < 0)
        {
            Flip();
        }
    }

    public void Update()
    {
        //jumping
        rb.velocity = new Vector2(input * speed, rb.velocity.y);

        //canDoublejump = (isGrounded || isTouchingFront)? true : canDoublejump;


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                extraJumps = extraJumpsValue++;

                // rb.velocity = Vector2.up * jumpForce;
            }
            if (isTouchingFront)
            {
                extraJumps = extraJumpsValue + 2;
            }
            if (extraJumps > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
                //canDoublejump = false;
                JumpCount = extraJumps;
            }
            else if (extraJumps == 0 && isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
            }

        }



        wallsliding = (isTouchingFront && !isGrounded && input != 0);

        if (wallsliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        //walljump
        if (Input.GetKeyDown(KeyCode.UpArrow) && wallsliding == true)
        {
            walljumping = true;
            Invoke("setWallJumpingToFalse", wallJumpTime);
        }

        if (walljumping == true)
        {
            rb.velocity = new Vector2(xWallForce * -input, yWallForce);
        }

        if (isGrounded)
        {
            if (horizontalMove > 0 || horizontalMove < 0)
            {
                ChangeAnimationState(run);
            }
            else
            {
                ChangeAnimationState(idle);
            }
        }

        if (!isGrounded)
        {
            if (isTouchingFront == false)
            {
                ChangeAnimationState(jump);
            }
            else if (isTouchingFront == true && wallsliding == true)
            {
                ChangeAnimationState(wallSlide);
            }
            else
            {
                ChangeAnimationState(wallJump);
            }
        }
    }

    public void Death()
    {
        Destroy(rb);
        ChangeAnimationState(death);
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

    private void setWallJumpingToFalse()
    {
        walljumping = false;
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}