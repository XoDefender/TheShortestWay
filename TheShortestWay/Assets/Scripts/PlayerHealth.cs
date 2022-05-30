using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int healthPoints = 100;

    private const string playerName = "Player";

    // Update is called once per frame
    void Update()
    {
        if (healthPoints <= 0)
            Destroy(GameObject.Find(playerName));
    }

    public int HealthPoints { get { return healthPoints; } set { healthPoints = value; } }
}
