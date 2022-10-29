using Zenject;

public class WalletInstaller : MonoInstaller
{
    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.Bind<Wallet>().AsSingle().Lazy();
    }
}
