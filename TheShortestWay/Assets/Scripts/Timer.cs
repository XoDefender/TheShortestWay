using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float timeStart = 5;

    private const string playerName = "Player";
    private const string enemyName = "Enemy";

    void Start()
    {
        timerText.text = "Time: " + timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Round(timeStart) != 0)
        {
            timeStart -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timeStart).ToString();
        }
        else
        {
            Destroy(GameObject.Find(playerName));
            Destroy(GameObject.Find(enemyName));
        }
    }
}
