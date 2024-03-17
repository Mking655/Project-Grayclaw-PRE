using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(AudioSource))]
public class DoorController : MonoBehaviour
{
    [SerializeField] private Transform doorTransform; // The actual door object to rotate
    public float rotateAmount = 90;
    public float speed = 2f;
    public AudioClip openSound;
    public AudioClip closeSound;

    private AudioSource audioSource;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpening = false;

    private void Start()
    {
        // Ensure there's a doorTransform assigned
        if (doorTransform == null)
        {
            Debug.LogError("Door transform not assigned.", this);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        closedRotation = doorTransform.rotation; // Store the initial rotation of the doorTransform as the closed state
        openRotation = closedRotation * Quaternion.Euler(0, rotateAmount, 0); // Calculate the open rotation
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 doorToPlayer = other.transform.position - doorTransform.position;
            float angle = Vector3.SignedAngle(doorTransform.forward, doorToPlayer, Vector3.up);
            Debug.Log(angle);
            if (angle < 90)
            {
                openRotation = closedRotation * Quaternion.Euler(0, rotateAmount, 0);
            }
            else
            {
                openRotation = closedRotation * Quaternion.Euler(0, -rotateAmount, 0);
                
            }

            StopAllCoroutines(); // Stop any existing rotation coroutines
            StartCoroutine(RotateDoor(openRotation, openSound));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isOpening)
        {
            StopAllCoroutines();
            StartCoroutine(RotateDoor(closedRotation, closeSound));
        }
    }

    private IEnumerator RotateDoor(Quaternion targetRotation, AudioClip sound)
    {
        isOpening = targetRotation == openRotation;
        audioSource.PlayOneShot(sound);

        while (Quaternion.Angle(doorTransform.rotation, targetRotation) > 0.01f)
        {
            doorTransform.rotation = Quaternion.Lerp(doorTransform.rotation, targetRotation, speed * Time.deltaTime);
            yield return null;
        }

        doorTransform.rotation = targetRotation; // Ensure the rotation is exactly the target in the end
    }
}
