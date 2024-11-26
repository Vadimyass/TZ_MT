using Gameplay.Scripts.DataManagement;
using Gameplay.Scripts.LevelManagement;
using Gameplay.Scripts.Signals;
using TMPro;
using UI.Scripts.Core;
using UnityEngine;
using Zenject;

namespace UI.Scripts
{
    public class GameplayScreen : WindowController
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        private PlayerPrefsSaveManager _playerPrefsSaveManager;
        private LevelController _levelController;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(PlayerPrefsSaveManager playerPrefsSaveManager, LevelController levelController,SignalBus signalBus)
        {
            _signalBus = signalBus;
            _levelController = levelController;
            _playerPrefsSaveManager = playerPrefsSaveManager;
        }

        public override void Show()
        {
            _signalBus.Subscribe<HealthChangedSignal>(SetHealthText);
            _signalBus.Subscribe<ScoreChangedSignal>(SetScoreText);
            _signalBus.Subscribe<LevelChangedSignal>(SetLevelText);
            
            SetScoreText();
            SetLevelText();
            
            base.Show();
        }

        public override void Hide()
        {
            _signalBus.TryUnsubscribe<HealthChangedSignal>(SetHealthText);
            _signalBus.TryUnsubscribe<ScoreChangedSignal>(SetScoreText);
            _signalBus.TryUnsubscribe<LevelChangedSignal>(SetLevelText);
            base.Hide();
        }

        private void SetLevelText()
        {
            _levelText.text =$"LVL: {_playerPrefsSaveManager.PrefsData.GameplayModel.CurrentLevel + 1}";
        }

        private void SetScoreText()
        {
            _scoreText.text = $"Score: {_playerPrefsSaveManager.PrefsData.SessionModel.CurrentSession.Score}";
        }

        private void SetHealthText(HealthChangedSignal healthChangedSignal)
        {
            _healthText.text = $"Health: {healthChangedSignal.Health}";
        }
    }
}