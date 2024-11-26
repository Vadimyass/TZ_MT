using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Scripts.PlayerShipManagement
{
    public class AsteroidSpriteConfig : ScriptableObject
    {
        [SerializeField] private List<Sprite> _asteroidSprites;

        public int SpritesCount => _asteroidSprites.Count;

        public Sprite GetSpriteByIndex(int index)
        {
            return _asteroidSprites[index];
        }
    }
}