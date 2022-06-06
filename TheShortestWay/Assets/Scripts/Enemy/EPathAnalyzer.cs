using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPathAnalyzer : MonoBehaviour
{
    private const string trapName = "Pf_Trap_Needle(Clone)";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool HasTraps(List<EWaypointData> path)
    {
        foreach (EWaypointData waypoint in path)
        {
            if (waypoint.transform.Find(trapName))
            {
                return true;
            }
        }

        return false;
    }
}
