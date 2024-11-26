using System;
using System.Collections.Generic;
using Gameplay.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Gameplay.Scripts.DataManagement
{
    [Serializable]
    public class SessionModel : IPlayerPrefsData
    {
        private SignalBus _signalBus;
        public List<Session> Sessions { get; private set; } = new();

        public Session CurrentSession { get; private set; }

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            CurrentSession = new Session();
        }

        public void AddScore(int value)
        {
            _signalBus.Fire(new ScoreChangedSignal(value));
            CurrentSession.AddScore(value);
        }


        public void RecordSession(bool isWin)
        {
            if (Sessions.Contains(CurrentSession)) return;

            CurrentSession.SetIsWin(isWin);
            Sessions.Add(CurrentSession);
            if (Sessions.Count > 10)
            {
                Sessions.RemoveAt(0);
            }

            CurrentSession = new Session();
        }
    }
}