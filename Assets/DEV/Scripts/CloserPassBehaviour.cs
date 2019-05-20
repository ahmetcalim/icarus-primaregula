using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloserPassBehaviour : MonoBehaviour
{
    PowerUpController upController;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        upController = FindObjectOfType<PowerUpController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Player.IsGameRunning)
        {
            if (other.CompareTag("Obstacle") && !upController.IsPhaseActive)
            {
                audioSource.Play();
            }
        }
    }
 
}
