using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IProperty
{
    public static Cube Instance;
    [SerializeField] private Color pickupColor;

    private Rigidbody cubeRigidbody;
    private Collider cubeCollider;

    public Color PickupColor { get => pickupColor; set => pickupColor = value; }

    private Settings settings;
    private PlayerController playerController;
    private GameManager gameManager;

    private bool collided = false;

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
        renderer.material.SetColor(Constants.COLOR, PickupColor);
        settings = ObjectManager.Instance.Settings;
        playerController = PlayerController.Instance;
        gameManager = GameManager.Instance;

    }


    private void MyKick()
    {
        transform.parent = null;
        cubeCollider.enabled = true;
        cubeRigidbody.isKinematic = false;
        cubeRigidbody.AddForce(new Vector3(0, settings.ForceReducer, 150 * 3f));
    }

    public void Interact()
    {
        if (playerController.IsPlaying)
        {
            Rigidbody otherRigidbody = transform.GetComponent<Rigidbody>();

            if (playerController.CaseColor == pickupColor)
            {
                if (!collided)
                {
                    PlayerController.Kick += playerController.StartKickAnimation;
                    PlayerController.Kick += MyKick;
                    otherRigidbody.isKinematic = true;
                    this.enabled = false;

                    transform.parent = playerController.StackPosition;
                    transform.position = playerController.StackPosition.position;
                    playerController.ChildCount++;
                    transform.position += Vector3.up * (playerController.ChildCount * (transform.localScale.y + 0.0009f));
                    collided = true;
                }

            }
            else
            {
                if (playerController.ChildCount > 0)
                {
                    Destroy(playerController.StackPosition.GetChild(playerController.ChildCount - 1).gameObject);
                    Destroy(transform.gameObject);
                    playerController.ChildCount--;
                }

                else
                {
                    gameManager.GameOverAction();
                }
            }
        }
        

    }

    private void OnDestroy()
    {
        PlayerController.Kick -= MyKick;
        PlayerController.Kick -= playerController.StartKickAnimation;
    }
}
