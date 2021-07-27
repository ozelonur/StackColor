using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private PlayerController playerController;
    private ObjectManager objectManager;

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
        objectManager.EndGameText.text = "Tap to Start";
        objectManager.BestScoreText.gameObject.SetActive(false);
    }


    public void GameOver()
    {
        playerController.IsGameOver = true;
        playerController.PlayerRigidbody.velocity = Vector3.zero;
        playerController.IsPlaying = false;
        playerController.PlayerAnimator.SetTrigger("Die");
        StartCoroutine(WaitAndFinish());
    }

    public IEnumerator GameComplete()
    {
        playerController.IsPlaying = false;
        playerController.PlayerRigidbody.velocity = Vector3.zero;
        playerController.LaunchStack();
        yield return new WaitForSeconds(3);

        Character.Instance.PlayDance();
        playerController.IsGameComplete = true;
        objectManager.EndGameText.gameObject.SetActive(true);
        objectManager.EndGameText.text = "Next Level";
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

    IEnumerator WaitAndFinish()
    {
        yield return new WaitForSeconds(3);
        objectManager.EndGameText.gameObject.SetActive(true);
        objectManager.EndGameText.text = "Try Again";
    }

}
