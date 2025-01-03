using UnityEngine;
public enum BuildingType
{
    None,
    Turbine,
    SolarPanel,
    RecyclingStation
    // Add more if needed
}
public class GridCell : MonoBehaviour
{
    // Stores the current building type in this cell
    public BuildingType CurrentBuildingType { get; private set; } = BuildingType.None;

    // This function can be called when an object is placed in this cell
    public void SetBuildingType(BuildingType newType)
    {
        CurrentBuildingType = newType;
        // You can add extra logic here, such as playing a sound or
        // updating visuals, etc.
    }

    // This function can be called if the object is removed
    public void ClearBuildingType()
    {
        CurrentBuildingType = BuildingType.None;
    }
}