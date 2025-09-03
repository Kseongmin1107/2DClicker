using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold
{
    private double gold;

    public event Action<double> OnGoldChanged;
    public double Gold => gold;

    public void SetGold(double amount)
    {
        amount = Math.Max(0, amount);
        gold = amount;
        OnGoldChanged?.Invoke(gold);
    }

    public void AddGold(double amount)
    {
        if (amount <= 0) return;
        gold += amount;
        OnGoldChanged?.Invoke(gold);
    }

    public bool TrySpendGold(double cost)
    {
        if(cost <= 0) return true;
        if (gold < cost) return false;

        gold -= cost;
        OnGoldChanged?.Invoke(gold);
        return true;
    }
}
