using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private PlayerController playerController;
    private ObjectManager objectManager;

    public Action GameComplete;
    public Action GameOverAction;
    public Action CoinAction;


    public int Coin { get { return PlayerPrefs.GetInt(Constants.COIN_COUNT, 0); } set { PlayerPrefs.SetInt(Constants.COIN_COUNT, value); } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerController = PlayerController.Instance;
        playerController.IsPlaying = false;

        objectManager = ObjectManager.Instance;
        objectManager.EndGameText.text = Constants.TAP_TO_START;
        objectManager.BestScoreText.gameObject.SetActive(false);
        objectManager.CoinText.text = Coin.ToString();
    }

    public void OnClickStart()
    {
        if (playerController.IsGameOver == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        else if (playerController.IsGameComplete)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        else
        {
            playerController.IsPlaying = true;
            objectManager.EndGameText.gameObject.SetActive(false);
        }
    }



}
