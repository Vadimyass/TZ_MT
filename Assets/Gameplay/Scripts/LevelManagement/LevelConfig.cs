using System.Collections.Generic;
using UI.Scripts;
using UnityEngine;

namespace Gameplay.Scripts.LevelManagement
{
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private List<LevelModel> _levels;

        public LevelModel GetLevelByIndex(int index)
        {
            if (_levels.Count < index)
            {
                Debug.LogError($"Error with index {index} in LevelConfig");
                return null;
            }

            return _levels[index];
        }

    }
}