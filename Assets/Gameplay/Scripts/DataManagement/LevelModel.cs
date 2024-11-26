using System;
using UnityEngine;

namespace UI.Scripts
{
    [Serializable]
    public class LevelModel
    {
        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public string  Seed { get; private set; }
    }
}