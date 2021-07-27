using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance = null;
    public Transform stackPosition;
    public Color caseColor;

    private float xRange = 1;


    private Renderer playerRenderer;

    private Rigidbody playerRigidbody;

    private float forwardSpeed = 2f;
    private float sensivity = 1f;

    private Vector3 difference;
    private Vector3 firstPosition;
    private Vector3 mousePosition;

    private bool atEnd;
    private float forwardForce;
    private float forceAdder;
    private float forceReducer;

    private bool isPlaying;

    public static Action<float> Kick;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        playerRenderer = GameObject.Find("kasa").GetComponent<Renderer>();
        playerRigidbody = GetComponent<Rigidbody>();

    }
    // Start is called before the first frame update
    void Start()
    {
        playerRenderer.materials[1].SetColor("_Color", caseColor);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPlaying)
        {
            playerRigidbody.velocity = Vector3.Lerp(playerRigidbody.velocity, new Vector3(difference.x, playerRigidbody.velocity.y, forwardSpeed), 1f);
            if (transform.position.x < -xRange)
            {
                transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > xRange)
            {
                transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
            }
        }

    }
    private void Update()
    {
        if (isPlaying)
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
        if (atEnd)
        {
            forwardForce -= forceReducer * Time.deltaTime;
            if (forwardForce < 0)
            {
                forwardForce = 0;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (atEnd)
            {
                forwardForce += forceAdder;
            }
        }


    }

    private void MouseDown(Vector3 inputPosition)
    {
        mousePosition = ObjectManager.Instance.orthographicCamera.ScreenToWorldPoint(inputPosition);
        firstPosition = mousePosition;
    }

    private void MouseHold(Vector3 inputPosition)
    {
        mousePosition = ObjectManager.Instance.orthographicCamera.ScreenToWorldPoint(inputPosition);
        difference = mousePosition - firstPosition;
        difference *= sensivity;
    }

    private void MouseUp()
    {
        difference = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag ==  "FinishLineStart")
        {
            atEnd = true;
        }
        if (other.tag == "FinishLineEnd")
        {
            playerRigidbody.velocity = Vector3.zero;
            LaunchStack();
        }

        if (atEnd)
        {
            return;
        }
        if (other.gameObject.tag == "Cube")
        {
            Transform otherTransform = other.transform;
            Rigidbody otherRigidbody = otherTransform.GetComponent<Rigidbody>();
            if (caseColor == other.GetComponent<Cube>().pickupColor)
            {
                otherRigidbody.isKinematic = true;
                other.enabled = false;

                otherTransform.parent = stackPosition;
                otherTransform.position = stackPosition.position;
                otherTransform.position += Vector3.up * (stackPosition.childCount * (otherTransform.localScale.y + 0.0018f));

            }
            else
            {
                if (stackPosition.childCount < 1)
                {
                    print("Game Over!!");
                }

                else
                {
                    Destroy(stackPosition.GetChild(stackPosition.childCount - 1).gameObject);

                }

            }
            
        }

        else if (other.gameObject.tag == "Wall")
        {
            Color wallColor = other.gameObject.GetComponent<Renderer>().material.color;
            caseColor = wallColor;
            caseColor.a = 1f;

            playerRenderer.materials[1].SetColor("_Color", caseColor);
            Renderer[] allCubes = stackPosition.GetComponentsInChildren<Renderer>();

            foreach (Renderer cube in allCubes)
            {
                cube.material.SetColor("_Color", caseColor);
            }
        }
    }

    private void LaunchStack()
    {
        Kick(forwardForce);
    }
}
