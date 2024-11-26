using Gameplay.Scripts.DataManagement;
using UI.Scripts.Core;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace UI.Scripts
{
    public class StatisticsScreen : WindowController
    {
        [SerializeField] private StatisticItem _statisticItemPrefab;
        [SerializeField] private Transform _statisticRoot;
        [SerializeField] private Button _backButton;
        
        private PlayerPrefsSaveManager _playerPrefsSaveManager;
        private UIManager _uiManager;

        [Inject]
        private void Construct(PlayerPrefsSaveManager playerPrefsSaveManager,UIManager uiManager)
        {
            _uiManager = uiManager;
            _playerPrefsSaveManager = playerPrefsSaveManager;
        }
        public override void Show()
        {
            base.Show();
            
            _backButton.onClick.AddListener(() =>
            {
                _uiManager.Show<MainScreen>();
            });
            
            var sessions = _playerPrefsSaveManager.PrefsData.SessionModel.Sessions;
            foreach (var session in sessions)
            {
                var sessionView = Instantiate(_statisticItemPrefab, _statisticRoot);
                sessionView.Initialize(session);
            }
        }

        public override void Hide()
        {
            _backButton.onClick.RemoveAllListeners();
            base.Hide();
        }
    }
}