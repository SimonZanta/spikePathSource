using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public playerController controller;
    public Animator animator;

    public float horizontalMove = 0;
    public float moveInput;

    bool isTouchingFront;
    public float checkFeetRadius;
    public float checkHandRadius;
    public LayerMask whatIsGround;
    public Transform frontCheck;
    public Transform groundCheck;
    private bool isGrounded;

    public float runSpeed = 10f;

    bool jump = false;
    bool wallJumping = false;
    bool wallSliding;

    // Update is called once per frame
    void Update()
    {
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkFeetRadius, whatIsGround);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkFeetRadius, whatIsGround);

        moveInput = Input.GetAxisRaw("Horizontal");
        horizontalMove = moveInput * runSpeed;

        animator.SetFloat("run", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }

        if (Input.GetButtonDown("Jump") && wallSliding == true)
        {
            wallJumping = true;
        }

        if (wallJumping == true)
        {
            animator.SetBool("wallClimb", true);
        }
        else
        {
            animator.SetBool("wallClimb", false);
        }

        if (isTouchingFront == true && isGrounded == false && moveInput != 0 && wallJumping == false)

        {
            wallSliding = true;
        }
    }

    public void death()
    {
        animator.SetBool("death", true);
    }

    private void FixedUpdate()
    {
        controller.move(horizontalMove * Time.fixedDeltaTime, jump, wallJumping, wallSliding);
        jump = false;
        wallJumping = false;
        wallSliding = false;
    }
}