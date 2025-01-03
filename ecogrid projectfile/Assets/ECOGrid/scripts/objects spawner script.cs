using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Points")]
    public Transform solarPanelSpawnPoint;
    public Transform recyclingBinSpawnPoint;
    public Transform turbineSpawnPoint;

    [Header("Object Prefabs")]
    public GameObject solarPanelPrefab;
    public GameObject recyclingBinPrefab;
    public GameObject turbinePrefab;

    private GameObject currentSolarPanel;
    private GameObject currentRecyclingBin;
    private GameObject currentTurbine;

    public void DispenseObject(string objectType)
    {
        switch (objectType)
        {
            case "Solar_Panel":
                if (currentSolarPanel != null)
                {
                    Destroy(currentSolarPanel);
                }
                currentSolarPanel = Instantiate(solarPanelPrefab, solarPanelSpawnPoint.position, Quaternion.identity);
                break;

            case "Recycling_Bin":
                if (currentRecyclingBin != null)
                {
                    Destroy(currentRecyclingBin);
                }
                currentRecyclingBin = Instantiate(recyclingBinPrefab, recyclingBinSpawnPoint.position, Quaternion.identity);
                break;

            case "Turbine":
                if (currentTurbine != null)
                {
                    Destroy(currentTurbine);
                }
                currentTurbine = Instantiate(turbinePrefab, turbineSpawnPoint.position, Quaternion.identity);
                break;

            default:
                Debug.LogError("Invalid object type specified.");
                break;
        }
    }

    public void OnObjectPlaced(string objectType)
    {
        // Reset the current object reference when placed
        switch (objectType)
        {
            case "Solar_Panel":
                currentSolarPanel = null;
                break;
            case "Recycling_Bin":
                currentRecyclingBin = null;
                break;
            case "Turbine":
                currentTurbine = null;
                break;
        }
    }
}
