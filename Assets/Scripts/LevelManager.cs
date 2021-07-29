using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;
    private Levels levels;
    public int LevelIndex { get => PlayerPrefs.GetInt("LevelIndex", 0); set => PlayerPrefs.SetInt("LevelIndex", value); }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        levels = ObjectManager.Instance.Levels;
        if (LevelIndex >= levels.LevelPrefab.Length)
        {
            LevelIndex = 0;
        }

        Instantiate(levels.LevelPrefab[LevelIndex]);
    }

   
}
