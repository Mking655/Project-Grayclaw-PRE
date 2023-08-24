using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(FPSCamera))]
public class LaptopPlacer : MonoBehaviour
{
    //Code inspired by suggestions from Chat GPT 3.5

    public GameObject holoPrefab;  // Reference to the hologram prefab
    public GameObject tufftop;    // Reference to the real laptop GameObject
    public LayerMask raycastLayer;   // Layer mask for the raycast

    private GameObject hologramInstance;

    private void Update()
    {
        if (GamemodeManager.hasTufftop)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Destroy(hologramInstance);
                hologramInstance = null;
                createHologram();
            }
            if (Input.GetKey(KeyCode.Q) && hologramInstance != null)
            {
                moveHologram();
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                if (hologramInstance != null)
                {
                    // If the hologram is active, move the real laptop and clean up
                    placeTufftop();
                }
            }
        }
    }

    private void createHologram()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, raycastLayer))
        {
            //Only one FPS camerea per scene
            hologramInstance = Instantiate(holoPrefab, hit.point, Quaternion.Euler(0, gameObject.GetComponent<FPSCamera>().bodyTransform.rotation.eulerAngles.y, 0));
            hologramInstance.SetActive(true);
        }
    }
    private void moveHologram()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, raycastLayer))
        {
            hologramInstance.transform.position = hit.point;
            hologramInstance.transform.eulerAngles = new Vector3(0, gameObject.GetComponent<FPSCamera>().bodyTransform.rotation.eulerAngles.y, 0);
        }
    }
    private void placeTufftop()
    {
        tufftop.transform.position = hologramInstance.transform.position;
        tufftop.transform.rotation = hologramInstance.transform.rotation;
        tufftop.SetActive(true);
        GamemodeManager.hasTufftop = false;
        Debug.Log("player has tuffTop?" + GamemodeManager.hasTufftop);
        Destroy(hologramInstance);
        hologramInstance = null;
    }
}

