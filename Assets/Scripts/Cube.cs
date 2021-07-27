using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public static Cube Instance;
    [SerializeField] private Color pickupColor;

    private Rigidbody cubeRigidbody;
    private Collider cubeCollider;

    public Color PickupColor { get => pickupColor; set => pickupColor = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        cubeRigidbody = GetComponent<Rigidbody>();
        cubeCollider = GetComponent<Collider>();
    }
    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color", PickupColor);
    }

    private void OnEnable()
    {
        PlayerController.Kick += MyKick;
    }

    private void OnDisable()
    {
        PlayerController.Kick -= MyKick;
    }

    private void MyKick(float forceSent)
    {
        transform.parent = null;
        cubeCollider.enabled = true;
        cubeRigidbody.isKinematic = false;
        cubeRigidbody.AddForce(new Vector3(0, 200, forceSent));
    }
}
