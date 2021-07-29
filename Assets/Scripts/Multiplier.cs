using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{

    [SerializeField] private Color multiplierColor;

    private Renderer multiplierRenderer;

    private PlayerController playerController;

    private void Awake()
    {
        multiplierRenderer = GetComponent<Renderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        multiplierRenderer.material.color = multiplierColor;
        playerController = PlayerController.Instance;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Constants.CUBE))
        {
            playerController.UpdateMultiplier(20);
        }
    }

}
