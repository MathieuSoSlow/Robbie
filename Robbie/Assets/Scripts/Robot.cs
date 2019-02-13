using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public float CurrentBattery { get; private set; }
    [SerializeField] private float MaxBattery;

    public bool IsAlive()
    {
        return CurrentBattery > 0;
    }

    public void AddBattery(float charge)
    {
        CurrentBattery += charge;
        if (CurrentBattery > MaxBattery)
            CurrentBattery = MaxBattery;
    }

    public void RemoveBattery(float charge)
    {
        CurrentBattery -= charge;
        if (CurrentBattery <= 0)
            CurrentBattery = 0;
    }
}