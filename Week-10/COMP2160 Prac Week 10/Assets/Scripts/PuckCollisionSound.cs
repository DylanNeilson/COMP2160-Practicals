using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]

public class PuckCollisionSound : MonoBehaviour
{
    private AudioSource audioSource;
    private Rigidbody puckRigidbody;

    [SerializeField] private float minVolume = 0.1f;
    [SerializeField] private float maxVolume = 1.0f;
    [SerializeField] private float volumeScale = 10.0f;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        puckRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Total impulse applied
        // float collisionImpulse = collision.impulse.magnitude;
        float collisionImpulse = collision.relativeVelocity.magnitude;

        // Scale the impulse based on the volume scale
        float normalizedImpulse = collisionImpulse / volumeScale;

        // Calculate volume based on the impulse
        float volume = Mathf.Lerp(minVolume, maxVolume, Mathf.Clamp01(normalizedImpulse));

        // Set volume and play sound
        audioSource.volume = volume;
        audioSource.Play();
    }
}
