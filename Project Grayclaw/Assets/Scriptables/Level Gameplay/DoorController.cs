using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(AudioSource))]
public class DoorController : MonoBehaviour
{
    [SerializeField] private Transform doorTransform; // The actual door object to rotate or move
    public bool vertical = false;
    public float rotateAmount = 90;
    public float lip = 0;
    public float speed = 2f;
    public AudioClip openSound;
    public AudioClip closeSound;

    private AudioSource audioSource;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Vector3 closedPosition;
    private Vector3 openPosition;

    [SerializeField]
    bool open = false;
    [SerializeField]
    private bool isReady = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (doorTransform == null)
        {
            Debug.LogError("Door transform not assigned.", this);
            return;
        }

        if (!vertical)
        {
            closedRotation = doorTransform.rotation;
            openRotation = closedRotation * Quaternion.Euler(0, rotateAmount, 0);
        }
        else
        {
            closedPosition = doorTransform.position;
            openPosition = closedPosition + new Vector3(0, doorTransform.GetComponent<BoxCollider>().size.y - lip, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !open && isReady)
        {
            if (!vertical)
            {
                // Determine door opening direction based on player position
                DetermineOpeningDirection(other.transform.position);
                StopAllCoroutines();
                StartCoroutine(RotateDoor(openRotation, openSound));
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(OpenDoor(openPosition, openSound));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && open && isReady)
        {
            //StopAllCoroutines(); // Ensure any ongoing movement completes before closing
            if (!vertical)
            {
                StartCoroutine(RotateDoor(closedRotation, closeSound));
            }
            else
            {
                StartCoroutine(OpenDoor(closedPosition, closeSound));
            }
        }
    }

    private IEnumerator RotateDoor(Quaternion targetRotation, AudioClip sound)
    {
        isReady = false;
        audioSource.PlayOneShot(sound);

        float startTime = Time.time;
        Quaternion startRotation = doorTransform.rotation;

        while (Quaternion.Angle(doorTransform.rotation, targetRotation) > 0.01f)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / Quaternion.Angle(startRotation, targetRotation);
            doorTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, fractionOfJourney);
            yield return null;
        }

        doorTransform.rotation = targetRotation;
        isReady = true;
        open = doorTransform.rotation == openRotation;
    }

    private IEnumerator OpenDoor(Vector3 targetPosition, AudioClip sound)
    {
        isReady = false;
        audioSource.PlayOneShot(sound);

        float startTime = Time.time;
        Vector3 startPosition = doorTransform.position;

        while (Vector3.Distance(doorTransform.position, targetPosition) > 0.1f)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / Vector3.Distance(startPosition, targetPosition);
            doorTransform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        doorTransform.position = targetPosition;
        isReady = true;
        open = Mathf.Abs(doorTransform.position.y - openPosition.y) < 0.01f;
    }

    private void DetermineOpeningDirection(Vector3 playerPosition)
    {
        Vector3 doorToPlayer = playerPosition - doorTransform.position;
        float angle = Vector3.SignedAngle(doorTransform.forward, doorToPlayer, Vector3.up);
        openRotation = angle < 90 ? closedRotation * Quaternion.Euler(0, rotateAmount, 0) : closedRotation * Quaternion.Euler(0, -rotateAmount, 0);
    }
}
