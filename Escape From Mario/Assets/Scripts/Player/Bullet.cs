using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bulletRigidBody;
    PlayerMovement playerMovement;

    [Header("Settings")]
    [SerializeField] float bulletSpeed = 15f;
    void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    void Start()
    {
        float xBulletSpeed = bulletSpeed * playerMovement.transform.localScale.x;
        bulletRigidBody.velocity = new Vector2(xBulletSpeed, 0f);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

}
