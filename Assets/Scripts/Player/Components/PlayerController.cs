using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float groundCheckRadius = 10f;
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float crouchSpeed = 2.0f;

    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    private bool isFacingRight = true;
    private bool isCrouching = false;
    private float horizontalInput;
    private Transform tr;
    private Animator animator;
    private bool grounded;
    private Vector3 originalScale;
    private Vector3 crouchScale;
    private Vector2 originalColliderSize;
    private float originalXScale;
    private PlayerState state;

    public enum PlayerState
    {
        Idle,
        Running,
        Crouching
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        tr = transform;
        animator = GetComponent<Animator>();
        originalScale = tr.localScale;
        crouchScale = originalScale;
        crouchScale.y *= 0.7f;
        originalColliderSize = playerCollider.size;
        originalXScale = tr.localScale.x;
        state = PlayerState.Idle;
    }

    private void Update()
    {
        checkMovement();

        isGrounded();

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCrouch();
        }

        if (!grounded)
        {
            animator.SetBool("isJumping", false);
        }

        if (isCrouching)
        {
            Crouch();
        } else
        {
            Stand();
        }
    }

    private void checkMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        Debug.Log(horizontalInput);

        if (horizontalInput != 0f)
        {
            state = PlayerState.Running;
        } else
        {
            state = PlayerState.Idle;
        }

        if (state == PlayerState.Running)
        {
            flipSprite();
        }
    }

    private void Stand()
    {
        tr.localScale = Vector3.Lerp(tr.localScale, originalScale, crouchSpeed * Time.deltaTime);

        playerCollider.size = Vector2.Lerp(playerCollider.size, originalColliderSize, crouchSpeed * Time.deltaTime);
    }

    private void Crouch()
    {
        tr.localScale = Vector3.Lerp(tr.localScale, crouchScale, crouchSpeed * Time.deltaTime);

        Vector2 targetColliderSize = originalColliderSize;
        targetColliderSize.y *= 0.7f;
        playerCollider.size = Vector2.Lerp(playerCollider.size, targetColliderSize, crouchSpeed);
    }

    private void ToggleCrouch()
    {
        isCrouching = !isCrouching;
        animator.SetBool("isCrouching", isCrouching);
    }

    private void FixedUpdate()
    {
        if (!isCrouching)
        {
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        } else
        {
            rb.linearVelocity = new Vector2(horizontalInput * crouchSpeed, rb.linearVelocity.y);
        }
        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private void flipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = tr.localScale;
            ls.x *= -1f;
            tr.localScale = ls;
        }
    }

    public void isGrounded()
    {
        Vector2 raycastOrigin = playerCollider.bounds.center;
        raycastOrigin.y -= playerCollider.bounds.extents.y;

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, 0.1f, platformLayer);

        grounded = (hit.collider != null);
    }
   
}
