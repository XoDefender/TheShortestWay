using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private WaypointData[] waypoints;
    private GameObject player;
    private ScoreManager scoreManager;

    private const string playerName = "Player";

    private void Awake()
    {
        waypoints = FindObjectsOfType<WaypointData>();
        player = GameObject.Find(playerName);
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
         PickUpCoin(waypoints);
    }

    private void PickUpCoin(WaypointData[] waypoints)
    {
        foreach(WaypointData waypoint in waypoints)
        {
            if(Mathf.Approximately(waypoint.transform.position.x, player.transform.position.x) && Mathf.Approximately(waypoint.transform.position.z, player.transform.position.z))
            {
                if(waypoint.TextMesh.text != "")
                {
                    scoreManager.Score += int.Parse(waypoint.TextMesh.text);
                    waypoint.TextMesh.text = "";
                }
            }
        }
    }
}
