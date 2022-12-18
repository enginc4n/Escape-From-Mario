using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D enemyRigidBody;
    [Header("Settings")]
    [SerializeField] float moveSpeed = 1f;
    void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        enemyRigidBody.velocity = new Vector2(moveSpeed, 0f);

    }
    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipSprite();

    }
    void FlipSprite()
    {
        transform.Rotate(0f, 180f, 0f);
    }
}
