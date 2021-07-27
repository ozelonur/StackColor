using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Settings", menuName = "Settings/Setting", order = 1)]
public class Settings : ScriptableObject
{
    public static Settings Instance = null;
    [SerializeField] private float xRange = 0;
    [SerializeField] private float forceReducer;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float sensivity;
    [SerializeField] private float multiplierValue;


    public float XRange { get => xRange; }
    public float ForceReducer { get => forceReducer; }
    public float ForwardSpeed { get => forwardSpeed; }
    public float Sensivity { get => sensivity; }
    public float MultiplierValue { get => multiplierValue;}


}
