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
        playerController.GetComponent<Collider>().enabled = false;
        playerController.IsPlaying = false;
        playerController.PlayerRigidbody.velocity = Vector3.zero;
        playerController.StartKickAnimation();
        Invoke(Constants.THROW_CUBES, .65f);

        Invoke(Constants.GAME_COMPLETE_INTERACTS, 2.5f);
    }

    private void GameComplete()
    {
        gameManager.GameComplete();
    }

    private void ThrowCubes()
    {
        Rigidbody[] allCubes = PlayerController.Instance.StackPosition.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < allCubes.Length; i++)
        {
            allCubes[i].transform.parent = null;
            allCubes[i].gameObject.GetComponent<Collider>().enabled = true;
            allCubes[i].isKinematic = false;
            allCubes[i].AddForce(new Vector3(0, 250, i * 18));
        }
    }
}
