using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void OnRelease()
    {
        // Check if the object is inside a valid snap zone
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
        bool isSnapped = false;

        foreach (var col in colliders)
        {
            if (col.CompareTag("GridCell"))
            {
                isSnapped = true;
                break;
            }
        }

        // If not snapped, reset position
        if (!isSnapped)
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }
    }
}




/*using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Netcode;

public class ObjectPlacement : MonoBehaviour
{
    private bool isSnapped = false; // Tracks whether the object is snapped
    private Vector3 initialPosition; // Stores the original position of the object
    private Quaternion initialRotation; // Stores the original rotation of the object
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable; // Reference to the XRGrabInteractable component
    private Transform snappedCell; // Tracks the grid cell where the object is snapped

    private void Start()
    {
        // Store the initial position and rotation of the object
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Get the XRGrabInteractable component and attach event listeners
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab); // Triggered when object is grabbed
            grabInteractable.selectExited.AddListener(OnRelease); // Triggered when object is released
        }
        else
        {
            Debug.LogError("XRGrabInteractable component is missing!");
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Object grabbed.");
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("Object released.");

        if (isSnapped)
        {
            // Spawn a new object at the original position only after snapping
            Debug.Log("Object snapped successfully. Spawning a new object at the original location.");
            SpawnNewCopyAtInitialPosition();
        }
        else
        {
            // Destroy the object if not snapped
            Debug.Log("Object not snapped. Destroying the object.");
            SpawnNewCopyAtInitialPosition();
            Destroy(gameObject);
        }
    }

    private void SpawnNewCopyAtInitialPosition()
    {
        // Create a new object at the initial position and rotation
        GameObject newObject = Instantiate(gameObject, initialPosition, initialRotation);

        // Ensure the new object's components are fully active
        ActivateObjectComponents(newObject);

        Debug.Log("New object spawned at the original position: " + initialPosition);
    }

    private void ActivateObjectComponents(GameObject obj)
    {
        // Enable all MonoBehaviour components
        foreach (var component in obj.GetComponents<MonoBehaviour>())
        {
            component.enabled = true;
        }

        // Ensure all critical components are active
        foreach (var component in obj.GetComponents<Component>())
        {
            if (component is Renderer renderer)
                renderer.enabled = true; // Enable renderers
            else if (component is Collider collider)
                collider.enabled = true; // Enable colliders
            else if (component is Rigidbody rigidbody)
                rigidbody.isKinematic = false; // Ensure Rigidbody interacts properly
        }

        // Re-enable the XRGrabInteractable
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable xrGrab = obj.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (xrGrab != null)
        {
            xrGrab.enabled = true; // Make sure it is interactable
            xrGrab.interactionLayers = LayerMask.GetMask("Default"); // Enable interaction
        }

        obj.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle snapping logic when entering a grid cell
        if (other.CompareTag("GridCell") && !isSnapped)
        {
            Debug.Log("Object entered a grid cell.");

            // Align with the grid cell position and mark as snapped
            snappedCell = other.transform;
            AlignWithGridCell(other.transform.position);
        }
    }

    private void AlignWithGridCell(Vector3 targetPosition)
{
    if (snappedCell == null)
    {
        Debug.LogError("snappedCell is null. Cannot assign parent.");
        return;
    }

    Debug.Log($"Attempting to parent {gameObject.name} to {snappedCell.name}");

    // Snap the object to the target position
    transform.position = targetPosition;

    // Validate NetworkObject presence
    var networkObject = GetComponent<NetworkObject>();
    var parentNetworkObject = snappedCell.GetComponent<NetworkObject>();

    if (networkObject == null || parentNetworkObject == null)
    {
        Debug.LogError("One or both objects are missing a NetworkObject component.");
        return;
    }

    // Validate network spawn status
    if (!networkObject.IsSpawned || !parentNetworkObject.IsSpawned)
    {
        Debug.LogError("One or both objects are not network-spawned.");
        return;
    }

    // Validate ownership
    if (!networkObject.IsOwner)
    {
        Debug.LogError($"{gameObject.name} is not owned by this client. Parenting failed.");
        return;
    }

    // Enable AutoObjectParentSync if required
    networkObject.AutoObjectParentSync = true;

    // Try to set the parent using NetworkObject
    if (networkObject.TrySetParent(snappedCell))
    {
        Debug.Log("Parenting succeeded via TrySetParent.");
    }
    else
    {
        Debug.LogError("Parenting failed via TrySetParent.");
    }

    // Confirm snapping
    isSnapped = true;
    Debug.Log("Object snapped to grid cell.");
}

    private void OnTriggerExit(Collider other)
    {
        // Handle unsnapping logic when leaving a grid cell
        if (other.CompareTag("GridCell") && snappedCell == other.transform)
        {
            Debug.Log("Object unsnapped from grid cell.");
            snappedCell = null;
            isSnapped = false;

            // Optionally, reset the object's parent when it unsnaps
            transform.SetParent(null);

            // Re-enable interaction for grabbing
            if (grabInteractable != null)
            {
                grabInteractable.interactionLayers = LayerMask.GetMask("Default"); // Enable interaction again
            }
        }
    }
}
*/