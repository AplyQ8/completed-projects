using System;
using UnityEngine;

public class Context: MonoBehaviour
{
    [SerializeField] public GameObject strategy;
    public event Action ExecuteStrategy = delegate { };

    public void SetStrategy(GameObject strategy)
    {
        this.strategy = strategy;
    }

    public void Execute()
    {
        ExecuteStrategy.Invoke();
    }
}