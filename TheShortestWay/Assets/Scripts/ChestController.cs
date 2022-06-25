using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator animator;
    private GameObject player;
    private GameObject enemy;
    private EnemyMovement enemyMovement;
    private ScoreManager scoreManager;
    private ECoinCollector coinCollector;

    private const string playerName = "Player";
    private const string enemyName = "Enemy";

    private int requiredAmountOfPoints = 150;

    private bool hasOpened = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find(playerName);
        enemy = GameObject.Find(enemyName);
        scoreManager = FindObjectOfType<ScoreManager>();
        coinCollector = FindObjectOfType<ECoinCollector>();
        enemyMovement = FindObjectOfType<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 chestPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 enemyPosition = new Vector2(enemy.transform.position.x, enemy.transform.position.z);

        if (scoreManager.Score >= requiredAmountOfPoints || coinCollector.PickedCoins >= requiredAmountOfPoints)
        {
            if (chestPosition - playerPosition == new Vector2(0, 10) || (chestPosition - enemyPosition == new Vector2(0, 10) && !enemyMovement.IsGoing))
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
