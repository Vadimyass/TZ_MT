using System;
using Gameplay.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Gameplay.Scripts.DataManagement
{
    [Serializable]
    public class PlayerPrefsData
    {
        public GameplayModel GameplayModel => _gameplayModel;
        private GameplayModel _gameplayModel = new ();

        public SessionModel SessionModel => _sessionModel;
        private SessionModel _sessionModel = new ();
        
        public void Initialize(DiContainer container)
        {
            foreach (var model in ReflectionUtils.GetFieldsOfType<IPlayerPrefsData>(this))
            {
                container.Inject(model);
                model.Initialize();
            }
        }
    }
}