using Zenject;

namespace Gameplay.Scripts.Signals
{
    public class SignalsDeclarator : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<HealthChangedSignal>();
            Container.DeclareSignal<ScoreChangedSignal>();
            Container.DeclareSignal<LevelChangedSignal>();
        }
    }
}