using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public static Cube Instance;
    public Color pickupColor;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }   
    }
    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color", pickupColor);
    }
}
