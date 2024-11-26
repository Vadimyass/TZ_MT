using Gameplay.Scripts.DataManagement;
using Gameplay.Scripts.LevelManagement;
using TMPro;
using UI.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Scripts
{
    public class MainScreen : WindowController
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _statisticsButton;
        private UIManager _uiManager;
        private LevelController _levelController;
        private PlayerPrefsSaveManager _playerPrefsSaveManager;

        [Inject]
        private void Construct(UIManager uiManager, LevelController levelController, PlayerPrefsSaveManager playerPrefsSaveManager)
        {
            _playerPrefsSaveManager = playerPrefsSaveManager;
            _levelController = levelController;
            _uiManager = uiManager;
        }
        

        private void OpenStatistics()
        {
            _uiManager.Show<StatisticsScreen>();
        }

        public override void Show()
        {
            base.Show();
            _playButton.onClick.AddListener(PlayLevel);
            _statisticsButton.onClick.AddListener(OpenStatistics);
            _levelText.text = $"LVL: {_playerPrefsSaveManager.PrefsData.GameplayModel.CurrentLevel + 1}";
        }

        public override void Hide()
        {
            base.Hide();
            _playButton.onClick.RemoveAllListeners();
            _statisticsButton.onClick.RemoveAllListeners();
        }
        

        private void PlayLevel()
        {
            _uiManager.Show<GameplayScreen>();
            _levelController.Initialize(_playerPrefsSaveManager.PrefsData.GameplayModel.CurrentLevel);
        }
    }
}