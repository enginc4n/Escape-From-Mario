using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [Header("Audios")]
    [SerializeField] AudioClip coinPickUpSound;
    [SerializeField] AudioClip arrowSound;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip bounceSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayCoinPickUP()
    {
        audioSource.PlayOneShot(coinPickUpSound);
    }
    public void playArrowSound()
    {
        audioSource.PlayOneShot(arrowSound);
    }
    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }
    public void PlayBounceJump()
    {
        audioSource.PlayOneShot(bounceSound, 0.5f);
    }
}
