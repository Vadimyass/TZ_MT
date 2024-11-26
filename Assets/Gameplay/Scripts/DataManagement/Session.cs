using System;
using Newtonsoft.Json;

namespace Gameplay.Scripts.DataManagement
{
    [Serializable]
    public class Session
    {
        [JsonIgnore] public int Score => _score;
        [JsonProperty] private int _score;
        
        [JsonIgnore] public int Duration => _duration;
        [JsonProperty] private int _duration;
        
        [JsonIgnore] public int Level => _level;
        [JsonProperty] private int _level;
        
        [JsonIgnore] public bool IsWin => _isWin;
        [JsonProperty] private bool _isWin;

        public void AddScore(int value)
        {
            _score += value;
        }

        public void AddDuration(int value)
        {
            _duration += value;
        }

        public void SetLevel(int value)
        {
            _level = value;
        }

        public void SetIsWin(bool isWin)
        {
            _isWin = isWin;
        }
    }
}