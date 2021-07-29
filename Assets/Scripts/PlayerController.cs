using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance = null;

    

    public static Action Kick;

    public bool IsPlaying { get { return isPlaying; } set { isPlaying = value; } }
    public Rigidbody PlayerRigidbody { get => playerRigidbody; set => playerRigidbody = value; }
    public Animator PlayerAnimator { get => playerAnimator; set => playerAnimator = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public bool IsGameComplete { get => isGameComplete; set => isGameComplete = value; }
    public bool AtEnd { get => atEnd; set => atEnd = value; }
    public float ForwardForce { get => forwardForce; set => forwardForce = value; }
    public float ForceAdder { get => forceAdder; set => forceAdder = value; }
    public int Score { get => score; }
    public Color CaseColor { get => caseColor; set => caseColor = value; }
    public Renderer PlayerRenderer { get => playerRenderer; set => playerRenderer = value; }
    public Transform StackPosition { get => stackPosition; set => stackPosition = value; }
    public int ChildCount { get => childCount; set => childCount = value; }
    public bool AtFirst { get => atFirst; set => atFirst = value; }

    private Renderer playerRenderer;

    private Rigidbody playerRigidbody;

    // Managers
    private ObjectManager objectManager;
    private GameManager gameManager;
    private HighScoreController highScoreController;

    [SerializeField] private Transform stackPosition;
    [SerializeField] private Color caseColor;

    private GameObject character;
    private bool isPlaying;
    private bool isGameOver;
    private bool isGameComplete;
    private bool atFirst = true;
    private bool atEnd;
    

    private float forwardForce = 100;
    private float forceAdder;
    private float multiplierValue;

    private int score;
    private int childCount = 0;


    private Animator playerAnimator;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        PlayerRenderer = transform.GetChild(0).transform.Find(Constants.CASE).GetComponent<Renderer>();
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerAnimator = GetComponentInChildren<Animator>();


    }
    // Start is called before the first frame update
    void Start()
    {
        objectManager = ObjectManager.Instance;
        gameManager = GameManager.Instance;
        highScoreController = HighScoreController.Instance;
        gameManager.GameComplete += GameComplete;
        gameManager.GameOverAction += GameOver;
        PlayerRenderer.materials[1].SetColor(Constants.COLOR, CaseColor);
        IsGameOver = false;
        character = transform.GetChild(2).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IProperty>()?.Interact();
    }

    public void GameEndPhysicOperations()
    {
        GetComponent<Collider>().enabled = false;
        IsPlaying = false;
        PlayerRigidbody.velocity = Vector3.zero;
        StartKickAnimation();
    }

    public void GameComplete()
    {
        Character.Instance.PlayDance();
        highScoreController.HighScoreCheck(Score);
        objectManager.BestScoreText.gameObject.SetActive(true);
        IsGameComplete = true;
        objectManager.EndGameText.gameObject.SetActive(true);
        objectManager.EndGameText.text = Constants.NEXT_LEVEL;
        Invoke(Constants.LAUNCH_CONFETTI, .5f);
        LevelManager.Instance.LevelIndex++;
    }

    private void LaunchConfetti()
    {
        ParticleSystem confetti = Instantiate(objectManager.Confetti, new Vector3(character.transform.position.x -.5f, character.transform.position.y, character.transform.position.z -.5f), character.transform.rotation);
        ParticleSystem confetti1 = Instantiate(objectManager.Confetti, new Vector3(-character.transform.position.x - .5f, character.transform.position.y, character.transform.position.z - .5f), character.transform.rotation);
    }

    public void GameOver()
    {
        IsGameOver = true;
        PlayerRigidbody.velocity = Vector3.zero;
        IsPlaying = false;
        PlayerAnimator.SetTrigger(Constants.ANIM_DIE);
        objectManager.EndGameText.gameObject.SetActive(true);

        objectManager.EndGameText.text = Constants.TRY_AGAIN;
    }
    public void StartKickAnimation()
    {
        PlayerAnimator.SetFloat(Constants.ANIM_KICK, 1);
    }

    public void LaunchStack()
    {
        Kick();
    }
    public void UpdateScore(int value)
    {
        score += value;
        objectManager.ScoreText.text = score.ToString();
    }
    public void UpdateMultiplier(float value)
    {
        UpdateScore((int)value);
        if (value <= multiplierValue)
        {
            return;
        }

        multiplierValue = value;
        objectManager.ScoreText.text = (score * multiplierValue).ToString();

    }


}
