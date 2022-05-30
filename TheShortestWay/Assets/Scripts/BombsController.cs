using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombsController : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private List<WaypointData> explodedBombs = new List<WaypointData>();
    private HashSet<WaypointData> bombs = new HashSet<WaypointData>();
    private WaypointData[] waypoints;
    private GameObject player;
    private StartEndWaypoints startEndWaypoints;

    private const string playerName = "Player";

    private int HitPoints = 50;

    private void Awake()
    {
        player = GameObject.Find(playerName);
        playerHealth = player.GetComponent<PlayerHealth>();
        startEndWaypoints = GetComponent<StartEndWaypoints>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int bombsAmount = 20;

        waypoints = FindObjectsOfType<WaypointData>();

        while(bombs.Count < bombsAmount)
        {
            WaypointData randomBomb = waypoints[Random.Range(0, waypoints.Length)];

            if (!Mathf.Approximately(randomBomb.transform.position.x, player.transform.position.x) && !Mathf.Approximately(randomBomb.transform.position.z, player.transform.position.z) && randomBomb.transform.position != startEndWaypoints.EndWaypoint.transform.position)
                bombs.Add(randomBomb);
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
            bomb.GetComponent<MeshRenderer>().material.color = Color.red;

            if (Mathf.Approximately(bomb.transform.position.x, player.transform.position.x) && Mathf.Approximately(bomb.transform.position.z, player.transform.position.z))
            {
                if(!explodedBombs.Contains(bomb))
                {
                    explodedBombs.Add(bomb);
                    playerHealth.HealthPoints -= HitPoints;
                }

                bomb.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
}
