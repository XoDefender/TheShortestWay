using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FenceGrid : MonoBehaviour
{
    private const int gridSize = 17;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 4, Mathf.RoundToInt(transform.position.z / gridSize) * gridSize);
    }
}
