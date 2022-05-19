using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointData : MonoBehaviour
{
    private const int gridSize = 10;

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
}
