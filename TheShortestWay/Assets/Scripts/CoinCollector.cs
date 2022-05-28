using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private WaypointData[] waypoints;
    private GameObject player;

    private const string playerName = "Player";
    private int coinsAmount = 0;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        waypoints = FindObjectsOfType<WaypointData>();
        player = GameObject.Find(playerName);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(coinsAmount);

        if (!playerMovement.IsGoing)
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
                    coinsAmount += int.Parse(waypoint.TextMesh.text);
                    waypoint.TextMesh.text = "";
                }
            }
        }
    }
}
