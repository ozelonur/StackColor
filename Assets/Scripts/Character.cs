using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    public static Character Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlayDance()
    {
        Vector3 camPos = Camera.main.transform.position;
        
        transform.DOLookAt(new Vector3(camPos.x, transform.position.y, camPos.z), 2);
        PlayerController.Instance.PlayerAnimator.SetTrigger(Constants.ANIM_DANCE);
    }
}
