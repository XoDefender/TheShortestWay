using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private StageEditing[] stages;

    // Start is called before the first frame update
    void Start()
    {
        stages = FindObjectsOfType<StageEditing>();
        Array.Reverse(stages);

        StartCoroutine(Log());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Log()
    {
        foreach (StageEditing stage in stages)
        {
            transform.position = new Vector3(stage.transform.position.x, transform.position.y, stage.transform.position.z);

            yield return new WaitForSeconds(1);
        }
    }
}
