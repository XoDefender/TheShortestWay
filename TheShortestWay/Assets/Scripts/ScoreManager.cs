using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;

    private int score = 0;
    private int highscore = 0;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        highscore = PlayerPrefs.GetInt("highscore", 0);
        
        highscoreText.text = "Highscore: " + highscore.ToString();
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public int Score { get { return score; } set { score = value; } }
    public int Highscore { get { return highscore; } }
}
