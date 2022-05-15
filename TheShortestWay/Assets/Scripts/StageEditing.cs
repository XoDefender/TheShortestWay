using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StageEditing : MonoBehaviour
{
    [SerializeField] private int snapOffset = 10;

    private TextMesh textMesh;

    // Update is called once per frame
    void Update()
    {
        float snapedPositionX = Mathf.RoundToInt(transform.position.x / snapOffset) * snapOffset;
        float snapedPositionZ = Mathf.RoundToInt(transform.position.z / snapOffset) * snapOffset;

        transform.position = new Vector3(snapedPositionX, 0, snapedPositionZ);

        string labelText = (transform.position.x / snapOffset).ToString() + "," + (transform.position.z / snapOffset).ToString();

        textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
