using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombsController : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private List<WaypointData> explodedBombs = new List<WaypointData>();
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

    private int HitPoints = 50;

    private void Awake()
    {
        player = GameObject.Find(playerName);
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int bombsAmount = 10;

        waypoints = FindObjectsOfType<WaypointData>();

        while(bombs.Count < bombsAmount)
        {
            WaypointData randomBomb = waypoints[Random.Range(0, waypoints.Length)];

            if (!Mathf.Approximately(randomBomb.transform.position.x, player.transform.position.x) && !Mathf.Approximately(randomBomb.transform.position.z, player.transform.position.z))
                bombs.Add(randomBomb);
        }

        foreach (WaypointData waypoint in waypoints)
        {
            if (!roadWaypoints.ContainsKey(waypoint.GetGridPosition()))
                roadWaypoints.Add(waypoint.GetGridPosition(), waypoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
            Bombs();
    }

    private void Bombs()
    {
        foreach (WaypointData bomb in bombs)
        {
            //bomb.GetComponent<MeshRenderer>().material.color = Color.red;

            if (Mathf.Approximately(bomb.transform.position.x, player.transform.position.x) && Mathf.Approximately(bomb.transform.position.z, player.transform.position.z))
            {
                WaypointData exploringBomb = bomb;

                if(!explodedBombs.Contains(bomb))
                {
                    explodedBombs.Add(bomb);
                    playerHealth.HealthPoints -= HitPoints;
                }

                bomb.GetComponent<MeshRenderer>().material.color = Color.red;

                foreach (Vector2Int direction in directions)
                {
                    Vector2Int exploredBombCoordinates = exploringBomb.GetGridPosition() + direction;

                    if (roadWaypoints.ContainsKey(exploredBombCoordinates))
                    {
                        if (bombs.Contains(roadWaypoints[exploredBombCoordinates]))
                            roadWaypoints[exploredBombCoordinates].SetColor(Color.red);
                        else
                            roadWaypoints[exploredBombCoordinates].SetColor(Color.green);
                    }
                }
            }
        }
    }
}
