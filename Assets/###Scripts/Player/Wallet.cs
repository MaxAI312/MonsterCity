using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int Amount { get; private set; }

    public event Action<int> AmountChanged;

    public Wallet()
    {
        Amount = 50;
    }

    public bool TrySpend(int amount)
    {
        if (Amount < amount)
            return false;

        Amount -= amount;

        AmountChanged?.Invoke(Amount);

        return true;
    }

    public void Fill(int amount)
    {
        Amount += amount;

        AmountChanged?.Invoke(Amount);
    }
}