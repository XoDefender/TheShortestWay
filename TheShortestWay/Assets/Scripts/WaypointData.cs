using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointData : MonoBehaviour
{
    private WaypointData previousWaypoint;
    private Pathfinder pathfinder;
    private StartEndWaypoints startEndWaypoints;
    private const int gridSize = 10;

    private void Awake()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        startEndWaypoints = FindObjectOfType<StartEndWaypoints>();
    }

    private void Update()
    {
        pathfinder.AreEqual = AreEqual(startEndWaypoints.EndWaypoint);
    }
    public int GridSize { get { return gridSize; } }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(
                Mathf.RoundToInt(transform.position.x / gridSize),
                Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }

    public void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }

    public bool AreEqual(WaypointData endWaypoint)
    {
        if (endWaypoint == previousWaypoint)
            return true;
        else
        {
            previousWaypoint = endWaypoint;
            return false;
        }
    }
}
