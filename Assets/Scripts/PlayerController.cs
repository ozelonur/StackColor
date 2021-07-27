using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance = null;

    // Managers
    private ObjectManager objectManager;
    private GameManager gameManager;

    [SerializeField]private Transform stackPosition;
    [SerializeField] private Color caseColor;

    public static Action<float> Kick;

    public bool IsPlaying { get { return IsPlaying1; } set { IsPlaying1 = value; } }

    public Rigidbody PlayerRigidbody { get => playerRigidbody; set => playerRigidbody = value; }
    public Animator PlayerAnimator { get => playerAnimator; set => playerAnimator = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public bool IsGameComplete { get => isGameComplete; set => isGameComplete = value; }
    public bool IsPlaying1 { get => isPlaying; set => isPlaying = value; }
    public bool AtEnd { get => atEnd; set => atEnd = value; }
    public float ForwardForce { get => forwardForce; set => forwardForce = value; }
    public float ForceAdder { get => forceAdder; set => forceAdder = value; }

    private Renderer playerRenderer;

    private Rigidbody playerRigidbody;


    private bool atEnd;
    private bool isPlaying;
    private bool isGameOver;
    private bool isGameComplete;

    private float forwardForce = 100;
    private float forceAdder;
    private float multiplierValue;

    private int score;

    private Animator playerAnimator;

    private Settings settings;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        playerRenderer = transform.GetChild(0).transform.Find("kasa").GetComponent<Renderer>();
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerAnimator = GetComponentInChildren<Animator>();


    }
    // Start is called before the first frame update
    void Start()
    {
        objectManager = ObjectManager.Instance;
        gameManager = GameManager.Instance;
        playerRenderer.materials[1].SetColor("_Color", caseColor);
        IsGameOver = false;
        settings = objectManager.Settings;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.FINIS_LINE_END))
        {
            print("Hit");
            StartCoroutine(gameManager.GameComplete());
        }

        if (AtEnd)
        {
            return;
        }
        if (other.gameObject.CompareTag(Constants.CUBE))
        {
            Transform otherTransform = other.transform;
            Rigidbody otherRigidbody = otherTransform.GetComponent<Rigidbody>();
            if (caseColor == other.GetComponent<Cube>().PickupColor)
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
                    gameManager.GameOver();
                }

                else
                {
                    Destroy(stackPosition.GetChild(stackPosition.childCount - 1).gameObject);
                    Destroy(other.gameObject);

                }

            }

        }

        else if (other.gameObject.CompareTag(Constants.WALL))
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

    public void LaunchStack()
    {
        Kick(ForwardForce);
    }
    public void UpdateScore(int value)
    {
        score += value;
        objectManager.ScoreText.text = score.ToString();
    }
    public void UpdateMultiplier(float value)
    {
        if (true)
        {

        }
        UpdateScore((int)value);
        if (value <= multiplierValue)
        {
            return;
        }

        multiplierValue = value;
        objectManager.ScoreText.text = (score * multiplierValue).ToString();

    }


}
