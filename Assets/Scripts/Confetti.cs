using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Confetti : MonoBehaviour
{
    public static Confetti Instance = null;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void LaunchEffect()
    {
        GetComponent<ParticleSystem>().Play();
    }

}
