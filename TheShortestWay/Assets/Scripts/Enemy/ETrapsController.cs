using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETrapsController : MonoBehaviour
{
    [SerializeField] GameObject trapPrefab;

    private EnemyHealth enemyHealth;
    private List<EWaypointData> triggeredTraps = new List<EWaypointData>();
    private HashSet<EWaypointData> traps = new HashSet<EWaypointData>();
    private EWaypointData[] waypoints;
    private GameObject enemy;
    private EStartTargetWaypoints startTargetWaypoints;

    private const string enemyName = "Enemy";
    private const string trapName = "Pf_Trap_Needle(Clone)";

    private int HitPoints = 50;

    private void Awake()
    {
        enemy = GameObject.Find(enemyName);
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        startTargetWaypoints = GetComponent<EStartTargetWaypoints>();
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
        if (enemy)
            OnTrapTrigger();
    }

    private void OnTrapTrigger()
    {
        foreach (EWaypointData trap in traps)
        {
            if (Mathf.Approximately(trap.transform.position.x, enemy.transform.position.x) && Mathf.Approximately(trap.transform.position.z, enemy.transform.position.z))
            {
                if (!triggeredTraps.Contains(trap))
                {
                    triggeredTraps.Add(trap);
                    trap.transform.Find(trapName).GetComponent<Animation>().Play();

                    enemyHealth.HealthPoints -= HitPoints;
                }
            }
        }
    }

    private void SpawnTraps()
    {
        foreach (EWaypointData trap in traps)
        {
            var newTrap = Instantiate(trapPrefab, new Vector3(trap.transform.position.x, trap.transform.position.y + 5, trap.transform.position.z), Quaternion.identity);
            newTrap.transform.parent = trap.transform;
        }
    }

    private void CreateTraps()
    {
        int trapsAmount = 5;

        waypoints = FindObjectsOfType<EWaypointData>();

        while (traps.Count < trapsAmount)
        {
            EWaypointData randomTrap = waypoints[Random.Range(0, waypoints.Length)];

            if (!Mathf.Approximately(randomTrap.transform.position.x, enemy.transform.position.x)
                && !Mathf.Approximately(randomTrap.transform.position.z, enemy.transform.position.z)
                && randomTrap.transform.position != startTargetWaypoints.EndWaypoint.transform.position)
                traps.Add(randomTrap);
        }
    }
}
