using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof(WaypointData))]
public class WaypointEditing : MonoBehaviour
{
    private WaypointData waypointData;

    private void Awake()
    {
        waypointData = GetComponent<WaypointData>();
    }

    void Update()
    {
        SnapToGrid();
        PutLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypointData.GridSize;
        transform.position = new Vector3(waypointData.GetGridPosition().x * gridSize, 0, waypointData.GetGridPosition().y * gridSize);
    }

    private void PutLabel()
    {
        string labelText = waypointData.GetGridPosition().x.ToString() + "," + waypointData.GetGridPosition().y.ToString();
        gameObject.name = labelText;
    }
}
