using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = PlayerController.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.CUBE))
        {
            if (!PlayerController.Instance.IsPlaying)
            {
                ThrowCubes();
                playerController.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void ThrowCubes()
    {
        Rigidbody[] allCubes = playerController.StackPosition.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < allCubes.Length; i++)
        {
            allCubes[i].transform.parent = null;
            allCubes[i].gameObject.GetComponent<Collider>().enabled = true;
            allCubes[i].isKinematic = false;
            allCubes[i].AddForce(new Vector3(0, 250, i * 18));
        }
    }
}
