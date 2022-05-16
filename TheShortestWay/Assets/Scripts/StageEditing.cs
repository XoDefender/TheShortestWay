using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof(Waypoint))]
public class StageEditing : MonoBehaviour
{
    private TextMesh textMesh;
    private Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        textMesh = GetComponentInChildren<TextMesh>();
    }

    void Update()
    {
        SnapToGrid();
        PutLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();

        transform.position = new Vector3(waypoint.GetGridPosition().x * gridSize, 0, waypoint.GetGridPosition().y * gridSize);
    }

    private void PutLabel()
    {
        string labelText = waypoint.GetGridPosition().x.ToString() + "," + waypoint.GetGridPosition().y.ToString();

        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
