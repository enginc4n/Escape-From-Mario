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
    bool isHitted;
    void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isHitted)
        {
            enemyRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            enemyRigidBody.velocity = new Vector2(0, 0f);
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipSprite();

    }
    void FlipSprite()
    {
        if (!isHitted)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bow")
        {
            isHitted = true;
            deathParticle.Play();
            if (spawnCoin)
            {
                Instantiate(coin, other.transform.position, Quaternion.identity);
            }
            Destroy(gameObject, 2);
        }
    }
}
