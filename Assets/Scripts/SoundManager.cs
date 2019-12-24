using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //outlets
    AudioSource audioSource;
    public AudioClip missSound;
    public AudioClip hitSound;

    void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundMiss()
    {
        audioSource.PlayOneShot(missSound);
    }

    public void PlaySoundHit()
    {
        audioSource.PlayOneShot(hitSound);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            SoundManager.instance.PlaySoundHit();
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            SoundManager.instance.PlaySoundMiss();
        }

        Destroy(gameObject);
    }
}
