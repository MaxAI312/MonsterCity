using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StarsCounter", menuName = "CreateSrarsCounter", order = 51)]

public class StarsCounter : ScriptableObject
{
    [SerializeField] private List<int> _count;

    public int this[int index] => _count[index];

    public int ToStars(int value)
    {
        return  _count.IndexOf(value) + 1;
    }
}