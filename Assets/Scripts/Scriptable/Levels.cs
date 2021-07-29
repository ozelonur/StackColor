using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Level", menuName = "Levels/Level", order = 1)]
public class Levels : ScriptableObject
{
    public static Levels Instance = null;
    [SerializeField] private GameObject[] levelPrefab;

    public GameObject[] LevelPrefab { get => levelPrefab; set => levelPrefab = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
