using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 offset = new Vector3(1.5f, 1.5f, -2.4f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        if (PlayerController.Instance.IsGameComplete)
        {
            Vector3 characterPos = player.transform.GetChild(2).transform.position;
            transform.DOLookAt(new Vector3(characterPos.x, transform.position.y, characterPos.z), 1);
        }
    }
}
