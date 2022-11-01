public interface IMonsterData
{
    int Cost { get; }
    Monster Prefab { get; }
    int FreeCount { get; }
    UpgradeCardOptions Options { get; }
}