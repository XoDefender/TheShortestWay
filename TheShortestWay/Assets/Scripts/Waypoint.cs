using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint exploredFrom;

    private GameObject player;

    private const string playerName = "Player";

    private const int gridSize = 10;

    private bool hasPickedStartColor = false;
    private bool hasPickedEndColor = false;
    private bool hasPickedEndWaypoint = false;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find(playerName);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void ColorStartAndEnd(Color startColor, Color endColor, Waypoint[] waypoints)
    {
        if (!hasPickedStartColor)
        {
            foreach(Waypoint waypoint in waypoints)
            {
                if (Mathf.Approximately(waypoint.transform.position.x, player.transform.position.x) && Mathf.Approximately(waypoint.transform.position.z, player.transform.position.z))
                {
                    waypoint.GetComponent<MeshRenderer>().material.color = startColor;
                    hasPickedStartColor = true;

                    break;
                }
            }
        }

        if (!hasPickedEndColor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = endColor;
                    hasPickedEndColor = true;
                }
            }
        }
    }

    public bool GetHasPickedEndColorValue()
    {
        return hasPickedEndColor;
    }

    public void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }

    public Waypoint GetStartWaypoint(Waypoint[] waypoints)
    {
        foreach (Waypoint waypoint in waypoints)
        {
            if (Mathf.Approximately(waypoint.transform.position.x, player.transform.position.x) && Mathf.Approximately(waypoint.transform.position.z, player.transform.position.z))
                return waypoint;
        }

        return null;
    }

   /* public Waypoint GetEndWaypoint()
    {
        if (!hasPickedEndWaypoint)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    hasPickedEndWaypoint = true;

                    return hit.collider.gameObject.GetComponent<Waypoint>();
                }
            }
        }

        return null;
    }*/
}
