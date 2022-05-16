using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private List<Waypoint> stages;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Log());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Log()
    {
        foreach (Waypoint stage in stages)
        {
            transform.position = new Vector3(stage.transform.position.x, transform.position.y, stage.transform.position.z);

            Debug.Log(stage);

            yield return new WaitForSeconds(1);
        }
    }
}
