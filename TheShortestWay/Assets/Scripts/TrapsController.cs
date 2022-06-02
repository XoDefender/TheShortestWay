using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsController : MonoBehaviour
{
    [SerializeField] GameObject trapPrefab;

    private PlayerHealth playerHealth;
    private List<WaypointData> triggeredTraps = new List<WaypointData>();
    private HashSet<WaypointData> traps = new HashSet<WaypointData>();
    private WaypointData[] waypoints;
    private GameObject player;
    private StartEndWaypoints startEndWaypoints;
    private ChestController chestController;

    private const string playerName = "Player";
    private const string trapName = "Pf_Trap_Needle(Clone)";

    private int HitPoints = 50;

    private void Awake()
    {
        player = GameObject.Find(playerName);
        playerHealth = player.GetComponent<PlayerHealth>();
        startEndWaypoints = GetComponent<StartEndWaypoints>();
        chestController = FindObjectOfType<ChestController>();
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
                if(!triggeredTraps.Contains(trap))
                {
                    triggeredTraps.Add(trap);
                    trap.transform.Find(trapName).GetComponent<Animation>().Play();

                    playerHealth.HealthPoints -= HitPoints;
                }
            }
        }
    }

    private void SpawnTraps()
    {
        foreach(WaypointData trap in traps)
        {
            var newTrap = Instantiate(trapPrefab, new Vector3(trap.transform.position.x, trap.transform.position.y + 5, trap.transform.position.z), Quaternion.identity);
            newTrap.transform.parent = trap.transform;
        }
    }

    private void CreateTraps()
    {
        int trapsAmount = 20;

        waypoints = FindObjectsOfType<WaypointData>();

        while (traps.Count < trapsAmount)
        {
            WaypointData randomTrap = waypoints[Random.Range(0, waypoints.Length)];

            if (!Mathf.Approximately(randomTrap.transform.position.x, player.transform.position.x) 
                && !Mathf.Approximately(randomTrap.transform.position.z, player.transform.position.z) 
                && randomTrap.transform.position != startEndWaypoints.EndWaypoint.transform.position 
                && !Mathf.Approximately(randomTrap.transform.position.x, chestController.transform.position.x) 
                && !Mathf.Approximately(randomTrap.transform.position.z, chestController.transform.position.z))
                traps.Add(randomTrap);
        }
    }
}
