using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointData : MonoBehaviour
{
    private WaypointData previousWaypoint;
    private Pathfinder pathfinder;
    private StartEndWaypoints startEndWaypoints;
    private TextMesh textMesh;
    private GameObject player;

    private const int gridSize = 10;
    private const string playerName = "Player";

    private void Awake()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        startEndWaypoints = FindObjectOfType<StartEndWaypoints>();
        textMesh = GetComponentInChildren<TextMesh>();
        player = GameObject.Find(playerName);
    }

    private void Start()
    {
        if (!Mathf.Approximately(transform.position.x, player.transform.position.x) || !Mathf.Approximately(transform.position.z, player.transform.position.z))
            textMesh.text = SetCoins().ToString();
    }

    private void Update()
    {
        startEndWaypoints.EndWaypoint.GetComponentInChildren<TextMesh>().text = "";
        pathfinder.AreEqual = AreEqual(startEndWaypoints.TargetWaypoint);
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

    public bool AreEqual(WaypointData targetWaypoint)
    {
        if (targetWaypoint == previousWaypoint)
            return true;
        else
        {
            previousWaypoint = targetWaypoint;
            return false;
        }
    }

    private int SetCoins()
    {
        return Random.Range(1, 20);
    }

    public TextMesh TextMesh { get { return textMesh; } set { textMesh = value; } }
}
