using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombsController : MonoBehaviour
{
    private HashSet<WaypointData> bombs = new HashSet<WaypointData>();
    private Dictionary<Vector2Int, WaypointData> roadWaypoints = new Dictionary<Vector2Int, WaypointData>();
    private WaypointData[] waypoints;
    private GameObject player;
    private Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right,
        new Vector2Int(1, 1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, 1),
        new Vector2Int(-1, -1),
    };

    private const string playerName = "Player";

    private void Awake()
    {
        player = GameObject.Find(playerName);
    }

    // Start is called before the first frame update
    void Start()
    {
        int bombsAmount = 5;

        waypoints = FindObjectsOfType<WaypointData>();

        while(bombs.Count < bombsAmount)
            bombs.Add(waypoints[Random.Range(0, waypoints.Length)]);

        foreach (WaypointData waypoint in waypoints)
        {
            if (!roadWaypoints.ContainsKey(waypoint.GetGridPosition()))
                roadWaypoints.Add(waypoint.GetGridPosition(), waypoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (WaypointData bomb in bombs)
        {
            bomb.GetComponent<MeshRenderer>().material.color = Color.red;

            if(Mathf.Approximately(bomb.transform.position.x, player.transform.position.x) && Mathf.Approximately(bomb.transform.position.z, player.transform.position.z))
            {
                WaypointData exploringBomb = bomb;

                foreach (Vector2Int direction in directions)
                {
                    Vector2Int exploredBombCoordinates = exploringBomb.GetGridPosition() + direction;

                    if(roadWaypoints.ContainsKey(exploredBombCoordinates))
                    {
                        if(bombs.Contains(roadWaypoints[exploredBombCoordinates]))
                            roadWaypoints[exploredBombCoordinates].SetColor(Color.red);
                        else
                            roadWaypoints[exploredBombCoordinates].SetColor(Color.green);
                    }
                }
            }
        }
    }
}
