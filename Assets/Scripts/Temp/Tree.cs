using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private GameObject box;
    [SerializeField] private AudioClip treeFallingSound;

    private Animator anim;
    private AudioSource audioSource;
    private bool hasPlayedAnimation = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayedAnimation)
        {
            box.SetActive(false);
            hasPlayedAnimation |= true;
            anim.SetTrigger("Fall");
            audioSource.PlayOneShot(treeFallingSound, 1f);
        }
    }
}
