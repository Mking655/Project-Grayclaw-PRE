using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPOV : MonoBehaviour
{
    private void OnEnable()
    {
        FindAnyObjectByType<InGameUIManager>().resetReticle();
        Cursor.lockState = CursorLockMode.None;
        //Set the fov of the main camera
        gameObject.GetComponent<POV>().getManager().cam.gameObject.GetComponent<Camera>().fieldOfView = PlayerSettings.InteractingFOV;
    }
    [Tooltip("For if you want to disable this component locally, leave true if not.")]
    [SerializeField]
    public bool localActive = true;
    public float turnSpeed = 4.0f;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    private float localMinX;
    private float localMaxX;
    private float localMinY;
    private float localMaxY;
    private float rotX;
    private float rotY;
    //handles how the camera looks at the laptop
    private void Update()
    {
        if (localActive)
        {
            MouseAiming();
        }
        //Go back
        if (Input.GetKeyDown(KeyCode.Space))
        {
            POVManager.Instance.changeState(POVManager.Instance.previousActiveState);
        }
    }
    //calculate cam borders
    private void Awake()
    {
        localMinY = transform.localEulerAngles.y + minTurnAngle;
        localMaxY = transform.localEulerAngles.y + maxTurnAngle;
        localMinX = transform.localEulerAngles.x + minTurnAngle;
        localMaxX = transform.localEulerAngles.x + maxTurnAngle;
        //apply negitives if applicable
    }
    void MouseAiming()
    {
        // get the mouse inputs
        rotY += Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, localMinX, localMaxX);
        rotY = Mathf.Clamp(rotY, localMinY, localMaxY);
        // rotate camera
        transform.localEulerAngles = new Vector3 (-rotX, rotY, 0);
    }
}
