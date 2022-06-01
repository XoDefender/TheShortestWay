using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator animator;
    private GameObject player;
    private ScoreManager scoreManager;

    private const string playerName = "Player";

    private int requiredAmountOfPoints = 150;

    private bool hasOpened = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find(playerName);
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 chestPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.z);

        if (scoreManager.Score >= requiredAmountOfPoints)
        {
            if (chestPosition - playerPosition == new Vector2(0, 10))
            {
                animator.SetBool("Bounce", false);
                animator.SetBool("ReadyToOpen", true);

                player.transform.forward = transform.position - player.transform.position;
                hasOpened = true;
            }
        }
        else
        {
            if (chestPosition - playerPosition == new Vector2(0, 10) || chestPosition - playerPosition == new Vector2(10, 0) || chestPosition - playerPosition == new Vector2(0, -10) || chestPosition - playerPosition == new Vector2(-10, 0))
                Debug.Log("You need more points");
        }
    }

    public bool HasOpened { get { return hasOpened; } }
}
