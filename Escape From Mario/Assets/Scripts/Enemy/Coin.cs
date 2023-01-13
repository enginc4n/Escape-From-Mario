using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int coinPoints = 10;
    bool isCollected;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            FindObjectOfType<GameManager>().AddScore(coinPoints);
            FindObjectOfType<AudioManager>().PlayCoinPickUP();
            Destroy(gameObject);
        }
    }
}
