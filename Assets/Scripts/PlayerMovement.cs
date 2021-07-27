using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    private Vector3 difference;
    private Vector3 firstPosition;
    private Vector3 mousePosition;

    private Settings settings;
    private PlayerController playerController;
    private ObjectManager objectManager;

    private void Start()
    {
        playerController = PlayerController.Instance;
        objectManager = ObjectManager.Instance;
        settings = objectManager.Settings;


        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponentInChildren<Animator>();
    }
    void FixedUpdate()
    {
        if (playerController.IsPlaying)
        {
            playerRigidbody.velocity = Vector3.Lerp(playerRigidbody.velocity, new Vector3(difference.x, playerRigidbody.velocity.y, settings.ForwardSpeed), 1f);
            playerAnimator.SetFloat("Run", 1);
        }

    }
    private void Update()
    {
        if (playerController.IsPlaying)
        {
            Movement();
        }

        GameEndMovement();

        if (transform.position.x < -settings.XRange)
        {
            transform.position = new Vector3(-settings.XRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > settings.XRange)
        {
            transform.position = new Vector3(settings.XRange, transform.position.y, transform.position.z);
        }
    }

    private void MouseDown(Vector3 inputPosition)
    {
        mousePosition = objectManager.OrthographicCamera.ScreenToWorldPoint(inputPosition);
        firstPosition = mousePosition;
    }

    private void MouseHold(Vector3 inputPosition)
    {
        mousePosition = objectManager.OrthographicCamera.ScreenToWorldPoint(inputPosition);
        difference = mousePosition - firstPosition;
        difference *= settings.Sensivity;
    }

    private void MouseUp()
    {
        difference = Vector3.zero;
    }

    private void Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
        else if (Input.GetMouseButton(0))
        {
            MouseHold(Input.mousePosition);
        }
    }

    private void GameEndMovement()
    {
        if (playerController.AtEnd)
        {
            playerController.ForwardForce -= playerController.ForwardForce * Time.deltaTime;
            if (playerController.ForwardForce < 0)
            {
                playerController.ForwardForce = 0;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (playerController.AtEnd)
            {
                playerController.ForwardForce += playerController.ForceAdder;
            }
        }
    }

}
