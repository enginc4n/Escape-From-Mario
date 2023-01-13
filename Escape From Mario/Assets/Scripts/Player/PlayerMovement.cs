using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    bool playerHasHorizontalSpeed;
    bool playerHasVerticalSpeed;
    bool isAlive = true;
    bool isClimbing;
    float gravityScaleAtStart;
    Vector2 moveInput;
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;
    AudioManager audioManager;

    [Header("Settings")]
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deadthKick;

    [Header("Bow")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bow;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponentInChildren<CapsuleCollider2D>();
        playerFeetCollider = GetComponentInChildren<BoxCollider2D>();
        gravityScaleAtStart = playerRigidBody.gravityScale;

    }
    void Update()
    {
        if (isAlive)
        {
            Run();
            Climbing();
            Die();
            FlipSprite();
        }
    }
    void OnMove(InputValue value)
    {
        if (isAlive)
        {
            moveInput = value.Get<Vector2>();
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;

        playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);

    }
    void OnJump(InputValue value)
    {
        if (value.isPressed && playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")) && isAlive)
        {
            playerRigidBody.velocity += new Vector2(0f, jumpForce);
            audioManager.PlayJumpSound();
        }
    }

    void OnFire(InputValue value)
    {
        if (value.isPressed && isAlive && !playerHasHorizontalSpeed && !playerHasVerticalSpeed)
        {
            Instantiate(bullet, bow.position, Quaternion.Euler(0, 0, -90));
            playerAnimator.SetTrigger("Attacking");
            audioManager.playArrowSound();
        }
    }
    void Climbing()
    {
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            playerRigidBody.gravityScale = gravityScaleAtStart;
            playerHasVerticalSpeed = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
            playerAnimator.SetBool("IsClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2(playerRigidBody.velocity.x, moveInput.y * climbSpeed);
        playerRigidBody.velocity = climbVelocity;
        playerRigidBody.gravityScale = 0f;

        playerAnimator.SetBool("IsClimbing", true);
        playerAnimator.SetFloat("HasVelocityOnY", Mathf.Abs(playerRigidBody.velocity.y));
    }
    void FlipSprite()
    {
        playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f);
        }

    }
    void Die()
    {
        if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;

            playerRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
            playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

            playerAnimator.SetTrigger("Death");

            playerRigidBody.velocity = deadthKick;
            FindObjectOfType<GameManager>().ProcessPlayerDeath();
        }
    }

}
