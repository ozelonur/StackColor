using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;
    [SerializeField]private Camera orthographicCamera;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text endGameText;
    [SerializeField] private Text bestScoreText;

    [SerializeField] private Settings settings;

    public Camera OrthographicCamera { get => orthographicCamera; set => orthographicCamera = value; }
    public Text ScoreText { get => scoreText; set => scoreText = value; }
    public Text EndGameText { get => endGameText; set => endGameText = value; }
    public Text BestScoreText { get => bestScoreText; set => bestScoreText = value; }
    public Settings Settings { get => settings; set => settings = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
