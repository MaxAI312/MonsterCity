using UnityEngine;

public class ShotingVisualisation : MonoBehaviour
{
    [SerializeField] private Fireball _fireballPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Unit _unit;

    // Used in animation
    public void ShowShot()
    {
        var spawnedWhizzbang = Instantiate(_fireballPrefab, _spawnPoint.transform.position, Quaternion.identity);
        spawnedWhizzbang.Initialization(_unit.Target.transform);
    }
}