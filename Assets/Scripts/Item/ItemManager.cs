using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Singleton;

public class ItemManager : MonoBehaviour
{

    public static ItemManager Instance;
    public TextMeshProUGUI collectedCoins;
    public SOInt coins;
    public SOInt planets;
    public TextMeshProUGUI collectedPlanets;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Reset();
    }
    private void Reset()
    {
        coins.value = 0;
        planets.value = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
    }

    public void AddPlanets(int amount = 1)
    {
        planets.value += amount;
    }
}
