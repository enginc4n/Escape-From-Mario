using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D enemyRigidBody;
    [Header("Settings")]
    [SerializeField] float moveSpeed = 1f;
    [Header("Refrence")]
    [SerializeField] GameObject coin;
    [SerializeField] ParticleSystem deathParticle;

    public bool spawnCoin;

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
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidBody.velocity.x)), 1f);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bow"))
        {
            Instantiate(deathParticle, transform.position, Quaternion.Euler(-90, 0, 0));
            if (spawnCoin)
            {
                Instantiate(coin, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
