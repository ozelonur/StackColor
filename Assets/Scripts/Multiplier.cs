using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{

    [SerializeField] private Color multiplierColor;

    private Renderer multiplierRenderer;

    private Settings settings;


    private void Awake()
    {
        multiplierRenderer = GetComponent<Renderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        multiplierRenderer.material.color = multiplierColor;
        settings = ObjectManager.Instance.Settings;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Cube")
        {
            PlayerController.Instance.UpdateMultiplier(20);
        }
    }

}
