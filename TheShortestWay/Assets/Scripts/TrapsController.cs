using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsController : MonoBehaviour
{
    [SerializeField] GameObject trapPrefab;

    private HashSet<WaypointData> traps = new HashSet<WaypointData>();
    private WaypointData[] waypoints;
    private GameObject player;
    private StartEndWaypoints startEndWaypoints;
    private ChestController chestController;
    private GameObject enemy;

    private const string playerName = "Player";
    private const string enemyName = "Enemy";
    private const string trapName = "Pf_Trap_Needle(Clone)";

    private void Awake()
    {
        player = GameObject.Find(playerName);
        startEndWaypoints = GetComponent<StartEndWaypoints>();
        chestController = FindObjectOfType<ChestController>();
        enemy = GameObject.Find(enemyName);  
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateTraps();
        SpawnTraps();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
            OnTrapTrigger();
    }

    private void OnTrapTrigger()
    {
        foreach (WaypointData trap in traps)
        {
            if (Mathf.Approximately(trap.transform.position.x, player.transform.position.x) && Mathf.Approximately(trap.transform.position.z, player.transform.position.z))
            {
                trap.transform.Find(trapName).GetComponent<Animation>().Play();
                Destroy(player);
            }
            
            if(Mathf.Approximately(trap.transform.position.x, enemy.transform.position.x) && Mathf.Approximately(trap.transform.position.z, enemy.transform.position.z))
            {
                trap.transform.Find(trapName).GetComponent<Animation>().Play();
                Destroy(enemy);
            }
        }
    }

    private void SpawnTraps()
    {
        foreach(WaypointData trap in traps)
        {
            var newTrap = Instantiate(trapPrefab, new Vector3(trap.transform.position.x, trap.transform.position.y + 5, trap.transform.position.z), Quaternion.identity);
            newTrap.transform.parent = trap.transform;

            trap.TextMesh.text = "";
        }
    }

    private void CreateTraps()
    {
        int trapsAmount = 10;

        waypoints = FindObjectsOfType<WaypointData>();

        while (traps.Count < trapsAmount)
        {
            WaypointData randomTrap = waypoints[Random.Range(0, waypoints.Length)];

            if (!Mathf.Approximately(randomTrap.transform.position.x, player.transform.position.x) 
                && !Mathf.Approximately(randomTrap.transform.position.z, player.transform.position.z) 
                && randomTrap.transform.position != startEndWaypoints.EndWaypoint.transform.position 
                && !Mathf.Approximately(randomTrap.transform.position.x, chestController.transform.position.x) 
                && !Mathf.Approximately(randomTrap.transform.position.z, chestController.transform.position.z)
                && !Mathf.Approximately(randomTrap.transform.position.x, enemy.transform.position.x)
                && !Mathf.Approximately(randomTrap.transform.position.z, enemy.transform.position.z))
                traps.Add(randomTrap);
        }
    }
}
