using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EWaypointData : MonoBehaviour
{
    private TextMesh textMesh;
    private GameObject enemy;

    private const int gridSize = 10;
    private const string enemyName = "Enemy";

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        enemy = GameObject.Find(enemyName);
    }

    private void Start()
    {
        if (!Mathf.Approximately(transform.position.x, enemy.transform.position.x)
            || !Mathf.Approximately(transform.position.z, enemy.transform.position.z))
            textMesh.text = SetCoins().ToString();
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

    private int SetCoins()
    {
        return Random.Range(1, 20);
    }

    public TextMesh TextMesh { get { return textMesh; } set { textMesh = value; } }
}
