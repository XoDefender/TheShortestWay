using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private List<Waypoint> stages;

    private bool isGoing = false;

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Waypoint>().GetHasPickedEndColorValue() && !isGoing)
        {
            StartCoroutine(StartMovement());
            isGoing = true;
        }
    }

    IEnumerator StartMovement()
    {
        foreach (Waypoint stage in stages)
        {
            transform.position = new Vector3(stage.transform.position.x, transform.position.y, stage.transform.position.z);

            yield return new WaitForSeconds(1);
        }
    }
}
