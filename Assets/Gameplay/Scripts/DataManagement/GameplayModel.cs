using System;
using Gameplay.Scripts.Signals;
using Newtonsoft.Json;
using Zenject;

namespace Gameplay.Scripts.DataManagement
{
    [Serializable]
    public class GameplayModel : IPlayerPrefsData
    {
        [JsonIgnore] public int CurrentLevel => _currentLevel;
        [JsonProperty] private int _currentLevel;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        public void Initialize()
        {
            
        }

        public void SetCurrentLevel(int level)
        {
            _currentLevel = level;
            _signalBus.Fire(new LevelChangedSignal(_currentLevel));
        }
        
    }
}