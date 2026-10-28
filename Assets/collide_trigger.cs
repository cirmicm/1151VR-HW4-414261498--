using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class collide_trigger : MonoBehaviour
{
    [SerializeField] private AudioClip collisionSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayCollisionSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayCollisionSound();
    }

    private void PlayCollisionSound()
    {
        if (collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
        }
    }
}
