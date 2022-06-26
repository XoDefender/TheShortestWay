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
    private GameObject enemy;
    private ChestController chestController;

    private const int gridSize = 10;
    private const string playerName = "Player";
    private const string enemyName = "Enemy";

    private void Awake()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        startEndWaypoints = FindObjectOfType<StartEndWaypoints>();
        textMesh = GetComponentInChildren<TextMesh>();
        player = GameObject.Find(playerName);
        enemy = GameObject.Find(enemyName);
        chestController = FindObjectOfType<ChestController>();
    }

    private void Start()
    {
        if ((!Mathf.Approximately(transform.position.x, player.transform.position.x) || !Mathf.Approximately(transform.position.z, player.transform.position.z))
            && 
            (!Mathf.Approximately(transform.position.x, enemy.transform.position.x) || !Mathf.Approximately(transform.position.z, enemy.transform.position.z))
            &&
            (!Mathf.Approximately(transform.position.x, chestController.transform.position.x) || !Mathf.Approximately(transform.position.z, chestController.transform.position.z)))
            textMesh.text = SetCoins().ToString();
    }

    private void Update()
    {
        DeleteText();

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
        return Random.Range(1, 50);
    }

    private void DeleteText()
    {
        Vector2 waypointPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 chestPosition = new Vector2(chestController.transform.position.x, chestController.transform.position.z);

        if (waypointPosition == chestPosition)
            textMesh.text = "";

        startEndWaypoints.EndWaypoint.GetComponentInChildren<TextMesh>().text = "";
    }

    public TextMesh TextMesh { get { return textMesh; } set { textMesh = value; } }
}
