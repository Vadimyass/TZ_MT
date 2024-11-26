using UI.Scripts;
using UI.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Gameplay.Scripts.Installers
{
    public class UIInstaller : ScriptableObjectInstaller<UIInstaller>
    {
        [SerializeField] private UIManager _uiManager;
        public override void InstallBindings()
        {
            Container.Bind<UIManager>().FromComponentsInNewPrefab(_uiManager).AsSingle().NonLazy();
        }
    }
}