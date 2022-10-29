using System;
using UnityEngine;

public struct ClampedAmount
{
    private float _amount;
    private readonly float _minLimit;
    private readonly float _maxLimit;

    public event Action LimitReached;

    public ClampedAmount(float amount, float minLimit, float maxLimit) : this()
    {
        _minLimit = minLimit;
        _maxLimit = maxLimit;
        Amount = amount;
    }

    public float Amount
    {
        get => _amount;

        set
        {
            _amount = Mathf.Clamp(value, _minLimit, _maxLimit);

            if (_amount == _minLimit || _amount == _maxLimit)
                LimitReached?.Invoke();
        }
    }
}