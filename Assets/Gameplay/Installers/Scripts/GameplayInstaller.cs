using Gameplay.Scripts.DataManagement;
using Gameplay.Scripts.LevelManagement;
using Gameplay.Scripts.PlayerShipManagement;
using UnityEngine;
using Zenject;

namespace Gameplay.Scripts.Installers
{
    public class GameplayInstaller : ScriptableObjectInstaller<GameplayInstaller>
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private AsteroidSpriteConfig _asteroidSpriteConfig;
        public override void InstallBindings()
        {
            Container.Bind<PlayerPrefsSaveManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<LevelController>().FromComponentsInNewPrefab(_levelController).AsSingle().WithArguments(_asteroidSpriteConfig).NonLazy();
        }
    }
}