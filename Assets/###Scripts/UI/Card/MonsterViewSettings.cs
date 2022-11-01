using UnityEngine;

[CreateAssetMenu(fileName = "Monster Settings", menuName = "Create Monster Settings", order = 51)]
public class MonsterViewSettings : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public Monster Monster { get; private set; }
    [field: SerializeField] public float SpawnTime { get; private set; }
}