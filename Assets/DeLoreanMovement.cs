using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class DeLoreanMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private AudioClip movementSound;

    private Rigidbody movementRigidbody;
    private AudioSource movementAudioSource;
    private Vector3 movementInput;

    private void Awake()
    {
        movementRigidbody = GetComponent<Rigidbody>();
        movementRigidbody.isKinematic = false;
        movementRigidbody.useGravity = false;
        movementRigidbody.constraints |= RigidbodyConstraints.FreezeRotation;
        movementRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        movementRigidbody.interpolation = RigidbodyInterpolation.Interpolate;

        movementAudioSource = GetComponent<AudioSource>();
        movementAudioSource.playOnAwake = false;
        movementAudioSource.loop = true;
        movementAudioSource.clip = movementSound;
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        float horizontal = 0f;
        float vertical = 0f;
        float up_down = 0f;

        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            {
                horizontal -= 1f;
            }

            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            {
                horizontal += 1f;
            }

            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
            {
                vertical -= 1f;
            }

            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
            {
                vertical += 1f;
            }

            if (keyboard.spaceKey.isPressed)
            {
                up_down += 1f;
            }

            if (keyboard.leftShiftKey.isPressed)
            {
                up_down -= 1f;
            }
        }

        movementInput = new Vector3(horizontal, up_down, vertical).normalized;

        if (movementSound != null && movementRigidbody.linearVelocity.sqrMagnitude > 0.01f)
        {
            if (!movementAudioSource.isPlaying)
            {
                movementAudioSource.Play();
            }
        }
        else if (movementAudioSource.isPlaying)
        {
            movementAudioSource.Stop();
        }
    }

    private void FixedUpdate()
    {
        movementRigidbody.linearVelocity = movementInput * moveSpeed;
    }
}
