using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour, IProperty
{
    private GameManager gameManager;
    private PlayerController playerController;

    private void Start()
    {
        playerController = PlayerController.Instance;
        gameManager = GameManager.Instance;
    }
    public void Interact()
    {
        playerController.GameEndPhysicOperations();
        Invoke(Constants.GAME_COMPLETE_INTERACTS, 2.5f);
    }

    private void GameComplete()
    {
        gameManager.GameComplete();
    }

   
}
