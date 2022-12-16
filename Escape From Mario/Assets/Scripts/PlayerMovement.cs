using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool playerHasVerticalVelocity;
    float gravityScaleOnStart;
    Vector2 moveInput;
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;

    [Header("Settings")]
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 2f;
    [SerializeField] float climbSpeed = 5f;

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
        Run();
        FlipSprite();
        Climbing();
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (value.isPressed && playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;
        playerHasVerticalVelocity = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("IsRunning", playerHasVerticalVelocity);
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
        playerHasVerticalVelocity = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasVerticalVelocity)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f);
        }
    }

}
