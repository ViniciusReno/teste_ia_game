using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public int woodCount;

    public event Action<int> OnWoodChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddWood(int amount)
    {
        woodCount += amount;
        OnWoodChanged?.Invoke(woodCount);
        AudioManager.Instance?.PlayPickup();
    }

    public void RemoveWood(int amount)
    {
        woodCount = Mathf.Max(0, woodCount - amount);
        OnWoodChanged?.Invoke(woodCount);
    }
}
