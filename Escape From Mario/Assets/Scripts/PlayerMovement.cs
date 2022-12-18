using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool playerHasVerticalVelocity;
    bool charLookingRight = true;
    bool isAlive = true;
    float gravityScaleOnStart;
    Vector2 moveInput;
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;

    [Header("Settings")]
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deadthKick;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponentInChildren<CapsuleCollider2D>();
        playerFeetCollider = GetComponentInChildren<BoxCollider2D>();
        gravityScaleOnStart = playerRigidBody.gravityScale;

    }
    void Update()
    {
        if (isAlive)
        {
            Run();
            Climbing();
            Die();
        }
    }
    void OnMove(InputValue value)
    {
        if (isAlive)
        {
            moveInput = value.Get<Vector2>();
        }
    }
    void OnJump(InputValue value)
    {
        if (value.isPressed && playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && isAlive)
        {
            playerRigidBody.velocity += new Vector2(0f, jumpForce);
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;

        playerHasVerticalVelocity = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("IsRunning", playerHasVerticalVelocity);

        if (moveInput.x > 0 && !charLookingRight)
        {
            FlipSprite();
        }
        else if (moveInput.x < 0 && charLookingRight)
        {
            FlipSprite();
        }
    }
    void Climbing()
    {
        if (playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            Vector2 climbVelocity = new Vector2(playerRigidBody.velocity.x, moveInput.y * climbSpeed);
            playerRigidBody.velocity = climbVelocity;
            playerRigidBody.gravityScale = 0f;

            playerAnimator.SetBool("IsClimbing", true);
            playerAnimator.SetFloat("HasVelocityOnY", Mathf.Abs(playerRigidBody.velocity.y));
        }

        else
        {
            playerRigidBody.gravityScale = gravityScaleOnStart;
            playerAnimator.SetBool("IsClimbing", false);
        }
    }
    void FlipSprite()
    {
        charLookingRight = !charLookingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    void Die()
    {
        if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;

            playerRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
            playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

            playerAnimator.SetTrigger("Death");

            playerRigidBody.velocity = deadthKick;
        }
    }
}
