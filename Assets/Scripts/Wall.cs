using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IProperty
{
    public Color color;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.SetColor(Constants.COLOR, color);
        playerController = PlayerController.Instance;
    }

    public void Interact()
    {
        Color wallColor = GetComponent<Renderer>().material.color;
        playerController.CaseColor = new Color(wallColor.r, wallColor.g, wallColor.b, 1);

        playerController.PlayerRenderer.materials[1].SetColor(Constants.COLOR, playerController.CaseColor);
        Renderer[] allCubes = playerController.StackPosition.GetComponentsInChildren<Renderer>();
        foreach (Renderer cube in allCubes)
        {
            cube.material.SetColor(Constants.COLOR, playerController.CaseColor);
        }
    }

}
