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

    private int coin = 0;

    public int Coin { get => coin; set => coin = value; }

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
    }

    public void OnClickStart()
    {
        if (playerController.IsGameOver == true)
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
