using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerController : MonoBehaviour
{
    public float speed = 10;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkFeetRadius;
    public LayerMask whatIsGround;

    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    private int extraJumps;
    public int extraJumpsValue;

    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    public Animator animator;

    private void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkFeetRadius, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkFeetRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    public void move(float move, bool Jump, bool wallJumping, bool wallSliding)
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (isGrounded == true || isTouchingFront == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Jump && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Jump && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        /*if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            animator.SetBool("isSliding", true);
        }
        else
        {
            animator.SetBool("isSliding", false);
        }*/

        if (wallJumping)
        {
            rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
            animator.SetBool("wallClimb", true);
        }
        else
        {
            animator.SetBool("wallClimb", false);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}