using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{
    public static HighScoreController Instance = null;
    public int HighScore { get { return PlayerPrefs.GetInt(Constants.HIGH_SCORE, 0); } set { PlayerPrefs.SetInt(Constants.HIGH_SCORE, value); } }
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
        bestScoreText.text = Constants.BEST_SCORE + HighScore.ToString();
    }

    public void HighScoreCheck(int score)
    {
        if (score > HighScore)
        {
            HighScore = score;
        }

        bestScoreText.text = Constants.BEST_SCORE + HighScore.ToString();
    }
}
