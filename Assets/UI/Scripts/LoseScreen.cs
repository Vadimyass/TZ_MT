using Gameplay.Scripts.DataManagement;
using Gameplay.Scripts.LevelManagement;
using UI.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Scripts
{
    public class LoseScreen : WindowController
    {
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private Button _exitButton;
        private PlayerPrefsSaveManager _playerPrefsSaveManager;
        private LevelController _levelController;
        private UIManager _uiManager;

        [Inject]
        private void Construct(PlayerPrefsSaveManager playerPrefsSaveManager,LevelController levelController,UIManager uiManager)
        {
            _uiManager = uiManager;
            _levelController = levelController;
            _playerPrefsSaveManager = playerPrefsSaveManager;
        }

        public override void Show()
        {
            base.Show();
            _levelController.Hide();
            _playerPrefsSaveManager.PrefsData.SessionModel.RecordSession(false);
            _playerPrefsSaveManager.ForceSave();
            _playAgainButton.onClick.AddListener(() =>
            {
                _levelController.RestartLevel();
            });
            
            _exitButton.onClick.AddListener(() =>
            {
                _uiManager.Show<MainScreen>();
            });
        }

        public override void Hide()
        {
            _playAgainButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            base.Hide();
        }
    }
}