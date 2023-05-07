using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            audioSource.Play();
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }
}
