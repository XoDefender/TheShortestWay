using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointData : MonoBehaviour
{
    private WaypointData startWaypoint;
    private WaypointData endWaypoint;
    private GameObject player;

    private const string playerName = "Player";

    private const int gridSize = 10;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find(playerName);
    }

    public int GetGridSize()
    {
        return gridSize;
    }

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

    public void PickStartWaypoint(Color startColor, WaypointData[] waypoints)
    {
        if(startWaypoint == null)
        {
            foreach (WaypointData waypoint in waypoints)
            {
                if (Mathf.Approximately(waypoint.transform.position.x, player.transform.position.x) && Mathf.Approximately(waypoint.transform.position.z, player.transform.position.z))
                {
                    waypoint.GetComponent<MeshRenderer>().material.color = startColor;
                    startWaypoint = waypoint;

                    break;
                }
            }
        }
    }

    public void PickEndWaypoint(Color endColor)
    {
        if (endWaypoint == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = endColor;
                    endWaypoint = hit.collider.gameObject.GetComponent<WaypointData>();
                }
            }
        }
    }

    public WaypointData GetStartWaypoint()
    {
        return startWaypoint;
    }

    public WaypointData GetEndWaypoint()
    {
        return endWaypoint;
    }
}
