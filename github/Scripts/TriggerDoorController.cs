using UnityEngine;

public class TriggerDoorRotatorWithAudio : MonoBehaviour
{
    [SerializeField] private Transform doorTransform; // Reference to the door's Transform
    [SerializeField] private Vector3 openRotation = new Vector3(0, 90, 0); // Rotation when the door is open
    [SerializeField] private float rotationSpeed = 2.0f; // Speed of rotation
    [SerializeField] private AudioSource audioSource; // AudioSource for door sound
    [SerializeField] private AudioClip openDoorSound; // Audio clip for opening the door
    [SerializeField] private AudioClip closeDoorSound; // Audio clip for closing the door

    private Vector3 closedRotation; // Initial rotation when the door is closed
    private bool isDoorOpen = false; // Track door state
    private bool isRotating = false; // Prevent multiple rotations at the same time

    private void Start()
    {
        // Store the initial rotation of the door
        closedRotation = doorTransform.localEulerAngles;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRotating)
        {
            // Toggle door state
            isDoorOpen = !isDoorOpen;

            // Start the door rotation coroutine
            StopAllCoroutines();
            StartCoroutine(RotateDoor(isDoorOpen ? openRotation : closedRotation));
        }
    }

    private System.Collections.IEnumerator RotateDoor(Vector3 targetRotation)
    {
        isRotating = true; // Set rotation flag to prevent multiple triggers

        // Play the appropriate audio based on door state
        if (audioSource != null)
        {
            if (isDoorOpen && openDoorSound != null)
            {
                audioSource.clip = openDoorSound;
                audioSource.Play();
            }
            else if (!isDoorOpen && closeDoorSound != null)
            {
                audioSource.clip = closeDoorSound;
                audioSource.Play();
            }
        }

        // Smoothly rotate the door to the target rotation
        while (Vector3.Distance(doorTransform.localEulerAngles, targetRotation) > 0.1f)
        {
            doorTransform.localEulerAngles = Vector3.Lerp(
                doorTransform.localEulerAngles,
                targetRotation,
                Time.deltaTime * rotationSpeed
            );
            yield return null;
        }

        // Snap to final rotation to prevent jitter
        doorTransform.localEulerAngles = targetRotation;

        // Stop the audio source once the rotation is complete
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        isRotating = false; // Allow further rotation if needed (when player re-enters trigger)
    }
}
