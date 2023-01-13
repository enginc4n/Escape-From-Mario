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

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        int numberOfAudioManager = FindObjectsOfType<GameManager>().Length;

        if (numberOfAudioManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetAudioManager()
    {
        Destroy(gameObject);
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
}
