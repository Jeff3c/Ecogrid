using UnityEngine;
using Unity.Netcode;

public class RoundManager : NetworkBehaviour
{
    public GameObject[] objectsToRespawn; // Assign all spawnable objects
    public Transform[] spawnPoints; // Assign their respective spawn points
    public GameObject confirmButton;

    public void ConfirmTurn()
    {
        if (!IsServer) return;

        // End of the turn
        foreach (var obj in objectsToRespawn)
        {
            ResetObject(obj);
        }
        
        // Check if the game is over (all cells occupied logic can be added here)
        // Otherwise, move to the next turn
    }

    private void ResetObject(GameObject obj)
    {
        NetworkObject networkObj = obj.GetComponent<NetworkObject>();
        if (networkObj)
        {
            obj.transform.position = GetSpawnPoint(obj).position;
            obj.transform.rotation = GetSpawnPoint(obj).rotation;
        }
    }

    private Transform GetSpawnPoint(GameObject obj)
    {
        int index = System.Array.IndexOf(objectsToRespawn, obj);
        return spawnPoints[index];
    }
}
