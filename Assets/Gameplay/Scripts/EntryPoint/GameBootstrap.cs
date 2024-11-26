using System;
using Gameplay.Scripts.DataManagement;
using UI;
using UI.Scripts;
using UI.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Gameplay.Scripts.EntryPoint
{
    public class GameBootstrap : MonoBehaviour
    {
        private UIManager _uiManager;
        private PlayerPrefsSaveManager _playerPrefsSaveManager;

        [Inject]
        private void Construct(PlayerPrefsSaveManager playerPrefsSaveManager,UIManager uiManager)
        {
            _playerPrefsSaveManager = playerPrefsSaveManager;
            _uiManager = uiManager;
        }
        
        private void Start()
        {
            _playerPrefsSaveManager.Init();
            _uiManager.Show<MainScreen>();
        }
    }
}