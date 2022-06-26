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

    private int requiredAmountOfPoints = 200;

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

        if ((scoreManager.Score >= requiredAmountOfPoints || coinCollector.PickedCoins >= requiredAmountOfPoints) && !hasOpened)
        {
            if (chestPosition - playerPosition == new Vector2(0, 10) || (chestPosition - enemyPosition == new Vector2(0, 10) && !enemyMovement.IsGoing))
            {
                animator.SetBool("Bounce", false);
                animator.SetBool("ReadyToOpen", true);

                player.transform.forward = transform.position - player.transform.position;
                enemy.transform.forward = transform.position - enemy.transform.position;

                hasOpened = true;

                if (chestPosition - playerPosition == new Vector2(0, 10))
                    enemy.GetComponent<ECoinCollector>().PickedCoins -= requiredAmountOfPoints;
                else
                    scoreManager.Score -= requiredAmountOfPoints;
            }
        }
        else
        {
            if (chestPosition - playerPosition == new Vector2(0, 10) || chestPosition - playerPosition == new Vector2(10, 0) || chestPosition - playerPosition == new Vector2(0, -10) || chestPosition - playerPosition == new Vector2(-10, 0))
                Debug.Log("You need more points");
        }
    }

    public bool HasOpened { get { return hasOpened; } }
    public int RequiredAmountOfPoints { get { return requiredAmountOfPoints; } }
}
