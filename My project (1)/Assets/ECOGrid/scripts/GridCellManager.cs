using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GridCellSocket : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor _socket;

    void Awake()
    {
        _socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
        _socket.selectEntered.AddListener(OnObjectSnapped);
        _socket.selectExited.AddListener(OnObjectUnsnapped);
    }

    private void OnObjectSnapped(SelectEnterEventArgs args)
    {
        // The object that was snapped
        GameObject placedObj = args.interactableObject.transform.gameObject;

        // Log the object's tag and the name of this cell
        Debug.Log($"{placedObj.tag} snapped on {gameObject.name}.");

        // Notify your GameManager or other systems if necessary
        // Example:
        // FindObjectOfType<GameManager>().OnObjectSnapped(placedObj.tag, gameObject.name);
    }

    private void OnObjectUnsnapped(SelectExitEventArgs args)
    {
        // The object that was just removed
        GameObject removedObj = args.interactableObject.transform.gameObject;

        // Log the object's tag and the name of this cell
        Debug.Log($"{removedObj.tag} removed from {gameObject.name}.");

        // Optionally notify other systems if necessary
    }
}
