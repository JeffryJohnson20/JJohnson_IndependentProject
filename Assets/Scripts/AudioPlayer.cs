using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource playSound;
    private bool isGrounded;

    void OnTriggerEnter(Collider other)
    {
        isGrounded = false;
        if (isGrounded == false)
        {
            playSound.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isGrounded = true;
    }
}
