public struct UpgradeCardOptions
{
    public bool IsAvailable;
    public int FreeLimit;
    public int Damage;
    public int Speed;
    public int Cost;

    public UpgradeCardOptions(bool isAvailable, int freeLimit, int damage, int speed, int cost)
    {
        IsAvailable = isAvailable;
        FreeLimit = freeLimit;
        Damage = damage;
        Speed = speed;
        Cost = cost;
    }
}