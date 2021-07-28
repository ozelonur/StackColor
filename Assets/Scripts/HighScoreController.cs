using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{
    public static HighScoreController Instance = null;
    public int HighScore { get { return PlayerPrefs.GetInt("HighScore", 0); } set { PlayerPrefs.SetInt("HighScore", value); } }
    private Text bestScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        bestScoreText = ObjectManager.Instance.BestScoreText;
        bestScoreText.text = "Best Score \n" + HighScore.ToString();
    }

    public void HighScoreCheck(int score)
    {
        if (score > HighScore)
        {
            HighScore = score;
        }

        bestScoreText.text = HighScore.ToString();
    }
}
