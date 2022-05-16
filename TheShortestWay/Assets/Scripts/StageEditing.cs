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
        transform.position = new Vector3(waypoint.GetGridPosition().x, 0, waypoint.GetGridPosition().y);
    }

    private void PutLabel()
    {
        int gridSize = waypoint.GetGridSize();
        string labelText = (transform.position.x / gridSize).ToString() + "," + (transform.position.z / gridSize).ToString();

        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
