using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Pathfinder pathController;

    private bool isGoing = false;

    private void Awake()
    {
        pathController = GameObject.Find("Road").GetComponent<Pathfinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Waypoint>().GetHasPickedEndColorValue() && !isGoing && pathController.readyToGetPath)
        {
            StartCoroutine(StartMovement());
            isGoing = true;
        }
    }

    IEnumerator StartMovement()
    {
        foreach (Waypoint waypoint in pathController.GetPath())
        {
            transform.position = new Vector3(waypoint.transform.position.x, transform.position.y, waypoint.transform.position.z);

            yield return new WaitForSeconds(1);
        }
    }
}
