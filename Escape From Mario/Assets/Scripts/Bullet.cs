using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bulletRigidBody;
    PlayerMovement playerMovement;

    float xBulletSpeed;
    [Header("Settings")]
    [SerializeField] float bulletSpeed = 15f;
    void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    void Start()
    {
        xBulletSpeed = bulletSpeed * playerMovement.transform.localScale.x;
    }

    void Update()
    {
        bulletRigidBody.velocity = new Vector2(xBulletSpeed, 0f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
