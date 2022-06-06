using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int healthPoints = 200;

    private const string enemyName = "Enemy";

    // Update is called once per frame
    void Update()
    {
        if (healthPoints <= 0)
            Destroy(GameObject.Find(enemyName));
    }

    public int HealthPoints { get { return healthPoints; } set { healthPoints = value; } }
}
