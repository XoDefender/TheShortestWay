using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private WaypointData[] waypoints;
    private GameObject player;
    private ScoreManager scoreManager;

    private const string playerName = "Player";

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        waypoints = FindObjectsOfType<WaypointData>();
        player = GameObject.Find(playerName);
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
       // if (!playerMovement.IsGoing)
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

                    if(scoreManager.Score > scoreManager.Highscore)
                        PlayerPrefs.SetInt("highscore", scoreManager.Score);

                    waypoint.TextMesh.text = "";
                }
            }
        }
    }
}
