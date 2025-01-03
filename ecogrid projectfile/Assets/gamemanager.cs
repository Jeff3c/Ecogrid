using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Spawner Settings")]
    public ObjectSpawner spawner;

    void Start()
    {
        // Spawn all objects at the start of the game
        spawner.DispenseObject("Solar_Panel");
        spawner.DispenseObject("Recycling_Bin");
        spawner.DispenseObject("Turbine");
    }

    public void OnObjectSnapped(string objectType)
    {
        Debug.Log($"{objectType} snapped to a cell!");

        // Notify the spawner that the object has been placed
        spawner.OnObjectPlaced(objectType);

        // Respawn the same object type
        spawner.DispenseObject(objectType);
    }
}
